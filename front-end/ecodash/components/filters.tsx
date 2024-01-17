interface filterProps{
    technologies: string[],
    languages: string[],
    subEcosystems: string[],
    removeFilter(filter: string, filterType: string): void,
}

export default function Filters(props: filterProps){
    const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];
    return(
        <div className=" w-full ">
            <ul className="flex flex-row gap-3">
                {props.subEcosystems != null && props.subEcosystems.map((item, i) => (
   
                                <li key={i} className='flex flex-row gap-5 mb-1'>
           
                                    <button
                                        onClick={() => props.removeFilter(item, "ecosystems",)}
                                        style={{ backgroundColor: COLORS[0] }}
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
                                                style={{ backgroundColor: COLORS[2] }}
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
                                                style={{ backgroundColor: COLORS[3] }}
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