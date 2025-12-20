<template>
  <section
    class="w-full max-w-[480px] rounded-2xl p-8 md:p-10 flex flex-col gap-8 bg-[#120f23]/60 border border-white/10 backdrop-blur-[12px] shadow-[0_25px_50px_-12px_rgba(0,0,0,0.5)]"
  >
    <div class="flex flex-col gap-2 text-center">
      <h2 class="text-3xl font-bold text-white tracking-tight">Kayıt Ol</h2>
      <p class="text-[#9690cb] text-sm">CV Creator deneyimine başla ve özgeçmişini dakikalar içinde oluştur.</p>
    </div>

    <form class="flex flex-col gap-5" @submit.prevent="signup">
      <div class="flex flex-col gap-2">
        <label class="text-sm font-medium text-gray-300 ml-1" for="username">Kullanıcı Adı</label>
        <div class="relative group">
          <div class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none">
            <span class="material-symbols-outlined text-[#9690cb] group-focus-within:text-primary transition-colors text-[20px]">
              person
            </span>
          </div>
          <input
            id="username"
            v-model="username"
            type="text"
            placeholder="kullaniciadi"
            class="w-full bg-[#1c1836] border border-[#2d2850] text-white rounded-xl py-3.5 pl-11 pr-4 focus:ring-2 focus:ring-primary/50 focus:border-primary placeholder-[#645d94] transition-all outline-none text-sm"
            required
          />
        </div>
      </div>

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
        <label class="text-sm font-medium text-gray-300 ml-1" for="password">Şifre</label>
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

      <div class="flex flex-col gap-2">
        <label class="text-sm font-medium text-gray-300 ml-1" for="passwordConfirm">Şifre (Tekrar)</label>
        <div class="relative group">
          <div class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none">
            <span class="material-symbols-outlined text-[#9690cb] group-focus-within:text-primary transition-colors text-[20px]">
              verified
            </span>
          </div>
          <input
            id="passwordConfirm"
            v-model="passwordConfirm"
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
        class="mt-2 w-full bg-primary hover:bg-[#5b4ddb] text-white font-bold py-3.5 rounded-xl transition-all duration-300 flex items-center justify-center gap-2 group hover:shadow-[0_0_20px_rgba(110,94,247,0.5)]"
        type="submit"
      >
        <span>Kayıt Ol</span>
        <span class="material-symbols-outlined text-[18px] group-hover:translate-x-1 transition-transform">arrow_forward</span>
      </button>
    </form>

    <div class="text-center md:hidden">
      <span class="text-[#9690cb] text-sm">Zaten hesabın var mı? </span>
      <RouterLink class="text-primary font-semibold text-sm hover:underline" to="/auth/login">Giriş Yap</RouterLink>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";
import { authStore } from "@/stores/authStore";

const router = useRouter();
const auth = authStore();
const username = ref("");
const email = ref("");
const password = ref("");
const passwordConfirm = ref("");
const showPassword = ref(false);

const togglePassword = () => {
  showPassword.value = !showPassword.value;
};

async function signup() {
  await auth.createUser({
    userName: username.value,
    email: email.value,
    password: password.value
  });

  if (!auth.error) {
    await router.push("/app");
  }
}
</script>
