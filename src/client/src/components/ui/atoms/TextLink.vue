<script setup lang="ts">
import { RouterLink } from 'vue-router';

const props = defineProps<{
  to?: string;
  href?: string;
}>();

const tag = props.to ? RouterLink : 'a';
</script>

<template>
  <component
    :is="tag"
    :to="to"
    :href="href"
    class="text-primary font-semibold text-sm hover:underline transition-colors"
  >
    <slot />
  </component>
</template>
