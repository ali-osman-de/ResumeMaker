<template>
  <div class="relative flex w-full flex-col gap-6 lg:flex-row lg:gap-8">
    <div class="pointer-events-none absolute -left-24 -top-16 h-72 w-72 rounded-full bg-primary/15 blur-[120px]"></div>
    <div class="pointer-events-none absolute -right-16 top-24 h-64 w-64 rounded-full bg-cyan-400/10 blur-[110px]"></div>
    <section
      class="w-full rounded-2xl border border-[#1E293B] bg-[#0F1B2D]/80 shadow-[0_24px_80px_-50px_rgba(0,0,0,0.8)] backdrop-blur lg:basis-[55%]"
    >
      <div class="px-6 pt-6 pb-2 shrink-0">
        <nav class="flex gap-2 overflow-x-auto no-scrollbar pb-2">
          <button
            class="whitespace-nowrap px-4 py-2 rounded-lg bg-primary/10 text-primary border border-primary/20 text-sm font-medium transition-colors"
          >
            İçerik
          </button>
          <button
            class="whitespace-nowrap px-4 py-2 rounded-lg hover:bg-white/5 text-text-muted hover:text-white text-sm font-medium transition-colors"
          >
            Tasarım
          </button>
          <button
            class="whitespace-nowrap px-4 py-2 rounded-lg hover:bg-white/5 text-text-muted hover:text-white text-sm font-medium transition-colors"
          >
            Şablonlar
          </button>
        </nav>
      </div>

      <div class="custom-scroll flex-1 overflow-y-auto px-6 pb-20 pt-2 space-y-4">
        <!-- Progress Bar -->
        <div class="group relative mb-6 flex h-2 w-full items-center overflow-hidden rounded-full bg-[#162032]">
          <div class="h-full w-[65%] rounded-full bg-gradient-to-r from-primary to-[#22D3EE] shadow-[0_0_10px_rgba(34,211,238,0.5)]"></div>
          <div
            class="absolute right-2 text-[10px] text-text-muted opacity-0 transition-opacity group-hover:opacity-100"
          >
            CV Doluluk Oranı: %65
          </div>
        </div>

        <!-- Personal Info Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">person</span>
              </div>
              <h3 class="text-sm font-medium text-white">Kişisel Bilgiler</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="space-y-4 border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="grid grid-cols-2 gap-3 pt-4">
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">Ad</label>
                <input
                  v-model="contact.firstName"
                  class="custom-input w-full rounded-lg px-3 py-2.5 text-sm text-white placeholder-gray-600 focus:outline-none"
                  placeholder="Örn: Ahmet"
                  type="text"
                />
              </div>
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">Soyad</label>
                <input
                  v-model="contact.lastName"
                  class="custom-input w-full rounded-lg px-3 py-2.5 text-sm text-white placeholder-gray-600 focus:outline-none"
                  placeholder="Örn: Yılmaz"
                  type="text"
                />
              </div>
            </div>
            <div class="grid grid-cols-2 gap-3 pt-2">
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">E-posta</label>
                <div class="relative">
                  <span class="material-symbols-outlined absolute left-3 top-2.5 text-[18px] text-text-muted">mail</span>
                  <input
                    v-model="contact.email"
                    class="custom-input w-full rounded-lg px-3 py-2.5 pl-9 text-sm text-white placeholder-gray-600 focus:outline-none"
                    type="email"
                  />
                </div>
              </div>
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">Telefon</label>
                <div class="relative">
                  <span class="material-symbols-outlined absolute left-3 top-2.5 text-[18px] text-text-muted">call</span>
                  <input
                    v-model="contact.phone"
                    class="custom-input w-full rounded-lg px-3 py-2.5 pl-9 text-sm text-white placeholder-gray-600 focus:outline-none"
                    type="tel"
                  />
                </div>
              </div>
              <div class="col-span-2">
                <label class="mb-1.5 block text-xs font-medium text-text-muted">Lokasyon</label>
                <div class="relative">
                  <span class="material-symbols-outlined absolute left-3 top-2.5 text-[18px] text-text-muted">location_on</span>
                  <input
                    v-model="contact.location"
                    class="custom-input w-full rounded-lg px-3 py-2.5 pl-9 text-sm text-white placeholder-gray-600 focus:outline-none"
                    type="text"
                  />
                </div>
              </div>
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">GitHub</label>
                <div class="relative">
                  <span class="material-symbols-outlined absolute left-3 top-2.5 text-[18px] text-text-muted">code</span>
                  <input
                    v-model="contact.githubUrl"
                    class="custom-input w-full rounded-lg px-3 py-2.5 pl-9 text-sm text-white placeholder-gray-600 focus:outline-none"
                    type="url"
                    placeholder="https://github.com/kullanici"
                  />
                </div>
              </div>
              <div>
                <label class="mb-1.5 block text-xs font-medium text-text-muted">LinkedIn</label>
                <div class="relative">
                  <span class="material-symbols-outlined absolute left-3 top-2.5 text-[18px] text-text-muted">business_center</span>
                  <input
                    v-model="contact.linkedinUrl"
                    class="custom-input w-full rounded-lg px-3 py-2.5 pl-9 text-sm text-white placeholder-gray-600 focus:outline-none"
                    type="url"
                    placeholder="https://linkedin.com/in/kullanici"
                  />
                </div>
              </div>
            </div>
          </div>
        </details>

        <!-- Summary Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">segment</span>
              </div>
              <h3 class="text-sm font-medium text-white">Özgeçmiş</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="relative pt-4">
              <button
                class="absolute right-2 top-2 flex items-center gap-1 rounded bg-primary/10 px-2 py-1 text-xs font-semibold text-primary transition hover:bg-primary/20 disabled:opacity-60 disabled:cursor-not-allowed"
                type="button"
                disabled
              >
                <span class="material-symbols-outlined text-[14px]">auto_awesome</span>
                <span>AI ile Yaz</span>
              </button>
              <label class="mb-1.5 block text-xs font-medium text-text-muted">Profesyonel Özgeçmiş</label>
              <textarea
                v-model="summaryText"
                class="custom-input min-h-[120px] w-full resize-none rounded-lg px-3 py-2.5 text-sm text-white placeholder-gray-600 focus:outline-none"
                placeholder="Kendinizden ve hedeflerinizden kısaca bahsedin..."
              ></textarea>
            </div>
          </div>
        </details>

        <!-- Technical Info Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">terminal</span>
              </div>
              <h3 class="text-sm font-medium text-white">Teknik Bilgiler</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div class="relative space-y-3 overflow-hidden rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm">
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div>
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Yeni Kategori</label>
                  <input
                    v-model="newCategoryTitle"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                    placeholder="Örn: Programming Languages"
                  />
                </div>
                <button
                  class="flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-primary/40 px-3 py-2 text-sm font-semibold text-primary transition hover:border-primary/60 hover:bg-primary/10 disabled:opacity-50"
                  type="button"
                  :disabled="!newCategoryTitle"
                  @click="addTechnicalCategory"
                >
                  <span class="material-symbols-outlined text-[18px]">add_circle</span>
                  Kategori Ekle
                </button>
              </div>

              <div
                v-for="(category, cIdx) in technicalCategories"
                :key="cIdx"
                class="relative space-y-3 overflow-hidden rounded-2xl border border-surface-border bg-[#111c2d] p-5 pl-6 shadow-sm"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] rounded-l-2xl bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="flex items-center justify-between gap-3">
                  <input
                    v-model="category.title"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                  />
                  <div class="flex items-center gap-2">
                    <span class="text-[11px] text-white/50">Kategori</span>
                    <button
                      class="flex h-8 w-8 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                      type="button"
                      @click="removeCategory(cIdx)"
                    >
                      <span class="material-symbols-outlined text-[18px]">delete</span>
                    </button>
                  </div>
                </div>
                <div class="space-y-2">
                  <div
                    v-for="(skill, idx) in category.skills"
                    :key="idx"
                    class="grid grid-cols-[1fr_1fr_auto] items-end gap-3 rounded-lg border border-[#1E293B] bg-[#162032] px-3 py-2"
                  >
                    <div class="flex flex-col">
                      <label class="text-[11px] uppercase tracking-wide text-text-muted">Teknoloji</label>
                      <input
                        v-model="skill.name"
                        class="w-full rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                        placeholder="Python, React..."
                      />
                    </div>
                    <div class="flex flex-col">
                      <label class="text-[11px] uppercase tracking-wide text-text-muted">Seviye</label>
                      <input
                        v-model="skill.level"
                        class="w-full rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                        placeholder="Örn: Orta Derece"
                      />
                    </div>
                    <button
                      class="flex h-9 w-9 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                      type="button"
                      @click="removeSkill(cIdx, idx)"
                    >
                      <span class="material-symbols-outlined text-[18px]">close</span>
                    </button>
                  </div>

                  <div class="grid grid-cols-[1fr_1fr_auto] items-end gap-3 rounded-lg border border-dashed border-[#1E293B] bg-[#162032] px-3 py-2">
                    <div class="flex flex-col">
                      <label class="text-[11px] uppercase tracking-wide text-text-muted">Teknoloji</label>
                      <input
                        v-model="category.newSkillName"
                        class="w-full rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                        placeholder="Yeni teknoloji"
                      />
                    </div>
                    <div class="flex flex-col">
                      <label class="text-[11px] uppercase tracking-wide text-text-muted">Seviye</label>
                      <input
                        v-model="category.newSkillLevel"
                        class="w-full rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                        placeholder="Örn: Orta"
                      />
                    </div>
                    <button
                      class="flex h-9 items-center justify-center rounded-lg border border-primary/40 px-3 text-xs font-semibold text-primary transition hover:border-primary/60 hover:bg-primary/10 disabled:opacity-50"
                      type="button"
                      :disabled="!category.newSkillName || !category.newSkillLevel"
                      @click="addSkill(cIdx)"
                    >
                      Ekle
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </details>

        <!-- Experience Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">work</span>
              </div>
              <h3 class="text-sm font-medium text-white">İş Deneyimi</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="space-y-3 border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div
                v-for="(exp, eIdx) in experiences"
                :key="eIdx"
                class="relative overflow-hidden space-y-3 rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm transition-all hover:border-primary/50 hover:shadow-[0_10px_40px_-30px_rgba(110,94,247,0.6)]"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>

                <div class="flex flex-wrap items-center gap-3">
                  <div class="flex flex-1 items-center gap-2 rounded-lg border border-[#1E293B] bg-[#162032] px-3 py-2">
                    <span class="material-symbols-outlined text-[18px] text-text-muted">work</span>
                    <div class="flex flex-col">
                      <label class="text-[11px] uppercase tracking-wide text-text-muted">Rol</label>
                      <input
                        v-model="exp.role"
                        class="w-full rounded-md border border-transparent bg-transparent text-base font-semibold text-white focus:border-primary/40 focus:outline-none"
                        placeholder="Örn: Asistan Öğrenci"
                      />
                    </div>
                  </div>

                  <div class="flex flex-col rounded-lg border border-[#1E293B] bg-[#162032] px-3 py-2">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Tarih</label>
                    <input
                      v-model="exp.period"
                      class="min-w-[180px] rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Kas 2022 – Mar 2023"
                    />
                  </div>
                </div>

                <div class="flex flex-wrap items-center gap-2 rounded-lg border border-[#1E293B] bg-[#162032] px-3 py-2">
                  <span class="material-symbols-outlined text-[18px] text-text-muted">apartment</span>
                  <input
                    v-model="exp.company"
                    class="flex-1 rounded-md border border-transparent bg-transparent text-sm text-white focus:border-primary/40 focus:outline-none"
                    placeholder="Şirket / Kurum"
                  />
                </div>

                <div class="space-y-2 rounded-lg border border-[#1E293B] bg-[#162032] p-3">
                  <div class="flex items-center justify-between">
                    <p class="text-[11px] uppercase tracking-wide text-text-muted">Açıklamalar</p>
                    <button
                      class="flex h-8 items-center justify-center gap-1 rounded-lg border border-primary/30 px-2 text-xs font-semibold text-primary transition hover:border-primary/50 hover:bg-primary/10"
                      type="button"
                      :disabled="!exp.newBullet?.trim()"
                      @click="addBullet(eIdx)"
                    >
                      <span class="material-symbols-outlined text-[16px]">add</span>
                      Ekle
                    </button>
                  </div>
                  <div class="flex gap-2">
                    <input
                      v-model="exp.newBullet"
                      class="custom-input w-full rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600 focus:outline-none"
                      placeholder="Madde ekle"
                    />
                  </div>
                  <ul class="space-y-2">
                    <li
                      v-for="(bullet, bIdx) in exp.bullets"
                      :key="bIdx"
                      class="flex items-start gap-2 rounded-md border border-transparent bg-transparent px-1 py-1 text-sm text-white/80 hover:border-[#1E293B]"
                    >
                      <span class="mt-1 text-primary">•</span>
                      <span class="flex-1 leading-relaxed">{{ bullet }}</span>
                      <button
                        class="ml-2 flex h-7 w-7 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                        type="button"
                        @click="removeBullet(eIdx, bIdx)"
                      >
                        <span class="material-symbols-outlined text-[16px]">close</span>
                      </button>
                    </li>
                  </ul>
                </div>

                <div class="flex justify-end">
                  <button
                    class="flex items-center gap-1 rounded-lg border border-red-500/30 px-3 py-2 text-xs font-semibold text-red-400 transition hover:border-red-500/60 hover:bg-red-500/10"
                    type="button"
                    @click="removeExperience(eIdx)"
                  >
                    <span class="material-symbols-outlined text-[16px]">delete</span>
                    Sil
                  </button>
                </div>
              </div>

              <button
                class="mt-2 flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-surface-border py-3 text-sm font-medium text-text-muted transition-all hover:border-primary/50 hover:bg-primary/5 hover:text-primary"
                type="button"
                @click="addExperience"
              >
                <span class="material-symbols-outlined text-lg">add_circle</span>
                Yeni İş Deneyimi Ekle
              </button>
            </div>
          </div>
        </details>

        <!-- Education Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">school</span>
              </div>
              <h3 class="text-sm font-medium text-white">Eğitim Bilgileri</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div
                v-for="(edu, edIdx) in education"
                :key="edIdx"
                class="relative space-y-3 overflow-hidden rounded-2xl border border-surface-border bg-[#111c2d] p-5 pl-6 shadow-sm"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60 rounded-l-2xl"></div>
                <div class="flex flex-col gap-2">
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Okul</label>
                  <input
                    v-model="edu.school"
                    class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                    placeholder="Yıldız Teknik Üniversitesi"
                  />
                </div>
                <div class="grid grid-cols-1 gap-3 md:grid-cols-3">
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Program / Bölüm</label>
                    <input
                      v-model="edu.degree"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Lisans - BÖTE"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Sınıf</label>
                    <input
                      v-model="edu.year"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="3. sınıf"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">GPA</label>
                    <input
                      v-model="edu.gpa"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="3.0"
                    />
                  </div>
                </div>
                <div class="flex justify-end">
                  <button
                    class="flex items-center gap-1 rounded-lg border border-red-500/30 px-3 py-2 text-xs font-semibold text-red-400 transition hover:border-red-500/60 hover:bg-red-500/10"
                    type="button"
                    @click="removeEducation(edIdx)"
                  >
                    <span class="material-symbols-outlined text-[16px]">delete</span>
                    Sil
                  </button>
                </div>
              </div>

              <button
                class="flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-surface-border py-3 text-sm font-medium text-text-muted transition-all hover:border-primary/50 hover:bg-primary/5 hover:text-primary"
                type="button"
                @click="addEducation"
              >
                <span class="material-symbols-outlined text-lg">add_circle</span>
                Yeni Eğitim Ekle
              </button>
            </div>
          </div>
        </details>

        <!-- Projects Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">folder_open</span>
              </div>
              <h3 class="text-sm font-medium text-white">Projelerim</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div class="relative space-y-3 overflow-hidden rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm">
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="grid grid-cols-1 gap-3 md:grid-cols-[1.2fr_0.8fr]">
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Proje Adı</label>
                    <input
                      v-model="newProject.name"
                      class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Örn: CV Builder"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Proje Linki (Opsiyonel)</label>
                    <input
                      v-model="newProject.link"
                      class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="https://..."
                    />
                  </div>
                </div>
                <button
                  class="flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-primary/40 px-3 py-2 text-sm font-semibold text-primary transition hover:border-primary/60 hover:bg-primary/10 disabled:opacity-50"
                  type="button"
                  :disabled="!newProject.name.trim()"
                  @click="addProject"
                >
                  <span class="material-symbols-outlined text-[18px]">add_circle</span>
                  Proje Ekle
                </button>
              </div>

              <div
                v-for="(project, pIdx) in projects"
                :key="pIdx"
                class="relative space-y-3 overflow-hidden rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="grid grid-cols-1 gap-3 md:grid-cols-[1.2fr_0.8fr]">
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Proje Adı</label>
                    <input
                      v-model="project.name"
                      class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Proje Adı"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Proje Linki</label>
                    <input
                      v-model="project.link"
                      class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="https://..."
                    />
                  </div>
                </div>

                <div class="space-y-2 rounded-lg border border-[#1E293B] bg-[#162032] p-3">
                  <div class="flex items-center justify-between">
                    <p class="text-[11px] uppercase tracking-wide text-text-muted">Yapılanlar</p>
                    <button
                      class="flex h-8 items-center justify-center gap-1 rounded-lg border border-primary/30 px-2 text-xs font-semibold text-primary transition hover:border-primary/50 hover:bg-primary/10"
                      type="button"
                      :disabled="!project.newBullet?.trim()"
                      @click="addProjectBullet(pIdx)"
                    >
                      <span class="material-symbols-outlined text-[16px]">add</span>
                      Ekle
                    </button>
                  </div>
                  <div class="flex gap-2">
                    <input
                      v-model="project.newBullet"
                      class="custom-input w-full rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600 focus:outline-none"
                      placeholder="Madde ekle"
                    />
                  </div>
                  <ul class="space-y-2">
                    <li
                      v-for="(bullet, bIdx) in project.bullets"
                      :key="bIdx"
                      class="flex items-start gap-2 rounded-md border border-transparent bg-transparent px-1 py-1 text-sm text-white/80 hover:border-[#1E293B]"
                    >
                      <span class="mt-1 text-primary">•</span>
                      <span class="flex-1 leading-relaxed">{{ bullet }}</span>
                      <button
                        class="ml-2 flex h-7 w-7 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                        type="button"
                        @click="removeProjectBullet(pIdx, bIdx)"
                      >
                        <span class="material-symbols-outlined text-[16px]">close</span>
                      </button>
                    </li>
                  </ul>
                </div>

                <div class="flex justify-end">
                  <button
                    class="flex items-center gap-1 rounded-lg border border-red-500/30 px-3 py-2 text-xs font-semibold text-red-400 transition hover:border-red-500/60 hover:bg-red-500/10"
                    type="button"
                    @click="removeProject(pIdx)"
                  >
                    <span class="material-symbols-outlined text-[16px]">delete</span>
                    Sil
                  </button>
                </div>
              </div>
            </div>
          </div>
        </details>

        <!-- Volunteer Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">diversity_3</span>
              </div>
              <h3 class="text-sm font-medium text-white">Gönüllü Çalışmalar</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div
                v-for="(vol, vIdx) in volunteerWorks"
                :key="vIdx"
                class="relative space-y-3 overflow-hidden rounded-2xl border border-surface-border bg-[#111c2d] p-5 pl-6 shadow-sm"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] rounded-l-2xl bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="grid grid-cols-1 gap-3 md:grid-cols-[1.2fr_0.9fr_0.6fr]">
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Rol</label>
                    <input
                      v-model="vol.role"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Rol"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Kurum</label>
                    <input
                      v-model="vol.org"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="Kulüp / Organizasyon"
                    />
                  </div>
                  <div class="flex flex-col">
                    <label class="text-[11px] uppercase tracking-wide text-text-muted">Yıllar</label>
                    <input
                      v-model="vol.years"
                      class="w-full rounded-lg border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                      placeholder="2021-2023"
                    />
                  </div>
                </div>

                <div class="space-y-2 rounded-lg border border-[#1E293B] bg-[#162032] p-3">
                  <div class="flex items-center justify-between">
                    <p class="text-[11px] uppercase tracking-wide text-text-muted">Yapılanlar</p>
                    <button
                      class="flex h-8 items-center justify-center gap-1 rounded-lg border border-primary/30 px-2 text-xs font-semibold text-primary transition hover:border-primary/50 hover:bg-primary/10"
                      type="button"
                      :disabled="!vol.newBullet?.trim()"
                      @click="addVolunteerBullet(vIdx)"
                    >
                      <span class="material-symbols-outlined text-[16px]">add</span>
                      Ekle
                    </button>
                  </div>
                  <input
                    v-model="vol.newBullet"
                    class="custom-input w-full rounded-lg px-3 py-2 text-sm text-white placeholder-gray-600 focus:outline-none"
                    placeholder="Madde ekle"
                  />
                  <ul class="space-y-2">
                    <li
                      v-for="(b, bIdx) in vol.bullets"
                      :key="bIdx"
                      class="flex items-start gap-2 rounded-md border border-transparent bg-transparent px-1 py-1 text-sm text-white/80 hover:border-[#1E293B]"
                    >
                      <span class="mt-1 text-primary">•</span>
                      <span class="flex-1 leading-relaxed">{{ b }}</span>
                      <button
                        class="ml-2 flex h-7 w-7 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                        type="button"
                        @click="removeVolunteerBullet(vIdx, bIdx)"
                      >
                        <span class="material-symbols-outlined text-[16px]">close</span>
                      </button>
                    </li>
                  </ul>
                </div>

                <div class="flex justify-end">
                  <button
                    class="flex items-center gap-1 rounded-lg border border-red-500/30 px-3 py-2 text-xs font-semibold text-red-400 transition hover:border-red-500/60 hover:bg-red-500/10"
                    type="button"
                    @click="removeVolunteer(vIdx)"
                  >
                    <span class="material-symbols-outlined text-[16px]">delete</span>
                    Sil
                  </button>
                </div>
              </div>

              <button
                class="flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-surface-border py-3 text-sm font-medium text-text-muted transition-all hover:border-primary/50 hover:bg-primary/5 hover:text-primary"
                type="button"
                @click="addVolunteer"
              >
                <span class="material-symbols-outlined text-lg">add_circle</span>
                Yeni Gönüllü Çalışma Ekle
              </button>
            </div>
          </div>
        </details>

        <!-- Certificates Accordion -->
        <details
          class="group rounded-xl border border-surface-border bg-[#162032] transition-all duration-300 open:ring-1 open:ring-primary/30"
          open
        >
          <summary class="flex cursor-pointer list-none select-none items-center justify-between p-4">
            <div class="flex items-center gap-3">
              <div class="flex h-8 w-8 items-center justify-center rounded-lg bg-primary/20 text-primary">
                <span class="material-symbols-outlined text-lg">workspace_premium</span>
              </div>
              <h3 class="text-sm font-medium text-white">Sertifikalar</h3>
            </div>
            <span class="material-symbols-outlined text-text-muted transition-transform duration-300 group-open:rotate-180">
              expand_more
            </span>
          </summary>
          <div class="border-t border-surface-border/50 p-4 pt-0 animate-[fadeIn_0.3s_ease-out]">
            <div class="space-y-3 pt-4">
              <div class="relative space-y-3 overflow-hidden rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm md:grid md:grid-cols-[1.2fr_0.8fr] md:items-start md:space-y-0 md:gap-3">
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="flex flex-col">
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Sertifika Adı</label>
                  <input
                    v-model="newCertificate.name"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                    placeholder="Crash Course on Python"
                  />
                </div>
                <div class="flex flex-col">
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Link</label>
                  <input
                    v-model="newCertificate.link"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                    placeholder="https://..."
                  />
                </div>
                <button
                  class="md:col-span-2 flex w-full items-center justify-center gap-2 rounded-lg border border-dashed border-primary/40 px-3 py-2 text-sm font-semibold text-primary transition hover:border-primary/60 hover:bg-primary/10 disabled:opacity-50"
                  type="button"
                  :disabled="!newCertificate.name.trim()"
                  @click="addCertificate"
                >
                  <span class="material-symbols-outlined text-[18px]">add_circle</span>
                  Sertifika Ekle
                </button>
              </div>

              <div
                v-for="(cert, cIdx) in certificates"
                :key="cIdx"
                class="relative flex flex-wrap items-center gap-3 overflow-hidden rounded-xl border border-surface-border bg-[#111c2d] p-4 pl-5 shadow-sm"
              >
                <div class="absolute left-0 top-0 h-full w-[4px] bg-gradient-to-b from-primary via-cyan-400 to-primary/60"></div>
                <div class="flex-1 min-w-[220px]">
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Sertifika Adı</label>
                  <input
                    v-model="cert.name"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm font-semibold text-white focus:border-primary/40 focus:outline-none"
                    placeholder="Sertifika Adı"
                  />
                </div>
                <div class="flex-1 min-w-[200px]">
                  <label class="text-[11px] uppercase tracking-wide text-text-muted">Link</label>
                  <input
                    v-model="cert.link"
                    class="w-full rounded-md border border-transparent bg-[#162032] px-3 py-2 text-sm text-white focus:border-primary/40 focus:outline-none"
                    placeholder="https://..."
                  />
                </div>
                <button
                  class="flex h-9 w-9 items-center justify-center rounded-full border border-[#1E293B] text-text-muted transition hover:border-red-500/50 hover:text-red-400"
                  type="button"
                  @click="removeCertificate(cIdx)"
                >
                  <span class="material-symbols-outlined text-[18px]">delete</span>
                </button>
              </div>
            </div>
          </div>
        </details>
      </div>
    </section>

    <section
      class="w-full rounded-2xl border border-[#1E293B] bg-[#0F1B2D]/80 p-6 text-white shadow-[0_24px_80px_-50px_rgba(0,0,0,0.8)] backdrop-blur lg:basis-[45%]"
    >
      <div
        class="flex min-h-[320px] items-center justify-center rounded-2xl border border-dashed border-[#1E293B] bg-gradient-to-br from-[#0b1220] via-[#101a2e] to-[#0b1220] p-8 text-center text-white/70"
      >
        Editör entegrasyonu bu alana gömülecek. Sağ panel genişletildi; oran korunarak daha ferah görünüm sağlandı.
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

