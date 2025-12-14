<script setup lang="ts">
import IconMaterial from '../atoms/IconMaterial.vue';

defineProps<{
  label: string;
  disabled?: boolean;
}>();
</script>

<template>
  <button
    class="group flex w-full items-center justify-center gap-3 rounded-xl bg-[#262249]/50 border border-white/5 p-3.5 transition-all duration-200 text-white text-sm font-medium disabled:opacity-60 disabled:cursor-not-allowed"
    :disabled="disabled"
  >
    <IconMaterial name="g_mobiledata" class="text-white" />
    <span>{{ label }}</span>
  </button>
</template>
