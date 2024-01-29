import { projectDTO } from "@/interfaces/DTOs/projectDTO";
import projectClass from "@/classes/projectClass";

export function projectDTOConverter(project: projectDTO){
    return new projectClass(project.name, project.owner, project.numberOfStars);
}


export default function listprojectDTOConverter(projects: projectDTO[]){
    var projectClassList: projectClass[] = [];  
    for(var i = 0; i < projects.length; i++){
        projectClassList.push(projectDTOConverter(projects[i]));
    }
    return projectClassList;
}