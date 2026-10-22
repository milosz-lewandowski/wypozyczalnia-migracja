import { useState, type ComponentType } from 'react';
import { Tabs, type TabItem } from './components/Tabs';
import * as rezerwacja from './pages/RezerwacjaPage';
import * as wycena from './pages/WycenaPage';
import * as zwrot from './pages/ZwrotPage';

/** Strona-zaślepka eksportuje `migrated = false`; brak flagi = proces zmigrowany. */
function isPending(pageModule: object): boolean {
  return (pageModule as { migrated?: boolean }).migrated === false;
}

interface Page extends TabItem {
  component: ComponentType;
}

const pages: Page[] = [
  { id: 'wycena', label: 'Wycena', component: wycena.WycenaPage, pending: isPending(wycena) },
  { id: 'rezerwacja', label: 'Rezerwacja', component: rezerwacja.RezerwacjaPage, pending: isPending(rezerwacja) },
  { id: 'zwrot', label: 'Zwrot', component: zwrot.ZwrotPage, pending: isPending(zwrot) },
];

export function App() {
  // Na starcie pierwsza zakładka z przeniesionym procesem (zaślepki są wyszarzone).
  const [activeId, setActiveId] = useState((pages.find((page) => !page.pending) ?? pages[0]).id);
  const ActivePage = (pages.find((page) => page.id === activeId) ?? pages[0]).component;

  return (
    <div className="app">
      <header>
        <h1>Wypożyczalnia sprzętu</h1>
        <Tabs tabs={pages} activeId={activeId} onSelect={setActiveId} />
      </header>
      <main>
        <ActivePage />
      </main>
    </div>
  );
}
