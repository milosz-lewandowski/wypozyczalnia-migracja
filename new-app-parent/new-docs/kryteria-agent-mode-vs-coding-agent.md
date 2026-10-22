# Agent w edytorze czy agent w chmurze: kryteria wyboru

Agent w edytorze = lokalna kopia repo, nadzór na bieżąco. Agent w chmurze (Copilot coding agent) = uruchamiany z Issue albo z panelu agentów, pracuje na własnej gałęzi w środowisku GitHub Actions i kończy pracę Pull Requestem.

## Reguła w jednym zdaniu

Im stabilniejsza specyfikacja i im mniej nadzoru w trakcie, tym bardziej zadanie nadaje się do chmury; zadania eksploracyjne, z częstymi decyzjami albo wymagające lokalnego środowiska zostają w edytorze.

## Kryteria

| kryterium | edytor | chmura |
|---|---|---|
| Specyfikacja | rodzi się w trakcie | spisana z góry: Issue z szablonu `.github/ISSUE_TEMPLATE/zadanie-dla-agenta.md` |
| Nadzór | stały: akceptuję kroki, przerywam, koryguję | punktowy: obserwuję sesję, koryguję komentarzem w PR |
| Rozmiar i czas | krótkie pętle, minuty | zadanie domknięte w jednej sesji, kilkanaście minut i więcej |
| Środowisko | lokalne narzędzia, debugger, przeglądarka | tylko to, co przygotuje `.github/workflows/copilot-setup-steps.yml` |
| Instrukcje | `AGENTS.md`, `.github/copilot-instructions.md`, instrukcje i skill z `.github/` | te same pliki, czytane z repo |
| Wynik i review | zmiany w drzewie roboczym, review przed commitem | PR z historią commitów, CI z bramką, ocena wg `.github/ocena/kryteria.md` |
| Ochrona | instrukcje ścieżkowe, ustawienia edytora | chronione pliki w CI (`.github/workflows/pr-zakres.yml`), wymagany review |
| Koszt | czas przy klawiaturze, żądania modelu | żądania modelu i minuty GitHub Actions |

## Kiedy który

- **Chmura:** mały feature z gotowym kontraktem (`new-app-parent/new-docs/KONTRAKT-API.md`), testami i bramką (`tools/bramka.ps1`); kilka niezależnych zadań naraz.
- **Edytor:** analiza nieznanego kodu, decyzje architektoniczne, migracja procesu z częstymi korektami, debugging.
- **Sygnał ostrzegawczy:** nie umiesz spisać kryteriów akceptacji w 5-7 punktach = zadanie nie jest gotowe dla chmury.

## Pytania kontrolne przed przypisaniem Issue

1. Czy Issue ma cel, pliki startowe, kryteria akceptacji, testy, zakres wyłączony i definicję ukończenia?
2. Czy środowisko chmury zbuduje projekt i uruchomi bramkę (`copilot-setup-steps.yml`)?
3. Czy testy i przypadki bramki złapią naruszenie kontraktu, czy tylko potwierdzą szczęśliwą ścieżkę?
4. Kto i według jakich kryteriów zrobi review PR?
