<template>
  <div
    v-if="SimplifiedTableVisible"
    @click="ShowOriginalTable"
    class="cursor-pointer text-white hover:bg-blue-800 bg-blue-900  rounded-md font-semibold w-full flex items-center justify-start px-6 py-4 mb-2"
  >
    <original-table-icon />
    <span>SHOW ORIGINAL TABLE</span>
  </div>
  <div
    v-else
    @click="ShowSimplifiedTable"
    class="cursor-pointer text-white hover:bg-blue-600 bg-blue-500 rounded-md font-semibold w-full flex items-center justify-start px-6 py-4 mb-2"
  >
    <simplify-table-icon />
    <span>SIMPLIFY TABLE</span>
  </div>
</template>

<script lang="ts">
  import { defineComponent, computed } from "@vue/composition-api";
  import SimplifyTableIcon from "./SimplifyTableIcon.vue";
  import OriginalTableIcon from "./OriginalTableIcon.vue";
  import { expressionProvider } from "@/providers/expression/expressionProvider";
  export default defineComponent({
    components: { SimplifyTableIcon, OriginalTableIcon },
    setup() {
      const {
        ShowSimplifiedTable,
        ShowOriginalTable,
        evaluation
      } = expressionProvider();
      const SimplifiedTableVisible = computed(
        () => evaluation.value.TableToShow.type === "simplified"
      );
      return { ShowSimplifiedTable, ShowOriginalTable, SimplifiedTableVisible };
    }
  });
</script>

<style>
</style>