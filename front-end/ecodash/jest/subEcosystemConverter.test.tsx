import { expect } from "@jest/globals";
import subEcosystemClass from "../app/classes/subEcosystemClass";
import { subEcosystemDTO } from "../app/interfaces/DTOs/ecosystemDTO";
import listSubEcosystemDTOConverter, { subEcosystemDTOConverter } from "../app/utils/Converters/subEcosystemConverter";

const inputSubEcosystemDTO: subEcosystemDTO = Object.assign({
    topic: "Ethereum",
    projectCount: 31
});

const outputSubEcosystemClass = new subEcosystemClass("Ethereum", 31);

test('converts a single subEcosystemDTO to a single subEcosystemClass', () => {
    expect(subEcosystemDTOConverter(inputSubEcosystemDTO)).toStrictEqual(outputSubEcosystemClass);
});

const inputSubEcosystemDTOList: subEcosystemDTO[] = [
    Object.assign({
        topic: "Ethereum",
        projectCount: 31
    }),
    Object.assign({
        topic: "Blockchain",
        projectCount: 22
    })
];

const outputSubEcosystemClassList = [
    new subEcosystemClass("Ethereum", 31),
    new subEcosystemClass("Blockchain", 22)
];

test('converts a list of subEcosystemDTO to a list of subEcosystemClass', () => {
    expect(listSubEcosystemDTOConverter(inputSubEcosystemDTOList)).toStrictEqual(outputSubEcosystemClassList);
});