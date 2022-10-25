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
          class="px-4 py-2 shadow-md rounded-md focus:outline-none w-64"
        />
        <icon-loading
          v-if="checking"
          class="absolute animate-spin mt-2 mr-2 right-0 w-6 h-6 fill-current text-indigo-800"
        />
        <icon-check
          v-if="checkResult && showCheckResult"
          class="absolute mt-2 mr-2 right-0 w-6 h-6 fill-current text-green-500"
        />
        <icon-x
          v-if="!checkResult && showCheckResult"
          class="absolute mt-2 mr-2 right-0 w-6 h-6"
        />
      </div>
    </div>
    <div
      class="flex bg-white p-4 rounded-lg shadow-lg w-full h-screen overflow-y-auto"
      :class="{ 'skeleton-box ': IsTesting }"
    >
      <div
        :class="[[IsFinite ? 'w-1/2' : 'w-full'], { 'opacity-50': IsTesting }]"
      >
        <span class="text-indigo-800 font-semibold">Tested Words</span>
        <div class="flex flex-col items-start mt-2">
          <div
            v-for="(res, index) in wordsTestingReuslts"
            :key="index"
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
              class="bg-white text-gray-800 shadow-md px-2 rounded-md my-1 mx-2"
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
      <div v-if="IsFinite" class="w-1/2" :class="{ 'opacity-50': IsTesting }">
        <span class="text-gray-800 font-semibold">All Possible</span>
        <div class="flex flex-col items-start">
          <div
            v-for="(word, index) in allPossibleWords"
            :key="index"
            class="bg-green-600 shadow-md text-white px-2 rounded-md my-1 "
            :class="{ 'py-2': word === '' }"
          >
            {{ word }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent, computed, ref, watch } from "vue";
  import { withFiniteAutomataProvider } from "@/providers/finiteautomata/withFiniteAutomataProvider";
  import IconCheck from "../shared/IconCheck.vue";
  import IconX from "../shared/IconX.vue";
  import debounce from "@/providers/finiteautomata/utils/debounce";
  import IconLoading from "../shared/IconLoading.vue";

  export default defineComponent({
    components: { IconCheck, IconX, IconLoading },
    setup() {
      const checking = ref(false);

      const showCheckResult = ref(false);
      const checkResult = ref(false);

      const wordToCheck = ref("");

      const automataProvider = withFiniteAutomataProvider();

      const wordsTestingReuslts = computed(
        () => automataProvider.evaluation.value.Tests.WordCheckerResults
      );
      const allPossibleWords = computed(
        () => automataProvider.evaluation.value.Tests.AllPossibleWords
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
          .catch(() => (checking.value = false));
      }, 400);

      const setWord = (event: any) => {
        showCheckResult.value = false;
        wordToCheck.value = event.target.value;
        if (wordToCheck.value !== "") {
          checkWord(wordToCheck.value);
        }
      };
      const originalInstructions = computed(
        () => automataProvider.evaluation.value.OriginalInstructions
      );

      watch(originalInstructions, () => {
        if (
          automataProvider.evaluation.value.GraphVisible !== "DFA" &&
          wordToCheck.value
        ) {
          showCheckResult.value = false;
          checkWord(wordToCheck.value);
        }
      });

      return {
        showCheckResult,
        checking,
        checkResult,
        wordToCheck,
        setWord,
        wordsTestingReuslts,
        IsFinite: computed(
          () => automataProvider.evaluation.value.Tests.IsFinite.Answer
        ),
        IsTesting: computed(
          () =>
            automataProvider.evaluation.value.Testing ||
            automataProvider.evaluation.value.Processing
        ),
        allPossibleWords
      };
    }
  });
</script>

<style lang="scss">
  .skeleton-box {
    position: relative;
    overflow: hidden;
    background-color: #2c4f8908;
    // #f3f3f3 #2c4f8908
    &::after {
      position: absolute;
      top: 0;
      right: 0;
      bottom: 0;
      left: 0;
      transform: translateX(-100%);
      background-image: linear-gradient(
        90deg,
        rgba(240, 240, 240, 0) 0,
        rgba(240, 240, 240, 0.295) 20%,
        rgba(240, 240, 240, 0.7) 60%,
        rgba(240, 240, 240, 0)
      );
      animation: shimmer 1s infinite;
      content: "";
    }
  }
  @keyframes shimmer {
    100% {
      transform: translateX(100%);
    }
  }
</style>