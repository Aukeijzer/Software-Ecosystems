"use client"
import { ExtendedUser } from '@/app/utils/authOptions';
import { Card } from 'flowbite-react'
import { useSession } from 'next-auth/react';

interface ecoSystemDescriptionProps{
    ecosystem: string,
    description: string,
    editMode: boolean,
    changeDescription: (description: string) => void,
    subEcosystems?: string[],
    technologies?: string[],
    risingTechnologies?: string[],
    risingTopics?: string[],
    languages?: string[],
    removeTopic?: (topic: string) => void
    removeLanguage?: (language: string) => void
    removeRisingTopic?: (topic: string) => void
    removeRisingTechnology?: (technology: string) => void
    removeTechnology?: (technology: string) => void
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

    //Moeten we hier opnieuw de sessie vragen of is doorgeven dat we in edit mode zitten genoeg?
    //Retreive session
    const {data: session} = useSession();
    const user = session?.user as ExtendedUser;
    const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];


    //COLORS[0] = "#f2c4d8" = subEcosystems
    //COLORS[3] = "" = technologies
    //COLORS[4] = "#f8e3a1" = risingTechnologies

    return(
        <div data-cy='ecosystem description' className='h-full'>
            <Card className='h-full  flex flex-row  pl-10 gap-10  '>
                <div className='flex flex-col '>
                    <h1 data-cy='welcome ecosystem' className='text-3xl'>
                        Welcome to the <b>{props.ecosystem}</b> ecosystem page
                    </h1>    
                {props.editMode && 
                    <form>
                        <label> edit description: </label>
                        <input name="description" type='text' value={props.description} onChange={(e) => props.changeDescription(e.target.value)} />
                    </form>}
                {!props.editMode && props.description && 
                    <p data-cy='description ecosystem h-3'>
                        {props.description}
                    </p>
                } 
                 {!props.editMode && !props.description && 
                    <p data-cy='description ecosystem h-3'>
                        <br></br>
                    </p>
                } 


                </div>   
                
                <div className='flex h-16'>
                {((props.subEcosystems != null && props.subEcosystems.length > 0) ||
                    (props.technologies != null && props.technologies.length > 0) ||
                    (props.risingTechnologies != null && props.risingTechnologies.length > 0) ||
                    (props.risingTopics != null && props.risingTopics.length > 0) ||
                    (props.languages != null && props.languages.length > 0) )&& 
                    (
                        <div>
                            The following filters have been applied:
                            <ul data-cy='subecosystems' className='mt-2 mb-2 flex flex-row gap-5'>
                                {props.subEcosystems != null && props.subEcosystems.map((item, i) => (
                                    <li key={i} className='flex flex-row gap-5 mb-1'>
                                        <button
                                            onClick={() => props.removeTopic!(item)}
                                            style={{ backgroundColor: COLORS[0] }}
                                            className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                        >
                                            <span className="mr-2 text-black">✖</span>
                                            {item}
                                        </button>
                                    </li>
                                ))}
                                {props.technologies != null && props.technologies.length > 0 && (
                                    <>
                                        {props.technologies.map((item, i) => (
                                            <li key={i} className='flex flex-row gap-5 mb-1'>
                                                <button
                                                    onClick={() => props.removeTechnology!(item)}
                                                    style={{ backgroundColor: COLORS[3] }}
                                                    className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                                >
                                                    <span className="mr-2 text-black">✖</span>
                                                    {item}
                                                </button>
                                            </li>
                                        ))}
                                    </>
                                )}
                                {props.risingTechnologies != null && props.risingTechnologies.length > 0 && (
                                    <>
                                        {props.risingTechnologies.map((item, i) => (
                                            <li key={i} className='flex flex-row gap-5 mb-1'>
                                                <button
                                                    onClick={() => props.removeRisingTechnology!(item)}
                                                    style={{ backgroundColor: COLORS[4] }}
                                                    className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                                >
                                                    <span className="mr-2 text-black">✖</span>
                                                    {item}
                                                </button>
                                            </li>
                                        ))}
                                    </>
                                )}
                                {props.risingTopics != null && props.risingTopics.length > 0 && (
                                    <>

                                        {props.risingTopics.map((item, i) => (
                                            <li key={i} className='flex flex-row gap-5 mb-1'>
                                                <button
                                                    onClick={() => props.removeRisingTopic!(item)}
                                                    style={{ backgroundColor: COLORS[5] }}
                                                    className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                                >
                                                    <span className="mr-2 text-black">✖</span>
                                                    {item}
                                                </button>
                                            </li>
                                        ))}
                                    </>
                                )}
                                {props.languages != null && props.languages.length > 0 && (
                                    <>

                                        {props.languages.map((item, i) => (
                                            <li key={i} className='flex flex-row gap-5 mb-1'>
                                                <button
                                                    onClick={() => props.removeLanguage!(item)}
                                                    style={{ backgroundColor: COLORS[0] }}
                                                    className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                                >
                                                    <span className="mr-2 text-black">✖</span>
                                                    {item}
                                                </button>
                                            </li>
                                        ))}
                                    </>
                                )}
                            </ul>
                        </div>
                )}
                </div>   
            </Card>
        </div>
    )
}
