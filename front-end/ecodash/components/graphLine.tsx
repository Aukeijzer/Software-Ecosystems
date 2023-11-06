"use client"
import displayable from '@/app/classes/displayableClassPaged'
import dynamic from 'next/dynamic'
import { CartesianGrid, XAxis, YAxis, Line, Legend, ResponsiveContainer, Tooltip} from 'recharts'
import { lineData } from '@/mockData/mockAgriculture'

const LineChart = dynamic(() => import('recharts').then(mod => mod.LineChart), {
    ssr: false,
    loading: () => <p> loading Graph...</p>
})

interface graphLineProps{
    items: lineData[],
}

const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]

//Data format//
//name: 
//topic1: 
//topic2:
//topic3:
//topic4:
//topic5:


function lineFunctionTopic(index : number, datakey: string, color: string ) : JSX.Element{
    const topics = ["DAO", "protocols", "Wallets", "DApps", "Finance"]
    const newDataKey = datakey + index.toString();
    return(
        <Line name={topics[index]} type="monotone" dataKey={newDataKey} stroke={color} />
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

export default function GraphLine(props: graphLineProps){
    return(
        <div className='grid w-full h-full'>
            <ResponsiveContainer  >
                <LineChart height={250} data={props.items} margin={{ top: 5, right: 30, left: 20, bottom: 5 }} >
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