"use client"
import { Card } from 'flowbite-react'
import React from 'react'
import InfoCardDataList from './infoCardDataList'
import InfoCardDataTable from './infoCardDataTable'
import InfoCardDataGraph from './infoCardDataGraph'

interface infoCardProps{
    title: string,
    data: JSX.Element
}

export default function InfoCard<T extends {}>(props : infoCardProps){
    return(
        <Card className='flex max-w h-min mb-10 mr-5 p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
            <h5 className="text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>

            {props.data}
            
        </Card>
    )
}

