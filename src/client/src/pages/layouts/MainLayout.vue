<template>
  <div class="relative min-h-screen overflow-hidden bg-[#0B1220] text-white">
    
    <div class="pointer-events-none absolute -left-36 -top-40 h-[420px] w-[420px] rounded-full bg-primary/20 blur-[120px]"></div>
    <div class="pointer-events-none absolute -right-24 top-1/4 h-[360px] w-[360px] rounded-full bg-cyan-400/15 blur-[110px]"></div>


    <header class="fixed top-0 z-50 w-full border-b border-[#1E293B] bg-[#0F1B2D]/80 backdrop-blur-xl">
      <div class="mx-auto flex h-20 max-w-7xl items-center justify-between px-6">
        <div class="flex items-center gap-3">
          <div
            class="flex h-10 w-10 items-center justify-center rounded-xl bg-gradient-to-br from-[#6e5ef7] to-[#22D3EE] shadow-[0_0_20px_rgba(110,94,247,0.4)]"
          >
            <span class="material-symbols-outlined text-[22px] text-white">description</span>
          </div>
          <div class="leading-tight">
            <p class="text-lg font-bold tracking-tight">
              ResumeMaker
            </p>
            <p class="text-xs text-white/60">Dashboard</p>
          </div>
        </div>

        <div class="flex items-center gap-4">
          <div
            class="hidden items-center gap-2 rounded-xl border border-[#1E293B] bg-[#0F1B2D] px-3.5 py-2.5 text-sm text-white/80 transition hover:border-[#6e5ef7] hover:text-white md:flex"
          >
            <span class="material-symbols-outlined text-[20px] text-white/60">help</span>
            <span>Destek</span>
          </div>
          
          <RouterLink
            class="hidden items-center gap-2 rounded-xl border border-transparent bg-[#6e5ef7] px-3.5 py-2.5 text-sm font-semibold shadow-[0_0_20px_rgba(110,94,247,0.35)] transition hover:bg-[#5b4ce3] sm:flex"
            to="/app/editor"
          >
            <span class="material-symbols-outlined text-[18px]">add</span>
            Yeni CV
          </RouterLink>

          <div class="flex items-center gap-3 rounded-xl border border-[#1E293B] bg-[#0F1B2D] px-3 py-2">
            <div class="hidden text-right sm:block">
              <p class="text-sm font-semibold">Ali Osman</p>
              <p class="text-xs text-white/60">Deneme</p>
            </div>
            <div class="relative avatar-menu">
              <button
                class="relative h-10 w-10 rounded-full border-2 border-[#1E293B] bg-cover bg-center ring-primary/40 transition hover:ring-2"
                style="background-image: url('https://images.unsplash.com/photo-1508214751196-bcfd4ca60f91?auto=format&fit=crop&w=200&q=80');"
                type="button"
                @click="avatarMenuOpen = !avatarMenuOpen"
              >
                <span class="absolute bottom-0 right-0 h-3 w-3 rounded-full border-2 border-[#0B1220] bg-emerald-400"></span>
              </button>

              <div
                v-if="avatarMenuOpen"
                class="absolute right-0 mt-2 w-40 rounded-lg border border-[#1E293B] bg-[#0F1B2D] shadow-xl"
              >
                <button
                  class="flex w-full items-center gap-2 px-3 py-2 text-sm text-white transition hover:bg-white/5"
                  type="button"
                  @click="handleLogout"
                >
                  <span class="material-symbols-outlined text-[18px] text-red-400">logout</span>
                  Çıkış Yap
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>

    <main class="pt-24 pb-12">
      <div class="mx-auto max-w-7xl px-6">
        <RouterView />
      </div>
    </main>
  </div>
</template>

<script setup lang="ts">
import { onBeforeUnmount, onMounted, ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const avatarMenuOpen = ref(false);

const closeMenu = (event: MouseEvent) => {
  const target = event.target as HTMLElement | null;
  if (target && target.closest?.(".avatar-menu")) return;
  avatarMenuOpen.value = false;
};

const handleLogout = () => {
  localStorage.removeItem("access_token");
  localStorage.removeItem("refresh_token");
  avatarMenuOpen.value = false;
  router.push({ name: "login" });
};

onMounted(() => {
  window.addEventListener("click", closeMenu);
});

onBeforeUnmount(() => {
  window.removeEventListener("click", closeMenu);
});
</script>
