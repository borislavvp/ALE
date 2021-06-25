<template>
  <div class="ml-6 flex items-center bg-white shadow-md rounded-2xl px-4 py-2">
    <icon-loading
      v-if="IsTesting"
      class="w-6 h-6 fill-current animate-spin text-gray-600"
    />
    <div v-else class="mr-6">
      <icon-check
        v-if="IsFiniteAnswer || IsFiniteAnswer !== IsFiniteGuess"
        class="w-6 absolute -mt-3 h-6 z-10 fill-current text-green-600"
        :class="{
          'z-0 opacity-75': IsFiniteAnswer !== IsFiniteGuess && !IsFiniteAnswer
        }"
      />
      <icon-x
        v-if="!IsFiniteAnswer || IsFiniteAnswer !== IsFiniteGuess"
        class="w-6 absolute -mt-3 h-6 z-10"
        :class="{
          'z-0 opacity-50': IsFiniteAnswer !== IsFiniteGuess && IsFiniteAnswer
        }"
      />
    </div>
    <span class="mx-2 font-semibold text-gray-800 text-base">Finite</span>
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

      return {
        IsTesting: computed(
          () =>
            automataProvider.evaluation.value.Testing ||
            automataProvider.evaluation.value.Processing
        ),
        IsFiniteGuess: computed(
          () => automataProvider.evaluation.value.Tests.IsFinite.TestGuess
        ),
        IsFiniteAnswer: computed(
          () => automataProvider.evaluation.value.Tests.IsFinite.Answer
        )
      };
    }
  });
</script>

<style>
</style>