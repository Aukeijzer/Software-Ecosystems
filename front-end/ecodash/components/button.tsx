interface buttonProps{
    text: string,
    onClick: any
}
/**
 * @param props - text - string the text to display
 *              - onClick () => void, the onClick function for the button
 * @returns JSX button component
 */
export default function Button(props: buttonProps){
    
    return(
        <button
            onClick={props.onClick}
            className="p-2  bg-gray-300  text-gray-600 rounded-md"
        >
            {props.text}
        </button>
    );
}