"use client"
import { Card } from 'flowbite-react'

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
    subEcosystems?: string[],
    removeTopic?: (topic: string) => void
}

/**
 * Renders a card component displaying information about an ecosystem.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @param {string} props.description - The description of the ecosystem.
 * @param {string[]} [props.subEcosystems] - The list of sub-ecosystems.
 * @param {(topic: string) => void} [props.removeTopic] - The function to remove a topic.
 * @returns {JSX.Element} The rendered EcosystemDescription component.
 */

export default function EcosystemDescription(props: ecoSystemDescriptionProps){
    return(
        <div data-cy='ecosystem description' className='h-full'>
            <Card className='h-full p-5 border-2 border-odinAccent bg-amber shadow-2xl '>
                <h1 data-cy='welcome ecosystem' className='text-3xl'>
                    Welcome to the <b>{props.ecosystem}</b> ecosystem page
                </h1>
                {(props.subEcosystems != null && props.subEcosystems.length > 0) && <div>
                    The following sub-ecosystems have been selected:
                    <ul data-cy='subecosystems'className='flex flex-row gap-5'>
                        {props.subEcosystems.map((item, i) => (
                        <li key={i} className='flex flex-row gap-5 mb-1'>
                            <button onClick={() => props.removeTopic!(item)} className='bg-gray-300 rounded-md pl-1 pr-1'> X {item} 
                            </button>
                        </li>) )}
                    </ul>
                </div>
                }
                <p data-cy='description ecosystem'>
                    {props.description}
                </p>
            </Card>
        </div>
    )
}
