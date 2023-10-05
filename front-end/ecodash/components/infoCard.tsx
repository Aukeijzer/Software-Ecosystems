"use client"
import { Card } from 'flowbite-react'
import React from 'react'
import InfoCardDataList from './infoCardDataList'
import InfoCardDataTable from './infoCardDataTable'
import InfoCardDataGraph from './infoCardDataGraph'


interface infoCardProps<T>{
    title: string,
    type: string,
    
    items: T[],
    headers?: string[],
    renderFunction?:(item: T) => JSX.Element,
    renderGraph?:() => JSX.Element
}



export default function InfoCard<T extends {}>(props : infoCardProps<T>){
    //Change renderItem to renderFunction
    //Change graph to PieChart
    
    /*different cardData objects available
        - list: items : [T], renderItem(Function in cardDataList) : (T) => JSX.Element
        - table: items : [T], renderItem : (T) => JSX.Element, headers(Table headers) : string[]
        - Graph: items : [T], renderGraph : () => JSX.Element

    */
    
    var infoCardData;
    switch(props.type){
        case "list": {
            infoCardData = <InfoCardDataList items={props.items} renderItem={props.renderFunction!}/>
            break;
        }
        case "table": {
            infoCardData = <InfoCardDataTable items={props.items} renderItem={props.renderFunction!} headers={props.headers ? props.headers : []} />
            break;
        }
        case "graph": {
            infoCardData = <InfoCardDataGraph items={props.items} renderGraph={props.renderGraph!}/>
            break;
        }
    }
    
    return(
        <Card className='flex max-w h-min mb-10 mr-5 p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
            <h5 className="text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>

            {infoCardData}
            
        </Card>
    )
}

