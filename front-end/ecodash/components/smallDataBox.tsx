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
                        <div className="flex text-lg text-center text-green-500">{increase}% &#8593;</div>
                    </div>
                    <div className="text-xl">{item}</div>
                </div>
            </div>
            
          
        </div>
    );
}