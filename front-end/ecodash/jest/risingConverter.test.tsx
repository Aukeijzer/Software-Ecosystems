import { expect } from "@jest/globals";
import risingClass from "../app/classes/risingClass";
import { risingDTO } from "../app/interfaces/DTOs/risingDTO";
import listRisingDTOConverter, { risingDTOConverter } from "../app/utils/Converters/risingConverter";

const inputRisingDTO: risingDTO = Object.assign({
    item: "Ethereum",
    percentage: 31,
    growth: 5
});

const outputRisingClass = new risingClass("Ethereum", 31, 5);

test('converts a single risingDTO to a single risingClass', () => {
    expect(risingDTOConverter(inputRisingDTO)).toStrictEqual(outputRisingClass);
});

const inputRisingDTOList: risingDTO[] = [
    Object.assign({
        item: "Ethereum",
        percentage: 31,
        growth: 5
    }),
    Object.assign({
        item: "Blockchain",
        percentage: 22,
        growth: 3
    })
];

const outputRisingClassList = [
    new risingClass("Ethereum", 31, 5),
    new risingClass("Blockchain", 22, 3)
];

test('converts a list of risingDTO to a list of risingClass', () => {
    expect(listRisingDTOConverter(inputRisingDTOList)).toStrictEqual(outputRisingClassList);
});