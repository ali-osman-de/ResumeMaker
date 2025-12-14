<script setup lang="ts">
import { computed, onBeforeUnmount, onMounted, reactive, ref, watch } from 'vue';
import type { CSSProperties } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import IconMaterial from '../../components/ui/atoms/IconMaterial.vue';
import BaseButton from '../../components/ui/atoms/BaseButton.vue';
import { useResumeStore, createEmptyResume } from '../../stores/resume.store';
import { useAppStore } from '../../stores/app.store';

const route = useRoute();
const router = useRouter();
const resumeStore = useResumeStore();
const appStore = useAppStore();

const leftWidth = ref(45); // percent
const dragging = ref(false);

const form = reactive(createEmptyResume());

const previewScrollEl = ref<HTMLElement | null>(null);
const previewContentEl = ref<HTMLElement | null>(null);
const previewBaseSize = reactive({ width: 0, height: 0 });

const zoom = ref(1);
const zoomPercent = computed(() => Math.round(zoom.value * 100));

const previewPanning = ref(false);
let previewPanStartX = 0;
let previewPanStartY = 0;
let previewPanStartScrollLeft = 0;
let previewPanStartScrollTop = 0;
let previewPanMoved = false;
let suppressNextPreviewClick = false;

const displayName = computed(() => {
  const fullName = `${form.personal.firstName} ${form.personal.lastName}`.trim();
  return fullName || form.title || 'Yeni CV';
});

const upperTr = (value: string) => value.toLocaleUpperCase('tr-TR');
const displayNameUpper = computed(() => upperTr(displayName.value));

const summaryParagraphs = computed(() => {
  return (form.summary || '')
    .split(/\n\s*\n/g)
    .map((p) => p.trim())
    .filter(Boolean);
});

const firstPageProjects = computed(() => form.projects.slice(0, 1));
const remainingProjects = computed(() => form.projects.slice(1));
const showSecondPage = computed(
  () => remainingProjects.value.length > 0 || form.volunteer.length > 0 || form.certifications.length > 0
);

