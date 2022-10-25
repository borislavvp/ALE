<template>
  <div class="bg-white relative p-20">
    <div class="flex justify-between mb-10">
      <span class="text-gray-800 font-semibold text-lg mr-4"
        >Infix Notation:</span
      >
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.InfixNotation
      }}</span>
    </div>
    <div class="flex justify-between mb-10">
      <span class="text-gray-800 font-semibold text-lg mr-4">Leafs:</span>
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.Leafs
      }}</span>
    </div>
    <div class="flex justify-between mb-10">
      <span class="text-gray-800 font-semibold text-lg mr-4">Hex:</span>
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.HexResult
      }}</span>
    </div>
    <div class="flex justify-between mb-10">
      <span class="text-gray-800 font-semibold text-lg mr-4"
        >Simplified DNF:</span
      >
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.SimplifiedDNF
      }}</span>
    </div>
    <div class="flex justify-between mb-10">
      <span class="text-gray-800 font-semibold text-lg mr-4">Nandify:</span>
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.Nandify
      }}</span>
    </div>
    <div class="flex justify-between ">
      <span class="text-gray-800 font-semibold text-lg mr-4">DNF:</span>
      <span class="text-green-600 font-semibold text-lg">{{
        EvalutionData.DNF
      }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { ExpressionEvaluation } from "@/types/expression/ExpressionEvaluation";

export default defineComponent({
  props: {
    EvalutionData: {
      type: Object as () => ExpressionEvaluation,
      required: true
    }
  }
});
</script>

<style></style>
