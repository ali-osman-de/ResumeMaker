import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import LoginPage from './pages/auth/LoginPage.vue';
import SignupPage from './pages/auth/SignupPage.vue';
import DashboardPage from './pages/app/DashboardPage.vue';
import EditorPage from './pages/app/EditorPage.vue';

const routes: RouteRecordRaw[] = [
  { path: '/', redirect: '/dashboard' },
  { path: '/dashboard', name: 'dashboard', component: DashboardPage },
  { path: '/editor/:id?', name: 'editor', component: EditorPage, props: true },
  { path: '/login', name: 'login', component: LoginPage },
  { path: '/signup', name: 'signup', component: SignupPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
