<script setup lang="ts">
import BaseInput from '../atoms/BaseInput.vue';
import IconMaterial from '../atoms/IconMaterial.vue';

interface Props {
  id: string;
  label: string;
  type?: string;
  placeholder?: string;
  modelValue?: string;
  hintLinkText?: string;
  hintLinkHref?: string;
}

withDefaults(defineProps<Props>(), {
  type: 'text',
  placeholder: '',
  modelValue: '',
  hintLinkText: '',
  hintLinkHref: '',
});

const emit = defineEmits<{ 'update:modelValue': [value: string] }>();
</script>

<template>
  <div class="flex flex-col gap-2">
    <div class="flex justify-between items-center ml-1">
      <label class="text-sm font-medium text-gray-300" :for="id">{{ label }}</label>
      <a v-if="hintLinkText" class="text-xs font-medium text-primary hover:text-[#8b7eff] transition-colors" :href="hintLinkHref">
        {{ hintLinkText }}
      </a>
    </div>
    <div class="relative group">
      <div class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none">
        <IconMaterial :name="type === 'password' ? 'lock' : 'mail'" class="text-[#9690cb] group-focus-within:text-primary transition-colors text-[20px]" />
      </div>
      <BaseInput
        :id="id"
        :type="type"
        :placeholder="placeholder"
        :model-value="modelValue"
        @update:modelValue="emit('update:modelValue', $event)"
        :class="['pl-11 pr-4']"
      />
    </div>
  </div>
</template>
