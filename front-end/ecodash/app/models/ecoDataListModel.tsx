import Link from "next/link"
import { apiNamedEcosystemModel } from "./apiResponseModel"

export interface Project {
    id?: number,
    name: string,
    about?: string,
    owner?: string,
    readMe?: string,
    numberOfStars?: number,
}


export const renderProject = (project: Project) => {
    return(
        <div className="flex flex-row">
            <p className="flex mr-20 max-w-xl basis-56"> name: {project.name}</p>
            <p className="flex mr-20 max-w-xl basis-56"> about: {project.about} </p>
            <p className="flex mr-20 max-w-5xl basis-56"> readme: {project.readMe? project.readMe : "" }</p>
            {/*<p className="flex mr-5"> number of stars: {project.numberOfStars} </p> */}
        </div>
    )
}


export const renderEcosystem = (ecosystem : apiNamedEcosystemModel) => {
    return(
        <div className="flex flex-row">
            <Link href={`/ecosystem/${ecosystem.name}`} > 
                <p> name: {ecosystem.name} </p>
                <p> projects: {ecosystem.projects?.length} </p>
                
                {/*<p> number of stars: {ecosystem.numberOfStars}</p> */}
            </Link>
        </div>
    )
}