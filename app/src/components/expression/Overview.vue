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
        nandifyData
      }}</span>
    </div>
    <div class="flex justify-between ">
      <span class="text-gray-800 font-semibold text-lg mr-4">DNF:</span>
      <span class="text-green-600 font-semibold text-lg">{{
        dnfData
      }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from "vue";
import { ExpressionEvaluation } from "@/types/expression/ExpressionEvaluation";

export default defineComponent({
  props: {
    EvalutionData: {
      type: Object as () => ExpressionEvaluation,
      required: true
    }
  },
  setup(props){

    const nandifyData = computed(
      () => props.EvalutionData.Nandify.length > 500 
      ? props.EvalutionData.Nandify.substring(0,500) + "..." 
      : props.EvalutionData.Nandify
    )
    
    const dnfData = computed(
      () => props.EvalutionData.DNF.length > 500 
      ? props.EvalutionData.DNF.substring(0,500) + "..." 
      : props.EvalutionData.DNF
    )

    return {
      nandifyData,
      dnfData
    }
  }
});
</script>

<style></style>
