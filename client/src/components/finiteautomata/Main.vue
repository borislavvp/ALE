<template>
  <div class="flex flex-row h-screen overflow-hidden p-10">
    <file-viewer></file-viewer>
    <automata-graph></automata-graph>
    <words-checker />
  </div>
</template>

<script>
  import FileViewer from "./FileViewer.vue";
  import AutomataGraph from "./AutomataGraph.vue";
  import WordsChecker from "./WordsChecker.vue";
  export default {
    components: {
      FileViewer,
      AutomataGraph,
      WordsChecker
    }
  };
</script>
