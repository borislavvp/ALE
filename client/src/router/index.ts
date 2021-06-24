import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/ale1",
    name: "ALE1",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/ExpressionView.vue")
  },
  {
    path: "/ale2",
    name: "ALE2",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/FiniteAutomataView.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
