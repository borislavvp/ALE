<template>
  <div class="relative">
    <div
      class="flex relative items-center bg-white shadow-md rounded-2xl px-4 py-2"
    >
      <icon-loading
        v-if="IsTesting"
        class="w-6 h-6 fill-current animate-spin text-gray-600"
      />
      <icon-check
        v-if="IsDFA && !IsTesting"
        class="w-6 h-6 fill-current text-green-600"
      />
      <icon-x v-if="!IsDFA && !IsTesting" class="w-6 h-6 fill-current " />
      <span class="mx-2 font-semibold text-gray-800 text-base">DFA</span>
      <div v-if="!IsDFA && !IsTesting" class="flex ml-2 bg-white rounded-xl ">
        <label for="toggleDFA" class="flex items-center cursor-pointer">
          <div class="relative flex">
            <input
              id="toggleDFA"
              type="checkbox"
              @change="toggleGraph"
              class="sr-only"
            />
            <div class="w-10 h-4 bg-gray-400 rounded-full shadow-inner"></div>
            <div
              class="dot absolute w-6 h-6 bg-gray-600 rounded-full shadow -mt-1 -ml-1 transition-default duration-300"
            ></div>
          </div>
        </label>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent, computed } from "@vue/composition-api";
  import { withFiniteAutomataProvider } from "@/providers/finiteautomata/withFiniteAutomataProvider";
  import IconCheck from "../shared/IconCheck.vue";
  import IconX from "../shared/IconX.vue";
  import IconLoading from "../shared/IconLoading.vue";
  export default defineComponent({
    components: {
      IconCheck,
      IconX,
      IconLoading
    },
    setup() {
      const automataProvider = withFiniteAutomataProvider();

      const toggleGraph = (event: any) => {
        if (event.target.checked) {
          automataProvider.showDFA();
        } else {
          automataProvider.showOriginal();
        }
      };
      return {
        toggleGraph,
        IsTesting: computed(
          () =>
            automataProvider.evaluation.value.Testing ||
            automataProvider.evaluation.value.Processing
        ),
        IsDFA: computed(
          () => automataProvider.evaluation.value.Tests.IsDFA.Answer
        )
      };
    }
  });
</script>

<style >
  input:checked ~ .dot {
    transform: translateX(100%);
    background-color: #48bb78;
  }
</style>