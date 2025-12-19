<template>
  <div class="bg-background-dark text-white min-h-screen flex flex-col relative overflow-hidden font-body">
    <div class="gradient-blob bg-primary w-[500px] h-[500px] rounded-full top-[-100px] left-[-100px] animate-pulse"></div>
    <div class="gradient-blob bg-secondary-accent w-[400px] h-[400px] rounded-full bottom-[-50px] right-[-50px] opacity-30"></div>

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
        <h1 class="text-white text-xl font-bold tracking-tight">CV Creator</h1>
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

    <div class="absolute bottom-0 w-full h-1/3 pointer-events-none z-0">
      <img
        class="w-full h-full object-cover opacity-30"
        src="https://lh3.googleusercontent.com/aida-public/AB6AXuDWplV7zmx0zABzNzRUB6UTW9uyY-x5L-OzlPBxX34cD097d-rqrCfhWXldNJh_OdyiUKeVd_IEffyl_sZcQxMc5jKR19alfVgLwcaDVIE2TDCcIR5_49D1L0GrYjYcqFSYtau9u_nUdP354gR4oVJuQSJCodmNAh79ms7bcdNBNKpyzVXloRqb3OSkUAVdS1SSbe4EaRGRXHfB3KyNQNn50Jkg-m2hPIkaRkDz5GbdbvwA3yeB2Jd2zmklKT8CNsKXGMCYqljQrg"
        alt="Dekoratif arka plan"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

const isLogin = computed(() => route.name === "login");
const navPrompt = computed(() => (isLogin.value ? "Hesabın yok mu?" : "Zaten hesabın var mı?"));
const navCta = computed(() => (isLogin.value ? "Kayıt Ol" : "Giriş Yap"));
const navTarget = computed(() => (isLogin.value ? "/auth/signup" : "/auth/login"));
</script>

<style scoped>
:global(body) {
  font-family: "Inter", sans-serif;
  background-color: #0b1220;
}

.gradient-blob {
  position: absolute;
  filter: blur(100px);
  opacity: 0.4;
  z-index: 0;
}

:global(.glass-panel) {
  background: rgba(18, 16, 35, 0.6);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);
  border: 1px solid rgba(255, 255, 255, 0.08);
  box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
}

:global(.glow-button) {
  transition: all 0.3s ease;
}

:global(.glow-button:hover) {
  box-shadow: 0 0 20px rgba(110, 94, 247, 0.5);
}
</style>