type TechnicalSkill = {
  name: string;
  level: string;
};

type TechnicalCategory = {
  title: string;
  skills: TechnicalSkill[];
  newSkillName?: string;
  newSkillLevel?: string;
};

type Experience = {
  role: string;
  company: string;
  period: string;
  bullets: string[];
  newBullet?: string;
};

const contact = ref({
  firstName: "Ali Osman",
  lastName: "Demirkollu",
  email: "demirkolluosmanali@gmail.com",
  phone: "(533) 406-9864",
  location: "Başakşehir, Istanbul",
  githubUrl: "https://github.com/demirkolluosmanali",
  linkedinUrl: "https://www.linkedin.com/in/demirkolluosmanali",
});

const experiences = ref<Experience[]>([
  {
    role: "Asistan Öğrenci",
    company: "Yıldız Teknik Üniversitesi",
    period: "Kas 2022 – Mar 2023",
    bullets: [
      "Akademisyenlerle takım çalışması yaparak araştırma projeleri ve akademik görevleri yürüttüm.",
      "Bilgisayar laboratuvarlarının denetlenmesi, yazılım yüklemeleri ve sınav süreçlerinde teknik destek sağladım.",
      "Öğrencilerle aktif iletişim kurarak yönlendirme ve danışmanlık verdim.",
    ],
  },
]);

