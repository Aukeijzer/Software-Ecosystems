import subEcosystemClass from "@/classes/subEcosystemClass";
import { subEcosystemDTO } from "@/interfaces/DTOs/ecosystemDTO";

/**
 * Converts a subEcosystemDTO object to a subEcosystemClass object.
 * 
 * @param subEcosystem - The subEcosystemDTO object to be converted.
 * @returns The converted subEcosystemClass object.
 */
export function subEcosystemDTOConverter(subEcosystem: subEcosystemDTO){
    return new subEcosystemClass(subEcosystem.topic, subEcosystem.projectCount);
}

/**
 * Converts an array of subEcosystemDTO objects to an array of subEcosystemClass objects.
 * 
 * @param subEcosystems - The array of subEcosystemDTO objects to be converted.
 * @returns The converted array of subEcosystemClass objects.
 */
export default function listSubEcosystemDTOConverter(subEcosystems: subEcosystemDTO[]){
    var subEcosystemClassList : subEcosystemClass[] = [];
    for(var i = 0; i < subEcosystems.length; i++){
        subEcosystemClassList.push(subEcosystemDTOConverter(subEcosystems[i]))
    }
    return subEcosystemClassList;
}
