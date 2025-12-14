<script setup lang="ts">
import type { ButtonHTMLAttributes } from 'vue';

type Variant = 'primary' | 'ghost' | 'outline';

interface Props extends /* @vue-ignore */ ButtonHTMLAttributes {
  variant?: Variant;
  loading?: boolean;
  disabled?: boolean;
  block?: boolean;
}

const props = withDefaults(defineProps<Props>(), {
  variant: 'primary',
  loading: false,
  disabled: false,
  block: false,
});

const variantClasses: Record<Variant, string> = {
  primary:
    'bg-primary hover:bg-[#5b4ddb] text-white border border-primary/70 hover:shadow-glow rounded-xl',
  ghost:
    'bg-transparent border border-white/10 text-white hover:bg-white/5 hover:border-white/20 text-sm rounded-xl',
  outline:
    'bg-transparent border border-[#2d2850] text-white hover:border-primary/80 hover:text-white hover:bg-primary/5 rounded-xl',
};
</script>

<template>
  <button
    v-bind="$attrs"
    :disabled="props.disabled || props.loading"
    class="inline-flex items-center justify-center gap-2 px-4 py-3 font-semibold text-sm transition-all focus:outline-none focus:ring-2 focus:ring-primary/50 disabled:opacity-60 disabled:cursor-not-allowed rounded-xl"
    :class="[
      variantClasses[props.variant],
      props.block ? 'w-full' : '',
      props.loading ? 'cursor-wait' : '',
    ]"
  >
    <slot />
  </button>
</template>
