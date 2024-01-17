"use client"

import { useEffect} from "react"
import { useRouter } from 'next/navigation'
import { fetcherHomePage } from '@/app/utils/apiFetcher';
import useSWRMutation from 'swr/mutation'
import { cardWrapper } from "@/app/interfaces/cardWrapper";
import { totalInformation } from "@/mockData/mockEcosystems";
import InfoCard from "./infoCard";
import EcosystemButton from "./ecosystemButton";
import SpinnerComponent from "./spinner";
import GridLayout from "./gridLayout";
import SmallDataBox from "./smallDataBox";

/**
 * Renders the layout for the home page.
 * 
 * @returns The rendered layout component.
 * 
 *  The useSWRMutation hook is used to fetch data from the /ecosystems endpoint of the API. 
 *  The data, trigger, error, and isMutating variables are destructured from the hook. 
 *  The trigger function is used to manually fetch the data, data holds the fetched data, 
 *  error holds any error that occurred during fetching, and isMutating indicates whether a fetch is in progress.
 * 
 *  The useEffect hook is used to call the trigger function when the component mounts, 
 *  causing the data to be fetched from the API.
 *  
 *  If an error occurs during fetching, the component renders a paragraph with an error message.
 *  
 *  The onClickEcosystem function is a handler for click events. 
 *  It constructs a URL using the ecosystem argument and the NEXT_PUBLIC_LOCAL_ADRESS environment variable, and then navigates to that URL using the Router.push method.
 *  
 *  The cardWrappedList variable is an array that will hold cardWrapper objects.
 *  If the data variable is truthy (i.e., if data has been fetched successfully), the code creates a div with some information about "SECODash", wraps it in an InfoCard component, and then wraps that in a cardWrapper object.
 *  The cardWrapper object is then pushed into the cardWrappedList array.
 *  
 *  The cardWrapper object has properties for the card component (card), its dimensions (width and height), 
 *  its position (x and y), and whether it's static (static). 
 *  The InfoCard component likely represents a card in a card-based layout, and the cardWrapper object is used to control its layout properties.
 */

export default function LayoutHomePage(){
    //Set up router
    const Router = useRouter();

    //Set up API handler
    const { data, trigger, error, isMutating} = useSWRMutation(process.env.NEXT_PUBLIC_BACKEND_ADRESS + '/ecosystems', fetcherHomePage)

    //Trigger useEffect on load component. 
    useEffect(() => {
        trigger();
    }, [])

    //If error we display error message
    if(error){
        return(
            <p>
                Error making API call:
                {error}
            </p>
        )
    }
    
    function onClickEcosystem(ecosystem: string){
        //Get local adress and append ecosystem to it
        var url = process.env.NEXT_PUBLIC_LOCAL_ADRESS!.split("//");
        var finalUrl = url[0] + "//" + ecosystem + '.' + url[1] ;
        Router.push(finalUrl);
    }

    var cardWrappedList : cardWrapper[] = [];
    if(data){
        const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];

        //General information about SECODash
        const info = (<div className="flex flex-col"> 
                <span> Total ecosystems: {totalInformation.totalEcosystems}</span>
                <span> Total projects: {totalInformation.totalProjects} </span>
                <span> Total topics: {totalInformation.totalTopics} </span>
            </div>
        )

        const infoCard = <InfoCard title="Information about SECODash" data={info} alert="This is mock data!"/>
        const infoCardWrapped : cardWrapper = {card: infoCard, width: 6, height: 3, x: 0, y: 0, static: false}
        cardWrappedList.push(infoCardWrapped);

         //Agriculture card
        const agricultureButton = <EcosystemButton ecosystem="agriculture" projectCount={1000} topics={231} />
        const agricultureButtonCard = <InfoCard title="agriculture" data={agricultureButton} onClick={onClickEcosystem} Color={COLORS[0]}/>
        const agricultureButtonCardWrapped : cardWrapper = { card: agricultureButtonCard, width: 2, height: 3, x: 0, y : 2, static:true}
        cardWrappedList.push(agricultureButtonCardWrapped)
        
        //Quantum card
        const quantumButton = <EcosystemButton ecosystem="quantum" projectCount={1000} topics={231} />
        const quantumButtonCard = <InfoCard title="quantum" data={quantumButton}onClick={onClickEcosystem} Color={COLORS[1]} />
        const quantumButtonCardWrapped: cardWrapper = {card: quantumButtonCard, width: 2, height: 3, x: 2, y : 2, static: false}
        cardWrappedList.push(quantumButtonCardWrapped)
        
        //Artificial-intelligence card
        const aiButton = <EcosystemButton ecosystem="artificial-intelligence" projectCount={900} topics={231} />
        const aiButtonCard = <InfoCard title="artificial-intelligence" data={aiButton} onClick={onClickEcosystem} Color={COLORS[2]}/>
        const aiButtonCardWrapped: cardWrapper = {card: aiButtonCard, width: 2, height: 3, x: 4, y : 2, static: false}
        cardWrappedList.push(aiButtonCardWrapped);

    } else {
        //When still loading display spinner
        return(
            <div>
                <SpinnerComponent />
            </div>
        )
    }

    return(
        <div className="ml-44 mr-44">
            <GridLayout cards={cardWrappedList} />
        </div>
    )
}