const technicalCategories = ref<TechnicalCategory[]>([
  {
    title: "Programming Languages",
    skills: [
      { name: "Python", level: "Orta Derece" },
      { name: "Reactjs", level: "Orta Derece" },
      { name: "C#", level: "Orta Derece" },
      { name: "Java", level: "Orta Derece" },
    ],
  },
  {
    title: "Databases and Cloud Technologies",
    skills: [
      { name: "SQL", level: "Orta Derece" },
      { name: "SQLite", level: "Orta Derece" },
      { name: "DBeaver", level: "Orta Derece" },
      { name: "Git", level: "Orta Derece" },
      { name: "Firebase", level: "Orta Derece" },
    ],
  },
]);

const newCategoryTitle = ref("");
const addTechnicalCategory = () => {
  if (!newCategoryTitle.value.trim()) return;
  technicalCategories.value.push({
    title: newCategoryTitle.value.trim(),
    skills: [],
  });
  newCategoryTitle.value = "";
};

const removeCategory = (idx: number) => {
  technicalCategories.value.splice(idx, 1);
};

const addSkill = (categoryIdx: number) => {
  const category = technicalCategories.value[categoryIdx];
  if (!category) return;
  const name = category.newSkillName?.trim() ?? "";
  const level = category.newSkillLevel?.trim() ?? "";
  if (!name || !level) return;
  category.skills.push({ name, level });
  category.newSkillName = "";
  category.newSkillLevel = "";
};

