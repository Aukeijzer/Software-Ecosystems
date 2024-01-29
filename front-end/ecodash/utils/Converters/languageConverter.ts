import languageClass from "@/classes/languageClass";
import { languageDTO } from "@/interfaces/DTOs/languageDTO";

/**
 * Converts a languageDTO object to a languageClass object.
 * 
 * @param language - The languageDTO object to be converted.
 * @returns The converted languageClass object.
 */
export function languageDTOConverter(language: languageDTO){
    return new languageClass(language.language as string, language.percentage);
}

/**
 * Converts a list of languageDTO objects to a list of languageClass objects.
 * Calculates the remaining percentage and adds an "Other" language to the list.
 * 
 * @param languages - The list of languageDTO objects to be converted.
 * @returns The converted list of languageClass objects.
 */
export default function listLanguageDTOConverter(languages: languageDTO[]){
    var languageClassList : languageClass[] = [];
    var totalPercentage = 0;
    for(var i = 0; i < languages.length; i++){
        languageClassList.push(languageDTOConverter(languages[i]));
        totalPercentage += languages[i].percentage;
    }
    var remaining = 100 - totalPercentage;
    //Add other to language list
    languageClassList.push(new languageClass("Other", remaining));
    return languageClassList;
}