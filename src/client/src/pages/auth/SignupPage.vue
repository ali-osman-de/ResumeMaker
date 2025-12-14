<script setup lang="ts">
import { reactive, onMounted } from 'vue';
import AuthLayout from '../../components/layout/AuthLayout.vue';
import BackgroundDecor from '../../components/auth/BackgroundDecor.vue';
import IconMaterial from '../../components/ui/atoms/IconMaterial.vue';
import TextLink from '../../components/ui/atoms/TextLink.vue';
import FormField from '../../components/ui/molecules/FormField.vue';
import BaseButton from '../../components/ui/atoms/BaseButton.vue';
import DividerLabel from '../../components/ui/molecules/DividerLabel.vue';
import SocialAuthButton from '../../components/ui/molecules/SocialAuthButton.vue';
import { useAuthStore } from '../../stores/auth.store';

const authStore = useAuthStore();

const form = reactive({
  name: '',
  email: '',
  password: '',
});

const submit = async () => {
  await authStore.login({ email: form.email, password: form.password });
};

onMounted(() => {
  authStore.fetchMetadataMock();
});
</script>

<template>
  <AuthLayout>
    <template #brand>
      <IconMaterial name="description" size="28" />
    </template>
    <template #title>ResumeMaker</template>
    <template #cta>
      <span class="text-[#9690cb] text-sm font-medium mr-2">Hesabın var mı?</span>
      <TextLink to="/login">Giriş Yap</TextLink>
    </template>

    <div class="glass-panel w-full max-w-[520px] rounded-2xl p-8 md:p-10 flex flex-col gap-8">
      <div class="flex flex-col gap-2 text-center">
        <h2 class="text-3xl font-bold text-white tracking-tight">Kayıt Ol</h2>
        <p class="text-[#9690cb] text-sm">Yeni hesabınla CV’lerini güvenle oluştur.</p>
      </div>

      <SocialAuthButton label="Google ile devam et (yakında)" :disabled="authStore.isSocialDisabled" />

      <DividerLabel>veya e-posta ile</DividerLabel>

      <form class="flex flex-col gap-5" @submit.prevent="submit">
        <FormField
          id="name"
          label="Ad Soyad"
          placeholder="Adınız Soyadınız"
          v-model="form.name"
          type="text"
        />
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
        />
        <BaseButton type="submit" variant="primary" block>Kayıt Ol</BaseButton>
      </form>
    </div>

    <template #footer-decor>
      <BackgroundDecor />
    </template>
  </AuthLayout>
</template>
