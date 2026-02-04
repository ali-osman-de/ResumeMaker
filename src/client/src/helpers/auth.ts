const NAMEID_KEY = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
const ACCESS_TOKEN_KEY = "access_token";
const REFRESH_TOKEN_KEY = "refresh_token";

let accessTokenCache: string | null = null;
let refreshTokenCache: string | null = null;

function decodeBase64Url(value: string): string {
  const normalized = value.replace(/-/g, "+").replace(/_/g, "/");
  const padded = normalized.padEnd(normalized.length + (4 - (normalized.length % 4)) % 4, "=");
  return atob(padded);
}

export function hydrateTokensFromStorage() {
  accessTokenCache = localStorage.getItem(ACCESS_TOKEN_KEY);
  refreshTokenCache = localStorage.getItem(REFRESH_TOKEN_KEY);
}

export function setTokens(tokens: { accessToken?: string | null | undefined; refreshToken?: string | null | undefined }) {
  accessTokenCache = tokens.accessToken ?? null;
  refreshTokenCache = tokens.refreshToken ?? null;

  if (tokens.accessToken) {
    localStorage.setItem(ACCESS_TOKEN_KEY, tokens.accessToken);
  } else {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
  }

  if (tokens.refreshToken !== undefined) {
    if (tokens.refreshToken) {
      localStorage.setItem(REFRESH_TOKEN_KEY, tokens.refreshToken);
    } else {
      localStorage.removeItem(REFRESH_TOKEN_KEY);
    }
  }
}

export function clearTokens() {
  accessTokenCache = null;
  refreshTokenCache = null;
  localStorage.removeItem(ACCESS_TOKEN_KEY);
  localStorage.removeItem(REFRESH_TOKEN_KEY);
}

export function getAccessToken(): string | null {
  if (accessTokenCache !== null) return accessTokenCache;
  accessTokenCache = localStorage.getItem(ACCESS_TOKEN_KEY);
  return accessTokenCache;
}

export function getRefreshToken(): string | null {
  if (refreshTokenCache !== null) return refreshTokenCache;
  refreshTokenCache = localStorage.getItem(REFRESH_TOKEN_KEY);
  return refreshTokenCache;
}

export function getAuthHeaders(): Record<string, string> {
  const token = getAccessToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

export function getUserIdClaim(): string | null {
  const token = getAccessToken();
  if (!token) return null;
  const parts = token.split(".");
  if (parts.length !== 3) return null;
  try {
    const payload = JSON.parse(decodeBase64Url(parts[1]!)) as Record<string, unknown>;
    const id = payload[NAMEID_KEY] ?? payload["sub"];
    return typeof id === "string" ? id : null;
  } catch {
    return null;
  }
}
