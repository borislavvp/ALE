<template>
  <div class="relative max-h-3/4">
    <table class="table-auto w-full">
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
            :class="{
              'bg-gradient-to-t from-gray-200 to-gray-300 text-black shadow-md': DontCareAboutValue(
                getValue(v, row)
              )
            }"
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
  import { expressionProvider } from "@/providers/expression/expressionProvider";

  export default defineComponent({
    setup() {
      const { evaluation, DontCareCharacter } = expressionProvider();

      const variables = computed(() =>
        Object.keys(evaluation.value.TableToShow.value)
      );

      const rows = computed(
        () => Object.values(evaluation.value.TableToShow.value)[0]?.length
      );

      const getValue = (variable: string, row: number) => {
        return evaluation.value.TableToShow.value[variable][row - 1];
      };
      const DontCareAboutValue = (value: number) =>
        `${value}` === DontCareCharacter.value;

      return {
        evaluation,
        variables,
        rows,
        getValue,
        DontCareAboutValue
      };
    }
  });
</script>

<style>
</style>