import { getJson, postJson } from './http';

/** Rezerwacja z POST i GET /api/reservations (daty ISO yyyy-MM-ddTHH:mm, kwota z kropką dziesiętną). */
export interface Reservation {
  id: number;
  customerId: number;
  equipmentId: number;
  from: string;
  to: string;
  status: 'NEW' | 'CONFIRMED' | 'CANCELLED' | 'COMPLETED';
  estimatedPrice: number;
  notes: string | null;
}

export interface NewReservation {
  customerId: number;
  equipmentId: number;
  from: string;
  to: string;
  notes?: string;
}

export function createReservation(payload: NewReservation): Promise<Reservation> {
  return postJson<Reservation>('/api/reservations', payload);
}

export function fetchCustomerReservations(customerId: string): Promise<Reservation[]> {
  return getJson<Reservation[]>('/api/reservations', { customerId });
}
