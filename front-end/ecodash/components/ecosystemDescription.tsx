"use client"

import { Card } from 'flowbite-react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string
}
export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div>
            <Card className='h-auto p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
                <h1 className='text-3xl'>
                    Welcome to the <b> {props.ecosystem} </b> ecosystem page
                </h1>
                <p>
                    {props.description}
                </p>
            </Card>
        </div>
    )
}