import risingClass from "@/app/classes/risingClass";
import { risingDTO } from "../../interfaces/DTOs/risingDTO";

export function risingDTOConverter(rising: risingDTO){
    return new risingClass(rising.item, rising.percentage, rising.growth);
}

export function listRisingDTOConverter(rising: risingDTO[]){
    var risingClassList : risingClass[] = [];
    for(var i = 0; i < rising.length; i++){
        
        risingClassList.push(risingDTOConverter(rising[i]));
    }
    return risingClassList;
}