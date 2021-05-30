import { DataInterfaceEdges, DataInterfaceNodes, Edge, Network, Node } from "vis-network/peer/esm/vis-network";
import { DataSet } from "vis-data/peer/esm/vis-data";
import { reactive, Ref, ref } from "@vue/composition-api";
import { FiniteAutomataEvaluation } from "@/types/finiteautomata/FiniteAutomataEvaluation";
import { State } from "@/types/finiteautomata/State";
import { Transition } from "@/types/finiteautomata/Transition";

const finalAutomataImg = new Image().src = `${process.env.BASE_URL}FinalAutomata.svg`
const initialAutomataImg = new Image().src = `${process.env.BASE_URL}InitialAutomata.svg`
const AutomataImg = new Image().src = `${process.env.BASE_URL}Automata.svg`

const network = ref({} as Network);

const graphData = reactive({
    nodes: new DataSet() as DataInterfaceNodes,
    edges: new DataSet() as DataInterfaceEdges,
    options: {}
});
  
const selfReferencedEdges = ref<number[]>([]);
export const withAutomataGraph = () => {
  const getOptions = () => ({
    interaction: {
        // dragNodes:true,
        // dragView: true,
        hover: true,
        // hoverConnectedEdges: true,
        // selectConnectedEdges: true,
        // zoomView: true    
      },
     layout: {
        hierarchical: {
            direction: "LR",
            sortMethod: "directed",
            nodeSpacing: 200,
            treeSpacing: 200
        }
    },
      physics: {
        enabled:false,
      },
      nodes: {
        font: {
          size: 25,
          strokeWidth: 1,
          strokeColor: "#404040",
          align: "center"
        },
        shadow: {
          enabled: true,
          color: "#003300",
          x: 0
        }
      },
      edges: {
        length:500,
        color: {
          color: "#26734d",
          highlight: "#3399ff",
          hover: "#39ac73"
        },
      // smooth: { type: "cubicBezier" },
    }
  })
  const getNodes = (data: State[]) => data.map((n, index) => ({
        id: n.Id,
        label: n.Value,
        level: index,
        text:n.Value,
        shape: "image", image: n.Final ? finalAutomataImg : n.Initial ? initialAutomataImg : AutomataImg,
  }))
  
  const getEdges = (data: Transition[]) =>  data.map((e, _index) => {
        e.From.Value === e.To.Value && selfReferencedEdges.value.push(e.From.Id);
        return {
          id: e.Id,
          from: e.From.Id,
          to: e.To.Id,
          label: e.Value.Value,
          smooth: {
            enabled: true,
            roundness: _index % 2 == 0 ? (e.From.Id - e.To.Id) / 25 + 0.5 : -(e.From.Id - e.To.Id) / 25 - 0.5,
            type: 'curvedCW'
          },
          arrows: {
            to: {
              enabled: e.To.Id !== e.From.Id
            }
          },
          font: {
            size: 25,
            strokeWidth: 1,
            strokeColor: "#404040",
            align: "center"
          },
          selfReference: {
            size: e.From.Id === e.To.Id ? selfReferencedEdges.value.filter(s => s === e.From.Id).length * 15 : 15,
          }
        }
      })

  const showGraph = (data: FiniteAutomataEvaluation) => {
    selfReferencedEdges.value = []
    const container = document.getElementById("automataGraph");
    graphData.nodes = new DataSet(getNodes(data.States))
    graphData.edges = new DataSet(getEdges(data.Transitions));
    graphData.options = getOptions();
      network.value = container
        ? new Network(
          container,
          { nodes: graphData.nodes, edges: graphData.edges },
          graphData.options
        )
      : {} as Network;
   
   
    return network;
  };
  return { showGraph };
}
