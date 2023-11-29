"use client"

import dynamic from 'next/dynamic'
import { Pie, Legend } from "recharts";
import displayableGraphItem from '@/app/classes/displayableGraphItem';

//This must be imported dynamicly so that SSR can be disabled
//TODO: Maybe add a spinner to loading time?
const PieChart = dynamic(() => import('recharts').then(mod => mod.PieChart), {
    ssr: false,
    loading: () => <p> Loading Graph...</p> 
    
})

/**
 * Props for the infoCardDataGraph component.
 */
interface infoCardDataGraphProps{
    items: displayableGraphItem[],
}

//Green Blue Orange Yellow
const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]

/**
 * Renders a graph component that displays data in a pie chart.
 * @param {infoCardDataGraphProps<T>} props - The props for the graph component.
 * @returns {JSX.Element} The rendered graph component.
 */
export default function GraphComponent(props: infoCardDataGraphProps){
    return(
        <div>
              <PieChart width={400} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}} >
                <Pie data={props.items} nameKey="language" dataKey="percentage" cx="50%" cy="50%"  labelLine={false} label>
                    {props.items.map((entry, index) => (
                       entry.renderAsGraphItem(index)
                    ))}
                </Pie>
                <Legend align="left" layout="vertical" verticalAlign="middle" />
           </PieChart>    
        </div>
    );
}  
