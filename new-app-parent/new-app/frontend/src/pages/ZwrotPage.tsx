// Zaślepka: proces zwrotu jeszcze nie przeniesiony z legacy. Przeniesienie zastępuje ten plik w całości.
// Flaga `migrated = false` wyszarza zakładkę w App.tsx; nowa strona może jej nie eksportować (brak flagi = przeniesiona).
export const migrated = false;

export function ZwrotPage() {
  return (
    <section>
      <h2>Zwrot</h2>
      <p className="placeholder">Proces jeszcze nie przeniesiony z legacy.</p>
    </section>
  );
}
