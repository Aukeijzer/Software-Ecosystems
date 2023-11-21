import subEcosystemClass from "@/app/classes/subEcosystemClass";
import { subEcosystemDTO } from "../../interfaces/DTOs/ecosystemDTO";

export function subEcosystemDTOConverter(subEcosystem: subEcosystemDTO){
    return new subEcosystemClass(subEcosystem.topic, subEcosystem.projectCount);
}

export default function listSubEcosystemDTOConverter(subEcosystems: subEcosystemDTO[]){
    var subEcosystemClassList : subEcosystemClass[] = [];
    for(var i = 0; i < subEcosystems.length; i++){
        subEcosystemClassList.push(subEcosystemDTOConverter(subEcosystems[i]))
    }
    return subEcosystemClassList;
}

