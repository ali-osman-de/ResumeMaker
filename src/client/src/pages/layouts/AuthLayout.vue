<template>
  <div class="min-h-screen flex items-center justify-center p-4">
    <div class="w-full max-w-md">
      <header class="mb-6 flex items-center justify-between">
        <h1 class="font-bold text-xl">CV Creator</h1>
        <RouterLink to="/auth/register">Kayıt Ol</RouterLink>
      </header>

      <RouterView />
    </div>
  </div>
</template>

<script setup lang="ts"></script>
