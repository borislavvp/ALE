import { createRouter, createWebHistory } from "vue-router";

const routes = [
  {
    path: "/",
    name: "Home",
    component: () => import("../views/Home.vue")
  },
  {
    path: "/logic",
    name: "ALE1",
    component: () => import("../views/ExpressionView.vue")
  },
  {
    path: "/automata",
    name: "ALE2",
    component: () => import("../views/FiniteAutomataView.vue")
  },
];

export const router = createRouter({
  history: createWebHistory(),
  routes
});

router.beforeEach(guard => {
  !guard.matched.length && router.push("/").catch({});
})
