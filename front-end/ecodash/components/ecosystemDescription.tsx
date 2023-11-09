"use client"

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
                    <ul className='flex flex-row gap-5'>
                        {props.subEcosystems?.map((item, i) => (
                        <li key={i} className='flex flex-row gap-5 mb-1'>
                            
                             

                            <button onClick={() => props.removeTopic!(item)} className='bg-gray-300 rounded-md pl-1 pr-1'> X {item} </button>
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
