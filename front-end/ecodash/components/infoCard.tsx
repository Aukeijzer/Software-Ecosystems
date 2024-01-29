"use client"

/**
 * Represents a card containing a title and a JSX.Element.
 * @component
 * @example
 * // Usage:
 * <InfoCard
 *    title="Card Title"
 *    data={<List items={data} />}
 *    alert="Alert message"
 * />
 * @param {string} title - The title to be displayed at the top of the card.
 * @param {JSX.Element} data - The JSX.Element to be displayed in the card.
 * @param {string} [alert] - If provided, renders a small alert box with the provided string.
 * @param {string} [className] - Additional CSS class name for the card.
 * @param {Function} [onClick] - The function to be called when the card is clicked.
 * @param {string} [Color] - The color of the top of the card.
 * @returns {JSX.Element} The rendered InfoCard component.
 */
import { Alert } from 'flowbite-react'
import Card from './card'
import React, { ReactNode } from 'react'
import { HiInformationCircle } from 'react-icons/hi'

interface InfoCardProps {
    title: string,
    data: JSX.Element,
    alert?: string,
    className?: string,
    onClick?: any,
    Color?: string
    remove?: boolean,
    onRemove?: any,
    ecoystem?: string
    children?: ReactNode
}

export default function InfoCard(props: InfoCardProps) {
    var func = function onClick(t: string) {
        return;
    }
    if (props.onClick) {
        func = props.onClick;
    }
    return (
        <Card onClick={() => func(props.title)} className={'relative w-full h-full p-2 justify-normal' + props.className}>
            <div className="absolute top-0 left-0 w-full h-2 bg-skew" style={{ backgroundColor: props.Color }}> </div>
            
            {props.remove && 
            <div className="absolute top-0 right-0 mt-2 mr-1">
                <button className="hover:bg-red-500  z-10  p-1 rounded-sm" onClick={(e) => props.onRemove(e, props.ecoystem)}> ✖ </button>
            </div>
            }

            <h5 className="flex text-2xl justify-center w-full m-3 font-medium text-gray-900 tracking-tight text-gray-900">
                {props.title}   
            </h5>

            {props.children? props.children : <></>}

            {props.alert && <Alert color="green" icon={HiInformationCircle} rounded className='mb-2 text-yellow-700 bg-yellow-100 border-yellow-500 dark:bg-yellow-200 dark:text-yellow-800'> <p>{props.alert}  </p></Alert>}
            <div className='mt-3'>
                {props.data}
            </div>
        </Card>
    )
}
