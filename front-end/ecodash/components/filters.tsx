import { colors } from "@/app/enums/filterColor"
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
        <div className=" w-full h-8 overflow-x-scroll">
            <ul className="flex flex-row gap-3">
                {props.subEcosystems != null && props.subEcosystems.map((item, i) => (
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