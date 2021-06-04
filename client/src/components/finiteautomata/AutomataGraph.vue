<template>
  <div class="relative mx-10 flex-auto h-full">
    <div
      class="absolute inset-0 z-0 bg-gradient-to-b from-yellow-800 to-yellow-500 shadow-lg transform -skew-y-6 sm:skew-y-0 sm:rotate-3 rounded-3xl "
    ></div>
    <div class="finiteAutomatagraph" id="finiteAutomatagraph">
      <div class="flex items-center w-full justify-between px-16 mb-10">
        <div class="flex items-center justify-between">
          <DFAIndication />
          <FiniteIndication />
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import DFAIndication from "./DFAIndication.vue";
  import FiniteIndication from "./FiniteIndication.vue";
  import { defineComponent } from "@vue/composition-api";
  export default defineComponent({
    components: {
      FiniteIndication,
      DFAIndication
    }
  });
</script>

<style lang="scss" >
  .finiteAutomatagraph {
    #graph {
      @apply w-full h-full select-none flex px-6;
    }

    @apply shadow-lg relative rounded-3xl flex-auto h-full flex flex-col-reverse items-start bg-white;
  }
</style>