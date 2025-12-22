<template>
  <div class="space-y-8">
    <section class="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
      <div class="space-y-2">
        <p class="text-sm text-white/70">Hoş geldin!</p>
        <h1 class="text-3xl font-bold tracking-tight">Ali Osman</h1>
        <p class="text-sm text-white/60">
          CV’lerini düzenle, profil gücünü artır ve dakikalar içinde hazır şablonları kullan.
        </p>
      </div>
    </section>

    <section class="grid gap-6 lg:grid-cols-2">
      <div class="space-y-4 lg:col-span-2">
        <div class="flex items-center justify-between">
          <div>
            <p class="text-xs uppercase tracking-[0.08em] text-white/60">Özgeçmişlerin</p>
            <h2 class="text-xl font-semibold">Hazır taslaklar ve mevcut CV’ler</h2>
          </div>
          <button
            class="hidden items-center gap-2 rounded-lg border border-[#1E293B] bg-[#0F1B2D] px-3 py-2 text-xs font-semibold text-white/80 transition hover:border-[#6e5ef7] hover:text-white sm:flex"
            type="button"
          >
            <span class="material-symbols-outlined text-[18px]">filter_alt</span>
            Filtrele
          </button>
        </div>

        <div class="grid gap-4 md:grid-cols-2">
          <article
            v-for="resume in resumes"
            :key="resume.id"
            class="group relative overflow-hidden rounded-2xl border border-[#1E293B] bg-gradient-to-b from-[#15172b] to-[#0f1b2d] p-5 shadow-[0_20px_50px_-28px_rgba(0,0,0,0.9)] transition hover:-translate-y-0.5 hover:border-[#6e5ef7]/40 hover:shadow-[0_24px_60px_-30px_rgba(110,94,247,0.35)]"
          >
            <div
              class="absolute inset-0 opacity-0 transition-opacity duration-200 group-hover:opacity-100"
              aria-hidden="true"
            >
              <div class="absolute inset-0 bg-black/35 backdrop-blur-[2px]"></div>
              <div class="absolute inset-0 flex items-center justify-center gap-3">
                <button
                  class="flex items-center gap-2 rounded-lg bg-[#6e5ef7] px-3 py-2 text-sm font-semibold shadow-[0_0_20px_rgba(110,94,247,0.35)] transition hover:bg-[#5b4ce3]"
                  type="button"
                >
                  <span class="material-symbols-outlined text-[18px]">edit</span>
                  Düzenle
                </button>
                <button
                  class="flex items-center gap-2 rounded-lg bg-white px-3 py-2 text-sm font-semibold text-[#0B1220] transition hover:bg-gray-100"
                  type="button"
                >
                  <span class="material-symbols-outlined text-[18px]">visibility</span>
                  Önizle
                </button>
              </div>
            </div>

            <header class="flex items-start justify-between">
              <div class="flex items-center gap-3">
                <div
                  class="flex h-11 w-11 items-center justify-center rounded-xl bg-gradient-to-br from-[#6e5ef7] to-[#22D3EE] text-white shadow-[0_0_20px_rgba(110,94,247,0.35)]"
                >
                  <span class="material-symbols-outlined text-[22px]">{{ resume.icon }}</span>
                </div>
                <div>
                  <p class="text-xs uppercase tracking-[0.08em] text-white/60">{{ resume.subtitle }}</p>
                  <p class="text-base font-semibold">{{ resume.title }}</p>
                </div>
              </div>
              <span class="rounded-full border border-[#1E293B] bg-[#0F1B2D] px-3 py-1 text-[11px] font-semibold text-white/70">
                {{ resume.updated }}
              </span>
            </header>

            <div class="mt-4 space-y-3">
              <div class="h-2 w-full overflow-hidden rounded-full bg-[#1E293B]">
                <div
                  class="h-full rounded-full bg-gradient-to-r from-[#6e5ef7] to-[#22D3EE]"
                  :style="`width: ${resume.progress}%`"
                ></div>
              </div>
              <div class="flex items-center justify-between text-xs text-white/60">
                <span>{{ resume.progress }}% Profil Gücü</span>
                <span>{{ resume.strengthLabel }}</span>
              </div>
            </div>
          </article>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">

const resumes = [
  {
    id: 1,
    title: "UX Designer Portfolio",
    subtitle: "Behance / Portfolio",
    updated: "Dün güncellendi",
    progress: 65,
    strengthLabel: "Taslak hazır",
    icon: "color_lens",
  },
  {
    id: 2,
    title: "Full Stack Developer",
    subtitle: "LinkedIn / Github",
    updated: "2 gün önce",
    progress: 82,
    strengthLabel: "Mülakat modunda",
    icon: "terminal",
  },
];
</script>
