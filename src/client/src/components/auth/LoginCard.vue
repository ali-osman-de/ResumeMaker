<script setup lang="ts">
import AuthHeader from './AuthHeader.vue';
import LoginForm from './LoginForm.vue';

const emit = defineEmits<{
  (e: 'login-success'): void;
}>();
</script>

<template>
  <LoginForm @login-success="emit('login-success')">
    <template #header>
      <AuthHeader />
    </template>
  </LoginForm>
</template>
