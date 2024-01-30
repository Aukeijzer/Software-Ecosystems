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

import { colors } from "@/enums/filterColor"
/**
 * Props for the Filters component.
 */
interface filterProps{
    technologies: string[],
    languages: string[],
    subEcosystems: string[],
    removeFilter(filter: string, filterType: string): void,
}

/**
 * Renders the selected Filters in horizontal line with ability to remove them.
 * @param technologies The technologies to be displayed.
 * @param languages The languages to be displayed.
 * @param subEcosystems The subEcosystems to be displayed.
 * @param removeFilter The function to be called when a filter is removed.
 * @returns The rendered Filters component.
 */
export default function Filters(props: filterProps){
    return(
        <div className=" w-full h-10">
            <ul className="flex flex-row gap-3">
                {props.subEcosystems != null && props.subEcosystems.length > 0 && (
                    <>
                    {props.subEcosystems.map((item, i) => (
                                <li key={i} className='flex flex-row gap-5 mb-1'>
           
                                    <button
                                        onClick={() => props.removeFilter(item, "ecosystems",)}
                                        style={{ backgroundColor: colors.topic }}
                                        className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                    >
                                        <span className="mr-2 text-black">✖</span>
                                        {item}
                                    </button>
                                </li>
                            )
                        )
                    }
                    </>
                    )
                }
                {props.languages != null && props.languages.length > 0 && (
                                <>

                                    {props.languages.map((item, i) => (
                                        <li key={i} className='flex flex-row gap-5 mb-1'>
                                            <button
                                                onClick={() => props.removeFilter( item, "languages")}
                                                style={{ backgroundColor: colors.language }}
                                                className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                            >
                                                <span className="mr-2 text-black">✖</span>
                                                {item}
                                            </button>
                                        </li>
                                    ))}
                                </>
                            )
                }

                {props.technologies != null && props.technologies.length > 0 && (
                                <>
                                    {props.technologies.map((item, i) => (
                                        <li key={i} className='flex flex-row gap-5 mb-1'>
                                            <button
                                                onClick={() => props.removeFilter(item, "technologies")}
                                                style={{ backgroundColor: colors.technology }}
                                                className='font-bold px-2 py-1 rounded-md hover:text-red-500'
                                            >
                                                <span className="mr-2 text-black">✖</span>
                                                {item}
                                            </button>
                                        </li>
                                    ))}
                                </>
                            )
                }
            </ul>
        </div>
    )
}