/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

"use client"

import dynamic from 'next/dynamic'
import { Pie, Legend, Tooltip } from "recharts";
import displayableGraphItem from '@/classes/displayableGraphItem';

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
    onClick: (sub: string) => void;
}

/**
 * Renders a graph component that displays data in a pie chart.
 * @param {infoCardDataGraphProps<T>} props - The props for the graph component.
 * @returns {JSX.Element} The rendered graph component.
 */
export default function GraphComponent(props: infoCardDataGraphProps){
    if(props.items.length > 1){
    return(
        <div data-cy='pie-chart'>
              <PieChart width={350} height={400} margin={{top: 5, right: 5, bottom: 5, left: 5}} >
                <Pie className="cursor-pointer" data={props.items} nameKey="language" dataKey="percentage" cx="50%" cy="50%"  labelLine={false} label>
                    {props.items.map((entry, index) => (
                       entry.renderAsGraphItem(index, props.onClick)
                    ))}
                </Pie>
                <Legend align="left" layout="vertical" verticalAlign="middle" />
                <Tooltip />
           </PieChart>    
        </div>
    );
    } else {
        return(
            <div>
                No projects found
            </div>
        )
    }
}  
