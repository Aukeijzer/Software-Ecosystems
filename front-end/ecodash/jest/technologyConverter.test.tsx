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
import technologyClass from "@/classes/technologyClass";
import { technologyDTO } from "@/interfaces/DTOs/technologyDTO";
import listTechnologyDTOConverter, { technologyDTOConverter } from "@/utils/Converters/technologyConverter";

const inputTechnologyDTO: technologyDTO = {
    technology: "Ethereum",
    projectCount: 31
};

const outputTechnologyClass = new technologyClass("Ethereum", 31);

test('converts a single technologyDTO to a single technologyClass', () => {
    expect(technologyDTOConverter(inputTechnologyDTO)).toStrictEqual(outputTechnologyClass);
});

const inputTechnologyDTOList: technologyDTO[] = [
    {
        technology: "Ethereum",
        projectCount: 31
    },
    {
        technology: "Blockchain",
        projectCount: 22
    }
];

const outputTechnologyClassList = [
    new technologyClass("Ethereum", 31),
    new technologyClass("Blockchain", 22)
];

test('converts a list of technologyDTO to a list of technologyClass', () => {
    expect(listTechnologyDTOConverter(inputTechnologyDTOList)).toStrictEqual(outputTechnologyClassList);
});