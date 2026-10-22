#!/usr/bin/env python3
import argparse
import collections
import re
from pathlib import Path


TEXT_SUFFIXES = {".asax", ".aspx", ".config", ".sln", ".sql", ".txt", ".vb", ".vbproj", ".wsdl", ".xml"}
METHOD_RE = re.compile(
    r"^\s*(?:(Public|Private|Protected|Friend)\s+)?"
    r"(?:(?:Shared|Overloads|Overrides|Overridable|MustOverride|NotOverridable|Shadows|Async|Iterator)\s+)*"
    r"(Sub|Function)\s+([A-Za-z_]\w*)",
    re.IGNORECASE,
)
TYPE_RE = re.compile(
    r"^\s*(?:(?:Public|Private|Protected|Friend)\s+)?"
    r"(?:(?:Partial|MustInherit|NotInheritable)\s+)*(Class|Module|Structure|Interface)\s+([A-Za-z_]\w*)",
    re.IGNORECASE,
)


def read_text(path):
    data = path.read_bytes()
    for encoding in ("utf-8-sig", "utf-8", "cp1250", "latin-1"):
        try:
            return data.decode(encoding)
        except UnicodeDecodeError:
            pass
    raise UnicodeDecodeError("unknown", data, 0, 1, str(path))


def lines(path):
    return read_text(path).splitlines()


def relative(path, root):
    return str(path.relative_to(root)).replace("/", "\\")


def markdown_table(headers, rows):
    print("| " + " | ".join(headers) + " |")
    print("|" + "|".join("---" for _ in headers) + "|")
    for row in rows:
        values = []
        for value in row:
            value = str(value).replace("|", "\\|").replace("\r", " ").replace("\n", " ")
            values.append(value)
        print("| " + " | ".join(values) + " |")


def text_files(root):
    return sorted(path for path in root.rglob("*") if path.is_file() and path.suffix.lower() in TEXT_SUFFIXES)


def vb_files(root):
    return sorted(root.rglob("*.vb"))


def role_from_name(path, root):
    rel = relative(path, root)
    name = path.name
    if name.endswith(".aspx.vb"):
        return "code-behind strony"
    if name.endswith(".designer.vb"):
        return "plik projektanta"
    if name.endswith("Manager.vb"):
        return "manager"
    if name.endswith("DAL.vb"):
        return "DAL"
    if name == "DbHelper.vb":
        return "helper bazy"
    if name.endswith("Client.vb"):
        return "klient integracji"
    if name.startswith("Import") and name.endswith(".vb"):
        return "import"
    if name.startswith("Eksport") and name.endswith(".vb"):
        return "eksport"
    if "\\Model\\" in "\\" + rel:
        return "model"
    if name == "Program.vb":
        return "punkt wejścia zadania"
    if name == "Global.asax.vb":
        return "cykl życia aplikacji"
    if name == "Reference.vb":
        return "wygenerowane proxy"
    if name.endswith(".aspx"):
        return "strona WebForms"
    if name.endswith(".sql"):
        return "skrypt SQL"
    return "rola nierozpoznana z nazwy"


def measure_tree(root):
    files = text_files(root)
    directories = [root] + sorted(path for path in root.rglob("*") if path.is_dir())
    rows = []
    for directory in directories:
        direct = [path for path in files if path.parent == directory]
        recursive = [path for path in files if directory == path.parent or directory in path.parents]
        rows.append(
            (
                "." if directory == root else relative(directory, root),
                len(direct),
                sum(len(lines(path)) for path in direct),
                len(recursive),
                sum(len(lines(path)) for path in recursive),
            )
        )
    markdown_table(
        ["Katalog", "Pliki bezpośrednio", "Linie bezpośrednio", "Pliki łącznie", "Linie łącznie"],
        rows,
    )


def measure_roles(root):
    rows = []
    for path in text_files(root):
        if path.suffix.lower() not in {".vb", ".aspx", ".sql"}:
            continue
        declarations = []
        if path.suffix.lower() == ".vb":
            for number, line in enumerate(lines(path), 1):
                match = TYPE_RE.match(line)
                if match:
                    declarations.append(f"{match.group(1)} {match.group(2)}@{number}")
        rows.append((relative(path, root), role_from_name(path, root), ", ".join(declarations) or "—"))
    markdown_table(["Plik", "Rola z nazwy", "Deklaracje typów"], rows)


