"use client"
import {useEffect, useState} from "react"
import { handleApi } from "./apiHandler"
import { ecosystemModel } from "@/app/interfaces/DTOs/ecosystemDTO"
import GridLayout from "./gridLayout";
import { cardWrapper } from "@/app/interfaces/cardWrapper";
import { totalInformation } from "@/mockData/mockEcosystems";
import EcosystemButton from "./ecosystemButton";
import InfoCard from "./infoCard";
import { useRouter } from "next/navigation";

export default function LayoutHomePage(){
    const router = useRouter();
    const [dataLoaded, setDataLoaded] = useState<boolean>(false);
    
    useEffect(() => {
        handleApi("ecosystems").then(data => transferData(data))
    }, [])

    function transferData(data: ecosystemModel[]){
        //This data is still not finished. Not clear yet what needs to be displayed on homePage
        setDataLoaded(true);
    }

    
    function onClickEcosystem(ecosystem: string){
        //Local host is now still fixed string
        router.push("http://" + ecosystem + ".localhost:3000");
    }


    //General information about SECODash
    const info = (<div className="flex flex-col"> 
        <span> Total ecosystems: {totalInformation.totalEcosystems}</span>
        <span> Total projects: {totalInformation.totalProjects} </span>
        <span> Total topics: {totalInformation.totalTopics} </span>
    </div>)

    const infoCard = <InfoCard title="Information about SECODash" data={info} alert="This is mock data!"/>
    const infoCardWrapped : cardWrapper = {card: infoCard, width: 6, height: 2, x: 0, y: 0, static: false}


    //Agriculture card
    const agricultureButton = <EcosystemButton ecosystem="agriculture" projectCount={1000} topics={231} />
    const agricultureButtonCard = <InfoCard title="agriculture" data={agricultureButton} onClick={onClickEcosystem}/>
    const agricultureButtonCardWrapped : cardWrapper = { card: agricultureButtonCard, width: 2, height: 3, x: 0, y : 2, static:true}
    //Quantum card
    const quantumButton = <EcosystemButton ecosystem="quantum" projectCount={1000} topics={231} />
    const quantumButtonCard = <InfoCard title="quantum" data={quantumButton}onClick={onClickEcosystem} />
    const quantumButtonCardWrapped: cardWrapper = {card: quantumButtonCard, width: 2, height: 3, x: 2, y : 2, static: false}

    //Artificial-intelligence card
    const aiButton = <EcosystemButton ecosystem="artificial-intelligence" projectCount={900} topics={231} />
    const aiButtonCard = <InfoCard title="artificial-intelligence" data={aiButton} onClick={onClickEcosystem}/>
    const aiButtonCardWrapped: cardWrapper = {card: aiButtonCard, width: 2, height: 3, x: 4, y : 2, static: false}

    //Add to card list
    var cards: cardWrapper[] = [];

    cards.push(infoCardWrapped)
    cards.push(agricultureButtonCardWrapped);
    cards.push(quantumButtonCardWrapped);
    cards.push(aiButtonCardWrapped);

    if(dataLoaded){
        return(
            <GridLayout cards={cards} />
        )
    } else {
        return(
            <div>
                loading...
            </div>
        )
    }
}