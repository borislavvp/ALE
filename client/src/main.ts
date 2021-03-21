import Vue from "vue";
import VueCompositioAPI from '@vue/composition-api';
Vue.use(VueCompositioAPI)
import App from "./App.vue";

Vue.config.productionTip = false;

new Vue({
  render: h => h(App)
}).$mount("#app");
