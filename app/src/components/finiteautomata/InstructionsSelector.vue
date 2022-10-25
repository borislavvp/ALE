<template>
  <div class="mb-4 relative">
    <div
      class="flex items-center w-full text-center bg-gray-200 mb-4 shadow-inner rounded-3xl"
    >
      <span
        @click="TurnOffRegexMode"
        :class="[
          RegexModeOn
            ? 'text-gray-800 bg-white shadow-lg cursor-pointer hover:bg-gray-100'
            : 'text-indigo-600'
        ]"
        class="tex-sm w-1/2  font-semibold px-4  py-2 rounded-3xl"
        >Instructions</span
      >
      <span
        @click="TurnOnRegexMode"
        :class="[
          RegexModeOn
            ? 'text-indigo-600'
            : 'text-gray-800 bg-white shadow-lg cursor-pointer hover:bg-gray-100'
        ]"
        class="tex-sm w-1/2 text-gray-800 py-2 px-4  font-semibold rounded-3xl"
        >Regex</span
      >
    </div>
    <span v-if="RegexModeOn">
      <input
        id="regex"
        class="px-4 py-2 text-gray-800 font-semibold rounded block w-full shadow-md"
        type="text"
        :value="regex"
        @input="setRegex"
        placeholder="Regex"
      />
    </span>
    <select
      v-else
      :value="CurrentInstructionName"
      @change="OnInstructionChanged"
      class="cursor-pointer shadow-md text-gray-800 font-semibold px-4 py-2 rounded block w-full hover:bg-gray-100 focus:outline-none appearance-none focus:bg-gray-200 focus:shadow-inner"
    >
      <option
        v-for="instruction in instructionsArray"
        :key="instruction.title"
        :value="instruction.title"
      >
        {{ instruction.title }}
      </option>
    </select>
    <svg
      v-if="!RegexModeOn"
      class="absolute pointer-events-none right-0 -mt-8 mr-4 h-6 w-6 fill-current text-gray-500 "
      viewBox="0 0 12 12"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        d="M2.14592 4.64592C2.19236 4.59935 2.24754 4.56241 2.30828 4.5372C2.36903 4.512 2.43415 4.49902 2.49992 4.49902C2.56568 4.49902 2.63081 4.512 2.69155 4.5372C2.7523 4.56241 2.80747 4.59935 2.85392 4.64592L5.99992 7.79292L9.14592 4.64592C9.2398 4.55203 9.36714 4.49929 9.49992 4.49929C9.63269 4.49929 9.76003 4.55203 9.85392 4.64592C9.9478 4.7398 10.0005 4.86714 10.0005 4.99992C10.0005 5.13269 9.9478 5.26003 9.85392 5.35392L6.35392 8.85392C6.30747 8.90048 6.2523 8.93742 6.19155 8.96263C6.13081 8.98784 6.06568 9.00081 5.99992 9.00081C5.93415 9.00081 5.86903 8.98784 5.80828 8.96263C5.74754 8.93742 5.69236 8.90048 5.64592 8.85392L2.14592 5.35392C2.09935 5.30747 2.06241 5.2523 2.0372 5.19155C2.012 5.13081 1.99902 5.06568 1.99902 4.99992C1.99902 4.93415 2.012 4.86903 2.0372 4.80828C2.06241 4.74754 2.09935 4.69236 2.14592 4.64592V4.64592Z"
      />
    </svg>
  </div>
</template>

<script lang="ts">
  import {
    defineComponent,
    onMounted,
    computed,
    ref
  } from "vue";
  import { withFiniteAutomataProvider } from "@/providers/finiteautomata/withFiniteAutomataProvider";
  import { withAutomataGraph } from "@/providers/finiteautomata/withAutomataGraph";
  import debounce from "@/providers/finiteautomata/utils/debounce";
  export default defineComponent({
    setup(_, context) {
      const regex = ref("");

      const automataProvider = withFiniteAutomataProvider();
      const instructionsArray = computed(
        () => automataProvider.evaluation.value.PredefinedInstructions
      );

      const chageInstructions = (instructionTitle: string) => {
        const getFileContent = instructionsArray.value.find(
          i => i.title === instructionTitle
        )?.src;
        getFileContent?.()
          .then((content: any) => {
            const blob = new Blob([content.default as string], {
              type: "text/plain"
            });
            const file = new File([blob], `${instructionTitle}.txt`, {
              type: "text/plain"
            });
            context.emit("OnInstructionLoaded", file);
          })
          .catch(e => console.log(e));
      };
      const OnInstructionChanged = (event: any) =>
        chageInstructions(event.target.value);

      onMounted(() => chageInstructions(instructionsArray.value[0].title));

      const evaluateRegex = debounce((regex: string) => {
        const graphProvider = withAutomataGraph();
        graphProvider.clearGraph();
        automataProvider
          .evaluate(`regex: ${regex} \n`)
          .then(() =>
            graphProvider.showGraph(automataProvider.evaluation.value.Original)
          );
      }, 200);

      const setRegex = (event: any) => {
        regex.value = event.target.value;
        evaluateRegex(regex.value);
      };

      return {
        instructionsArray,
        OnInstructionChanged,
        regex,
        setRegex,
        TurnOnRegexMode: () => automataProvider.turnOnRegexMode(),
        TurnOffRegexMode: () => automataProvider.turnOffRegexMode(),
        RegexModeOn: computed(() => automataProvider.evaluation.value.RegexMode),
        CurrentInstructionName: computed(
          () => automataProvider.evaluation.value.CurrentInstructionName
        )
      };
    }
  });
</script>

<style>
</style>