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
import { CartesianGrid, XAxis, YAxis, Line, Legend, ResponsiveContainer, Tooltip, LineChart} from 'recharts'
import { COLORS } from '@/interfaces/colors'
//Need to import recharts dynamicly so that SSR can be disabled
import { lineData } from '@/interfaces/lineData'

/** Interface for the props of the graphLine component
  * items: lineData[] - The data that should be displayed in the graph
  */
interface graphLineProps{
    items: lineData[],
    labels: string[]
}

//const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]

/**
 * Renders a line element for a specific topic.
 * 
 * @param index - The index of the topic.
 * @param datakey - The data key for the line.
 * @param color - The color of the line.
 * @returns The JSX element representing the line.
 */
function lineFunctionTopic(index : number, datakey: string, color: string, labels: string[] ) : JSX.Element{
    const newDataKey = datakey + index.toString();
    return(
        <Line isAnimationActive={false} key={index} name={labels[index]} type="monotone" dataKey={newDataKey} stroke={color} />
    )
}

function drawLines(amount : number, labels: string[]) : JSX.Element{
    var dataKey = "topic"
    var lines : JSX.Element[]  = []
    for(var i = 0 ; i < amount; i++){ 
        lines.push(lineFunctionTopic(i, dataKey, COLORS[i], labels))
    }
    console.log(lines)
    return(
    < >
        {lines}
    </>
    )
}


/**
 * Renders a graph component that displays data in a line chart.
 * @param {graphLineProps} props - The props for the graph component.
 * @returns {JSX.Element} The rendered graph component.
 */
export default function GraphLine(props: graphLineProps){
    const items = props.items;
    console.log("GraphLine");
    console.log(items);
    
    if (!items || props.labels.length == 0) {
        return <p>No more data to show.</p>;
    }
    return(
        
            <div data-cy='line-graph' className='h-[450px] ' >
                <ResponsiveContainer width="100%" >
                    <LineChart  width={450} height={400} data={props.items} margin={{ top: 5, right: 30, left: 20, bottom: 5 }} >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey={"date"}/>  
                        <YAxis />
                        <Legend wrapperStyle={{ position: 'relative' }}/>
                        <Tooltip itemSorter={(item) => {
                            //-1 to sort in descending order
                            return (item.value as number) * -1;
                        }}/>
                        {drawLines(props.labels.length, props.labels)}  
                                   
                    </LineChart>
                </ResponsiveContainer>
        
        </div>
      
        
    )
}