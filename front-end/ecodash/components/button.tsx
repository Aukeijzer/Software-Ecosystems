interface buttonProps{
    text: string,
    onClick: any
}
export default function Button(props: buttonProps){
    
    return(
        <button
            onClick={props.onClick}
            className="p-2 border-2 bg-gray-300 border-gray-500 text-gray-600 rounded-md"
        >
            {props.text}
        </button>
    );
}