<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  name: string;
  size?: string | number;
  className?: string;
}>();

const computedSize = computed(() => (typeof props.size === 'number' ? `${props.size}px` : props.size || '20px'));
</script>

<template>
  <span
    class="material-symbols-outlined align-middle"
    :class="className"
    :style="{ fontSize: computedSize }"
    aria-hidden="true"
  >
    {{ name }}
  </span>
</template>
