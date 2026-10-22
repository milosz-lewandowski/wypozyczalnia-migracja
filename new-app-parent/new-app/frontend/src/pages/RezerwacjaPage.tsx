import { useState, type FormEvent } from 'react';
import { fetchAvailable, type AvailableEquipment } from '../api/equipment';
import { createReservation, fetchCustomerReservations, type Reservation } from '../api/reservations';
import { ErrorMessage } from '../components/ErrorMessage';
import { Money } from '../components/Money';

const categories = ['BUD', 'OGR', 'IMP', 'TRN'];

const statusLabels: Record<Reservation['status'], string> = {
  NEW: 'nowa',
  CONFIRMED: 'potwierdzona',
  CANCELLED: 'anulowana',
  COMPLETED: 'zrealizowana',
};

/** Data z API (yyyy-MM-ddTHH:mm) do wyświetlenia w formacie pl-PL. */
function formatDateTime(value: string): string {
  return new Date(value).toLocaleString('pl-PL', { dateStyle: 'short', timeStyle: 'short' });
}

/**
 * Proces „rezerwacja sprzętu": 1) dostępność w kategorii i terminie, 2) wybór sprzętu z listy i zapis
 * dla klienta (Id) z ceną szacunkową, 3) lista rezerwacji klienta.
 */
export function RezerwacjaPage() {
  const [category, setCategory] = useState(categories[0]);
  const [from, setFrom] = useState('');
  const [to, setTo] = useState('');
  const [available, setAvailable] = useState<AvailableEquipment[] | null>(null);
  const [equipmentId, setEquipmentId] = useState<number | null>(null);
  const [customerId, setCustomerId] = useState('');
  const [notes, setNotes] = useState('');
  const [created, setCreated] = useState<Reservation | null>(null);
  const [customerReservations, setCustomerReservations] = useState<Reservation[] | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function run(action: () => Promise<void>) {
    setLoading(true);
    setError(null);
    try {
      await action();
    } catch (failure) {
      setError(failure instanceof Error ? failure.message : 'Nieznany błąd');
    } finally {
      setLoading(false);
    }
  }

  async function loadCustomerReservations(id: string) {
    setCustomerReservations(await fetchCustomerReservations(id));
  }

  function check(event: FormEvent) {
    event.preventDefault();
    void run(async () => {
      setAvailable(null);
      setEquipmentId(null);
      setCreated(null);
      setAvailable(await fetchAvailable({ category, from, to }));
    });
  }

  function submit(event: FormEvent) {
    event.preventDefault();
    if (equipmentId === null) {
      setError('Wybierz sprzęt z listy dostępnych');
      return;
    }
    void run(async () => {
      setCreated(null);
      const reservation = await createReservation({
        customerId: Number(customerId),
        equipmentId,
        from,
        to,
        notes: notes.trim() ? notes : undefined,
      });
      setCreated(reservation);
      setAvailable((list) => list?.filter((item) => item.id !== reservation.equipmentId) ?? null);
      setEquipmentId(null);
      await loadCustomerReservations(String(reservation.customerId));
    });
  }

  function showCustomerList() {
    if (!customerId.trim()) {
      setError('Podaj Id klienta');
      return;
    }
    void run(() => loadCustomerReservations(customerId.trim()));
  }

  return (
    <section>
      <h2>Rezerwacja sprzętu</h2>

      <h3>1. Dostępność w terminie</h3>
      <form onSubmit={check} className="form">
        <label>
          Kategoria
          <select data-testid="res-category" value={category} onChange={(e) => setCategory(e.target.value)}>
            {categories.map((item) => (
              <option key={item} value={item}>
                {item}
              </option>
            ))}
          </select>
        </label>
        <label>
          Od
          <input
            data-testid="res-from"
            type="datetime-local"
            value={from}
            onChange={(e) => setFrom(e.target.value)}
            required
          />
        </label>
        <label>
          Do
          <input
            data-testid="res-to"
            type="datetime-local"
            value={to}
            onChange={(e) => setTo(e.target.value)}
            required
          />
        </label>
        <button data-testid="res-check" type="submit" disabled={loading}>
          Sprawdź dostępność
        </button>
      </form>

      {available &&
        (available.length === 0 ? (
          <p data-testid="res-available-empty">Brak wolnego sprzętu w tym terminie.</p>
        ) : (
          <ul>
            {available.map((item) => (
              <li key={item.id} data-testid={`res-available-${item.id}`}>
                <label>
                  <input
                    type="radio"
                    name="res-equipment"
                    checked={equipmentId === item.id}
                    onChange={() => setEquipmentId(item.id)}
                  />{' '}
                  {item.inventoryNumber} {item.name} - <Money value={item.dailyRate} /> / doba, kaucja{' '}
                  <Money value={item.deposit} />
                </label>
              </li>
            ))}
          </ul>
        ))}

      <h3>2. Zapis dla klienta</h3>
      <form onSubmit={submit} className="form">
        <label>
          Klient (Id)
          <input
            data-testid="res-customer"
            type="number"
            min="1"
            value={customerId}
            onChange={(e) => setCustomerId(e.target.value)}
            required
          />
        </label>
        <label>
          Uwagi (opcjonalnie, do 500 znaków)
          <textarea
            data-testid="res-notes"
            maxLength={500}
            value={notes}
            onChange={(e) => setNotes(e.target.value)}
          />
        </label>
        <button data-testid="res-submit" type="submit" disabled={loading || equipmentId === null}>
          Zarezerwuj wybrany sprzęt
        </button>
      </form>

      <ErrorMessage message={error} testId="res-error" />

      {created && (
        <p data-testid="res-created">
          Zapisano nr {created.id}: sprzęt {created.equipmentId}, {formatDateTime(created.from)} -{' '}
          {formatDateTime(created.to)}, status {statusLabels[created.status]}, cena szacunkowa{' '}
          <Money value={created.estimatedPrice} testId="res-created-price" />
        </p>
      )}

      <h3>3. Lista klienta</h3>
      <button data-testid="res-customer-list" type="button" onClick={showCustomerList} disabled={loading}>
        Pokaż listę klienta
      </button>
      {customerReservations && (
        <table data-testid="res-list">
          <thead>
            <tr>
              <th>Nr</th>
              <th>Sprzęt</th>
              <th>Od</th>
              <th>Do</th>
              <th>Status</th>
              <th>Cena szacunkowa</th>
            </tr>
          </thead>
          <tbody>
            {customerReservations.map((item) => (
              <tr key={item.id} data-testid={`res-row-${item.id}`}>
                <td>{item.id}</td>
                <td>{item.equipmentId}</td>
                <td>{formatDateTime(item.from)}</td>
                <td>{formatDateTime(item.to)}</td>
                <td>{statusLabels[item.status]}</td>
                <td>
                  <Money value={item.estimatedPrice} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </section>
  );
}
