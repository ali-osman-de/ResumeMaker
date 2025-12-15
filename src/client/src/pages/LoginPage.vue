<template>
  <section class="border rounded-xl p-6">
    <h2 class="text-2xl font-bold mb-4">Giriş</h2>

    <form class="flex flex-col gap-3" @submit.prevent="login">
      <input v-model="email" placeholder="E-posta" class="border rounded p-2" />
      <input v-model="password" type="password" placeholder="Şifre" class="border rounded p-2" />
      <button class="border rounded p-2 font-bold">Giriş Yap</button>
    </form>
  </section>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();
const email = ref("");
const password = ref("");

async function login() {
  await router.push("/app");
}
</script>
