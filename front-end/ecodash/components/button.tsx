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