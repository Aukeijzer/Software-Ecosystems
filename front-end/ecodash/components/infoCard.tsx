"use client"

/*
infoCard exports:
- InFoCard: JSX.Element Card containg a title, and a JSX.Element (Can be anything)
    - input:
            - title: string: title to be displayed at the top of the card
            - data: JSX.Element: three types used for now: List, Graph and Table
            - Alert: string: if alert is provided renders a small alert box with the provided string
    - output: 
             - JSX.Element
*/

import { Card, Alert } from 'flowbite-react'
import React from 'react'
import {HiInformationCircle} from 'react-icons/hi'

interface infoCardProps{
    title: string,
    data: JSX.Element,
    alert?: string,
    className?: string ,
    onClick?: any,
}

export default function InfoCard(props : infoCardProps){
    var func = function onClick(t: string){
        return;
    }
    if (props.onClick){
        func = props.onClick;
    }
    return(
        <Card onClick={() => func(props.title)}className={'flex h-full p-5 border-2 border-odinAccent bg-amber shadow-2xl resize content-evenly' + props.className}>
            <h5 className="flex text-2xl font-bold tracking-tight text-gray-900">
                {props.title}
            </h5>

            {props.alert && <Alert color="green" icon={HiInformationCircle} rounded className='mb-2 text-yellow-700 bg-yellow-100 border-yellow-500 dark:bg-yellow-200 dark:text-yellow-800'> <p>{props.alert}  </p></Alert>} 

            {props.data}
            
        </Card>
    )
}

