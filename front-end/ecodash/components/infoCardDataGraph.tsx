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
const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];



export default function InfoCardDataGraph<T extends {}>(props: infoCardDataGraphProps<T>){
    console.log("test")
    console.log(props.items);
    return(
        <div>
            {props.renderFunction(props.items)}
        </div>
    );
}  

export function renderPieGraph(items : languageModel[]){

    return(
        <div>
            <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}}>
                <Pie data={items} nameKey="language" dataKey="percentage" cx="50%" cy="50%" label>
                    {items.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
           </PieChart>    

        </div>  
    )
}
