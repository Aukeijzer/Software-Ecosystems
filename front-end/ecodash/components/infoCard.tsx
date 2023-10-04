"use client"
import { Card } from 'flowbite-react'
import React from 'react'
import InfoCardDataList from './infoCardDataList'
import InfoCardDataTable from './infoCardDataTable'
//Not needed anymore hopefully after putting in components
interface infoCardProps<T>{
    title: string,
    renderFunction :(item: T) => JSX.Element,
    items: T[],
    headers?: string[]
}


//Add prop that check what type of data thingy you want

export default function InfoCard<T extends {}>(props : infoCardProps<T>){
    return(
        <Card className='flex max-w-md mb-10 mr-10'>
            <h5 className="text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>
            {props.headers ? 
                <InfoCardDataTable items={props.items} renderItem={props.renderFunction} headers={props.headers ? props.headers : []} />
                :
                <InfoCardDataList items={props.items} renderItem={props.renderFunction}/>
            }   
        </Card>
    )
}