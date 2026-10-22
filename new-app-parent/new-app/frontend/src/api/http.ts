// Jedyne miejsce z fetch: ścieżki względne /api/... (proxy Vite), błąd biznesowy = pole "error" z odpowiedzi.

export class ApiError extends Error {
  readonly status: number;

  constructor(message: string, status: number) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
  }
}

async function readBody(response: Response): Promise<unknown> {
  const text = await response.text();
  if (!text) {
    return null;
  }
  try {
    return JSON.parse(text);
  } catch {
    return text;
  }
}

async function handle<T>(response: Response): Promise<T> {
  const body = await readBody(response);
  if (!response.ok) {
    const message =
      body !== null && typeof body === 'object' && 'error' in body && typeof body.error === 'string'
        ? body.error
        : `Błąd serwera (${response.status})`;
    throw new ApiError(message, response.status);
  }
  return body as T;
}

export async function getJson<T>(path: string, params?: Record<string, string>): Promise<T> {
  const query = params ? `?${new URLSearchParams(params).toString()}` : '';
  const response = await fetch(`${path}${query}`);
  return handle<T>(response);
}

export async function postJson<T>(path: string, payload: unknown): Promise<T> {
  const response = await fetch(path, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(payload),
  });
  return handle<T>(response);
}
