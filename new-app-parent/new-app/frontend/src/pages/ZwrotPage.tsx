import { useEffect, useState, type FormEvent } from 'react';
import {
  DAMAGES,
  fetchOpenRentals,
  registerReturn,
  type DamageCode,
  type OpenRental,
  type ReturnResult,
} from '../api/rentals';
import { ErrorMessage } from '../components/ErrorMessage';
import { Money } from '../components/Money';

const dateFormat = new Intl.DateTimeFormat('pl-PL', { dateStyle: 'short', timeStyle: 'short' });

/** Data ISO bez strefy (yyyy-MM-ddTHH:mm) w formacie pl-PL; wejście traktowane jako czas lokalny. */
function formatDate(iso: string): string {
  const date = new Date(iso);
  return Number.isNaN(date.getTime()) ? iso : dateFormat.format(date);
}

/** Proces „zwrot sprzętu": wybór otwartego wypożyczenia, data i godzina zwrotu, szkody -> kara, opłata, zwrot kaucji. */
export function ZwrotPage() {
  const [rentals, setRentals] = useState<OpenRental[]>([]);
  const [selected, setSelected] = useState<OpenRental | null>(null);
  const [returnedAt, setReturnedAt] = useState('');
  const [damages, setDamages] = useState<DamageCode[]>([]);
  const [result, setResult] = useState<ReturnResult | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function loadRentals() {
    try {
      setRentals(await fetchOpenRentals());
    } catch (failure) {
      setError(failure instanceof Error ? failure.message : 'Nieznany błąd');
    }
  }

  useEffect(() => {
    void loadRentals();
  }, []);

  function pick(rental: OpenRental) {
    setSelected(rental);
    setReturnedAt('');
    setDamages([]);
    setResult(null);
    setError(null);
  }

  function toggleDamage(code: DamageCode, checked: boolean) {
    setDamages((current) => (checked ? [...current, code] : current.filter((item) => item !== code)));
  }

  async function submit(event: FormEvent) {
    event.preventDefault();
    if (!selected) {
      return;
    }
    setLoading(true);
    setError(null);
    setResult(null);
    try {
      const settled = await registerReturn(selected.id, returnedAt, damages);
      setResult(settled);
      setSelected(null);
      await loadRentals();
    } catch (failure) {
      setError(failure instanceof Error ? failure.message : 'Nieznany błąd');
    } finally {
      setLoading(false);
    }
  }

  return (
    <section>
      <h2>Zwrot sprzętu</h2>

      <h3>Otwarte wypożyczenia</h3>
      {rentals.length === 0 ? (
        <p className="placeholder">Brak otwartych wypożyczeń.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Nr</th>
              <th>Klient</th>
              <th>Sprzęt</th>
              <th>Wydanie</th>
              <th>Planowany zwrot</th>
              <th>Kaucja</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {rentals.map((rental) => (
              <tr key={rental.id} data-testid={`return-row-${rental.id}`}>
                <td>{rental.id}</td>
                <td>{rental.customerName}</td>
                <td>{rental.equipmentName}</td>
                <td>{formatDate(rental.issuedAt)}</td>
                <td>{formatDate(rental.plannedReturn)}</td>
                <td>
                  <Money value={rental.depositTaken} />
                </td>
                <td>
                  <button data-testid={`return-pick-${rental.id}`} type="button" onClick={() => pick(rental)}>
                    Wybierz
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {selected && (
        <form onSubmit={submit} className="form">
          <h3>
            Wypożyczenie {selected.id}: {selected.equipmentName}
          </h3>
          <label>
            Data i godzina zwrotu
            <input
              data-testid="return-returned-at"
              type="datetime-local"
              value={returnedAt}
              onChange={(e) => setReturnedAt(e.target.value)}
              required
            />
          </label>
          <fieldset>
            <legend>Szkody</legend>
            {DAMAGES.map((damage) => (
              <label key={damage.code}>
                <input
                  data-testid={`return-damage-${damage.code}`}
                  type="checkbox"
                  checked={damages.includes(damage.code)}
                  onChange={(e) => toggleDamage(damage.code, e.target.checked)}
                />{' '}
                {damage.label}
              </label>
            ))}
          </fieldset>
          <button data-testid="return-submit" type="submit" disabled={loading}>
            {loading ? 'Rozliczam...' : 'Rozlicz'}
          </button>
        </form>
      )}

      <ErrorMessage message={error} testId="return-error" />

      {result && (
        <dl className="result">
          <dt>Wypożyczenie</dt>
          <dd>{result.rentalId}</dd>
          <dt>Opłata</dt>
          <dd>
            <Money value={result.fee} testId="return-fee" />
          </dd>
          <dt>Doby spóźnienia</dt>
          <dd data-testid="return-late-days">{result.lateDays}</dd>
          <dt>Kara</dt>
          <dd data-testid="return-penalty">
            <Money value={result.penalty} />
          </dd>
          <dt>Szkody</dt>
          <dd>
            <Money value={result.damagesTotal} testId="return-damages-total" />
          </dd>
          <dt>Zwrot kaucji</dt>
          <dd data-testid="return-deposit-refund">
            <Money value={result.depositReturned} />
          </dd>
        </dl>
      )}
    </section>
  );
}
