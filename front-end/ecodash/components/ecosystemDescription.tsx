"use client"

import subEcosystem from '@/app/classes/subEcosystemClass'
/*
ecosystemDescription exports:

 - EcosystemDescription: JSX.Element containing a provided description
    - input:  
            - ecosystem : string
            - description: string
    - output:
            - JSX.Element

*/
import { Card } from 'flowbite-react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
    subEcosystems?: string[],
    removeTopic?: (topic: string) => void
}

export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div className='h-full'>
            <Card className='h-full p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
                <h1 className='text-3xl'>
                    Welcome to the <b> {props.ecosystem} </b> ecosystem page
                </h1>
                {props.subEcosystems? <div>
                    The following sub-ecosystems have been selected
                    <ul>
                        {props.subEcosystems?.map((item, i) => (<li key={i} onClick={() => props.removeTopic!(item)}>
                            * {item}
                        </li>) )}
                    </ul>
                    
                </div> : <p></p>
                }
                <p>
                    {props.description}
                </p>

            </Card>
        </div>
    )
}
