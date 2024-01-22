"use client"
import dynamic from 'next/dynamic'
import { CartesianGrid, XAxis, YAxis, Line, Legend, ResponsiveContainer, Tooltip} from 'recharts'
import { lineData } from '@/mockData/mockAgriculture'

//Need to import recharts dynamicly so that SSR can be disabled
const LineChart = dynamic(() => import('recharts').then(mod => mod.LineChart), {
    ssr: false,
    loading: () => <p> loading Graph...</p>
})

/** Interface for the props of the graphLine component
  * items: lineData[] - The data that should be displayed in the graph
  */
interface graphLineProps{
    items: lineData[],
}

const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]

/**
 * Renders a line element for a specific topic.
 * 
 * @param index - The index of the topic.
 * @param datakey - The data key for the line.
 * @param color - The color of the line.
 * @returns The JSX element representing the line.
 */
function lineFunctionTopic(index : number, datakey: string, color: string ) : JSX.Element{
    const topics = ["DAO", "protocols", "Wallets", "DApps", "Finance"]
    const newDataKey = datakey + index.toString();
    return(
        <Line key={index} name={topics[index]} type="monotone" dataKey={newDataKey} stroke={color} />
    )
}

function drawLines(amount : number) : JSX.Element{
    var dataKey = "topic"
    var lines : JSX.Element[]  = []
    for(var i = 0 ; i < amount; i++){ 
        lines.push(lineFunctionTopic(i, dataKey, COLORS[i]))
    }
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
    return(
        <div data-cy='line-graph' className='relative h-80'>
            <ResponsiveContainer width="100%" height="100%">
                <LineChart data={props.items} margin={{ top: 5, right: 30, left: 20, bottom: 5 }} >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="date"/>
                    <YAxis />
                    <Legend />
                    <Tooltip itemSorter={(item) => {
                        //-1 to sort in descending order
                        return (item.value as number) * -1;
                    }}/>
                    {drawLines(5)}               
                </LineChart>
            </ResponsiveContainer>
           
        </div>
    )
}