import { defineStore } from 'pinia';

interface State {
  pending: number;
  error: string | null;
}

export const useAppStore = defineStore('app', {
  state: (): State => ({
    pending: 0,
    error: null,
  }),
  getters: {
    isLoading: (state) => state.pending > 0,
  },
  actions: {
    async runWithLoading<T>(fn: () => Promise<T>): Promise<T> {
      this.pending += 1;
      this.error = null;
      try {
        return await fn();
      } catch (err: any) {
        this.error = err?.message ?? 'Beklenmeyen bir hata oluştu.';
        throw err;
      } finally {
        this.pending = Math.max(0, this.pending - 1);
      }
    },
    clearError() {
      this.error = null;
    },
  },
});
