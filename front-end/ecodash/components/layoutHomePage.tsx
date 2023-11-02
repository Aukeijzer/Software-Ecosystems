"use client"
import {useEffect, useState} from "react"
import { handleApi } from "./apiHandler"
import { ecosystemModel } from "@/app/models/ecosystemModel"
import GridLayout from "./gridLayout";
import { cardWrapper } from "./layoutEcosystem";
import { totalInformation } from "@/mockData/mockEcosystems";
import InfoCard from "./infoCard";

export default function LayoutHomePage(){
    const [dataLoaded, setDataLoaded] = useState<boolean>(false);
    
    useEffect(() => {
        handleApi("ecosystems").then(data => transferData(data))
    })

    function transferData(data: ecosystemModel[]){
        //No idea what this data should be?

        setDataLoaded(true);
    }

    //General information about SECODash
    const info = (<div className="flex flex-col"> 
        <span> Total ecosystems: {totalInformation.totalEcosystems}</span>
        <span> Total projects: {totalInformation.totalProjects} </span>
        <span> Total topics: {totalInformation.totalTopics} </span>
    </div>)
    const infoCard = <InfoCard title="Information about SECODash" data={info} alert="This is mock data!"/>
    const infoCardWrapped : cardWrapper = {card: infoCard, width: 6, height: 1, x: 0, y: 0}


    //Add to card list
    var cards: cardWrapper[] = [];

    cards.push(infoCardWrapped)

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