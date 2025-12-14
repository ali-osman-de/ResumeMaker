<script setup lang="ts">
import { reactive } from 'vue';
import FormField from '../ui/molecules/FormField.vue';
import BaseButton from '../ui/atoms/BaseButton.vue';
import DividerLabel from '../ui/molecules/DividerLabel.vue';
import SocialAuthButton from '../ui/molecules/SocialAuthButton.vue';
import { useAuthStore } from '../../stores/auth.store';

const authStore = useAuthStore();

const form = reactive({
  email: '',
  password: '',
});

const emit = defineEmits<{
  (e: 'login-success'): void;
}>();

const submit = async () => {
  try {
    const ok = await authStore.login({ email: form.email, password: form.password });
    if (ok) emit('login-success');
  } catch (err) {
    // error already handled by app store; no-op
  }
};
</script>

<template>
  <div class="glass-panel w-full max-w-[520px] rounded-2xl p-8 md:p-10 flex flex-col gap-8">
    <slot name="header" />

    <SocialAuthButton label="Google ile devam et (yakında)" :disabled="authStore.isSocialDisabled" />

    <DividerLabel>veya e-posta ile</DividerLabel>

    <form class="flex flex-col gap-5" @submit.prevent="submit">
      <FormField
        id="email"
        label="E-posta Adresi"
        placeholder="ornek@email.com"
        v-model="form.email"
        type="email"
      />
      <FormField
        id="password"
        label="Şifre"
        placeholder="••••••••"
        v-model="form.password"
        type="password"
        hint-link-text="Şifremi Unuttum?"
        hint-link-href="#"
      />
      <BaseButton type="submit" variant="primary" block>Giriş Yap</BaseButton>
    </form>
  </div>
</template>
