<template>
  <section class="glass-panel w-full max-w-[480px] rounded-2xl p-8 md:p-10 flex flex-col gap-8">
    <div class="flex flex-col gap-2 text-center">
      <h2 class="text-3xl font-bold text-white tracking-tight">Hoş Geldiniz</h2>
      <p class="text-[#9690cb] text-sm">Hayalindeki işe bir adım daha yaklaşmak için giriş yap.</p>
    </div>

    <button
      class="group flex w-full items-center justify-center gap-3 rounded-xl bg-[#262249]/50 hover:bg-[#262249] border border-white/5 hover:border-white/10 p-3.5 transition-all duration-200"
      type="button"
    >
      <svg class="w-5 h-5" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
        <path d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z" fill="#4285F4"></path>
        <path d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z" fill="#34A853"></path>
        <path d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z" fill="#FBBC05"></path>
        <path d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z" fill="#EA4335"></path>
      </svg>
      <span class="text-white font-medium text-sm">Google ile devam et</span>
    </button>

    <div class="flex items-center gap-3 w-full">
      <div class="h-px flex-1 bg-[#262249]"></div>
      <span class="text-xs font-medium text-[#645d94] uppercase tracking-wider">veya e-posta ile</span>
      <div class="h-px flex-1 bg-[#262249]"></div>
    </div>

    <form class="flex flex-col gap-5" @submit.prevent="login">
      <div class="flex flex-col gap-2">
        <label class="text-sm font-medium text-gray-300 ml-1" for="email">E-posta Adresi</label>
        <div class="relative group">
          <div class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none">
            <span class="material-symbols-outlined text-[#9690cb] group-focus-within:text-primary transition-colors text-[20px]">mail</span>
          </div>
          <input
            id="email"
            v-model="email"
            type="email"
            placeholder="ornek@email.com"
            class="w-full bg-[#1c1836] border border-[#2d2850] text-white rounded-xl py-3.5 pl-11 pr-4 focus:ring-2 focus:ring-primary/50 focus:border-primary placeholder-[#645d94] transition-all outline-none text-sm"
            required
          />
        </div>
      </div>

      <div class="flex flex-col gap-2">
        <div class="flex justify-between items-center ml-1">
          <label class="text-sm font-medium text-gray-300" for="password">Şifre</label>
          <button class="text-xs font-medium text-primary hover:text-[#8b7eff] transition-colors" type="button">
            Şifremi Unuttum?
          </button>
        </div>
        <div class="relative group">
          <div class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none">
            <span class="material-symbols-outlined text-[#9690cb] group-focus-within:text-primary transition-colors text-[20px]">lock</span>
          </div>
          <input
            id="password"
            v-model="password"
            :type="showPassword ? 'text' : 'password'"
            placeholder="••••••••"
            class="w-full bg-[#1c1836] border border-[#2d2850] text-white rounded-xl py-3.5 pl-11 pr-12 focus:ring-2 focus:ring-primary/50 focus:border-primary placeholder-[#645d94] transition-all outline-none text-sm"
            required
          />
          <button
            class="absolute inset-y-0 right-0 pr-4 flex items-center cursor-pointer text-[#9690cb] hover:text-white transition-colors"
            type="button"
            @click="togglePassword"
          >
            <span class="material-symbols-outlined text-[20px]">
              {{ showPassword ? "visibility_off" : "visibility" }}
            </span>
          </button>
        </div>
      </div>

      <button
        class="glow-button mt-2 w-full bg-primary hover:bg-[#5b4ddb] text-white font-bold py-3.5 rounded-xl transition-all flex items-center justify-center gap-2 group"
        type="submit"
      >
        <span>Giriş Yap</span>
        <span class="material-symbols-outlined text-[18px] group-hover:translate-x-1 transition-transform">arrow_forward</span>
      </button>
    </form>

    <div class="text-center md:hidden">
      <span class="text-[#9690cb] text-sm">Hesabın yok mu? </span>
      <RouterLink class="text-primary font-semibold text-sm hover:underline" to="/auth/signup">Kayıt Ol</RouterLink>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const email = ref("");
const password = ref("");
const showPassword = ref(false);

const togglePassword = () => {
  showPassword.value = !showPassword.value;
};

async function login() {
  await router.push("/app");
}
</script>
