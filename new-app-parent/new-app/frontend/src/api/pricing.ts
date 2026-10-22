import { getJson } from './http';

/** Odpowiedź GET /api/pricing/quote (daty ISO yyyy-MM-ddTHH:mm, kwoty z kropką dziesiętną). */
export interface Quote {
  equipmentId: number;
  from: string;
  to: string;
  customerId: number | null;
  days: number;
  weekendDays: number;
  dailyRate: number;
  weekendRate: number;
  weeklyDiscountPercent: number;
  customerDiscountPercent: number;
  amount: number;
}

export interface QuoteQuery {
  equipmentId: string;
  from: string;
  to: string;
  customerId?: string;
}

export function fetchQuote(query: QuoteQuery): Promise<Quote> {
  const params: Record<string, string> = {
    equipmentId: query.equipmentId,
    from: query.from,
    to: query.to,
  };
  if (query.customerId) {
    params.customerId = query.customerId;
  }
  return getJson<Quote>('/api/pricing/quote', params);
}
