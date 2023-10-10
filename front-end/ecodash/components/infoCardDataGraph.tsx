"use client"
import { programmingLanguage } from '@/app/enums/ProgrammingLanguage';
import { languageModel } from '@/app/models/apiResponseModel';
/*
    This component renders a <T>[] list: a generic type list
    required props:
        - items <T>[] a list that contains objects that extend generic type. this should be able to convert to a dataPieChartModel
        - renderGraph () => JSX.Element. A function that returns a JSX.Element, 
    Renderfunctions (per T type):
        - render pie graph: dataPieChartModel
*/

import dynamic from 'next/dynamic'
import { Pie, Cell , ResponsiveContainer } from "recharts";

//This must be imported dynamicly so that SSR can be disabled
//TODO: Maybe add a spinner to loading time?
const PieChart = dynamic(() => import('recharts').then(mod => mod.PieChart), {
    ssr: false,
    loading: () => <p> Loading Graph...</p> 
    
})

interface infoCardDataGraphProps<T>{
    items: languageModel[],
    renderFunction: ((items: languageModel[]) => JSX.Element)
}


//Green Blue Orange Yellow
//const COLORS = [ '#bb0043', '#FFBB28', '#FF8042', '#800080','#0088FE', '#d0ebf9' ];
//const COLORS = ["#ea5545", "#f46a9b", "#ef9b20", "#edbf33", "#ede15b", "#bdcf32", "#87bc45", "#27aeef", "#b33dc6"];
//const COLORS = ["#b30000", "#7c1158", "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"];
const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]


export default function InfoCardDataGraph<T extends {}>(props: infoCardDataGraphProps<T>){
   //console.log("test")
    //console.log(props.items);
    return(
        <div>
            {props.renderFunction(props.items)}
        </div>
    );
}  

const renderCustomLabel = ({cx , cy, midAngle, innerRadius, outerRadius, percent, index, payload, value} ) =>{
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const RADIAN = Math.PI / 180;
    const x = cx + radius * Math.cos(-midAngle * RADIAN) * 1.5;
    const y = cy + radius * Math.sin(-midAngle * RADIAN) * 1.5;

    const rotation : string = "rotate-180";
    console.log("HELLO!???================================================================");
    //console.log(rotation)
    return(
        <text className="" x={x} y={y} fill="black" textAnchor={x > cx ? 'start' : 'end'} dominantBaseline={"central"}>
            {payload.language + ": " + value + '%'}
        </text>
    );
}

export function renderPieGraph(items : languageModel[]){

    return(
        <div>
            <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}}>
                <Pie data={items} nameKey="language" dataKey="percentage" cx="50%" cy="50%" label={renderCustomLabel} labelLine={false}>
                    {items.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
           </PieChart>    

        </div>  
    )
}