const removeSkill = (categoryIdx: number, skillIdx: number) => {
  const category = technicalCategories.value[categoryIdx];
  if (!category) return;
  category.skills.splice(skillIdx, 1);
};

const addExperience = () => {
  experiences.value.push({
    role: "Yeni Rol",
    company: "Şirket",
    period: "Başlangıç - Bitiş",
    bullets: ["Sorumluluk ekleyin"],
  });
};

const removeExperience = (idx: number) => {
  experiences.value.splice(idx, 1);
};

const addBullet = (expIdx: number) => {
  const exp = experiences.value[expIdx];
  if (!exp) return;
  const text = exp.newBullet?.trim() ?? "";
  if (!text) return;
  exp.bullets.push(text);
  exp.newBullet = "";
};

const removeBullet = (expIdx: number, bulletIdx: number) => {
  const exp = experiences.value[expIdx];
  if (!exp) return;
  exp.bullets.splice(bulletIdx, 1);
};

type EducationItem = {
  school: string;
  year: string;
  degree: string;
  gpa: string;
};

type Project = {
  name: string;
  link: string;
  bullets: string[];
  newBullet?: string;
};

type VolunteerWork = {
  role: string;
  org: string;
  years: string;
  bullets: string[];
  newBullet?: string;
};

type Certificate = {
  name: string;
  link: string;
};

