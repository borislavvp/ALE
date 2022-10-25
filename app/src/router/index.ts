import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/logic",
    name: "ALE1",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/ExpressionView.vue")
  },
  {
    path: "/automata",
    name: "ALE2",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/FiniteAutomataView.vue")
  }
];

export const router = createRouter({
  history: createWebHistory(),
  routes
});

