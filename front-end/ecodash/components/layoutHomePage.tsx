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
import { useSession} from "next-auth/react";
import { ExtendedUser } from '@/app/utils/authOptions';

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

    //Set up session
    const { data: session } = useSession();
    const user = session?.user as ExtendedUser;

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
    let staticProp = true;
    var cardWrappedList : cardWrapper[] = [];
    if(data){
        if(user){
            //If user is admin, make cards draggable
            if(user.userType === "Admin" || user.userType === "RootAdmin"){
                staticProp = false;
            
          
                //Create new dashboard card
                const newDashboardButton = <div  onClick={() => Router.push('/newDashboard')}>Create </div>
                const newDashboardButtonCard = <InfoCard title="Create new Dashboard" data={newDashboardButton}/>
                const newDashboardButtonWrapped : cardWrapper = {card: newDashboardButtonCard, width: 2, height: 3, x: 2, y: 5, static:true}
                cardWrappedList.push(newDashboardButtonWrapped);
            }
        }

        //General information about SECODash
        const info = (<div className="flex flex-col"> 
                <span> Total ecosystems: {totalInformation.totalEcosystems}</span>
                <span> Total projects: {totalInformation.totalProjects} </span>
                <span> Total topics: {totalInformation.totalTopics} </span>
            </div>
        )

        const infoCard = <InfoCard title="Information about SECODash" data={info} alert="This is mock data!"/>
        const infoCardWrapped : cardWrapper = {card: infoCard, width: 6, height: 2, x: 0, y: 0, static: false}
        cardWrappedList.push(infoCardWrapped);

         //Agriculture card
        const agricultureButton = <EcosystemButton ecosystem="agriculture" projectCount={1000} topics={231} />
        const agricultureButtonCard = <InfoCard title="agriculture" data={agricultureButton} onClick={onClickEcosystem}/>
        const agricultureButtonCardWrapped : cardWrapper = { card: agricultureButtonCard, width: 2, height: 3, x: 0, y : 2, static:staticProp}
        cardWrappedList.push(agricultureButtonCardWrapped)
        
        //Quantum card
        const quantumButton = <EcosystemButton ecosystem="quantum" projectCount={1000} topics={231} />
        const quantumButtonCard = <InfoCard title="quantum" data={quantumButton}onClick={onClickEcosystem} />
        const quantumButtonCardWrapped: cardWrapper = {card: quantumButtonCard, width: 2, height: 3, x: 2, y : 2, static: staticProp}
        cardWrappedList.push(quantumButtonCardWrapped)
        
        //Artificial-intelligence card
        const aiButton = <EcosystemButton ecosystem="artificial-intelligence" projectCount={900} topics={231} />
        const aiButtonCard = <InfoCard title="artificial-intelligence" data={aiButton} onClick={onClickEcosystem}/>
        const aiButtonCardWrapped: cardWrapper = {card: aiButtonCard, width: 2, height: 3, x: 4, y : 2, static: staticProp}
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
        <div>
            <GridLayout cards={cardWrappedList} />
        </div>
    )
}