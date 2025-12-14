<script setup lang="ts">
import { RouterView } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useAppStore } from './stores/app.store';

const appStore = useAppStore();
const { isLoading, error } = storeToRefs(appStore);
</script>

<template>
  <RouterView />

  <div
    v-if="isLoading"
    class="fixed inset-0 bg-black/40 flex items-center justify-center z-50 text-white text-sm"
  >
    Yükleniyor...
  </div>

  <div
    v-if="error"
    class="fixed bottom-4 right-4 bg-red-600 text-white px-4 py-3 rounded-lg shadow-lg z-50 text-sm"
  >
    {{ error }}
  </div>
</template>
