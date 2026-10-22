namespace Wypozyczalnia.Services;

/// <summary>Regula biznesowa nie pozwala wykonac operacji w obecnym stanie danych (HTTP 409, mapowanie w ApiExceptionHandler).</summary>
public class ConflictException(string message) : Exception(message);
