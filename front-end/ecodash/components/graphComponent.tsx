"use client"
/*
infoCardDataGraph exports:

- InfoCardDataGraph: JSX.element containing a div + the rendered graph
    - input: 
            - items <T>[] a list that contains objects that extend generic type. this should be able to convert to a dataPieChartModel
            - renderGraph (T[]) => JSX.Element. A function that returns a JSX.Element, provided a 
    - output: 
            - JSX.element
- renderPieGraphLanguage: renders a PieGraph given a list of Languages
    - input:
            - items: LanguageModel[] a list of {name: ..., data: ...} elements
    - output:
            - JSX.Element
*/

import dynamic from 'next/dynamic'
import { Pie, Legend } from "recharts";
import displayable from '@/app/classes/displayableClass';

//This must be imported dynamicly so that SSR can be disabled
//TODO: Maybe add a spinner to loading time?
const PieChart = dynamic(() => import('recharts').then(mod => mod.PieChart), {
    ssr: false,
    loading: () => <p> Loading Graph...</p> 
    
})

interface infoCardDataGraphProps<T>{
    items: displayable[],
}

//Green Blue Orange Yellow
const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]

export default function GraphComponent<T extends {}>(props: infoCardDataGraphProps<T>){
    return(
        <div>
              <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}} >
                <Pie data={props.items} nameKey="language" dataKey="percentage" cx="50%" cy="50%"  labelLine={false} label>
                    {props.items.map((entry, index) => (
                       entry.renderAsGraph(index)
                    ))}
                </Pie>
                <Legend align="left" layout="vertical" verticalAlign="middle" />
           </PieChart>    
        </div>
    );
}  
