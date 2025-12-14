<template>
  <div class="bg-background-dark text-white min-h-screen flex flex-col relative overflow-hidden">
    <div class="absolute gradient-blob bg-primary w-[500px] h-[500px] rounded-full top-[-100px] left-[-100px] opacity-40 blur-[100px]"></div>
    <div class="absolute gradient-blob bg-secondary w-[400px] h-[400px] rounded-full bottom-[-50px] right-[-50px] opacity-30 blur-[100px]"></div>
    <header class="relative z-10 w-full px-8 py-6 flex items-center justify-between">
      <div class="flex items-center gap-3">
        <div class="size-8 text-primary">
          <slot name="brand" />
        </div>
        <h1 class="text-white text-xl font-bold tracking-tight">
          <slot name="title">ResumeMaker</slot>
        </h1>
      </div>
      <div class="hidden md:block">
        <slot name="cta" />
      </div>
    </header>
    <main class="relative z-10 flex-1 flex items-center justify-center p-4">
      <slot />
    </main>
    <div class="absolute bottom-0 w-full h-1/3 pointer-events-none z-0">
      <slot name="footer-decor" />
    </div>
  </div>
</template>
