import { expect } from "@jest/globals";
import { getValidSubdomain } from "../app/utils/subdomain";

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