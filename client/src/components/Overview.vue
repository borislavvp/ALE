<template>
  <div class="flex flex-col">
    <div>
      <span>Infix Notation</span>
      <span>{{ EvalutionData.InfixNotation }}</span>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from "@vue/composition-api";
  import { ExpressionEvaluation } from "@/types/ExpressionEvaluation";

  export default defineComponent({
    props: {
      EvalutionData: {
        type: Object as () => ExpressionEvaluation,
        required: true
      }
    }
  });
</script>

<style>
</style>