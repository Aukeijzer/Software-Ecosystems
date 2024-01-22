"use client"
import Card from './card'
import { ReactNode } from 'react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
    editMode: boolean,
    changeDescription: (description: string) => void,
    children?: ReactNode
}

/**
 * Renders a card component displaying information about an ecosystem.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @param {string} props.description - The description of the ecosystem.
 * @param {}
 * @param {ReactNode} props.children? - optional children that will be rendered next to description
 * @returns {JSX.Element} The rendered EcosystemDescription component.
 */

export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div data-cy='ecosystem description' className='h-full w-full'>
            <Card className='h-full p-5 flex flex-col justify-between md:flex-row lg:flex-row'>
                <div className='flex flex-col'>
                    <h1 data-cy='welcome ecosystem' className='text-3xl'>
                        Welcome to the <b>{props.ecosystem}</b> ecosystem page
                    </h1>
                    {props.editMode &&
                        <form>
                            <label> Edit description: </label>
                            <input name="description" 
                            type='text' value={props.description} 
                            onChange={(e) => props.changeDescription(e.target.value)}
                             />
                        </form>}
                    {!props.editMode &&
                        <div data-cy='description ecosystem'>
                            {props.description}
                        </div>
                    }
                </div>

                <div className='flex'>
                    {props.children}
                </div>
            </Card>
        </div>
    )
}
