namespace Wypozyczalnia.Model;

/// <summary>
/// Cennik szkod przy zwrocie (legacy-app-parent/legacy-app/WypozyczalniaWeb/Zwrot.aspx.vb:224-226; ta sama lista
/// powielona w podgladzie :176-183). Jedno zrodlo kwot szkod w nowej aplikacji; Code = kod z kontraktu API.
/// </summary>
public sealed record DamageType(string Code, string Label, decimal Amount)
{
    public static readonly DamageType Casing = new("CASING", "Uszkodzona obudowa", 50.00m);
    public static readonly DamageType Accessories = new("ACCESSORIES", "Brak akcesoriów", 30.00m);
    public static readonly DamageType Dirt = new("DIRT", "Zabrudzenie", 20.00m);

    public static IReadOnlyList<DamageType> All { get; } = [Casing, Accessories, Dirt];

    /// <summary>Pozycja cennika po kodzie (porownanie dokladne, wielkosc liter jak w kontrakcie API); brak = null.</summary>
    public static DamageType? FindByCode(string code) =>
        All.FirstOrDefault(damage => string.Equals(damage.Code, code, StringComparison.Ordinal));
}
