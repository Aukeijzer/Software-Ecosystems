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

import { languageModel } from '@/app/models/languageModel';
import dynamic from 'next/dynamic'
import { Pie, Cell, Legend } from "recharts";
import displayable from '@/app/classes/displayableClassPaged';

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
              <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}}>
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

/*
renderCustomLabel: function passed to Pie Element
input = point passed by Pie element (Dont call this function yourself!)
Output = a single label for a given data point
*/

/*
const renderCustomLabel = ({cx , cy, midAngle, innerRadius, outerRadius, percent, index, payload, value} : LabelProps<ValueType, NameType> ) =>{
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const RADIAN = Math.PI / 180;
    const x = cx + radius * Math.cos(-midAngle * RADIAN) * 1.5;
    const y = cy + radius * Math.sin(-midAngle * RADIAN) * 1.5;

    const rotation : string = "rotate-180";
    //console.log(rotation)
    return(
        <text className="" x={x} y={y} fill="black" textAnchor={x > cx ? 'start' : 'end'} dominantBaseline={"central"}>
            {payload.language + ": " + value + '%'}
        </text>
    );
}
*/

export function renderPieGraph(items : languageModel[]){

    return(
        <div>
            <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}}>
                <Pie data={items} nameKey="language" dataKey="percentage" cx="50%" cy="50%"  labelLine={false} label>
                    {items.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
                <Legend align="left" layout="vertical" verticalAlign="middle" />
           </PieChart>    

        </div>  
    )
}
