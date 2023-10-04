"use client"

import dynamic from 'next/dynamic'
import { Pie, Cell , ResponsiveContainer } from "recharts";

//This must be imported dynamicly so that SSR can be disabled
//Maybe add a spinner to loading time?
const PieChart = dynamic(() => import('recharts').then(mod => mod.PieChart), {
    ssr: false,
    loading: () => <p> Loading Graph...</p> 
    
})


interface infoCardDataGraphProps<T>{
    items: T[],
    renderItem: (items: T) => JSX.Element
}


//Temporary test array for Data
//Don't have the data to fill in yet from backend
const dataTest = [
    { name: 'Group A', value: 400 },
    { name: 'Group B', value: 300 },
    { name: 'Group C', value: 300 },
    { name: 'Group D', value: 200 },
];


//Green Blue Orange Yellow
const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042'];



export default function infoCardDataGraph<T extends {}>(props: infoCardDataGraphProps<T>){
    //For now passes props.items[0] this should eventually be a valid data object
    return(
        <div>
            {props.renderItem(props.items[0])}
        </div>
    );
}  

export function renderPieGraph<T extends {}>(item : T){
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

/*

<Pie data={dataTest} dataKey="value" cx="50%" cy="50%">
                        {dataTest.map((entry, index) => (
                            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
                        ))}
                    </Pie>


                    */