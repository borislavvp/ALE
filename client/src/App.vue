<template>
  <div id="app" class="bg-gray-100">
    <ExpressionMain />
  </div>
</template>

<script lang="ts">
  import Vue from "vue";
  import ExpressionMain from "./components/expression/Main.vue";

  export default Vue.extend({
    name: "App",
    components: {
      ExpressionMain
    }
  });
</script>
