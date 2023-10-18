import { ecosystemModel } from "@/app/models/ecosystemModel";
import { handleApi } from "./apiHandler";
import EcosystemInformationData from "./ecosystemInformationData";
import EcosystemDescription from "./ecosystemDescription";
import InfoCard from "./infoCard";
import InfoCardDataTotal from "./infoCardDataTotal";
import ListComponent, { renderOrganization } from "./listComponent";

//Mock data
import { totalInformation, topOrganizations, ogranization } from "@/mockData/mockEcosystems";
import { cardWrapper } from "./layoutEcosytem";
import { InfoCardGridLayout } from "./infoCardGridLayout";

export default async function LayoutHomePage(){
    
    const result : ecosystemModel[] = await handleApi(`ecosystems`);
    console.log(result);

    ///General info about all ecosystems
    const info = (<div className="flex flex-col"> 
                    <span> Total ecosystems: {totalInformation.totalEcosystems}</span>
                    <span> Total projects: {totalInformation.totalProjects} </span>
                    <span> Total topics: {totalInformation.totalTopics} </span>
                </div>)
    const infoCard = <InfoCard title="Information about SECODash" data={info} alert="This is mock data!"/>
    const infoCardWrapped : cardWrapper = {card: infoCard, width: 6, height: 1, x: 0, y: 0}
    //Top languages

    //Top organizations
    const dataListTopOrganizations = <ListComponent items={topOrganizations} renderFunction={renderOrganization}/>
    const cardTopOrganizations = <InfoCard title={"Top active organizations"} data={dataListTopOrganizations} alert="This is mock data!" />
    const cardTopOrganizationsWrapped : cardWrapper = {card: cardTopOrganizations, width: 2, height: 2, x: 0, y: 2}

    //Add to card list
    var cards: cardWrapper[] = [];
    cards.push(infoCardWrapped)
    cards.push(cardTopOrganizationsWrapped)

    return(
        <div className="mt-10 ml-10 mr-10">
            <InfoCardGridLayout cards={cards} />
        </div>
    )
}