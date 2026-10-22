import { useState, type FormEvent } from 'react';
import { fetchQuote, type Quote } from '../api/pricing';
import { ErrorMessage } from '../components/ErrorMessage';
import { Money } from '../components/Money';

/** Proces „wycena wynajmu": sprzęt, termin i opcjonalnie klient (Id) -> kwota z liczbą dób i rabatami. */
export function WycenaPage() {
  const [equipmentId, setEquipmentId] = useState('');
  const [from, setFrom] = useState('');
  const [to, setTo] = useState('');
  const [customerId, setCustomerId] = useState('');
  const [quote, setQuote] = useState<Quote | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  async function submit(event: FormEvent) {
    event.preventDefault();
    setLoading(true);
    setError(null);
    setQuote(null);
    try {
      setQuote(await fetchQuote({ equipmentId, from, to, customerId: customerId.trim() }));
    } catch (failure) {
      setError(failure instanceof Error ? failure.message : 'Nieznany błąd');
    } finally {
      setLoading(false);
    }
  }

  return (
    <section>
      <h2>Wycena wynajmu</h2>
      <form onSubmit={submit} className="form">
        <label>
          Sprzęt (Id)
          <input
            data-testid="quote-equipment"
            type="number"
            min="1"
            value={equipmentId}
            onChange={(e) => setEquipmentId(e.target.value)}
            required
          />
        </label>
        <label>
          Wydanie
          <input
            data-testid="quote-from"
            type="datetime-local"
            value={from}
            onChange={(e) => setFrom(e.target.value)}
            required
          />
        </label>
        <label>
          Zwrot
          <input
            data-testid="quote-to"
            type="datetime-local"
            value={to}
            onChange={(e) => setTo(e.target.value)}
            required
          />
        </label>
        <label>
          Klient (Id, opcjonalnie)
          <input
            data-testid="quote-customer"
            type="number"
            min="1"
            value={customerId}
            onChange={(e) => setCustomerId(e.target.value)}
          />
        </label>
        <button data-testid="quote-submit" type="submit" disabled={loading}>
          {loading ? 'Liczę...' : 'Oblicz opłatę'}
        </button>
      </form>

      <ErrorMessage message={error} testId="quote-error" />

      {quote && (
        <dl className="result">
          <dt>Kwota</dt>
          <dd>
            <Money value={quote.amount} testId="quote-amount" />
          </dd>
          <dt>Liczba dób</dt>
          <dd data-testid="quote-days">{quote.days}</dd>
          <dt>Doby weekendowe</dt>
          <dd data-testid="quote-weekend-days">{quote.weekendDays}</dd>
          <dt>Rabat klienta</dt>
          <dd data-testid="quote-discount">{quote.customerDiscountPercent}%</dd>
          <dt>Rabat tygodniowy</dt>
          <dd>{quote.weeklyDiscountPercent}%</dd>
          <dt>Stawka dobowa / weekendowa</dt>
          <dd>
            <Money value={quote.dailyRate} /> / <Money value={quote.weekendRate} />
          </dd>
        </dl>
      )}
    </section>
  );
}
