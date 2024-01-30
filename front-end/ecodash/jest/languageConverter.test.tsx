import { expect } from "@jest/globals";
import languageClass from "@/classes/languageClass";
import { languageDTO } from "@/interfaces/DTOs/languageDTO";
import listLanguageDTOConverter, { languageDTOConverter } from "@/utils/Converters/languageConverter";

const inputLanguageDTO: languageDTO = {
    language: "Python",
    percentage: 50
};

const outputLanguageClass = new languageClass("Python", 50);

test('converts a single languageDTO to a single languageClass', () => {
    expect(languageDTOConverter(inputLanguageDTO)).toStrictEqual(outputLanguageClass);
});

const inputLanguageDTOList: languageDTO[] = [
    {
        language: "Python",
        percentage: 50
    },
    {
        language: "Ruby",
        percentage: 40
    }
];

const outputLanguageClassList = [
    new languageClass("Python", 50),
    new languageClass("Ruby", 40),
    new languageClass("Other", 10)
];

test('converts a list of languageDTO to a list of languageClass', () => {
    expect(listLanguageDTOConverter(inputLanguageDTOList)).toStrictEqual(outputLanguageClassList);
});