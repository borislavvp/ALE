import { Network } from "vis-network/peer/esm/vis-network";
import { DataSet } from "vis-data/peer/esm/vis-data";
import { reactive } from "@vue/composition-api";
import { ExpressionEvaluation } from "@/types/expression/ExpressionEvaluation";

export default function(data: ExpressionEvaluation) {
  const graphData = reactive({
    nodes: new DataSet(
      data.Nodes.map(n => ({ id: n.ID, label: n.Value, level: n.Level }))
    ),
    edges: new DataSet(
      data.Edges.map((e, _index) => ({
        id: _index,
        from: e.Child.ID,
        to: e.Parent.ID
      }))
    ),
    options: {
      interaction: {
        hover: true
      },
      layout: {
        hierarchical: {
          direction: "UD",
          sortMethod: "directed"
        }
      },
      physics: {
        stabilization: false
      },
      nodes: {
        shape: "dot",
        size: 25,
        font: {
          size: 25,
          strokeWidth: 1,
          strokeColor: "#404040",
          align: "left"
        },
        color: {
          background: "#26734d",
          border: "#206040",
          highlight: { background: "#3399ff", border: "#1a8cff" },
          hover: { background: "#39ac73", border: "#339966" }
        },
        borderWidth: 2,
        shadow: {
          enabled: true,
          color: "#003300",
          x: 0
        }
      },
      edges: {
        color: {
          color: "#26734d",
          highlight: "#3399ff",
          hover: "#39ac73"
        }
        // arrows: "to",
      }
    }
  });

  const showGraph = () => {
    const container = document.getElementById("mynetwork");

    return container
      ? new Network(
          container,
          { nodes: graphData.nodes, edges: graphData.edges },
          graphData.options
        )
      : null;
  };
  return { showGraph };
}
