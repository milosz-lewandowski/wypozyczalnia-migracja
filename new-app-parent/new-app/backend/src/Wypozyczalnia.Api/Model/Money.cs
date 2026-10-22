namespace Wypozyczalnia.Model;

/// <summary>
/// Zaokraglanie kwot jak w legacy: <c>Math.Round(Decimal, 2)</c> w .NET = polowki do parzystej
/// (legacy-app-parent/legacy-app/WypozyczalniaCore/PricingManager.vb:36, :47, :53).
/// </summary>
public static class Money
{
    /// <summary>
    /// Skala 2, polowki do parzystej (0.125 -> 0.12, 0.135 -> 0.14, 141.365 -> 141.36).
    /// Wynik ma zawsze dwa miejsca po przecinku (45 -> 45.00).
    /// </summary>
    public static decimal Round(decimal value)
    {
        decimal rounded = Math.Round(value, 2, MidpointRounding.ToEven);
        // Dodanie 0.00m ustawia skale na 2 bez zmiany wartosci (45m -> 45.00m), wiec JSON pokazuje "45.00".
        return rounded + 0.00m;
    }
}
