<template>
  <div id="app" class="bg-gray-100">
    <HelloWorld />
  </div>
</template>

<script lang="ts">
  import Vue from "vue";
  import HelloWorld from "./components/HelloWorld.vue";

  export default Vue.extend({
    name: "App",
    components: {
      HelloWorld
    }
  });
</script>
