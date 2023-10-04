"use client"
import { Card } from 'flowbite-react'
import React from 'react'
import InfoCardDataList from './infoCardDataList'
import InfoCardDataTable from './infoCardDataTable'
import InfoCardDataGraph from './infoCardDataGraph'


//Not needed anymore hopefully after putting in components
interface infoCardProps<T>{
    title: string,
    type: string,
    renderFunction :(item: T) => JSX.Element,
    items: T[],
    headers?: string[]
}


//Add prop that check what type of data thingy you want

export default function InfoCard<T extends {}>(props : infoCardProps<T>){
    var infoCardData;
    switch(props.type){
        case "list": {
            infoCardData = <InfoCardDataList items={props.items} renderItem={props.renderFunction}/>
            break;
        }
        case "table": {
            infoCardData = <InfoCardDataTable items={props.items} renderItem={props.renderFunction} headers={props.headers ? props.headers : []} />
            break;
        }
        case "graph": {
            
            infoCardData = <InfoCardDataGraph items={props.items} renderItem={props.renderFunction}/>
            break;
        }
    }
    
    return(
        <Card className='flex max-w mb-10 mr-10'>
            <h5 className="text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>

            {infoCardData}
            
        </Card>
    )
}