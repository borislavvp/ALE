<template>
  <div class="flex flex-row h-screen overflow-hidden p-10">
    <file-viewer></file-viewer>
    <automata-graph></automata-graph>
    <words-checker />
  </div>
</template>

<script lang="ts" setup>
  import FileViewer from "./FileViewer.vue";
  import AutomataGraph from "./AutomataGraph.vue";
  import WordsChecker from "./WordsChecker.vue";
</script>
