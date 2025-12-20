<template>
  <div class="relative min-h-screen flex flex-col overflow-hidden bg-background-dark text-white font-body">
    <div class="pointer-events-none absolute inset-0">
      <div class="absolute inset-0 bg-cover bg-center opacity-45" :style="{ backgroundImage: `url(${authLayoutBg})` }"></div>
      <div class="absolute inset-0 bg-gradient-to-b from-background-dark/90 via-background-dark/85 to-background-dark"></div>
    </div>

    <div
      class="pointer-events-none absolute -top-32 -left-32 h-[520px] w-[520px] rounded-full bg-primary blur-[140px] opacity-35"
      aria-hidden="true"
    ></div>
    <div
      class="pointer-events-none absolute -bottom-28 -right-28 h-[440px] w-[440px] rounded-full bg-secondary-accent blur-[150px] opacity-25"
      aria-hidden="true"
    ></div>

    <header class="relative z-10 w-full px-8 py-6 flex items-center justify-between">
      <div class="flex items-center gap-3">
        <div class="size-8 text-primary">
          <svg fill="none" viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg">
            <path
              clip-rule="evenodd"
              d="M12.0799 24L4 19.2479L9.95537 8.75216L18.04 13.4961L18.0446 4H29.9554L29.96 13.4961L38.0446 8.75216L44 19.2479L35.92 24L44 28.7521L38.0446 39.2479L29.96 34.5039L29.9554 44H18.0446L18.04 34.5039L9.95537 39.2479L4 28.7521L12.0799 24Z"
              fill="currentColor"
              fill-rule="evenodd"
            ></path>
          </svg>
        </div>
        <h1 class="text-white text-xl font-bold tracking-tight">ResumeMaker</h1>
      </div>
      <div class="hidden md:block">
        <span class="text-[#9690cb] text-sm font-medium mr-2">{{ navPrompt }}</span>
        <RouterLink :to="navTarget" class="text-white text-sm font-bold hover:text-primary transition-colors">
          {{ navCta }}
        </RouterLink>
      </div>
    </header>

    <main class="relative z-10 flex-1 flex items-center justify-center p-4">
      <RouterView />
    </main>
  </div>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import authLayoutBg from "@/assets/layouts/authlayout.png";

const route = useRoute();

const isLogin = computed(() => route.name === "login");
const navPrompt = computed(() => (isLogin.value ? "Hesabın yok mu?" : "Zaten hesabın var mı?"));
const navCta = computed(() => (isLogin.value ? "Kayıt Ol" : "Giriş Yap"));
const navTarget = computed(() => (isLogin.value ? "/auth/signup" : "/auth/login"));
</script>