const toHref = (value: string | undefined) => {
  if (!value) return '#';
  if (/^https?:\/\//i.test(value)) return value;
  if (/^mailto:/i.test(value)) return value;
  return '#';
};

const clamp = (value: number, min: number, max: number) => Math.min(max, Math.max(min, value));

const applyZoom = (nextZoom: number, anchor?: { clientX: number; clientY: number }) => {
  const scrollEl = previewScrollEl.value;
  const contentEl = previewContentEl.value;
  const prev = zoom.value;
  const next = clamp(nextZoom, 0.25, 3);
  if (!scrollEl || !contentEl || next === prev) {
    zoom.value = next;
    return;
  }

  const scrollRect = scrollEl.getBoundingClientRect();
  const contentRect = contentEl.getBoundingClientRect();

  const rawClientX = anchor?.clientX ?? scrollRect.left + scrollRect.width / 2;
  const rawClientY = anchor?.clientY ?? scrollRect.top + scrollRect.height / 2;

  const clientX = clamp(rawClientX, contentRect.left, contentRect.right);
  const clientY = clamp(rawClientY, contentRect.top, contentRect.bottom);

  const contentX = (clientX - contentRect.left) / prev;
  const contentY = (clientY - contentRect.top) / prev;

  zoom.value = next;

  requestAnimationFrame(() => {
    const nextContentRect = contentEl.getBoundingClientRect();
    const desiredLeft = clientX - contentX * next;
    const desiredTop = clientY - contentY * next;

    const nextScrollLeft = scrollEl.scrollLeft + (nextContentRect.left - desiredLeft);
    const nextScrollTop = scrollEl.scrollTop + (nextContentRect.top - desiredTop);

    scrollEl.scrollLeft = Math.round(nextScrollLeft);
    scrollEl.scrollTop = Math.round(nextScrollTop);
  });
};

const zoomIn = () => applyZoom(zoom.value + 0.1);
const zoomOut = () => applyZoom(zoom.value - 0.1);

const fitToScreen = () => {
  const scrollEl = previewScrollEl.value;
  if (!scrollEl || previewBaseSize.width <= 0) return;

  const style = window.getComputedStyle(scrollEl);
  const paddingX = parseFloat(style.paddingLeft) + parseFloat(style.paddingRight);
  const availableWidth = Math.max(0, scrollEl.clientWidth - paddingX);
  if (availableWidth <= 0) return;

  applyZoom(availableWidth / previewBaseSize.width);
};

const onPreviewWheel = (event: WheelEvent) => {
  if (!event.ctrlKey && !event.metaKey) return; // trackpad pinch / ctrl(or cmd)+wheel
  event.preventDefault();
  const factor = Math.exp(-event.deltaY / 250);
  applyZoom(zoom.value * factor, { clientX: event.clientX, clientY: event.clientY });
};

let previewResizeObserver: ResizeObserver | null = null;
let previewWheelTarget: HTMLElement | null = null;
let previewGestureTarget: HTMLElement | null = null;
let previewGestureBase = 1;

const updatePreviewBaseSize = () => {
  const el = previewContentEl.value;
  if (!el) return;
  previewBaseSize.width = el.offsetWidth;
  previewBaseSize.height = el.offsetHeight;
};

const onGestureStart = () => {
  previewGestureBase = zoom.value;
};

const onGestureChange = (event: Event) => {
  const ge = event as any;
  if (typeof ge?.scale !== 'number') return;
  event.preventDefault();
  const scrollEl = previewScrollEl.value;
  const rect = scrollEl?.getBoundingClientRect();
  const clientX =
    typeof ge.clientX === 'number' ? ge.clientX : rect ? rect.left + rect.width / 2 : 0;
  const clientY =
    typeof ge.clientY === 'number' ? ge.clientY : rect ? rect.top + rect.height / 2 : 0;
  applyZoom(previewGestureBase * ge.scale, { clientX, clientY });
};

const onPreviewClickCapture = (event: MouseEvent) => {
  if (!suppressNextPreviewClick) return;
  suppressNextPreviewClick = false;
  event.preventDefault();
  event.stopPropagation();
};

const onPreviewMouseMove = (event: MouseEvent) => {
  const scrollEl = previewScrollEl.value;
  if (!scrollEl) return;

  const dx = event.clientX - previewPanStartX;
  const dy = event.clientY - previewPanStartY;
  if (!previewPanMoved) {
    if (Math.hypot(dx, dy) < 4) return;
    previewPanMoved = true;
    previewPanning.value = true;
    suppressNextPreviewClick = true;
  }

  scrollEl.scrollLeft = previewPanStartScrollLeft - dx;
  scrollEl.scrollTop = previewPanStartScrollTop - dy;
};

const stopPreviewPan = () => {
  window.removeEventListener('mousemove', onPreviewMouseMove);
  window.removeEventListener('mouseup', stopPreviewPan);
  previewPanning.value = false;
};

const onPreviewMouseDown = (event: MouseEvent) => {
  if (event.button !== 0) return;
  const scrollEl = previewScrollEl.value;
  if (!scrollEl) return;

  event.preventDefault();
  previewPanStartX = event.clientX;
  previewPanStartY = event.clientY;
  previewPanStartScrollLeft = scrollEl.scrollLeft;
  previewPanStartScrollTop = scrollEl.scrollTop;
  previewPanMoved = false;

  window.addEventListener('mousemove', onPreviewMouseMove);
  window.addEventListener('mouseup', stopPreviewPan);
};

const loadData = async () => {
  const detail = await resumeStore.fetchDetail(route.params.id as string | undefined);
  if (detail) {
    Object.assign(form, detail);
  }
};

onMounted(() => {
  loadData();
  updatePreviewBaseSize();

  if (previewContentEl.value) {
    previewResizeObserver = new ResizeObserver(updatePreviewBaseSize);
    previewResizeObserver.observe(previewContentEl.value);
  }

  previewWheelTarget = previewScrollEl.value;
  if (previewWheelTarget) {
    previewWheelTarget.addEventListener('wheel', onPreviewWheel, { passive: false });
  }

  previewGestureTarget = previewScrollEl.value;
  if (previewGestureTarget) {
    previewGestureTarget.addEventListener('gesturestart', onGestureStart as any, { passive: false });
    previewGestureTarget.addEventListener('gesturechange', onGestureChange as any, { passive: false });
  }
});

watch(
  () => route.params.id,
  () => loadData()
);

const onMouseMove = (e: MouseEvent) => {
  if (!dragging.value) return;
  const container = document.getElementById('editor-container');
  if (!container) return;
  const rect = container.getBoundingClientRect();
  const percent = ((e.clientX - rect.left) / rect.width) * 100;
  leftWidth.value = Math.min(70, Math.max(30, percent));
};

const stopDrag = () => {
  dragging.value = false;
  window.removeEventListener('mousemove', onMouseMove);
  window.removeEventListener('mouseup', stopDrag);
};

const startDrag = () => {
  dragging.value = true;
  window.addEventListener('mousemove', onMouseMove);
  window.addEventListener('mouseup', stopDrag);
};

onBeforeUnmount(() => {
  stopDrag();
  stopPreviewPan();
  previewResizeObserver?.disconnect();
  previewResizeObserver = null;

  if (previewWheelTarget) {
    previewWheelTarget.removeEventListener('wheel', onPreviewWheel);
    previewWheelTarget = null;
  }

  if (previewGestureTarget) {
    previewGestureTarget.removeEventListener('gesturestart', onGestureStart as any);
    previewGestureTarget.removeEventListener('gesturechange', onGestureChange as any);
    previewGestureTarget = null;
  }
});

const save = async () => {
  await appStore.runWithLoading(async () => {
    await new Promise((resolve) => setTimeout(resolve, 300));
  });
};

const goDashboard = () => router.push('/dashboard');

const inputBase =
  'custom-input w-full rounded-lg px-3 py-2.5 text-sm text-white focus:outline-none placeholder-gray-500 bg-[#0F172A] border border-[#1A2538]';
const inputBaseWithIcon =
  'custom-input w-full rounded-lg pl-9 pr-3 py-2.5 text-sm text-white focus:outline-none placeholder-gray-500 bg-[#0F172A] border border-[#1A2538]';
const textareaBase =
  'custom-input w-full rounded-lg px-3 py-2.5 text-sm text-white focus:outline-none placeholder-gray-500 bg-[#0F172A] min-h-[120px] resize-none border border-[#1A2538]';

const addTechItem = (kind: 'programming' | 'databases') => {
  form.tech[kind].push('');
};
const removeTechItem = (kind: 'programming' | 'databases', idx: number) => {
  form.tech[kind].splice(idx, 1);
};

const addExperience = () => {
  form.experiences.push({ title: '', company: '', period: '', bullets: [''] });
};
const removeExperience = (idx: number) => {
  form.experiences.splice(idx, 1);
};
const addExperienceBullet = (expIdx: number) => {
  form.experiences[expIdx].bullets.push('');
};
const removeExperienceBullet = (expIdx: number, bulletIdx: number) => {
  form.experiences[expIdx].bullets.splice(bulletIdx, 1);
};

const addEducation = () => {
  form.education.push({ school: '', degree: '', period: '', gpa: '' });
};
const removeEducation = (idx: number) => {
  form.education.splice(idx, 1);
};

const addProject = () => {
  form.projects.push({ name: '', description: '', link: '', bullets: [''] });
};
const removeProject = (idx: number) => {
  form.projects.splice(idx, 1);
};
const addProjectBullet = (projIdx: number) => {
  form.projects[projIdx].bullets.push('');
};
const removeProjectBullet = (projIdx: number, bulletIdx: number) => {
  form.projects[projIdx].bullets.splice(bulletIdx, 1);
};

const addVolunteer = () => {
  form.volunteer.push({ title: '', org: '', period: '', bullets: [''] });
};
const removeVolunteer = (idx: number) => {
  form.volunteer.splice(idx, 1);
};
const addVolunteerBullet = (volIdx: number) => {
  form.volunteer[volIdx].bullets.push('');
};
const removeVolunteerBullet = (volIdx: number, bulletIdx: number) => {
  form.volunteer[volIdx].bullets.splice(bulletIdx, 1);
};

const addCertification = () => {
  form.certifications.push({ title: '', link: '' });
};
const removeCertification = (idx: number) => {
  form.certifications.splice(idx, 1);
};

const previewScaledBoxStyle = computed<CSSProperties>(() => {
  if (!previewBaseSize.width || !previewBaseSize.height) return {};
  return {
    width: `${Math.ceil(previewBaseSize.width * zoom.value)}px`,
    height: `${Math.ceil(previewBaseSize.height * zoom.value)}px`,
  };
});

const previewContentStyle = computed<CSSProperties>(() => ({
  willChange: 'transform',
  transform: `scale(${zoom.value})`,
  transformOrigin: 'top left',
}));
</script>

<template>
  <div class="bg-background-dark text-white font-display overflow-hidden h-screen flex flex-col antialiased selection:bg-primary/30 selection:text-white">
    <header class="glass sticky top-0 z-50 border-b border-surface-border h-16 shrink-0">
      <div class="h-full px-6 flex items-center justify-between w-full">
        <div class="flex items-center gap-6">
          <div class="flex items-center gap-2 text-primary">
            <IconMaterial name="description" class="text-3xl" />
          </div>
          <div class="h-6 w-px bg-surface-border"></div>
          <div class="flex flex-col justify-center">
            <div class="flex items-center gap-2 group cursor-pointer">
              <h1 class="text-sm font-semibold text-white group-hover:text-primary transition-colors">
                {{ displayName }}
              </h1>
              <IconMaterial name="edit" class="text-base text-text-muted group-hover:text-white transition-colors" />
            </div>
            <span class="text-[11px] text-text-muted flex items-center gap-1">
              <span class="w-1.5 h-1.5 rounded-full bg-emerald-500 inline-block"></span>
              Otomatik kaydedildi: {{ form.autoSavedAt }}
            </span>
          </div>
        </div>
        <div class="flex items-center gap-3">
          <BaseButton variant="outline" class="px-4 h-9 flex items-center gap-2">
            <span>AI Değerlendirmesi</span>
          </BaseButton>
          <BaseButton variant="primary" class="px-6 h-9 flex items-center gap-2" @click="save">
            <IconMaterial name="save" class="text-lg" />
            <span>Kaydet</span>
          </BaseButton>
          <button
            class="w-9 h-9 rounded-full bg-surface-dark border border-surface-border flex items-center justify-center ml-2 cursor-pointer hover:border-text-muted transition-colors"
            title="Dashboard"
            @click="goDashboard"
          >
            <img
              alt="User Avatar"
              class="w-full h-full rounded-full opacity-80"
              src="https://lh3.googleusercontent.com/aida-public/AB6AXuAuUi0ee9D-r4sKa9GiTwgtUHHDu92bAddPzDv6a11pTjDvtJTUoHwoEUyKzmr_pmRwOv3eOzPX4o4Z_JaD6yz67x1rUGg53YZmGhGYvF0INpl8RNaEwQGcjIvKuOZEBXyCRoRPdLR2agj8ASxpxIG-UoFUoy9VY6dmUUbDBIXoPPIr6CEEjGv5ZUHQAciGad0LH2ogB7Ge9IJhUeFMG3n3VAQTVivDXyUFp3WHDnkRWyfVWJpDNv0HlvP5LbDh9rAX2bSPfF-3gg"
            />
          </button>
        </div>
      </div>
    </header>

    <main id="editor-container" class="flex-1 flex overflow-hidden relative">
      <section
        class="min-w-[360px] max-w-[800px] flex flex-col bg-surface-dark h-full transition-[width] duration-150"
        :style="{ width: `${leftWidth}%` }"
      >
        <div class="px-6 pt-6 pb-2 shrink-0">
          <nav class="flex gap-2 overflow-x-auto no-scrollbar pb-2">
            <BaseButton variant="ghost" class="whitespace-nowrap px-4 py-2 border border-primary/20 bg-primary/10 text-primary">
              İçerik
            </BaseButton>
            <BaseButton variant="ghost" class="whitespace-nowrap px-4 py-2 text-text-muted">Tasarım</BaseButton>
            <BaseButton variant="ghost" class="whitespace-nowrap px-4 py-2 text-text-muted">Şablonlar</BaseButton>
          </nav>
        </div>

        <div class="flex-1 overflow-y-auto px-6 pb-20 pt-2 space-y-4">
          <div class="bg-[#0F172A] rounded-full h-2 w-full overflow-hidden mb-6 flex items-center relative group">
            <div class="bg-gradient-to-r from-primary to-secondary h-full w-[65%] rounded-full shadow-[0_0_10px_rgba(34,211,238,0.5)]"></div>
            <div class="absolute right-2 text-[10px] text-text-muted opacity-0 group-hover:opacity-100 transition-opacity">
              CV Doluluk Oranı: %65
            </div>
          </div>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300" open>
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-primary/20 flex items-center justify-center text-primary">
                  <IconMaterial name="person" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Kişisel Bilgiler</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70 space-y-4">
              <div class="pt-4 grid grid-cols-2 gap-3">
                <div>
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Ad</label>
                  <input v-model="form.personal.firstName" :class="inputBase" placeholder="Örn: Ali" type="text" />
                </div>
                <div>
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Soyad</label>
                  <input v-model="form.personal.lastName" :class="inputBase" placeholder="Örn: Demirkollu" type="text" />
                </div>
                <div class="col-span-2">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Ünvan (opsiyonel)</label>
                  <input v-model="form.personal.role" :class="inputBase" placeholder="Örn: Frontend Developer" type="text" />
                </div>
              </div>

              <div class="grid grid-cols-2 gap-3 pt-2">
                <div class="col-span-2 sm:col-span-1">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">E-posta</label>
                  <div class="relative">
                    <IconMaterial name="mail" class="absolute left-3 top-2.5 text-text-muted text-[18px]" />
                    <input v-model="form.personal.email" :class="inputBaseWithIcon" type="email" />
                  </div>
                </div>
                <div class="col-span-2 sm:col-span-1">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Telefon</label>
                  <div class="relative">
                    <IconMaterial name="call" class="absolute left-3 top-2.5 text-text-muted text-[18px]" />
                    <input v-model="form.personal.phone" :class="inputBaseWithIcon" type="tel" />
                  </div>
                </div>
                <div class="col-span-2">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Lokasyon</label>
                  <div class="relative">
                    <IconMaterial name="location_on" class="absolute left-3 top-2.5 text-text-muted text-[18px]" />
                    <input v-model="form.personal.location" :class="inputBaseWithIcon" type="text" />
                  </div>
                </div>
                <div class="col-span-2">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Github</label>
                  <input v-model="form.personal.github" :class="inputBase" placeholder="https://github.com/username" type="text" />
                </div>
                <div class="col-span-2">
                  <label class="block text-xs font-medium text-text-muted mb-1.5">Linkedin</label>
                  <input v-model="form.personal.linkedin" :class="inputBase" placeholder="https://linkedin.com/in/username" type="text" />
                </div>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-orange-500/20 flex items-center justify-center text-orange-500">
                  <IconMaterial name="segment" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Özgeçmiş</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-2">
                <div class="flex items-center justify-between">
                  <label class="block text-xs font-medium text-text-muted">Metin</label>
                  <BaseButton variant="outline" class="text-xs px-3 py-1 h-auto opacity-60 cursor-not-allowed" disabled>
                    AI ile Yaz (yakında)
                  </BaseButton>
                </div>
                <textarea
                  v-model="form.summary"
                  :class="textareaBase"
                  placeholder="Kendinizden ve hedeflerinizden kısaca bahsedin..."
                ></textarea>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-indigo-500/20 flex items-center justify-center text-indigo-400">
                  <IconMaterial name="code" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Teknik Bilgiler</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-5">
                <div class="space-y-2">
                  <div class="flex items-center justify-between">
                    <label class="text-xs font-medium text-text-muted">Programming Languages</label>
                    <BaseButton
                      variant="ghost"
                      class="text-xs px-3 py-1 h-auto border border-primary/20 bg-primary/10 text-primary hover:bg-primary/15"
                      @click.prevent="addTechItem('programming')"
                    >
                      <IconMaterial name="add_circle" class="text-[18px]" />
                      Ekle
                    </BaseButton>
                  </div>
                  <div v-for="(item, idx) in form.tech.programming" :key="`pl-${idx}`" class="flex items-center gap-2">
                    <input
                      :value="item"
                      :class="inputBase"
                      placeholder="Örn: Python (Orta Derece)"
                      type="text"
                      @input="form.tech.programming[idx] = ($event.target as HTMLInputElement).value"
                    />
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-white transition-colors"
                      title="Sil"
                      @click.prevent="removeTechItem('programming', idx)"
                    >
                      <IconMaterial name="close" class="text-[18px]" />
                    </button>
                  </div>
                </div>

                <div class="space-y-2">
                  <div class="flex items-center justify-between">
                    <label class="text-xs font-medium text-text-muted">Databases and Cloud Technologies</label>
                    <BaseButton
                      variant="ghost"
                      class="text-xs px-3 py-1 h-auto border border-primary/20 bg-primary/10 text-primary hover:bg-primary/15"
                      @click.prevent="addTechItem('databases')"
                    >
                      <IconMaterial name="add_circle" class="text-[18px]" />
                      Ekle
                    </BaseButton>
                  </div>
                  <div v-for="(item, idx) in form.tech.databases" :key="`db-${idx}`" class="flex items-center gap-2">
                    <input
                      :value="item"
                      :class="inputBase"
                      placeholder="Örn: SQL"
                      type="text"
                      @input="form.tech.databases[idx] = ($event.target as HTMLInputElement).value"
                    />
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-white transition-colors"
                      title="Sil"
                      @click.prevent="removeTechItem('databases', idx)"
                    >
                      <IconMaterial name="close" class="text-[18px]" />
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300" open>
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-blue-500/20 flex items-center justify-center text-blue-500">
                  <IconMaterial name="work" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">İş Deneyimi</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-4">
                <div v-if="!form.experiences.length" class="text-xs text-text-muted">Henüz deneyim eklenmedi.</div>

                <div
                  v-for="(exp, idx) in form.experiences"
                  :key="`exp-${idx}`"
                  class="bg-[#0B1220] border border-[#1A2538] rounded-xl p-4 space-y-4"
                >
                  <div class="flex items-start justify-between gap-3">
                    <div class="grid grid-cols-2 gap-3 flex-1">
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Pozisyon</label>
                        <input v-model="exp.title" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Kurum</label>
                        <input v-model="exp.company" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Tarih</label>
                        <input v-model="exp.period" :class="inputBase" placeholder="Örn: Kas 2022 – Mar 2023" type="text" />
                      </div>
                    </div>
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-red-300 transition-colors"
                      title="Sil"
                      @click.prevent="removeExperience(idx)"
                    >
                      <IconMaterial name="delete" class="text-[18px]" />
                    </button>
                  </div>

                  <div class="space-y-2">
                    <div class="flex items-center justify-between">
                      <label class="text-xs font-medium text-text-muted">Maddeler</label>
                      <BaseButton
                        variant="ghost"
                        class="text-xs px-3 py-1 h-auto border border-primary/20 bg-primary/10 text-primary hover:bg-primary/15"
                        @click.prevent="addExperienceBullet(idx)"
                      >
                        <IconMaterial name="add_circle" class="text-[18px]" />
                        Madde Ekle
                      </BaseButton>
                    </div>
                    <div
                      v-for="(b, bIdx) in exp.bullets"
                      :key="`exp-${idx}-b-${bIdx}`"
                      class="flex items-center gap-2"
                    >
                      <input
                        :value="b"
                        :class="inputBase"
                        placeholder="Madde..."
                        type="text"
                        @input="exp.bullets[bIdx] = ($event.target as HTMLInputElement).value"
                      />
                      <button
                        class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-white transition-colors"
                        title="Sil"
                        @click.prevent="removeExperienceBullet(idx, bIdx)"
                      >
                        <IconMaterial name="close" class="text-[18px]" />
                      </button>
                    </div>
                  </div>
                </div>

                <button
                  class="w-full py-3 rounded-lg border border-dashed border-[#1A2538] hover:border-primary/50 hover:bg-primary/5 text-sm font-medium text-text-muted hover:text-primary transition-all flex items-center justify-center gap-2"
                  @click.prevent="addExperience"
                >
                  <IconMaterial name="add_circle" class="text-lg" />
                  Yeni Deneyim Ekle
                </button>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-emerald-500/20 flex items-center justify-center text-emerald-500">
                  <IconMaterial name="school" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Eğitim Bilgileri</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-4">
                <div v-if="!form.education.length" class="text-xs text-text-muted">Henüz eğitim eklenmedi.</div>

                <div
                  v-for="(edu, idx) in form.education"
                  :key="`edu-${idx}`"
                  class="bg-[#0B1220] border border-[#1A2538] rounded-xl p-4 space-y-3"
                >
                  <div class="flex items-start justify-between gap-3">
                    <div class="grid grid-cols-2 gap-3 flex-1">
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Okul</label>
                        <input v-model="edu.school" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Bölüm / Derece</label>
                        <input v-model="edu.degree" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Tarih</label>
                        <input v-model="edu.period" :class="inputBase" placeholder="Örn: 2020 - Devam Ediyor" type="text" />
                      </div>
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">GPA (opsiyonel)</label>
                        <input v-model="edu.gpa" :class="inputBase" placeholder="Örn: GPA 3.17" type="text" />
                      </div>
                    </div>
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-red-300 transition-colors"
                      title="Sil"
                      @click.prevent="removeEducation(idx)"
                    >
                      <IconMaterial name="delete" class="text-[18px]" />
                    </button>
                  </div>
                </div>

                <button
                  class="w-full py-3 rounded-lg border border-dashed border-[#1A2538] hover:border-primary/50 hover:bg-primary/5 text-sm font-medium text-text-muted hover:text-primary transition-all flex items-center justify-center gap-2"
                  @click.prevent="addEducation"
                >
                  <IconMaterial name="add_circle" class="text-lg" />
                  Yeni Eğitim Ekle
                </button>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-sky-500/20 flex items-center justify-center text-sky-400">
                  <IconMaterial name="folder" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Yazılım Projelerim</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-4">
                <div v-if="!form.projects.length" class="text-xs text-text-muted">Henüz proje eklenmedi.</div>

                <div
                  v-for="(proj, idx) in form.projects"
                  :key="`proj-${idx}`"
                  class="bg-[#0B1220] border border-[#1A2538] rounded-xl p-4 space-y-4"
                >
                  <div class="flex items-start justify-between gap-3">
                    <div class="grid grid-cols-2 gap-3 flex-1">
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Proje Adı</label>
                        <input v-model="proj.name" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Açıklama</label>
                        <input v-model="proj.description" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Link (opsiyonel)</label>
                        <input v-model="proj.link" :class="inputBase" placeholder="https://..." type="text" />
                      </div>
                    </div>
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-red-300 transition-colors"
                      title="Sil"
                      @click.prevent="removeProject(idx)"
                    >
                      <IconMaterial name="delete" class="text-[18px]" />
                    </button>
                  </div>

                  <div class="space-y-2">
                    <div class="flex items-center justify-between">
                      <label class="text-xs font-medium text-text-muted">Maddeler</label>
                      <BaseButton
                        variant="ghost"
                        class="text-xs px-3 py-1 h-auto border border-primary/20 bg-primary/10 text-primary hover:bg-primary/15"
                        @click.prevent="addProjectBullet(idx)"
                      >
                        <IconMaterial name="add_circle" class="text-[18px]" />
                        Madde Ekle
                      </BaseButton>
                    </div>
                    <div
                      v-for="(b, bIdx) in proj.bullets"
                      :key="`proj-${idx}-b-${bIdx}`"
                      class="flex items-center gap-2"
                    >
                      <input
                        :value="b"
                        :class="inputBase"
                        placeholder="Madde..."
                        type="text"
                        @input="proj.bullets[bIdx] = ($event.target as HTMLInputElement).value"
                      />
                      <button
                        class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-white transition-colors"
                        title="Sil"
                        @click.prevent="removeProjectBullet(idx, bIdx)"
                      >
                        <IconMaterial name="close" class="text-[18px]" />
                      </button>
                    </div>
                  </div>
                </div>

                <button
                  class="w-full py-3 rounded-lg border border-dashed border-[#1A2538] hover:border-primary/50 hover:bg-primary/5 text-sm font-medium text-text-muted hover:text-primary transition-all flex items-center justify-center gap-2"
                  @click.prevent="addProject"
                >
                  <IconMaterial name="add_circle" class="text-lg" />
                  Yeni Proje Ekle
                </button>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-pink-500/20 flex items-center justify-center text-pink-300">
                  <IconMaterial name="volunteer_activism" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Gönüllü Çalışmalar</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-4">
                <div v-if="!form.volunteer.length" class="text-xs text-text-muted">Henüz gönüllü çalışma eklenmedi.</div>

                <div
                  v-for="(vol, idx) in form.volunteer"
                  :key="`vol-${idx}`"
                  class="bg-[#0B1220] border border-[#1A2538] rounded-xl p-4 space-y-4"
                >
                  <div class="flex items-start justify-between gap-3">
                    <div class="grid grid-cols-2 gap-3 flex-1">
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Görev</label>
                        <input v-model="vol.title" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2 sm:col-span-1">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Kurum</label>
                        <input v-model="vol.org" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Tarih</label>
                        <input v-model="vol.period" :class="inputBase" placeholder="Örn: 2021-2023" type="text" />
                      </div>
                    </div>
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-red-300 transition-colors"
                      title="Sil"
                      @click.prevent="removeVolunteer(idx)"
                    >
                      <IconMaterial name="delete" class="text-[18px]" />
                    </button>
                  </div>

                  <div class="space-y-2">
                    <div class="flex items-center justify-between">
                      <label class="text-xs font-medium text-text-muted">Maddeler</label>
                      <BaseButton
                        variant="ghost"
                        class="text-xs px-3 py-1 h-auto border border-primary/20 bg-primary/10 text-primary hover:bg-primary/15"
                        @click.prevent="addVolunteerBullet(idx)"
                      >
                        <IconMaterial name="add_circle" class="text-[18px]" />
                        Madde Ekle
                      </BaseButton>
                    </div>
                    <div
                      v-for="(b, bIdx) in vol.bullets"
                      :key="`vol-${idx}-b-${bIdx}`"
                      class="flex items-center gap-2"
                    >
                      <input
                        :value="b"
                        :class="inputBase"
                        placeholder="Madde..."
                        type="text"
                        @input="vol.bullets[bIdx] = ($event.target as HTMLInputElement).value"
                      />
                      <button
                        class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-white transition-colors"
                        title="Sil"
                        @click.prevent="removeVolunteerBullet(idx, bIdx)"
                      >
                        <IconMaterial name="close" class="text-[18px]" />
                      </button>
                    </div>
                  </div>
                </div>

                <button
                  class="w-full py-3 rounded-lg border border-dashed border-[#1A2538] hover:border-primary/50 hover:bg-primary/5 text-sm font-medium text-text-muted hover:text-primary transition-all flex items-center justify-center gap-2"
                  @click.prevent="addVolunteer"
                >
                  <IconMaterial name="add_circle" class="text-lg" />
                  Yeni Gönüllü Çalışma Ekle
                </button>
              </div>
            </div>
          </details>

          <details class="group bg-[#0F172A] border border-[#1A2538] rounded-xl open:ring-1 open:ring-primary/30 transition-all duration-300">
            <summary class="flex items-center justify-between p-4 cursor-pointer list-none select-none">
              <div class="flex items-center gap-3">
                <div class="w-8 h-8 rounded-lg bg-amber-500/20 flex items-center justify-center text-amber-400">
                  <IconMaterial name="workspace_premium" class="text-lg" />
                </div>
                <h3 class="font-medium text-white text-sm">Sertifikalar</h3>
              </div>
              <IconMaterial name="expand_more" class="text-text-muted transition-transform duration-300 group-open:rotate-180" />
            </summary>
            <div class="p-4 pt-0 border-t border-[#1A2538]/70">
              <div class="pt-4 space-y-4">
                <div v-if="!form.certifications.length" class="text-xs text-text-muted">Henüz sertifika eklenmedi.</div>

                <div
                  v-for="(cert, idx) in form.certifications"
                  :key="`cert-${idx}`"
                  class="bg-[#0B1220] border border-[#1A2538] rounded-xl p-4 space-y-3"
                >
                  <div class="flex items-start justify-between gap-3">
                    <div class="grid grid-cols-2 gap-3 flex-1">
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Başlık</label>
                        <input v-model="cert.title" :class="inputBase" type="text" />
                      </div>
                      <div class="col-span-2">
                        <label class="block text-xs font-medium text-text-muted mb-1.5">Link (opsiyonel)</label>
                        <input v-model="cert.link" :class="inputBase" placeholder="https://..." type="text" />
                      </div>
                    </div>
                    <button
                      class="p-2 rounded-lg border border-[#1A2538] hover:bg-white/5 text-text-muted hover:text-red-300 transition-colors"
                      title="Sil"
                      @click.prevent="removeCertification(idx)"
                    >
                      <IconMaterial name="delete" class="text-[18px]" />
                    </button>
                  </div>
                </div>

                <button
                  class="w-full py-3 rounded-lg border border-dashed border-[#1A2538] hover:border-primary/50 hover:bg-primary/5 text-sm font-medium text-text-muted hover:text-primary transition-all flex items-center justify-center gap-2"
                  @click.prevent="addCertification"
                >
                  <IconMaterial name="add_circle" class="text-lg" />
                  Yeni Sertifika Ekle
                </button>
              </div>
            </div>
          </details>
        </div>
      </section>

      <div
        class="w-[4px] bg-surface-dark hover:bg-primary cursor-col-resize z-10 flex items-center justify-center group relative"
        @mousedown.prevent="startDrag"
      >
        <div class="h-8 w-1 bg-surface-border rounded-full group-hover:bg-primary transition-colors"></div>
      </div>

	      <section class="flex-1 bg-[#0B1220] relative overflow-hidden flex flex-col">
		        <div class="shrink-0 pt-4 pb-2 flex justify-center relative z-20">
		          <div class="glass px-2 py-1.5 rounded-full border border-white/10 flex items-center gap-2 shadow-2xl">
		            <button
		              type="button"
		              class="w-8 h-8 flex items-center justify-center rounded-full hover:bg-white/10 text-text-muted hover:text-white transition-colors"
		              title="Uzaklaştır"
		              @click="zoomOut"
		            >
	              <IconMaterial name="remove" class="text-[20px]" />
	            </button>
	            <span class="text-xs font-medium text-white px-1 tabular-nums">{{ zoomPercent }}%</span>
		            <button
		              type="button"
		              class="w-8 h-8 flex items-center justify-center rounded-full hover:bg-white/10 text-text-muted hover:text-white transition-colors"
		              title="Yakınlaştır"
		              @click="zoomIn"
		            >
	              <IconMaterial name="add" class="text-[20px]" />
	            </button>
	            <div class="w-px h-4 bg-white/20 mx-1"></div>
		            <button
		              type="button"
		              class="w-8 h-8 flex items-center justify-center rounded-full hover:bg-white/10 text-text-muted hover:text-white transition-colors"
		              title="Ekrana sığdır"
		              @click="fitToScreen"
		            >
	              <IconMaterial name="fit_screen" class="text-[20px]" />
	            </button>
		            <button
		              type="button"
		              class="w-8 h-8 flex items-center justify-center rounded-full hover:bg-white/10 text-text-muted hover:text-white transition-colors"
		              title="Download PDF"
		            >
		              <IconMaterial name="download" class="text-[20px]" />
		            </button>
		          </div>
		        </div>
	
		        <div
		          ref="previewScrollEl"
		          class="flex-1 overflow-auto p-8 lg:p-12 bg-grid-pattern"
		          :class="previewPanning ? 'cursor-grabbing select-none' : 'cursor-grab'"
		          @mousedown="onPreviewMouseDown"
		          @click.capture="onPreviewClickCapture"
		        >
	          <div class="w-fit mx-auto">
		            <div :style="previewScaledBoxStyle" class="relative">
	              <div ref="previewContentEl" :style="previewContentStyle">
	                <div class="flex flex-col gap-10">
	              <!-- Page 1 -->
	              <div
		                class="bg-white w-[210mm] min-h-[297mm] border border-black/10"
	                style="font-family: 'Times New Roman', Times, serif;"
              >
                <div class="h-full p-[18mm] text-black text-[13px] leading-[1.55]">
                  <header class="text-center">
                    <h1 class="text-[28px] font-bold leading-none tracking-wide">
                      {{ displayNameUpper }}
                    </h1>

                    <div class="mt-3 text-[13px]">
                      <a :href="`mailto:${form.personal.email}`" class="text-[#0000EE] underline">
                        {{ form.personal.email }}
                      </a>
                      <span> | </span>
                      <span>{{ form.personal.phone }}</span>
                      <span> | </span>
                      <span>{{ form.personal.location }}</span>
                      <template v-if="form.personal.github">
                        <span> | </span>
                        <a :href="toHref(form.personal.github)" class="text-[#0000EE] underline">Github</a>
                      </template>
                      <template v-if="form.personal.linkedin">
                        <span> | </span>
                        <a :href="toHref(form.personal.linkedin)" class="text-[#0000EE] underline">Linkedin</a>
                      </template>
                    </div>
                  </header>

                  <section class="mt-6">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">ÖZGEÇMİŞ</h2>
                    <div class="text-[13px] leading-[1.6] text-justify">
                      <p v-for="(p, idx) in summaryParagraphs" :key="`sum-${idx}`" class="mb-4 last:mb-0">
                        {{ p }}
                      </p>
                    </div>
                  </section>

                  <section class="mt-6">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">TEKNİK BİLGİLER</h2>
                    <ul class="list-disc pl-7 space-y-1">
                      <li>
                        <span class="font-bold">Programming Languages:</span>
                        <span> {{ form.tech.programming.join(', ') }}</span>
                      </li>
                      <li>
                        <span class="font-bold">Databases and Cloud Technologies:</span>
                        <span> {{ form.tech.databases.join(', ') }}</span>
                      </li>
                    </ul>
                  </section>

                  <section class="mt-6" v-if="form.experiences.length">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">İŞ DENEYİMİ</h2>
                    <div v-for="(exp, idx) in form.experiences" :key="`pv-exp-${idx}`" class="mb-5 last:mb-0">
                      <div class="flex items-baseline justify-between gap-4">
                        <div class="font-bold">
                          {{ exp.title }} | {{ exp.company }}
                        </div>
                        <div class="font-bold whitespace-nowrap">{{ exp.period }}</div>
                      </div>
                      <ul class="list-disc pl-7 mt-1 space-y-1">
                        <li v-for="(b, i) in exp.bullets" :key="`pv-exp-${idx}-b-${i}`">{{ b }}</li>
                      </ul>
                    </div>
                  </section>

                  <section class="mt-6" v-if="form.education.length">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">EĞİTİM BİLGİLERİ</h2>
                    <div v-for="(edu, idx) in form.education" :key="`pv-edu-${idx}`" class="mb-4 last:mb-0">
                      <div class="flex items-baseline justify-between gap-4">
                        <div class="font-bold">
                          {{ edu.school }}
                        </div>
                        <div class="font-bold whitespace-nowrap">{{ edu.period }}</div>
                      </div>
                      <div class="flex items-baseline justify-between gap-4 mt-0.5">
                        <div>{{ edu.degree }}</div>
                        <div v-if="edu.gpa" class="font-bold whitespace-nowrap">{{ edu.gpa }}</div>
                      </div>
                    </div>
                  </section>

                  <section class="mt-6" v-if="firstPageProjects.length">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">YAZILIM PROJELERİM</h2>
                    <div v-for="(proj, idx) in firstPageProjects" :key="`pv-proj1-${idx}`" class="mb-5 last:mb-0">
                      <div class="font-bold text-[14px]">
                        <a :href="toHref(proj.link)" class="text-[#0000EE] underline">{{ proj.name }}</a>
                        <span v-if="proj.description"> | {{ proj.description }}</span>
                      </div>
                      <ul class="list-disc pl-7 mt-1 space-y-1">
                        <li v-for="(b, i) in proj.bullets" :key="`pv-proj1-${idx}-b-${i}`">{{ b }}</li>
                      </ul>
                    </div>
                  </section>
                </div>
              </div>

              <!-- Page 2 -->
              <div
                v-if="showSecondPage"
	                class="bg-white w-[210mm] min-h-[297mm] border border-black/10"
                style="font-family: 'Times New Roman', Times, serif;"
              >
                <div class="h-full p-[18mm] text-black text-[13px] leading-[1.55]">
                  <section v-if="remainingProjects.length" class="space-y-5">
                    <div v-for="(proj, idx) in remainingProjects" :key="`pv-proj2-${idx}`" class="space-y-1">
                      <div class="font-bold text-[14px]">
                        <a :href="toHref(proj.link)" class="text-[#0000EE] underline">{{ proj.name }}</a>
                        <span v-if="proj.description"> | {{ proj.description }}</span>
                      </div>
                      <ul class="list-disc pl-7 space-y-1">
                        <li v-for="(b, i) in proj.bullets" :key="`pv-proj2-${idx}-b-${i}`">{{ b }}</li>
                      </ul>
                    </div>
                  </section>

                  <section class="mt-6" v-if="form.volunteer.length">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">GÖNÜLLÜ ÇALIŞMALAR</h2>
                    <div v-for="(vol, idx) in form.volunteer" :key="`pv-vol-${idx}`" class="mb-5 last:mb-0">
                      <div class="font-bold">
                        {{ upperTr(vol.title) }} | {{ upperTr(vol.org) }}
                      </div>
                      <div class="font-bold mt-1">{{ vol.period }}</div>
                      <ul class="list-disc pl-7 mt-1 space-y-1">
                        <li v-for="(b, i) in vol.bullets" :key="`pv-vol-${idx}-b-${i}`">{{ b }}</li>
                      </ul>
                    </div>
                  </section>

                  <section class="mt-6" v-if="form.certifications.length">
                    <h2 class="text-[#5B9BD5] font-bold text-[16px] mb-2">SERTİFİKALAR</h2>
                    <div class="space-y-2">
                      <a
                        v-for="(cert, idx) in form.certifications"
                        :key="`pv-cert-${idx}`"
                        :href="toHref(cert.link)"
                        class="text-[#0000EE] underline block"
                      >
                        {{ cert.title }}
                      </a>
                    </div>
                  </section>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

        <div
          class="absolute inset-0 pointer-events-none z-0 opacity-[0.03]"
          style="background-image: radial-gradient(#6e5ef7 1px, transparent 1px); background-size: 24px 24px;"
        ></div>
      </section>
    </main>
  </div>
</template>
