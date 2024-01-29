"use client"

import { useEffect} from "react"
import { useRouter } from 'next/navigation'
import { fetcherHomePage } from '@/utils/apiFetcher';
import useSWRMutation from 'swr/mutation'
import { totalInformation } from "@/mockData/mockEcosystems";
import InfoCard from "./infoCard";
import EcosystemButton from "./ecosystemButton";
import SpinnerComponent from "./spinner";
import { ExtendedUser } from "@/utils/authOptions";
import { useSession } from "next-auth/react";
var abbreviate = require('number-abbreviate');

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
    const { data, trigger, error, isMutating} = useSWRMutation('/api/homePageGet', fetcherHomePage)

    //Set up session
    const { data: session } = useSession();
    const user = session?.user as ExtendedUser;
    const userEcosystems = user?.ecosystems;
    
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
        /* Old code for when we had middleware 
        var url = process.env.NEXT_PUBLIC_LOCAL_ADRESS!.split("//");
        var finalUrl = url[0] + "//" + ecosystem + '.' + url[1] ;
        Router.push(finalUrl);
        */
        Router.push('/' + ecosystem.toLowerCase().replaceAll(" ", "-"));
    }

    function removeEcosystem(event: any, ecosystem: string){
        event.stopPropagation()
        if(confirm("Are you sure you want to remove this ecosystem?")){
            //Remove ecosystem from user
            //Make api call to remove ecosystem from user
            var apiPostBody = {
                ecosystem: ecosystem,
                userEcosystems: user.ecosystems
            }
            fetch('/api/removeEcosystem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(apiPostBody)
            }).then(response => {
                if(response.ok){
                    if(response.status === 200){
                        alert("Ecosystem removed");
                        window.location.reload();
                    } else {
                        alert("Error removing ecosystem");
                    }
                } else {
                    throw new Error("Error in response");
                }
            }).catch(error => {
                throw new Error("Error in fetch");
            })
        }
    }

    var cardList = [];
    if(data){
        const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];

        var totalProjects = 0;
        var totalContributors = 0;
        var totalTopics = 0;
        var totalStars = 0;
        for(var i = 0; i < data.length; i++){
            totalProjects += data[i].numberOfProjects;
            totalContributors += data[i].numberOfContributors;
            totalTopics += data[i].numberOfSubTopics;
            totalStars += data[i].numberOfStars;
        }

        //General information about SECODash
        const info = (<div className="flex flex-col "> 
                <span> Total ecosystems: {data.length}</span>
                <span> Total projects: {abbreviate(totalProjects)} </span>
                <span> Total sub-topics: {abbreviate(totalTopics)} </span>
                <span> Total stars: {abbreviate(totalStars)} </span>
                <span> Total contributors: {abbreviate(totalContributors)} </span>
            </div>
        )

        const infoCard = <div className="col-span-4">
            <InfoCard title="Information about SECODash" data={info} />
        </div>
        cardList.push(infoCard);

        if(user){
            //If user is admin, make cards draggable
            if(user.userType === "Admin" || user.userType === "RootAdmin"){
                //Create new dashboard card
                const newDashboardButton = <div className="h-16"> </div>
                const newDashboardButtonCard = <div className="cursor-pointer col-span-2">
                    <InfoCard 
                    title="Create new Dashboard"
                    data={newDashboardButton}
                    Color={COLORS[3]}
                    onClick={() => Router.push('/newDashboard')}
                    />
                </div> 
                cardList.push(newDashboardButtonCard);
                if(user.userType === "RootAdmin"){
                    //Create new add admin card
                    const addAdminButton = <div> </div>
                    const addAdminButtonCard = <div className="cursor-pointer col-span-2  ">
                        <InfoCard 
                        title="Add new admin"
                        data={addAdminButton}
                        Color={COLORS[4]}
                        onClick={() => Router.push('/newAdmin')}
                        />
                    </div>
                    cardList.push(addAdminButtonCard);
                }
            }
        }

        //Prepare card for each ecosystem availlable
        for(var i = 0; i < data.length; i++){
            //const ecosystemname = data[i].displayName.toLowerCase().replaceAll(" ", "-");
            var removable = false;
            if(user && userEcosystems){
                removable = userEcosystems.includes(data[i].displayName);
                if(user.userType === "RootAdmin"){
                    removable = true;
                }
            }
            
            const button = <EcosystemButton ecosystem={data[i].displayName}
             projectCount={data[i].numberOfProjects}  
             topics={data[i].numberOfSubTopics} 
             contributors={data[i].numberOfContributors}
             stars={data[i].numberOfStars} />
        
            const card = <div className="col-span-1  cursor-pointer">
                <InfoCard title={data[i].displayName!} data={button} onClick={onClickEcosystem} Color={COLORS[i]} remove={removable} onRemove={removeEcosystem} ecoystem={data[i].displayName} />
            </div>
            cardList.push(card);
        }

      
    } else {
        //When still loading display spinner
        return(
            <div>
                <SpinnerComponent />
            </div>
        )
    }
    return(
        <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32">
            <div className="grid gap-3 grid-cols-4 " >
             {cardList.map((card, i) => (
                 card
             ))}
        </div>
     </div>   
    )
}