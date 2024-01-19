"use client"
import Card from './card'
import { ReactNode } from 'react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
    children?: ReactNode
}

/**
 * Renders a card component displaying information about an ecosystem.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @param {string} props.description - The description of the ecosystem.
 * @param {ReactNode} props.children? - optional children that will be rendered next to description
 * @returns {JSX.Element} The rendered EcosystemDescription component.
 */

export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div data-cy='ecosystem description' className='h-full w-full'>
            <Card className='h-full p-5 flex flex-col justify-between md:flex-row lg:flex-row'>
                <div className='flex flex-col '>
                    <h1 data-cy='welcome ecosystem' className='text-3xl'>
                        Welcome to the <b>{props.ecosystem}</b> ecosystem page
                    </h1>
                    <p data-cy='description ecosystem'>
                        {props.description}
                    </p>
                </div>

                <div className='flex'>
                    {props.children}
                </div>
            </Card>
        </div>
    )
}
