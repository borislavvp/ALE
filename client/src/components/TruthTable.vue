<template>
  <div class="relative">
    <div v-if="shouldShowButtons">
      <button
        v-if="shouldShowSimplifyButton"
        @click="Simplify"
        class="z-10 px-8 py-2 rounded-full outline-none focus:outline-none shadow-lg bg-blue-600 hover:bg-blue-600  text-white font-semibold fixed bottom-0 mb-4 -ml-7 bg-opacity-75 "
      >
        Simplify Table
      </button>
      <button
        v-else
        @click="Normalize"
        class="z-10 px-8 py-2 rounded-full outline-none focus:outline-none shadow-lg bg-yellow-600 hover:bg-yellow-600  text-white font-semibold fixed bottom-0 mb-4 -ml-7 bg-opacity-75 "
      >
        Normalize Table
      </button>
    </div>
    <table class="table-auto w-full ">
      <thead>
        <tr>
          <th
            class="py-2 bg-green-600 text-white sticky top-0 shadow-lg"
            v-for="v in variables"
            :key="v"
          >
            {{ v }}
          </th>
        </tr>
      </thead>
      <tbody class="divide-y">
        <tr v-for="row in rows" :key="row">
          <td
            class="text-center p-2 font-semibold"
            v-for="v in variables"
            :key="v + row"
          >
            {{ getValue(v, row) }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
  import { defineComponent, computed, ref } from "@vue/composition-api";
  import { TruthTable } from "@/types/TruthTable";

  export default defineComponent({
    props: {
      TruthTable: {
        type: Object as () => TruthTable,
        required: true
      },
      SimplifiedTruthTable: {
        type: Object as () => TruthTable,
        required: true
      }
    },
    setup(props) {
      const tableToShow = ref(props.TruthTable);
      const shouldShowSimplifyButton = computed(
        () => tableToShow.value === props.TruthTable
      );
      const shouldShowButtons = computed(
        () => Object.keys(tableToShow.value).length > 0
      );

      const Simplify = () => (tableToShow.value = props.SimplifiedTruthTable);
      const Normalize = () => (tableToShow.value = props.TruthTable);

      const variables = computed(() => Object.keys(tableToShow.value));
      const rows = computed(() => Object.values(tableToShow.value)[0]?.length);

      const getValue = (variable: string, row: number) => {
        return tableToShow.value[variable][row - 1];
      };
      return {
        variables,
        rows,
        getValue,
        Simplify,
        Normalize,
        shouldShowButtons,
        shouldShowSimplifyButton
      };
    }
  });
</script>

<style>
</style>