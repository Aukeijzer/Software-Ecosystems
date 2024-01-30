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

/**
 * Represents a small data box component.
 * @param {string} item - The item to display in the data box.
 * @param {number} count - The count value to display in the data box.
 * @param {number} increase - The increase value to display in the data box. This is colored green and is displayed as a percentage.
 * @param {string} [className] - Optional class name for additional styling.
 * @returns {JSX.Element} The rendered small data box component.
**/
interface smallDataBoxProps {
    item: string;
    count: number;
    increase: number;
    className?: string;
}

export default function SmallDataBox({ item, count, increase, className }: smallDataBoxProps) {
    return (
        <div className="shadow-b-sm h-full p-3 justify-center bg-white overflow-hidden ">
            <div className="flex justify-center">
                <div className="flex flex-col  ">
                    <div className="flex flex-row gap-2 ">
                        <div className="flex text-2xl text-center">{count}</div>
                    </div>
                    <div className="text-xl">{item}</div>
                </div>
            </div>
        </div>
    );
}