const education = ref<EducationItem[]>([
  {
    school: "Yıldız Teknik Üniversitesi",
    year: "3. sınıf",
    degree: "Lisans - Bilgisayar ve Öğretim Teknolojileri Eğitimi",
    gpa: "3.0",
  },
  {
    school: "Howard Community College, MD",
    year: "",
    degree: "ESL – English as a Second Language",
    gpa: "",
  },
]);

const addEducation = () => {
  education.value.push({
    school: "Yeni Okul",
    year: "Sınıf",
    degree: "Tür / Program",
    gpa: "",
  });
};

const removeEducation = (idx: number) => {
  education.value.splice(idx, 1);
};

const volunteerWorks = ref<VolunteerWork[]>([
  {
    role: "Sosyal Medya ve Tasarım Ekip Üyesi",
    org: "Mint Öğrenci Kulübü",
    years: "2021-2023",
    bullets: [
      "Illustrator, Photoshop, InDesign ve Canva ile posterler ve Instagram gönderileri tasarladım.",
      "E-dergi için içerik üretimi yaptım.",
    ],
  },
  {
    role: "Sekreter",
    org: "Eğitim ve Bilişim Teknolojileri Kulübü",
    years: "2022-2023",
    bullets: [
      "Toplantıları koordine ederek gerekli belge ve formları hazırladım.",
      "Üniversite ile kulüp arasındaki iletişimi yürüttüm.",
    ],
  },
]);

