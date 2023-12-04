import risingClass from "../../classes/risingClass";
import { risingDTO } from "../../interfaces/DTOs/risingDTO";

/**
 * Converts a risingDTO object to a risingClass object.
 * 
 * @param rising - The risingDTO object to be converted.
 * @returns The converted risingClass object.
 */
export function risingDTOConverter(rising: risingDTO){
    return new risingClass(rising.item, rising.percentage, rising.growth);
}

/**
 * Converts an array of risingDTO objects to an array of risingClass objects.
 * 
 * @param rising - The array of risingDTO objects to be converted.
 * @returns The converted array of risingClass objects.
 */
export default function listRisingDTOConverter(rising: risingDTO[]){
    var risingClassList : risingClass[] = [];
    for(var i = 0; i < rising.length; i++){
        risingClassList.push(risingDTOConverter(rising[i]));
    }
    return risingClassList;
}