<script setup lang="ts">
import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import AuthLayout from '../../components/layout/AuthLayout.vue';
import LoginCard from '../../components/auth/LoginCard.vue';
import BackgroundDecor from '../../components/auth/BackgroundDecor.vue';
import IconMaterial from '../../components/ui/atoms/IconMaterial.vue';
import TextLink from '../../components/ui/atoms/TextLink.vue';
import { useAuthStore } from '../../stores/auth.store';

const authStore = useAuthStore();
const router = useRouter();

onMounted(() => {
  authStore.fetchMetadataMock();
});

const handleLoginSuccess = () => {
  router.push('/dashboard');
};
</script>

<template>
  <AuthLayout>
    <template #brand>
      <IconMaterial name="description" size="28" />
    </template>
    <template #title>ResumeMaker</template>
    <template #cta>
      <span class="text-[#9690cb] text-sm font-medium mr-2">Hesabın yok mu?</span>
      <TextLink to="/signup">Kayıt Ol</TextLink>
    </template>

    <LoginCard @login-success="handleLoginSuccess" />

    <template #footer-decor>
      <BackgroundDecor />
    </template>
  </AuthLayout>
</template>
