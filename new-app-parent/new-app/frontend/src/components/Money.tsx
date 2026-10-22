const moneyFormat = new Intl.NumberFormat('pl-PL', { style: 'currency', currency: 'PLN' });

/** Kwota w formacie pl-PL, np. 141,36 zł (Intl wstawia twardą spację przed „zł"). */
export function formatMoney(value: number): string {
  return moneyFormat.format(value);
}

interface MoneyProps {
  value: number;
  testId?: string;
}

export function Money({ value, testId }: MoneyProps) {
  return <span data-testid={testId}>{formatMoney(value)}</span>;
}
