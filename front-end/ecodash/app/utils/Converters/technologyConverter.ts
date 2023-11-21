import technologyClass from "@/app/classes/technologyClass";
import { technologyDTO } from "../../interfaces/DTOs/technologyDTO";

export function technologyDTOConverter(technology: technologyDTO){
    return new technologyClass(technology.technology, technology.projectCount);
}

export default function listTechnologyDTOConverter(technologies: technologyDTO[]){
    var technologyClassList = [];
    for(var i = 0; i < technologies.length; i ++){
        technologyClassList.push(technologyDTOConverter(technologies[i]))
    }
    return technologyClassList;
}