const addVolunteer = () => {
  volunteerWorks.value.push({
    role: "Yeni Rol",
    org: "Kurum / Kulüp",
    years: "Yıllar",
    bullets: ["Yapılan iş ekleyin"],
  });
};

const removeVolunteer = (idx: number) => {
  volunteerWorks.value.splice(idx, 1);
};

const addVolunteerBullet = (idx: number) => {
  const vol = volunteerWorks.value[idx];
  if (!vol) return;
  const text = vol.newBullet?.trim() ?? "";
  if (!text) return;
  vol.bullets.push(text);
  vol.newBullet = "";
};

const removeVolunteerBullet = (vIdx: number, bIdx: number) => {
  const vol = volunteerWorks.value[vIdx];
  if (!vol) return;
  vol.bullets.splice(bIdx, 1);
};

const projects = ref<Project[]>([
  {
    name: "CV Builder",
    link: "https://example.com/cv",
    bullets: ["Form tabanlı veri girişleri", "PDF export desteği", "Şablon yönetimi"],
  },
]);

const newProject = ref<Project>({
  name: "",
  link: "",
  bullets: [],
});

const addProject = () => {
  if (!newProject.value.name.trim()) return;
  projects.value.push({
    name: newProject.value.name.trim(),
    link: newProject.value.link.trim(),
    bullets: [],
  });
  newProject.value = { name: "", link: "", bullets: [] };
};

