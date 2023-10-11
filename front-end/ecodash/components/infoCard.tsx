"use client"
import { Card, Alert } from 'flowbite-react'
import React from 'react'
import InfoCardDataList from './infoCardDataList'
import InfoCardDataTable from './infoCardDataTable'
import InfoCardDataGraph from './infoCardDataGraph'
import {HiInformationCircle} from 'react-icons/hi'

interface infoCardProps{
    title: string,
    data: JSX.Element,
    alert?: string 
}

export default function InfoCard(props : infoCardProps){
    return(
        <Card className='flex w-auto h-min  p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
            <h5 className="text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>

            {props.alert && <Alert color="green" icon={HiInformationCircle} rounded className='mb-2 text-yellow-700 bg-yellow-100 border-yellow-500 dark:bg-yellow-200 dark:text-yellow-800'> <p>{props.alert}  </p></Alert>} 

            {props.data}
            
        </Card>
    )
}

