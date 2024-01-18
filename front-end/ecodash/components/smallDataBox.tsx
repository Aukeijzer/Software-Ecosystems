/**
 * Represents a small data box component.
 * @param {string} item - The item to display in the data box.
 * @param {number} count - The count value to display in the data box.
 * @param {number} increase - The increase value to display in the data box. This is colored green and is displayed as a percentage.
 * @param {string} [className] - Optional class name for additional styling.
 * @returns {JSX.Element} The rendered small data box component.
 */
interface smallDataBoxProps {
    item: string;
    count: number;
    increase: number;
    className?: string;
}

export default function SmallDataBox({ item, count, increase, className }: smallDataBoxProps) {
    return (
        <div className="bg-white shadow-b-sm pl-16 pr-10 pt-2 pb-2 w-full ">
            <div className={"flex items-center" + className} >
                <div className="text-2xl mr-2">{count}</div>
                <div className="text-lg text-green-500">{increase}% &#8593;</div>
            </div>
            <div className="text-xl">{item}</div>
        </div>
    );
}