const removeProject = (idx: number) => {
  projects.value.splice(idx, 1);
};

const addProjectBullet = (projIdx: number) => {
  const proj = projects.value[projIdx];
  if (!proj) return;
  const text = proj.newBullet?.trim() ?? "";
  if (!text) return;
  proj.bullets.push(text);
  proj.newBullet = "";
};

const removeProjectBullet = (projIdx: number, bulletIdx: number) => {
  const proj = projects.value[projIdx];
  if (!proj) return;
  proj.bullets.splice(bulletIdx, 1);
};

const certificates = ref<Certificate[]>([
  { name: "Crash Course on Python – Coursera", link: "" },
  { name: "An Introduction to Interactive Programming in Python", link: "" },
]);

const newCertificate = ref<Certificate>({
  name: "",
  link: "",
});

const addCertificate = () => {
  if (!newCertificate.value.name.trim()) return;
  certificates.value.push({
    name: newCertificate.value.name.trim(),
    link: newCertificate.value.link.trim(),
  });
  newCertificate.value = { name: "", link: "" };
};

const removeCertificate = (idx: number) => {
  certificates.value.splice(idx, 1);
};

const summaryText = ref("");
</script>

<style scoped>
.no-scrollbar::-webkit-scrollbar {
  display: none;
}
.no-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}

.custom-scroll::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}
.custom-scroll::-webkit-scrollbar-thumb {
  background: #1e293b;
  border-radius: 4px;
}
.custom-scroll::-webkit-scrollbar-thumb:hover {
  background: #334155;
}

:global(.custom-input) {
  background-color: #162032;
  border: 1px solid #2d3a52;
  transition: all 0.2s ease;
}
:global(.custom-input:focus) {
  background-color: #1a2639;
  border-color: #6e5ef7;
  box-shadow: 0 0 0 1px #6e5ef7;
}
:global(.text-text-muted) {
  color: #94a3b8;
}
:global(.border-surface-border) {
  border-color: #1e293b;
}
:global(.bg-surface-border) {
  background-color: #1e293b;
}
</style>