def measure_dependencies(root):
    constructor_re = re.compile(
        r"\bNew\s+([A-Za-z_]\w*(?:Manager|DAL|Client))\s*\(",
        re.IGNORECASE,
    )
    db_re = re.compile(r"\bDbHelper\s*\.", re.IGNORECASE)
    session_re = re.compile(r"\bSession(?:\.Item)?\s*\(", re.IGNORECASE)
    rows = []
    for path in vb_files(root):
        content = "\n".join(
            line for line in lines(path) if not line.lstrip().startswith("'")
        )
        constructors = collections.Counter(match.group(1) for match in constructor_re.finditer(content))
        rendered = ", ".join(f"{name}={count}" for name, count in sorted(constructors.items())) or "—"
        db_calls = 0 if path.name == "DbHelper.vb" else len(db_re.findall(content))
        rows.append(
            (
                relative(path, root),
                rendered,
                db_calls,
                len(session_re.findall(content)),
            )
        )
    markdown_table(["Plik", "New ...Manager/DAL/Client()", "DbHelper.", "Session()"], rows)


def measure_sql_outside_dal(root):
    string_re = re.compile(r'"(?:[^"]|"")*"')
    sql_re = re.compile(
        r'^"\s*(?:SELECT\b|INSERT\s+INTO\b|UPDATE\s+\S+\s+SET\b|DELETE\s+FROM\b|'
        r'MERGE\b|EXEC(?:UTE)?\b|FROM\b|JOIN\b|WHERE\b|ORDER\s+BY\b)',
        re.IGNORECASE,
    )
    rows = []
    for path in vb_files(root):
        rel = relative(path, root)
        if "\\DAL\\" in "\\" + rel:
            continue
        for number, line in enumerate(lines(path), 1):
            if any(sql_re.search(literal) for literal in string_re.findall(line)):
                rows.append((rel, number, line.strip()))
    markdown_table(["Plik", "Linia", "Linia z literałem SQL"], rows or [("—", "—", "brak dopasowań")])


def measure_procedures(root):
    definition_re = re.compile(
        r"\bCREATE\s+(?:OR\s+ALTER\s+)?PROC(?:EDURE)?\s+(?:\[?dbo\]?\.)?\[?(usp_[A-Za-z0-9_]+)\]?",
        re.IGNORECASE,
    )
    use_re = re.compile(r"\busp_[A-Za-z0-9_]+\b", re.IGNORECASE)
    definitions = collections.defaultdict(list)
    calls = collections.defaultdict(list)
    for path in root.rglob("*.sql"):
        for number, line in enumerate(lines(path), 1):
            for match in definition_re.finditer(line):
                definitions[match.group(1).lower()].append(f"{relative(path, root)}:{number}")
    for path in vb_files(root):
        for number, line in enumerate(lines(path), 1):
            for match in use_re.finditer(line):
                calls[match.group(0).lower()].append(f"{relative(path, root)}:{number}")
    names = sorted(set(definitions) | set(calls))
    rows = [
        (
            name,
            ", ".join(definitions.get(name, [])) or "—",
            ", ".join(calls.get(name, [])) or "—",
        )
        for name in names
    ]
    markdown_table(["Procedura", "Definicje", "Użycia w VB"], rows or [("—", "brak", "brak")])


def measure_model_methods(root):
    model = root / "WypozyczalniaCore" / "Model"
    rows = []
    for path in sorted(model.glob("*.vb")):
        for number, line in enumerate(lines(path), 1):
            match = METHOD_RE.match(line)
            if match:
                rows.append(
                    (
                        relative(path, root),
                        number,
                        match.group(1) or "domyślny",
                        match.group(2),
                        match.group(3),
                    )
                )
    markdown_table(
        ["Plik", "Linia", "Widoczność", "Rodzaj", "Nazwa"],
        rows or [("—", "—", "—", "—", "brak metod")],
    )


