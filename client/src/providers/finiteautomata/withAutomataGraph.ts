import { DataInterfaceEdges, DataInterfaceNodes, Edge, Network, Node } from "vis-network/peer/esm/vis-network";
import { DataSet } from "vis-data/peer/esm/vis-data";
import { reactive,  ref } from "@vue/composition-api";
import { FiniteAutomataEvaluation, GraphValues } from "@/types/finiteautomata/FiniteAutomataEvaluation";
import { State } from "@/types/finiteautomata/State";
import { Transition } from "@/types/finiteautomata/Transition";

import Viz from 'viz.js';
import { Module, render } from 'viz.js/full.render.js';

const GRAPH_CONTAINER_ID = "finiteAutomatagraph";
const GRAPH_ID = "graph";
  
export const withAutomataGraph = () => {
  const clearGraph = () => {
    const container = document.getElementById(GRAPH_CONTAINER_ID);
    const graph = document.getElementById(GRAPH_ID);
    if (container !== null && graph !== null) {
      graph.setAttribute("class","opacity-50")
    }
  }
  const showGraph = (data: GraphValues) => {

    let viz = new Viz({ Module, render });
    const initialNode = data.States.find(n => n.Initial);

    const nodes = data.States.map(s =>
      `"${s.Id}" [${s.Value === ''
        ? `label="${s.Id}"`
        : `label="${s.Value}"`} ${s.Final ? 'fontcolor=goldenrod3 shape=doublecircle color=goldenrod2'
                                          : s.Initial ? 'color=indigo fontcolor=indigo'
                                            : 'color=gray25 fontcolor=gray10'}]`)
    
    const edges = data.Transitions.map(t => 
      `"${t.From.Id}" -> "${t.To.Id}" [label="${t.Value.Value}" ${t.From.Id === t.To.Id ? 'color=dodgerblue2' : 'color=gray24'}]`);
    
    const graph = `digraph "Graph" {
          rankdir=LR;
          node [shape="circle" ];
          "initial" [label= "", shape=point color=indigo]
          ${nodes.join('\n  ')}
          "initial" -> "${initialNode !== undefined ? initialNode.Id : '_'}" [color=indigo]
          ${edges.join('\n  ')}
        }`;
    
    viz.renderSVGElement(graph)
      .then(result => {
        result.setAttribute("id", GRAPH_ID);
        const container = document.getElementById(GRAPH_CONTAINER_ID);
        const graph = document.getElementById(GRAPH_ID);
        container?.contains(graph) ? graph?.replaceWith(result) : container?.appendChild(result)
      })
      .catch(error => {
        viz = new Viz({ Module, render });
        console.error(error);
      });
  };
  return { showGraph,clearGraph };
}
