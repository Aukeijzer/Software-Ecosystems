"use client"

interface ecosystemButtonProps{
    ecosystem: string,
    projectCount: number,
    topics: number
}

/**
 * Represents an Ecosystem Button component. When clicked go to corresponding ecosystem
 * @param {object} props - The component props.
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @param {number} props.projectCount - The number of projects in the ecosystem.
 * @param {number} props.topics - The number of topics in the ecosystem.
 * @returns {JSX.Element} The rendered Ecosystem Button component.
 */

export default function EcosystemButton(props: ecosystemButtonProps){
    return(
        <div>
            <ul>
                <li data-cy='ecosystem-projects'>
                    <b>projects</b>: {props.projectCount}
                </li>
                <li data-cy='ecosystem-topics'>
                    <b>topics</b>: {props.topics} 
                </li>
            </ul>
        </div>
    )
}