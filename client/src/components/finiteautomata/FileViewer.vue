<template>
  <div class="w-64 flex justify-between flex-col">
    <instructions-selector
      @OnInstructionLoaded="readFile"
    ></instructions-selector>
    <textarea
      id="instructions"
      name="instructions"
      rows="3"
      v-model="instructionsLoaded"
      class="resize-none shadow-lg p-2 flex-auto focus:outline-none focus:shadow-inner focus:bg-gray-200 border max-h-64 overscroll-y-auto mt-1 block w-full sm:text-sm border-gray-200 rounded-md"
      placeholder="Automata instructions"
    ></textarea>
    <div class="flex flex-col w-64 py-6 items-center bg-grey-lighter">
      <label
        class="flex items-center hover:bg-gray-100 justify-start w-full px-6 py-2 bg-white text-gray-700 rounded-lg shadow-lg tracking-wide uppercase  cursor-pointer "
      >
        <span class="flex items-center mr-4">
          <svg
            class="w-8 h-8 fill-current text-yellow-700"
            viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              fill-rule="evenodd"
              clip-rule="evenodd"
              d="M6 8C6 7.20435 6.31607 6.44129 6.87868 5.87868C7.44129 5.31607 8.20435 5 9 5H19C19.7956 5 20.5587 5.31607 21.1213 5.87868C21.6839 6.44129 22 7.20435 22 8V14C22 14.7956 21.6839 15.5587 21.1213 16.1213C20.5587 16.6839 19.7956 17 19 17H9C8.20435 17 7.44129 16.6839 6.87868 16.1213C6.31607 15.5587 6 14.7956 6 14V8ZM4 8.17C3.41493 8.37701 2.90842 8.76032 2.55024 9.26715C2.19206 9.77397 1.99982 10.3794 2 11V18C2 18.7956 2.31607 19.5587 2.87868 20.1213C3.44129 20.6839 4.20435 21 5 21H15C15.6206 21.0002 16.226 20.8079 16.7329 20.4498C17.2397 20.0916 17.623 19.5851 17.83 19H9C7.67392 19 6.40215 18.4732 5.46447 17.5355C4.52678 16.5979 4 15.3261 4 14V8.17ZM14 3C14.2652 3 14.5196 3.10536 14.7071 3.29289C14.8946 3.48043 15 3.73478 15 4V9.586L16.293 8.293C16.3852 8.19749 16.4956 8.12131 16.6176 8.0689C16.7396 8.01649 16.8708 7.9889 17.0036 7.98775C17.1364 7.9866 17.2681 8.0119 17.391 8.06218C17.5138 8.11246 17.6255 8.18671 17.7194 8.28061C17.8133 8.3745 17.8875 8.48615 17.9378 8.60905C17.9881 8.73194 18.0134 8.86362 18.0123 8.9964C18.0111 9.12918 17.9835 9.2604 17.9311 9.3824C17.8787 9.50441 17.8025 9.61475 17.707 9.707L14.707 12.707C14.5195 12.8945 14.2652 12.9998 14 12.9998C13.7348 12.9998 13.4805 12.8945 13.293 12.707L10.293 9.707C10.1975 9.61475 10.1213 9.50441 10.0689 9.3824C10.0165 9.2604 9.9889 9.12918 9.98775 8.9964C9.9866 8.86362 10.0119 8.73194 10.0622 8.60905C10.1125 8.48615 10.1867 8.3745 10.2806 8.28061C10.3745 8.18671 10.4861 8.11246 10.609 8.06218C10.7319 8.0119 10.8636 7.9866 10.9964 7.98775C11.1292 7.9889 11.2604 8.01649 11.3824 8.0689C11.5044 8.12131 11.6148 8.19749 11.707 8.293L13 9.586V4C13 3.73478 13.1054 3.48043 13.2929 3.29289C13.4804 3.10536 13.7348 3 14 3V3Z"
            />
          </svg>
        </span>
        <span class="flex leading-normal">Save file</span>
        <input
          type="file"
          class="hidden"
          accept="text/plain"
          @change="readFile"
        />
      </label>
      <label
        class="flex items-center hover:bg-gray-100 justify-start w-full mt-4 px-6 py-2 bg-white text-gray-700 rounded-lg shadow-lg tracking-wide uppercase  cursor-pointer "
      >
        <span class="flex items-center mr-4">
          <svg
            class="w-8 h-8 fill-current text-yellow-500"
            viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M14 2H6C5.46957 2 4.96086 2.21071 4.58579 2.58579C4.21071 2.96086 4 3.46957 4 4V20C4 20.5304 4.21071 21.0391 4.58579 21.4142C4.96086 21.7893 5.46957 22 6 22H18C18.5304 22 19.0391 21.7893 19.4142 21.4142C19.7893 21.0391 20 20.5304 20 20V8L14 2ZM9.998 14.768H8.895V18.042H7.978V14.768H6.893V14H9.998V14.768ZM12.723 18.042L12.358 17.311C12.208 17.029 12.112 16.819 11.999 16.585H11.986C11.903 16.818 11.801 17.028 11.674 17.311L11.339 18.042H10.294L11.465 15.997L10.336 14H11.386L11.74 14.738C11.861 14.983 11.95 15.181 12.046 15.409H12.059C12.155 15.151 12.233 14.971 12.335 14.738L12.676 14H13.719L12.58 15.973L13.778 18.042H12.723ZM17.107 14.768H16.003V18.042H15.086V14.768H14.001V14H17.106V14.768H17.107ZM14 9H13V4L18 9H14Z"
            />
          </svg>
        </span>
        <span class="flex leading-normal">Choose file</span>
        <input
          type="file"
          class="hidden"
          accept="text/plain"
          @change="readFile"
        />
      </label>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref, watch } from "@vue/composition-api";
  import { withFileManager } from "@/providers/finiteautomata/withFileManager";
  import { withFiniteAutomataProvider } from "@/providers/finiteautomata/withFiniteAutomataProvider";
  import { withAutomataGraph } from "@/providers/finiteautomata/withAutomataGraph";
  import { debounce } from "@/providers/finiteautomata/utils/debounce";
  import InstructionsSelector from "./InstructionsSelector.vue";
  export default defineComponent({
    components: { InstructionsSelector },
    setup() {
      const automataProvider = withFiniteAutomataProvider();
      const instructionsLoaded = ref("");

      const fileManager = withFileManager();

      const readFile = (event: any) => {
        console.log(event);
        fileManager
          .readFile(event.target ? event.target.files[0] : event)
          .then(res => {
            instructionsLoaded.value = res;
          });
      };

      const evaluateInstructions = debounce(
        (instructions: string, words: string) => {
          const graphProvider = withAutomataGraph();
          graphProvider.clearGraph();
          automataProvider
            .evaluate(instructions)
            .then(() =>
              graphProvider.showGraph(automataProvider.evaluation.value.Original)
            )
            .then(() => automataProvider.evaluateTests(words))
            .catch(err => console.log(err));
        },
        400
      );

      watch(instructionsLoaded, changedInstructions => {
        const parsed = changedInstructions.split("---");
        const instructions = parsed[0].replaceAll("\r", "");
        const words = parsed[1].replaceAll("\r", "");
        evaluateInstructions(instructions, words);
      });

      return {
        automataProvider,
        readFile,
        instructionsLoaded
      };
    }
  });
</script>
<style >
  @media (max-width: 481px) {
    ::-webkit-scrollbar {
      display: none;
    }
  }
  ::-webkit-scrollbar-track {
    -webkit-box-shadow: inset 0 0 6px rgba(221, 221, 221, 0.3);
    box-shadow: inset 0 0 6px rgba(209, 209, 209, 0.3);
    background-color: #ffffff;
  }

  ::-webkit-scrollbar {
    width: 9px;
    background-color: #f5f5f5;
  }

  ::-webkit-scrollbar-thumb {
    -webkit-box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
    box-shadow: inset 0 0 6px rgba(0, 0, 0, 0.3);
    background-color: rgb(168, 168, 168);
  }

  /* Handle on hover */
  ::-webkit-scrollbar-thumb:hover {
    background: rgb(150, 150, 150);
  }
</style>