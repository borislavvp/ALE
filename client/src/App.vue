<template>
  <div id="app" class="bg-gray-100">
    <Main />
  </div>
</template>

<script lang="ts">
  import Vue from "vue";
  import Main from "./components/Main.vue";

  export default Vue.extend({
    name: "App",
    components: {
      Main
    }
  });
</script>
