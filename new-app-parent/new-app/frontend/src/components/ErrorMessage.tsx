interface ErrorMessageProps {
  message: string | null;
  testId?: string;
}

/** Komunikat błędu biznesowego z pola "error" API, pokazany bez zmian. */
export function ErrorMessage({ message, testId }: ErrorMessageProps) {
  if (!message) {
    return null;
  }
  return (
    <p role="alert" className="error" data-testid={testId}>
      {message}
    </p>
  );
}
