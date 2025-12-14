export interface LoginPayload {
  email: string;
  password: string;
}

export interface AuthMetadata {
  socialLoginEnabled: boolean;
  provider: 'google';
}
