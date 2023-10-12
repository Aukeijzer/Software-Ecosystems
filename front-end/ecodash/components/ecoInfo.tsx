//This component will display the title and description of an ecosystem passed as props through ecoMain

interface ecoInfoProps{
    title: string,
    description: string,
}

export default function ecoInfo({title, description} : ecoInfoProps){
    return(
        <div className="flex flex-col text-center rounded-3xl ml-5 mt-5 mr-5  p-5 bg-gray-500">
            <h1 className="flex text-3xl text-center"> {title} </h1>
            <p className="flex "> {description} </p>
        </div>      
    );
}