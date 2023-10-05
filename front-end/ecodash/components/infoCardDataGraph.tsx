"use client"
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
    items: T[],
    renderGraph: (() => JSX.Element)
}

//Models for PieChart
//TODO: export models for all needed graphs
interface dataPieChartModel{
    data: dataPiePieceModel[]
}
 interface dataPiePieceModel{
    name: string,
    value: number
}
//Temporary test array for Data
//Don't have the data to fill in yet from backend
const dataTest  = [
    { name: 'Group A', value: 400 },
    { name: 'Group B', value: 300 },
    { name: 'Group C', value: 300 },
    { name: 'Group D', value: 200 },
];

const dataTestNew: dataPieChartModel = {data: dataTest}

//Green Blue Orange Yellow
const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];

export default function infoCardDataGraph<T extends {}>(props: infoCardDataGraphProps<T>){
    return(
        <div>
            {props.renderGraph()}
        </div>
    );
}  

export function renderPieGraph(){
    return(
        <div>
            <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}}>
                <Pie data={dataTest} dataKey="value" cx="50%" cy="50%">
                    {dataTest.map((entry, index) => (
                        <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                    ))}
                </Pie>
           </PieChart>    

        </div>  
    )
}
