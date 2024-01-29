"use client"
var abbreviate = require('number-abbreviate');

interface ecosystemButtonProps{
    ecosystem: string,
    projectCount: number,
    topics: number,
    contributors: number,
    stars: number


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
            <ul className="p-3">
                <li data-cy='ecosystem-projects'>
                    <b>Projects</b>: {abbreviate(props.projectCount)}
                </li>
                <li data-cy='ecosystem-topics'>
                    <b>Sub-ecosystems </b>: {abbreviate(props.topics)} 
                </li>

                <li data-cy='ecosystem-contributors'>
                    <b>Contributors</b>: {abbreviate(props.contributors)}
                </li>
                <li data-cy='ecosystem-stars'>
                    <b>Number of stars</b>: {abbreviate(props.stars)}
                </li>
            </ul>
           
        </div>
    )
}