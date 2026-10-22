import { getJson } from './http';

/** Pozycja odpowiedzi GET /api/equipment/available (kwoty z kropką dziesiętną). */
export interface AvailableEquipment {
  id: number;
  inventoryNumber: string;
  name: string;
  category: string;
  dailyRate: number;
  deposit: number;
}

export interface AvailabilityQuery {
  category: string;
  from: string;
  to: string;
}

export function fetchAvailable(query: AvailabilityQuery): Promise<AvailableEquipment[]> {
  return getJson<AvailableEquipment[]>('/api/equipment/available', {
    category: query.category,
    from: query.from,
    to: query.to,
  });
}
