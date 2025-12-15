<template>
  <div class="min-h-screen">
    <header class="h-14 px-6 flex items-center border-b">
      <nav class="flex gap-4">
        <RouterLink to="/app">Dashboard</RouterLink>
        <RouterLink to="/app/profile">Profil</RouterLink>
      </nav>
    </header>

    <main class="p-6">
      <RouterView />
    </main>
  </div>
</template>

<script setup lang="ts"></script>
