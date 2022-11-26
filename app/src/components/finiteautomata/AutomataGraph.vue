<template>
  <div class="relative mx-10 flex-auto h-full">
    <div
      class="absolute inset-0 z-0 bg-gradient-to-b from-indigo-800 to-blue-500 shadow-lg transform -skew-y-6 sm:skew-y-0 sm:rotate-3 rounded-3xl "
    ></div>
    <div class="finiteAutomatagraph" id="finiteAutomatagraph">
      <div
        class="flex absolute top-0 items-center w-full justify-between px-16 mt-6"
      >
        <div
          class="flex items-center justify-between bg-white shadow-md rounded-3xl px-4 py-2"
        >
          <span class="font-bold text-gray-800 text-lg">{{ GraphName }}</span>
          <span
            class="text-gray-900 font-semibold text-sm ml-3 px-4 py-1 bg-white-800 shadow-inner rounded-2xl"
            :class="{ 'text-indigo-700': IsDFA }"
            >{{ IsDFA ? "DFA" : "ORIGINAL" }}</span
          >
        </div>
      </div>
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
  import { defineComponent, computed } from "vue";
  import { withFiniteAutomataProvider } from "../../providers/finiteautomata/withFiniteAutomataProvider";
  export default defineComponent({
    components: {
      FiniteIndication,
      DFAIndication
    },
    setup() {
      const automataEvaluation = withFiniteAutomataProvider().evaluation;

      return {
        GraphName: computed(
          () =>automataEvaluation.value.CurrentInstructionName
        ),
        IsDFA: computed(
          () =>automataEvaluation.value.GraphVisible === "DFA"
        )
      };
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