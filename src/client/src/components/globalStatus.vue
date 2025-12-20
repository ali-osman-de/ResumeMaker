<template>
  <Transition name="fade">
    <div
      v-if="loading"
      class="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-sm"
      role="status"
      aria-live="polite"
    >
      <div class="flex flex-col items-center gap-4 text-white">
        <div class="h-14 w-14 rounded-full border-4 border-white/40 border-t-white animate-spin"></div>
        <p class="text-sm font-medium tracking-wide">Yükleniyor...</p>
      </div>
    </div>
  </Transition>

  <Transition name="slide-up">
    <div
      v-if="error"
      class="fixed bottom-4 right-4 z-50 max-w-sm rounded-xl border border-white/10 bg-[#1c1836]/90 px-4 py-3 shadow-lg text-white backdrop-blur"
      role="alert"
    >
      <div class="flex items-start gap-3">
        <span class="material-symbols-outlined text-primary mt-0.5">error</span>
        <div class="flex-1">
          <p class="text-sm font-semibold">Bir sorun oluştu</p>
          <p class="text-sm text-white/80 mt-1 break-words">{{ error }}</p>
        </div>
        <button
          type="button"
          class="text-white/70 hover:text-white transition-colors"
          @click="dismissError"
          aria-label="Kapat"
        >
          <span class="material-symbols-outlined text-[18px]">close</span>
        </button>
      </div>
    </div>
  </Transition>

  <Transition name="slide-up">
    <div
      v-if="success"
      class="fixed bottom-4 left-4 z-50 max-w-sm rounded-xl border border-green-200/40 bg-green-600/90 px-4 py-3 shadow-lg text-white backdrop-blur"
      role="status"
    >
      <div class="flex items-start gap-3">
        <span class="material-symbols-outlined text-white mt-0.5">check_circle</span>
        <div class="flex-1">
          <p class="text-sm font-semibold">Başarılı</p>
          <p class="text-sm text-white/90 mt-1 break-words">{{ success }}</p>
        </div>
        <button
          type="button"
          class="text-white/80 hover:text-white transition-colors"
          @click="dismissSuccess"
          aria-label="Kapat"
        >
          <span class="material-symbols-outlined text-[18px]">close</span>
        </button>
      </div>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import { useLoader } from "@/helpers/loader";

const { loading, error, success, setError, setSuccess } = useLoader();

const dismissError = () => setError(null);
const dismissSuccess = () => setSuccess(null);
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.slide-up-enter-active,
.slide-up-leave-active {
  transition: all 0.2s ease;
}

.slide-up-enter-from,
.slide-up-leave-to {
  opacity: 0;
  transform: translateY(8px);
}
</style>
