/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

import { expect } from "@jest/globals";
import risingClass from "@/classes/risingClass";
import { risingDTO } from "@/interfaces/DTOs/risingDTO";
import listRisingDTOConverter, { risingDTOConverter } from "@/utils/Converters/risingConverter";

const inputRisingDTO: risingDTO = {
    item: "Ethereum",
    percentage: 31,
    growth: 5
};

const outputRisingClass = new risingClass("Ethereum", 31, 5);

test('converts a single risingDTO to a single risingClass', () => {
    expect(risingDTOConverter(inputRisingDTO)).toStrictEqual(outputRisingClass);
});

const inputRisingDTOList: risingDTO[] = [
    {
        item: "Ethereum",
        percentage: 31,
        growth: 5
    },
    {
        item: "Blockchain",
        percentage: 22,
        growth: 3
    }
];

const outputRisingClassList = [
    new risingClass("Ethereum", 31, 5),
    new risingClass("Blockchain", 22, 3)
];

test('converts a list of risingDTO to a list of risingClass', () => {
    expect(listRisingDTOConverter(inputRisingDTOList)).toStrictEqual(outputRisingClassList);
});