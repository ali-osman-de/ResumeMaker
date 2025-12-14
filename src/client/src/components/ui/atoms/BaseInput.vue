<script setup lang="ts">
import type { InputHTMLAttributes } from 'vue';

interface Props extends /* @vue-ignore */ InputHTMLAttributes {
  modelValue?: string;
}

const props = defineProps<Props>();
const emit = defineEmits<{ 'update:modelValue': [value: string] }>();

const onInput = (event: Event) => {
  const target = event.target as HTMLInputElement;
  emit('update:modelValue', target.value);
};
</script>

<template>
  <input
    v-bind="props"
    :value="modelValue"
    @input="onInput"
    class="w-full bg-[#1c1836] border border-[#2d2850] text-white rounded-xl py-3.5 px-4 focus:ring-2 focus:ring-primary/50 focus:border-primary placeholder-[#645d94] transition-all outline-none text-sm"
  />
</template>
