namespace Wypozyczalnia.Services;

/// <summary>Bledne dane wejsciowe (HTTP 400, mapowanie w ApiExceptionHandler).</summary>
public class InvalidRequestException(string message) : Exception(message);
