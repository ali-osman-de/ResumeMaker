import { createRouter, createWebHistory } from "vue-router";
import { getAccessToken } from "@/helpers/auth";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/auth",
      component: () => import("@/pages/layouts/AuthLayout.vue"),
      children: [
        { path: "login", name: "login", component: () => import("@/pages/LoginPage.vue") },
        { path: "signup", name: "signup", component: () => import("@/pages/SignupPage.vue") },
        { path: "", redirect: { name: "login" } },
      ],
    },

    {
      path: "/app",
      component: () => import("@/pages/layouts/MainLayout.vue"),
      meta: { requiresAuth: true },
      children: [
        { path: "dashboard", name: "dashboard", component: () => import("@/pages/DashBoard.vue") },
        { path: "editor", name: "editor", component: () => import("@/pages/EditorPage.vue") },
        { path: "", redirect: { name: "dashboard" } },
      ],
    },

    { path: "/", redirect: "/auth/login" },

    { path: "/:pathMatch(.*)*", redirect: "/auth/login" },
  ],
});

function isAuthenticated(): boolean {
  return !!getAccessToken();
}

router.beforeEach((to) => {
  if (to.meta.requiresAuth && !isAuthenticated()) {
    return { name: "login", query: { redirect: to.fullPath } };
  }
});

export default router;
