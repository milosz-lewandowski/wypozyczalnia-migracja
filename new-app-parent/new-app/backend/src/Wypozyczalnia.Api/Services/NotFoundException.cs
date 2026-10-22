namespace Wypozyczalnia.Services;

/// <summary>Brak wskazanego zasobu (HTTP 404, mapowanie w ApiExceptionHandler).</summary>
public class NotFoundException(string message) : Exception(message);
