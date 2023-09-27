interface ecoItemProps{
    title: string,
    description: string,
    ecoData: JSX.Element
}

export default function ecoItem({title, description, ecoData} : ecoItemProps){
    return(
        <div className="bg-gray-400 ml-5 mt-3 p-5 mr-5 rounded-3xl">
            <h1 className="text-3xl"> {title} </h1>
            {ecoData}
            <p> {description} </p>
        </div>
    )
}