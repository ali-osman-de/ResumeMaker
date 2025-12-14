import { defineStore } from 'pinia';
import { useAppStore } from './app.store';
import type { AuthMetadata, LoginPayload } from './auth.types';

interface State {
  metadata: AuthMetadata | null;
  loading: boolean;
}

export const useAuthStore = defineStore('auth', {
  state: (): State => ({
    metadata: null,
    loading: false,
  }),
  getters: {
    isSocialDisabled: (state) => !state.metadata?.socialLoginEnabled,
  },
  actions: {
    async fetchMetadataMock() {
      const app = useAppStore();
      return app.runWithLoading(async () => {
        this.loading = true;

        await new Promise((resolve) => setTimeout(resolve, 200));
        this.metadata = {
          socialLoginEnabled: false,
          provider: 'google',
        };
        this.loading = false;
        return this.metadata;
      });
    },
    async login(payload: LoginPayload) {
      const app = useAppStore();
      return app.runWithLoading(async () => {
        await new Promise((resolve) => setTimeout(resolve, 300));
        const ok = (payload.email === '123@gmail.com') && payload.password === '123';
        if (!ok) {
          throw new Error('Geçersiz kullanıcı adı veya şifre.');
        }
        return true;
      });
    },
  },
});
