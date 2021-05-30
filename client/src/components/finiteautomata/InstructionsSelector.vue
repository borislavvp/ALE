<template>
  <div class="mb-4 relative">
    <select
      @change="evaluateInstructions"
      class="shadow-md text-gray-700 px-4 py-2 rounded block w-full focus:outline-none appearance-none border-2 focus:border-blue-400"
    >
      <option
        v-for="instruction in instructionsArray"
        :key="instruction.title"
        >{{ instruction.title }}</option
      >
    </select>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref } from "@vue/composition-api";

  export default defineComponent({
    setup(_, context) {
      const instructionsArray = ref([
        {
          title: "Epsilon Loop",
          src: "../../EpsilonLoop.txt"
        }
      ]);

      const evaluateInstructions = (event: any) => {
        context.emit(
          "OnInstructionLoaded",
          instructionsArray.value.find(i => i.title === event.target.value)?.src
        );
      };

      return {
        instructionsArray,
        evaluateInstructions
      };
    }
  });
</script>

<style>
</style>