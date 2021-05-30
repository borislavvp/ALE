<template>
  <div class="flex flex-row h-screen overflow-hidden p-8">
    <file-viewer></file-viewer>
    <automata-graph></automata-graph>
  </div>
</template>

<script>
  import FileViewer from "./FileViewer.vue";
  import AutomataGraph from "./AutomataGraph.vue";
  export default {
    components: {
      FileViewer,
      AutomataGraph
    }
  };
</script>
