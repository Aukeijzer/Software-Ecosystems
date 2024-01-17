"use client"
import { Card } from 'flowbite-react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
}

/**
 * Renders a card component displaying information about an ecosystem.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @param {string} props.description - The description of the ecosystem.
 * @returns {JSX.Element} The rendered EcosystemDescription component.
 */

export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div data-cy='ecosystem description' className='h-full'>
            <Card className='h-full p-5 '>
                <h1 data-cy='welcome ecosystem' className='text-3xl'>
                    Welcome to the <b>{props.ecosystem}</b> ecosystem page
                </h1>
             
                <p data-cy='description ecosystem'>
                    {props.description}
                </p>
            </Card>
        </div>
    )
}
