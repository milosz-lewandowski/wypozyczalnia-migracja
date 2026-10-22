import { getJson, postJson } from './http';

/** Pozycja GET /api/rentals (otwarte wypożyczenia; daty ISO yyyy-MM-ddTHH:mm). */
export interface OpenRental {
  id: number;
  customerId: number;
  customerName: string;
  equipmentId: number;
  equipmentName: string;
  issuedAt: string;
  plannedReturn: string;
  depositTaken: number;
}

/** Kody szkód z cennika (kwoty liczy backend). */
export type DamageCode = 'CASING' | 'ACCESSORIES' | 'DIRT';

export const DAMAGES: { code: DamageCode; label: string }[] = [
  { code: 'CASING', label: 'Uszkodzona obudowa' },
  { code: 'ACCESSORIES', label: 'Brak akcesoriów' },
  { code: 'DIRT', label: 'Zabrudzenie' },
];

/** Odpowiedź POST /api/rentals/{id}/return. */
export interface ReturnResult {
  rentalId: number;
  returnedAt: string;
  fee: number;
  lateMinutes: number;
  lateDays: number;
  penalty: number;
  damagesTotal: number;
  depositTaken: number;
  depositReturned: number;
  status: string;
}

export function fetchOpenRentals(): Promise<OpenRental[]> {
  return getJson<OpenRental[]>('/api/rentals');
}

export function registerReturn(rentalId: number, returnedAt: string, damages: DamageCode[]): Promise<ReturnResult> {
  return postJson<ReturnResult>(`/api/rentals/${rentalId}/return`, { returnedAt, damages });
}
