import technologyClass from "@/app/classes/technologyClass";
import { technologyDTO } from "../../interfaces/DTOs/technologyDTO";

/**
 * Converts a technologyDTO object to a technologyClass object.
 * 
 * @param technology - The technologyDTO object to be converted.
 * @returns The converted technologyClass object.
 */
export function technologyDTOConverter(technology: technologyDTO){
    return new technologyClass(technology.technology, technology.projectCount);
}

/**
 * Converts an array of technologyDTO objects to an array of technologyClass objects.
 * 
 * @param technologies - The array of technologyDTO objects to be converted.
 * @returns The converted array of technologyClass objects.
 */
export default function listTechnologyDTOConverter(technologies: technologyDTO[]){
    var technologyClassList = [];
    for(var i = 0; i < technologies.length; i ++){
        technologyClassList.push(technologyDTOConverter(technologies[i]))
    }
    return technologyClassList;
}