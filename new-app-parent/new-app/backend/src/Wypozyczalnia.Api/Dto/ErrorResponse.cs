namespace Wypozyczalnia.Dto;

/// <summary>Tresc odpowiedzi bledu API: {"error": "..."}.</summary>
public sealed record ErrorResponse(string Error);
