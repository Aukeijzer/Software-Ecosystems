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

export default function LayoutHomePage(){
    //Set up router?
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
        //Local host is now still fixed string
        var url = process.env.NEXT_PUBLIC_LOCAL_ADRESS!.split("//");
        var finalUrl = url[0] + "//" + ecosystem + '.' + url[1] ;
        console.log(finalUrl);
        Router.push(finalUrl);
    }

    var cardWrappedList : cardWrapper[] = [];
    if(data){

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
        const agricultureButtonCardWrapped : cardWrapper = { card: agricultureButtonCard, width: 2, height: 3, x: 0, y : 2, static:true}
        cardWrappedList.push(agricultureButtonCardWrapped)
        
        //Quantum card
        const quantumButton = <EcosystemButton ecosystem="quantum" projectCount={1000} topics={231} />
        const quantumButtonCard = <InfoCard title="quantum" data={quantumButton}onClick={onClickEcosystem} />
        const quantumButtonCardWrapped: cardWrapper = {card: quantumButtonCard, width: 2, height: 3, x: 2, y : 2, static: false}
        cardWrappedList.push(quantumButtonCardWrapped)
        
        //Artificial-intelligence card
        const aiButton = <EcosystemButton ecosystem="artificial-intelligence" projectCount={900} topics={231} />
        const aiButtonCard = <InfoCard title="artificial-intelligence" data={aiButton} onClick={onClickEcosystem}/>
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
        <div>
            <GridLayout cards={cardWrappedList} />
        </div>
    )
}