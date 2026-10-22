export interface TabItem {
  id: string;
  label: string;
  /** Proces jeszcze niezmigrowany: zakładka wyszarzona, ale klikalna (pokazuje stronę-zaślepkę). */
  pending?: boolean;
}

interface TabsProps {
  tabs: TabItem[];
  activeId: string;
  onSelect: (id: string) => void;
}

export function Tabs({ tabs, activeId, onSelect }: TabsProps) {
  return (
    <nav className="tabs">
      {tabs.map((tab) => {
        const classes = ['tab'];
        if (tab.id === activeId) classes.push('tab-active');
        if (tab.pending) classes.push('tab-pending');
        return (
          <button
            key={tab.id}
            type="button"
            className={classes.join(' ')}
            aria-pressed={tab.id === activeId}
            title={tab.pending ? 'Proces jeszcze niezmigrowany' : undefined}
            onClick={() => onSelect(tab.id)}
          >
            {tab.label}
          </button>
        );
      })}
    </nav>
  );
}
