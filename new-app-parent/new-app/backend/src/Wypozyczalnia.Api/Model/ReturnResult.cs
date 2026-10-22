namespace Wypozyczalnia.Model;

/// <summary>
/// Rozliczenie zwrotu: oplata, spoznienie (minuty i doby), kara, szkody, kaucja pobrana i zwrocona
/// (legacy-app-parent/legacy-app/WypozyczalniaCore/RentalManager.vb:109-125).
/// </summary>
public sealed record ReturnResult(
    long RentalId,
    DateTime ReturnedAt,
    decimal Fee,
    long LateMinutes,
    long LateDays,
    decimal Penalty,
    decimal DamagesTotal,
    decimal DepositTaken,
    decimal DepositReturned,
    RentalStatus Status);