def measure_integration_callers(root):
    names = ("KurierClient", "ImportCennikaXml", "EksportCsv")
    rows = []
    for path in vb_files(root):
        rel = relative(path, root)
        if "\\Integracje\\" in "\\" + rel:
            continue
        for number, line in enumerate(lines(path), 1):
            if line.lstrip().startswith("'"):
                continue
            for name in names:
                if re.search(rf"\b{re.escape(name)}\b", line, re.IGNORECASE):
                    rows.append((name, rel, number, line.strip()))
    markdown_table(
        ["Integracja", "Wołający plik", "Linia", "Treść"],
        rows or [("—", "—", "—", "brak dopasowań")],
    )


def measure_rounding_and_percentages(root):
    pattern = re.compile(
        r"Math\.Round|"
        r"\b\d+(?:[\.,]\d+)?[D]?\s*/\s*100(?:[D])?\b|"
        r"\b\d+(?:[\.,]\d+)?\s*%|"
        r"\b\w*(?:Vat|Rabat|Proc|Procent|Kara|Spozn|Stawka|Mnoznik)\w*"
        r"\s*(?:As\s+\w+\s*)?=\s*['\"]?\d+[\.,]\d+[D]?",
        re.IGNORECASE,
    )
    rows = []
    for path in vb_files(root):
        for number, line in enumerate(lines(path), 1):
            if pattern.search(line):
                rows.append((relative(path, root), number, line.strip()))
    markdown_table(["Plik", "Linia", "Dopasowana linia"], rows or [("—", "—", "brak dopasowań")])


def measure_session_keys(root):
    pattern = re.compile(r'\bSession(?:\.Item)?\s*\(\s*"([^"]+)"\s*\)', re.IGNORECASE)
    found = collections.defaultdict(list)
    for path in vb_files(root):
        for number, line in enumerate(lines(path), 1):
            for match in pattern.finditer(line):
                found[match.group(1)].append(f"{relative(path, root)}:{number}")
    rows = [(key, len(locations), ", ".join(locations)) for key, locations in sorted(found.items())]
    markdown_table(["Klucz", "Liczba dostępów", "Lokalizacje"], rows or [("—", 0, "brak dopasowań")])


def measure_public_usage(root):
    files = vb_files(root)
    contents = {path: read_text(path) for path in files}
    rows = []
    for path in files:
        for number, line in enumerate(contents[path].splitlines(), 1):
            match = METHOD_RE.match(line)
            if not match or (match.group(1) or "").lower() != "public":
                continue
            name = match.group(3)
            word = re.compile(rf"\b{re.escape(name)}\b", re.IGNORECASE)
            locations = []
            for candidate, content in contents.items():
                count = len(word.findall(content))
                if count:
                    locations.append(f"{relative(candidate, root)}={count}")
            total = sum(int(item.rsplit("=", 1)[1]) for item in locations)
            rows.append(
                (
                    relative(path, root),
                    number,
                    match.group(2),
                    name,
                    total,
                    "; ".join(locations),
                )
            )
    markdown_table(
        ["Deklaracja", "Linia", "Rodzaj", "Nazwa", "Wystąpienia leksykalne", "Wystąpienia per plik"],
        rows or [("—", "—", "—", "—", 0, "brak metod publicznych")],
    )


MEASURES = {
    "M1": measure_tree,
    "M2": measure_roles,
    "M3": measure_dependencies,
    "M4": measure_sql_outside_dal,
    "M5": measure_procedures,
    "M6": measure_model_methods,
    "M7": measure_integration_callers,
    "M8": measure_rounding_and_percentages,
    "M9": measure_session_keys,
    "M10": measure_public_usage,
}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("measure", choices=MEASURES)
    parser.add_argument(
        "--root",
        type=Path,
        default=Path("legacy-app-parent") / "legacy-app",
    )
    args = parser.parse_args()
    root = args.root.resolve()
    if not root.is_dir():
        parser.error(f"nie istnieje katalog: {root}")
    MEASURES[args.measure](root)


if __name__ == "__main__":
    main()
