import contributorClass from "@/app/classes/contributorClass";
import { contributorDTO } from "@/app/interfaces/DTOs/contributorDTO";

export function contributorDTOConverter(contributor: contributorDTO){
    return new contributorClass(contributor.login, contributor.contributions);
}

export default function listContributorDTOConverter(contributors: contributorDTO[]){
    var contributorClassList: contributorClass[] = [];
    for (var i = 0; i < contributors.length; i++){
        contributorClassList.push(contributorDTOConverter(contributors[i]))
    }
    return contributorClassList;
}