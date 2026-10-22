using System.Text.Json.Serialization;

namespace Wypozyczalnia.Dto;

/// <summary>
/// Zadanie POST /api/reservations: {"customerId", "equipmentId", "from", "to", "notes"?}.
/// Daty ISO (yyyy-MM-ddTHH:mm, sekundy opcjonalne); notes opcjonalne.
/// Pola nullowalne, bo brak pola zglasza kontroler komunikatem "Brak parametru: ...".
/// </summary>
public sealed record ReservationRequest(
    long? CustomerId,
    long? EquipmentId,
    [property: JsonConverter(typeof(IsoDateTimeJsonConverter))] DateTime? From,
    [property: JsonConverter(typeof(IsoDateTimeJsonConverter))] DateTime? To,
    string? Notes);
