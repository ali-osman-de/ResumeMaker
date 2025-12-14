<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import IconMaterial from '../../components/ui/atoms/IconMaterial.vue';
import BaseButton from '../../components/ui/atoms/BaseButton.vue';
import { useResumeStore } from '../../stores/resume.store';

const resumeStore = useResumeStore();
const router = useRouter();
const showProfileMenu = ref(false);

onMounted(() => {
  resumeStore.fetchMock();
});

const toggleMenu = () => {
  showProfileMenu.value = !showProfileMenu.value;
};

const closeMenu = () => {
  showProfileMenu.value = false;
};

const logout = () => {
  closeMenu();
  router.push('/login');
};

const goToEditor = (id?: string) => {
  router.push(id ? `/editor/${id}` : '/editor');
};
</script>

<template>
  <div class="bg-background-dark text-white min-h-screen flex flex-col" @click="closeMenu">
    <header class="fixed top-0 w-full z-50 glass-header border-b border-border-dark h-20">
      <div class="max-w-[1440px] mx-auto px-6 h-full flex items-center justify-between">
        <div class="flex items-center gap-3">
          <div class="size-10 bg-gradient-to-br from-primary to-secondary rounded-xl flex items-center justify-center shadow-glow">
            <IconMaterial name="description" size="24" class="text-white" />
          </div>
          <h1 class="text-xl font-bold tracking-tight text-white hidden sm:block">
            Resume<span class="text-primary">Maker</span>
          </h1>
        </div>
        <div class="flex items-center gap-6">
          <button
            class="hidden md:flex bg-surface-dark hover:bg-surface-hover border border-border-dark text-sm font-medium py-2.5 px-4 rounded-xl transition-all items-center gap-2 group"
          >
            <IconMaterial name="help" class="text-gray-400 group-hover:text-white text-[20px]" />
            <span class="text-gray-300 group-hover:text-white">Support</span>
          </button>
          <div class="h-8 w-[1px] bg-border-dark hidden md:block"></div>
          <div class="flex items-center gap-3 cursor-pointer group relative" @click.stop="toggleMenu">
            <div class="text-right hidden md:block">
              <p class="text-sm font-semibold text-white group-hover:text-secondary transition-colors">
                {{ resumeStore.metadata?.userName ?? 'Kullanıcı' }}
              </p>
              <p class="text-xs text-gray-500">{{ resumeStore.metadata?.plan ?? 'Plan' }}</p>
            </div>
            <div
              class="size-10 rounded-full bg-cover bg-center border-2 border-border-dark group-hover:border-primary transition-colors relative"
              style="background-image: url('https://lh3.googleusercontent.com/aida-public/AB6AXuDeZDu1Oe9p5YhQIVNKp10mR4RkJzaTQnQfE2i6QuQTzqPCTmFVMXT9JnQknexfEJBUzsQWf7if4Qj7V_nAKuo3Dtc7QRDR71FkFXAjRukioq9SdfV7lcIgzF27UqGAY6NEBcCex7hTEinx_5Pm6Pvnd50M2X0u16D1n33AtU-tTdH-ZXWKXpX394EToqsN3Gut0NnEORcYCe1D2VDRGwihyK7BPhkZlOip5uqjWZlZQTQj4QZ18R-87FUdeGwzPVtjqnH85zXOxQ');"
            >
              <div class="absolute bottom-0 right-0 size-3 bg-green-500 border-2 border-background-dark rounded-full"></div>
            </div>
            <button class="md:hidden text-gray-400 hover:text-white">
              <IconMaterial name="menu" />
            </button>

            <div
              v-if="showProfileMenu"
              class="absolute right-0 top-14 min-w-[180px] bg-surface-dark border border-border-dark rounded-xl shadow-card overflow-hidden"
            >
              <button
                class="w-full text-left px-4 py-3 text-sm text-white hover:bg-surface-hover flex items-center gap-2"
                @click="logout"
              >
                <IconMaterial name="logout" />
                Çıkış Yap
              </button>
            </div>
          </div>
        </div>
      </div>
    </header>

    <main class="flex-grow pt-28 px-6 pb-12 w-full max-w-[1440px] mx-auto">
      <section class="flex flex-col md:flex-row justify-between items-start md:items-end gap-6 mb-10">
        <div class="flex flex-col gap-2">
          <h2 class="text-3xl md:text-4xl font-bold leading-tight">
            Welcome back,
            <span class="text-transparent bg-clip-text bg-gradient-to-r from-primary to-secondary">
              {{ resumeStore.metadata?.userName ?? 'Creator' }}!
            </span>
            👋
          </h2>
          <p class="text-gray-400 text-base max-w-lg">
            You have
            <span class="text-white font-semibold">{{ resumeStore.metadata?.activeCount ?? 0 }} active resumes</span>
            ready for your next big opportunity.
          </p>
        </div>
        <BaseButton class="min-w-[160px]" variant="primary">
          <IconMaterial name="add_circle" />
          <span>Create New CV</span>
        </BaseButton>
      </section>

      <section class="flex flex-col sm:flex-row justify-between items-center gap-4 mb-8">
        <div class="flex items-center gap-2 overflow-x-auto w-full sm:w-auto pb-2 sm:pb-0 scrollbar-hide">
          <BaseButton variant="ghost" class="px-4 py-2 border border-primary/30 shadow-[0_0_15px_-3px_rgba(110,94,247,0.15)]">
            All Resumes
          </BaseButton>
          <BaseButton variant="ghost" class="px-4 py-2">Drafts</BaseButton>
          <BaseButton variant="ghost" class="px-4 py-2">Published</BaseButton>
          <BaseButton variant="ghost" class="px-4 py-2">Archived</BaseButton>
        </div>
        <div class="flex items-center gap-3 w-full sm:w-auto">
          <div class="relative group flex-1 sm:flex-none">
            <IconMaterial
              name="search"
              class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-500 group-focus-within:text-primary text-[20px]"
            />
            <input
              type="text"
              placeholder="Search..."
              class="bg-surface-dark border border-border-dark text-gray-300 text-sm rounded-xl focus:ring-primary focus:border-primary block w-full pl-10 p-2.5 placeholder-gray-600 transition-all"
            />
          </div>
          <button class="p-2.5 bg-surface-dark border border-border-dark rounded-xl text-gray-400 hover:text-white hover:border-gray-600 transition-colors">
            <IconMaterial name="sort" class="text-[20px]" />
          </button>
        </div>
      </section>

      <div class="rounded-2xl overflow-hidden border border-border-dark bg-surface-dark/40">
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 p-4">
          <div
            class="group relative flex flex-col items-center justify-center min-h-[320px] rounded-2xl border-2 border-dashed border-border-dark hover:border-primary bg-surface-dark/30 hover:bg-surface-dark cursor-pointer transition-all duration-300"
            @click="goToEditor()"
          >
            <div class="size-16 rounded-full bg-surface-hover group-hover:bg-primary/20 flex items-center justify-center mb-4 transition-colors">
              <IconMaterial name="add" class="text-gray-400 group-hover:text-primary text-[32px] transition-colors" />
            </div>
            <h3 class="text-lg font-bold text-white mb-1">New Resume</h3>
            <p class="text-sm text-gray-500 group-hover:text-gray-400 transition-colors">Start from scratch or template</p>
          </div>

          <div
            v-for="card in resumeStore.items"
            :key="card.id"
            class="group bg-surface-dark rounded-2xl overflow-hidden shadow-card border border-border-dark hover:border-primary/50 transition-all duration-300 hover:-translate-y-1 relative"
          >
            <div class="relative h-48 w-full bg-cover bg-top" :style="`background-image: url('${card.image}')`">
              <div class="absolute inset-0 bg-gradient-to-t from-surface-dark to-transparent opacity-60"></div>
              <div class="absolute top-3 left-3">
                <span
                  class="px-2.5 py-1 rounded-lg border text-xs font-bold backdrop-blur-sm"
                  :class="card.status === 'Completed'
                    ? 'bg-emerald-500/10 border-emerald-500/20 text-emerald-400'
                    : card.status === 'Draft'
                      ? 'bg-amber-500/10 border-amber-500/20 text-amber-400'
                      : 'bg-secondary/10 border-secondary/20 text-secondary flex items-center gap-1'"
                >
                  <span v-if="card.status === 'New'" class="size-1.5 rounded-full bg-secondary animate-pulse"></span>
                  {{ card.status }}
                </span>
              </div>
              <div
                class="absolute inset-0 bg-background-dark/60 opacity-0 group-hover:opacity-100 flex items-center justify-center gap-3 transition-opacity backdrop-blur-sm"
              >
                <button class="bg-primary hover:bg-[#5b4ce3] text-white p-2 rounded-lg shadow-lg" title="Edit" @click.stop="goToEditor(card.id)">
                  <IconMaterial name="edit" class="text-[20px]" />
                </button>
                <button class="bg-white hover:bg-gray-200 text-background-dark p-2 rounded-lg shadow-lg" title="Preview">
                  <IconMaterial name="visibility" class="text-[20px]" />
                </button>
              </div>
            </div>
            <div class="p-5">
              <div class="flex justify-between items-start mb-2">
                <div>
                  <h3 class="text-white font-semibold text-lg leading-tight group-hover:text-primary transition-colors">
                    {{ card.title }}
                  </h3>
                  <p class="text-gray-500 text-xs mt-1">Updated {{ card.updated }}</p>
                </div>
                <button class="text-gray-400 hover:text-white p-1 rounded-md hover:bg-surface-hover">
                  <IconMaterial name="more_vert" class="text-[20px]" />
                </button>
              </div>
              <div class="w-full bg-surface-hover h-1.5 rounded-full mt-3 overflow-hidden">
                <div class="bg-gradient-to-r from-primary to-secondary h-full" :style="`width:${card.strength}%`"></div>
              </div>
              <p class="text-[10px] text-right text-gray-500 mt-1">{{ card.strength }}% Profile Strength</p>
            </div>
          </div>
        </div>
      </div>
    </main>

    <div class="fixed top-0 left-0 w-full h-screen overflow-hidden -z-10 pointer-events-none">
      <div class="absolute -top-[20%] -left-[10%] w-[50%] h-[50%] bg-primary/10 rounded-full blur-[120px]"></div>
      <div class="absolute top-[40%] -right-[10%] w-[40%] h-[40%] bg-secondary/5 rounded-full blur-[100px]"></div>
    </div>
  </div>
</template>
