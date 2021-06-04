<template>
  <div class="w-64 flex justify-start flex-col">
    <div class="pb-6">
      <label class="tex-sm text-gray-800 font-semibold">Test a word</label>
      <div class="flex relative">
        <input
          :value="wordToCheck"
          @input="setWord"
          type="text"
          placeholder="Word"
          class="px-4 py-2 shadow-md rounded-md focus:outline-none w-full"
        />
        <icon-check
          class="absolute right-0 w-6 h-6 fill-current text-green-500"
        />
        <icon-x class="absolute right-0 w-6 h-6" />
      </div>
    </div>
    <div
      class="flex bg-white p-4 rounded-lg shadow-lg w-full h-screen overflow-y-auto"
    >
      <div :class="[IsFinite ? 'w-1/2' : 'w-full']">
        <span class="text-yellow-800 font-semibold">Tested Words</span>
        <div class="flex flex-col items-start mt-2">
          <div
            v-for="res in wordsTestingReuslts"
            :key="res.Word"
            class="flex items-center"
          >
            <span class="relative h-6 w-6">
              <icon-check
                v-if="res.Test.Answer || res.Test.Answer !== res.Test.TestGuess"
                class="absolute z-10 h-6 w-6 text-green-600 fill-current"
                :class="{
                  'z-0 opacity-50':
                    res.Test.Answer !== res.Test.TestGuess && !res.Test.Answer
                }"
              />
              <icon-x
                v-if="
                  !res.Test.Answer || res.Test.Answer !== res.Test.TestGuess
                "
                class="absolute z-10 h-6 w-6 text-red-600 fill-current"
                :class="{
                  'z-0 opacity-25':
                    res.Test.Answer !== res.Test.TestGuess && res.Test.Answer
                }"
              />
            </span>
            <div
              class="bg-white shadow-md px-2 rounded-md my-1 mx-2"
              :class="[
                { 'bg-red-300': res.Test.Answer !== res.Test.TestGuess },
                { 'py-2': res.Word === '' }
              ]"
            >
              {{ res.Word }}
            </div>
          </div>
        </div>
      </div>
      <div v-if="IsFinite" class="w-1/2">
        <span class="text-gray-800 font-semibold">All Possible</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent, computed, ref } from "@vue/composition-api";
  import { withFiniteAutomataProvider } from "@/providers/finiteautomata/withFiniteAutomataProvider";
  import IconCheck from "../shared/IconCheck.vue";
  import IconX from "../shared/IconX.vue";
  import debounce from "@/providers/finiteautomata/utils/debounce";

  export default defineComponent({
    components: { IconCheck, IconX },
    setup() {
      const checking = ref(false);

      const showCheckResult = ref(false);
      const checkResult = ref(false);

      const wordToCheck = ref("");

      const automataProvider = withFiniteAutomataProvider();
      const wordsTestingReuslts = computed(
        () => automataProvider.evaluation.value.Tests.WordCheckerResults
      );
      const checkWord = debounce((word: string) => {
        checking.value = true;
        automataProvider
          .checkWord(word)
          .then(res => {
            checkResult.value = res;
            showCheckResult.value = true;
            checking.value = false;
          })
          .catch(err => (checking.value = false));
      }, 400);
      const setWord = (event: any) => {
        showCheckResult.value = false;
        wordToCheck.value = event.target.value;
        if (wordToCheck.value !== "") {
          checkWord(wordToCheck.value);
        }
      };
      return {
        showCheckResult,
        checking,
        checkResult,
        wordToCheck,
        setWord,
        wordsTestingReuslts,
        IsFinite: computed(
          () => automataProvider.evaluation.value.Tests.IsFinite.Answer
        )
      };
    }
  });
</script>

<style>
</style>