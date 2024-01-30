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
import { getValidSubdomain } from "@/utils/subdomain";

test('should return null when no valid subdomain is found', () => {
    expect(getValidSubdomain()).toBeNull();
    expect(getValidSubdomain(null)).toBeNull();
    expect(getValidSubdomain("")).toBeNull();
    expect(getValidSubdomain("secodash")).toBeNull();
    expect(getValidSubdomain("secodash.science.uu.nl")).toBeNull();
    expect(getValidSubdomain("www.secodash.science.uu.nl")).toBeNull();
});

test('should return the valid subdomain when a valid subdomain is found', () => {
    expect(getValidSubdomain("agriculture.secodash.science.uu.nl")).toBe("agriculture");
    expect(getValidSubdomain("www.agriculture.secodash.science.uu.nl")).toBe("agriculture");
    expect(getValidSubdomain("quantum.secodash.science.uu.nl")).toBe("quantum");
    expect(getValidSubdomain("www.quantum.secodash.science.uu.nl")).toBe("quantum");
});