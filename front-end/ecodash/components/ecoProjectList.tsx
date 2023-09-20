import { project } from "@/app/models/ecoMainModel"

interface ecoDataListProps{
    projects?: project[],
}

export default function ecoDataList({projects} : ecoDataListProps){
    return(
        <div className="bg-white rounded-xl">
            <ul >
                
                {(projects?.length) && projects.map((project) => 
                    <li> - name: {project.name} author: {project.owner} number of stars: {project.numberOfStars}</li>
                )}
            </ul>
        </div>
    );
}