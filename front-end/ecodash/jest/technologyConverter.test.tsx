import { expect } from "@jest/globals";
import technologyClass from "../app/classes/technologyClass";
import { technologyDTO } from "../app/interfaces/DTOs/technologyDTO";
import listTechnologyDTOConverter, { technologyDTOConverter } from "../app/utils/Converters/technologyConverter";

const inputTechnologyDTO: technologyDTO = Object.assign({
    technology: "Ethereum",
    projectCount: 31
});

const outputTechnologyClass = new technologyClass("Ethereum", 31);

test('converts a single technologyDTO to a single technologyClass', () => {
    expect(technologyDTOConverter(inputTechnologyDTO)).toStrictEqual(outputTechnologyClass);
});

const inputTechnologyDTOList: technologyDTO[] = [
    Object.assign({
        technology: "Ethereum",
        projectCount: 31
    }),
    Object.assign({
        technology: "Blockchain",
        projectCount: 22
    })
];

const outputTechnologyClassList = [
    new technologyClass("Ethereum", 31),
    new technologyClass("Blockchain", 22)
];

test('converts a list of technologyDTO to a list of technologyClass', () => {
    expect(listTechnologyDTOConverter(inputTechnologyDTOList)).toStrictEqual(outputTechnologyClassList);
});