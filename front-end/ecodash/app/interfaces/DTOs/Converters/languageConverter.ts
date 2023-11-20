import languageClass from "@/app/classes/languageClass";
import { languageDTO } from "../languageDTO";

export function languageDTOConverter(language: languageDTO){
    return new languageClass(language.language, language.percentage);
}

export function listLanguageDTOConverter(languages: languageDTO[]){
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