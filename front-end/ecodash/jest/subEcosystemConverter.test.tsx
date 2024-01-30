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
import subEcosystemClass from "@/classes/subEcosystemClass";
import { subEcosystemDTO } from "@/interfaces/DTOs/ecosystemDTO";
import listSubEcosystemDTOConverter, { subEcosystemDTOConverter } from "@/utils/Converters/subEcosystemConverter";

const inputSubEcosystemDTO: subEcosystemDTO = {
    topic: "Ethereum",
    projectCount: 31
};

const outputSubEcosystemClass = new subEcosystemClass("Ethereum", 31);

test('converts a single subEcosystemDTO to a single subEcosystemClass', () => {
    expect(subEcosystemDTOConverter(inputSubEcosystemDTO)).toStrictEqual(outputSubEcosystemClass);
});

const inputSubEcosystemDTOList: subEcosystemDTO[] = [
    {
        topic: "Ethereum",
        projectCount: 31
    },
    {
        topic: "Blockchain",
        projectCount: 22
    }
];

const outputSubEcosystemClassList = [
    new subEcosystemClass("Ethereum", 31),
    new subEcosystemClass("Blockchain", 22)
];

test('converts a list of subEcosystemDTO to a list of subEcosystemClass', () => {
    expect(listSubEcosystemDTOConverter(inputSubEcosystemDTOList)).toStrictEqual(outputSubEcosystemClassList);
});