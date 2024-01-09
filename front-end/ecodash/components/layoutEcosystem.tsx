
"use client"

import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import GridLayout from "./gridLayout"
import SpinnerComponent from "./spinner"
import { buildLineGraphCard, buildListCard, buildPieGraphCard, buildTableCard } from "@/app/utils/cardBuilder"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine } from "@/mockData/mockAgriculture"
import EcosystemDescription from "./ecosystemDescription"
import  listLanguageDTOConverter  from "@/app/utils/Converters/languageConverter"
import { useRouter, useSearchParams } from 'next/navigation'
import { useSession} from "next-auth/react"
import { cardWrapper } from "@/app/interfaces/cardWrapper"
import listTechnologyDTOConverter from "@/app/utils/Converters/technologyConverter"
import  listRisingDTOConverter  from "@/app/utils/Converters/risingConverter"
import listSubEcosystemDTOConverter from "@/app/utils/Converters/subEcosystemConverter"
import { fetcherEcosystemByTopic } from "@/app/utils/apiFetcher"
import { ExtendedUser } from "@/app/utils/authOptions"
import listContributorDTOConverter from "@/app/utils/Converters/contributorConverter"
import SmallDataBox from "./smallDataBox"
interface layoutEcosystemProps{
    ecosystem: string
}

/**
 * 
 * Renders the layout for the ecosystem page.
 * 
 * @component
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @returns {JSX.Element} The rendered layout component.
 *  The LayoutEcosystem component accepts a single prop: ecosystem, 
 *  which is a string representing the name of the ecosystem.
 *
 *   The useRouter hook is used to get the Router object, 
 *   which is used to navigate between pages. 
 *   The useSearchParams hook is used to get the searchParams object, 
 *   which is used to access the search parameters in the current URL.
 *
 *   The useState hook from React is used to create a state variable selectedEcosystems and its setter function setSelectedEcosystems. 
 *   The initial value of selectedEcosystems is an array containing the ecosystem prop. 
 *   This state variable is used to keep track of the selected ecosystems.
 *
 *   The useSWRMutation hook is used to fetch data from the /ecosystems endpoint of the API. 
 *   The data, trigger, error, and isMutating variables are destructured from the hook. 
 *   The trigger function is used to manually fetch the data, 
 *   data holds the fetched data, error holds any error that occurred during fetching, 
 *   and isMutating indicates whether a fetch is in progress.
 *
 *   The useEffect hook is used to call the trigger function when the component mounts, 
 *   causing the data to be fetched from the API. 
 *   If the URL has additional parameters, these are added to the selectedEcosystems state 
 *   and the trigger function is called with these parameters. If there are no additional parameters, 
 *   the trigger function is called with the current selectedEcosystems state.
 */

export default function LayoutEcosystem(props: layoutEcosystemProps){
    //Set up router
    const Router = useRouter();

    //Set up search params
    const searchParams = useSearchParams();

    //Set up session
    const { data: session } = useSession();
    const user = session?.user as ExtendedUser;

    //Keep track of selected (sub)Ecosystems. start with ecosystem provided
    const [selectedEcosystems, setSelectedEcosystems] = useState<string[]>([props.ecosystem])

    //Keep track of selected technologies
    const [selectedTechnologies, setSelectedTechnologies] = useState<string[]>([])
    //Keep track of selected languages
    const [selectedLanguages, setSelectedLanguages] = useState<string[]>([])
    //Keep track of selected rising technologies
    const [selectedRisingTechnologies, setSelectedRisingTechnologies] = useState<string[]>([])
    //Keep track of selected rising topics
    const [selectedRisingTopics, setSelectedRisingTopics] = useState<string[]>([])
    //Keep track of selected contributors
    const [selectedContributors, setSelectedContributors] = useState<string[]>([])

    //Keep track of edit mode
    const [editMode, setEditMode] = useState<boolean>(false);
    const [description, setDescription] = useState<string>('');
    
    //Trigger = function to manually trigger fetcher function in SWR mutation. 
    //Data = data received from API. updates when trigger is called. causes update
    const { data, trigger, error, isMutating } = useSWRMutation(process.env.NEXT_PUBLIC_BACKEND_ADRESS + '/ecosystems', fetcherEcosystemByTopic)

    //Triggers upon page load once. Calls trigger function with no argument that calls api backend with selected ecosystem
    //Triggers twice in dev mode. Not once build tho / and npm run start. 
    

    useEffect(() => {
        //Check if URL has additional parameters
        const params = searchParams.get('topics');
    
        if(params){
            //Convert params to string list
            const topics = params.split(',')
            trigger({topics: [...selectedEcosystems, ...topics ]})
            setSelectedEcosystems([...selectedEcosystems, ...topics])
        } else {
            trigger({topics: selectedEcosystems})
        }

    },[]) 
    //[] means that it calls useEffect again if values inside [] are updated.
    //But since [] is empty it only is called upon page load

    //Function that gets trigger on Clicking on topic
    async function onClickTopic(topic: string){
        //Append topic to selected ecosystems
        await trigger({topics: [...selectedEcosystems, topic]});
        //Use shallow routing
        //Dont display the props.ecosystem 
        Router.push(`/?topics=${[...selectedEcosystems, topic].filter(n => n != props.ecosystem).toString()}`, {scroll: false})
        //Update selected topics?
        setSelectedEcosystems([...selectedEcosystems, topic]);
        
    }


    async function onClickTechnology(technology: string){
        //Append topic to selected technologies
        await trigger({topics: [...selectedTechnologies, technology]});
        console.log(technology)
        Router.push(`/?techhnologies=${[...selectedEcosystems, technology].filter(n => n != props.ecosystem).toString()}`, {scroll: false})

        setSelectedTechnologies([...selectedTechnologies, technology]);
        console.log(selectedTechnologies)
    }

    async function removeTechnology(technology: string){
        console.log(technology)
        console.log(selectedTechnologies)
        var updatedList = selectedTechnologies.filter(n => n != technology)
        await trigger({topics: selectedEcosystems})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedTechnologies(selectedTechnologies.filter(n => n != technology));
        Router.push(`/?technologies=${updatedList.filter(n => n != props.ecosystem)}`, {scroll: false})
        //Update selected topics?
        
    }

    
    async function onClickLanguage(language: string){
        //Append topic to selected languages
        await trigger({topics: [...selectedLanguages, language]});
        setSelectedLanguages([...selectedLanguages, language]);
    }

    async function onRemoveLanguage(language: string){
        var updatedList = selectedLanguages.filter(n => n != language)
        await trigger({topics: selectedEcosystems})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedLanguages(selectedLanguages.filter(n => n != language));
        Router.push(`/?topics=${updatedList.filter(n => n != props.ecosystem)}`, {scroll: false})
        //Update selected topics?
        
    }

    async function onClickRisingTopic(topic: string){
        //Append topic to selected rising topics
        await trigger({topics: [...selectedRisingTopics, topic]});
        setSelectedRisingTopics([...selectedRisingTopics, topic]);
    }

    async function onRemoveRisingTopic(topic: string){
        var updatedList = selectedRisingTopics.filter(n => n != topic)
        await trigger({topics: selectedEcosystems})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedRisingTopics(selectedRisingTopics.filter(n => n != topic));
        //Router.push(`/?topics=${updatedList.filter(n => n != props.ecosystem)}`, {scroll: false})
        //Update selected topics?
    }

    async function onClickRisingTechnology(technology: string){
        //Append topic to selected rising technologies
        await trigger({topics: [...selectedRisingTechnologies, technology]});
        setSelectedRisingTechnologies([...selectedRisingTechnologies, technology]);
    }

    async function onRemoveRisingTechnology(technology: string){
        var updatedList = selectedRisingTechnologies.filter(n => n != technology)
        await trigger({topics: selectedEcosystems})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedRisingTechnologies(selectedRisingTechnologies.filter(n => n != technology));
        //Router.push(`/?topics=${updatedList.filter(n => n != props.ecosystem)}`, {scroll: false})
        //Update selected topics?
    }

    //Function to remove sub-ecosystem from sub-ecosytem list
    async function removeSubEcosystem(subEcosystem: string){
        var updatedList = selectedEcosystems.filter(n => n != subEcosystem)
        await trigger({topics: selectedEcosystems})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedEcosystems(selectedEcosystems.filter(n => n != subEcosystem));
        Router.push(`/?topics=${updatedList.filter(n => n != props.ecosystem)}`, {scroll: false})
        //Update selected topics?
        
    }

    async function saveDescription(){
       
        var apiPostBody = {
            description: description,
            ecosystem: props.ecosystem
        }
        console.log(apiPostBody)

        //Send to backend

        const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/descriptionupdate", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(apiPostBody)
        })
    
        
        if (response.status == 500){
            console.log("Failed to update description");
            throw new Error(response.statusText)
        }
        const convertedReponse = await response.json();
        console.log(convertedReponse)
        
        
    }

    function changeDescription(description: string){
        console.log(description);
        setDescription(description);
    }

  



    //If error we display error message
    if(error){
        return(
            <>
                
                <h2 className='text-3xl m-5'>Something went wrong!</h2>
                <div className='flex flex-col gap-3 p-5 bg-gray-300 border-2 m-5 border-gray-900 rounded-sm'>
                    <p>
                        {error.message}
                    </p>
                    <p>
                        {error.stack}
                    </p>
                   
                </div>
            </>
        )
    }
  

    //Prepare variables before we have data so we can render before data
    var cardWrappedList : cardWrapper[] = []
    if(data){
        const yHeight = 2;
        const offSet = 0.7;
        //Real data
        const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];

        //Top 5 topics
        //First Convert DTO's to Classes
        const subEcosystems = listSubEcosystemDTOConverter(data.subEcosystems);
        //Make list element
   
        const subEcosystemCard = buildTableCard(subEcosystems, "", 0, yHeight * 2 + offSet, 2, 6, onClickTopic,"", COLORS[0]);
        //Add card to list
        cardWrappedList.push(subEcosystemCard);
        
      

        //Top 5 languages
        const languages = listLanguageDTOConverter(data.topLanguages);
        //Make graph card
        const languageCard = buildPieGraphCard(languages, "", 2,  yHeight * 2 + offSet, !editMode, COLORS[5]);
        //Add card to list
        cardWrappedList.push(languageCard);

        //Mock data
        //List of technologies
        const technologies = listTechnologyDTOConverter(topTechnologies)
        const technologyCard = buildTableCard(technologies, "", 4, yHeight * 2 + offSet, 2, 6, onClickTechnology, "", COLORS[3]);
        cardWrappedList.push(technologyCard)

        //List of rising technologies
        const risingTechnologies = listRisingDTOConverter(topTechnologyGrowing); 
        const risingTechnologiesCard = buildTableCard(risingTechnologies, "", 0, yHeight * 5 + offSet, 2, 6, onClickRisingTechnology, "", COLORS[4]);
        cardWrappedList.push(risingTechnologiesCard)

        //List of rising topics
        const risingTopics = listRisingDTOConverter(topTopicsGrowing);
        const risingTopicsCard = buildTableCard(risingTopics, "", 2, yHeight * 8 + offSet, 2, 6, onClickRisingTopic, "", COLORS[5]);
        cardWrappedList.push(risingTopicsCard)

        //Line graph topicsGrowing 
        //For now no data conversion needed as Mock data is already in correct format 
        //When working with real data there should be a conversion from DTO to dataLineGraphModel
        const cardLineGraphWrapped = buildLineGraphCard(topicGrowthLine, "", 4, yHeight * 5 + offSet , !editMode, COLORS[2]);
        cardWrappedList.push(cardLineGraphWrapped)

        //const contributors = listContributorDTOConverter(data.topContributors);
        //const contributorCard = buildTableCard(['username', 'contributions'], contributors, "", 0, yHeight * 4, 2, 5, "", COLORS[2]);
        //cardWrappedList.push(contributorCard);

        //Ecosystem description
        //Remove main ecosystem from selected sub-ecosystem list to display
       
        const ecosystemDescription =  <EcosystemDescription ecosystem={props.ecosystem} changeDescription={changeDescription} editMode={editMode}  removeTopic={removeSubEcosystem} description={description ? description : data.description}  
                subEcosystems={selectedEcosystems.filter(n => n!= props.ecosystem)} 
                technologies={selectedTechnologies} 
                risingTechnologies={selectedRisingTechnologies} 
                risingTopics={selectedRisingTopics}
                languages={selectedLanguages}
                removeLanguage={onRemoveLanguage}
                removeRisingTechnology={onRemoveRisingTechnology}
                removeRisingTopic={onRemoveRisingTopic}
                removeTechnology={removeTechnology}
             />
        const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 2, x: 0, y: yHeight * 0, static:true}
        cardWrappedList.push(ecosystemDescriptionWrapped)

        //Small boxes
         
        const smallBoxes = ( <div className="flex flex-row   mb-5 justify-around">
                <SmallDataBox item={"Topics"} count={100} increase={5}  />
                <SmallDataBox item={"Projects"} count={100} increase={5} />
                <SmallDataBox item={"Contributors"} count={100} increase={5} />
                <SmallDataBox item={"Contributions"} count={100} increase={5} />
            </div>
        )

      const smallBoxesWrapped: cardWrapper = {card: smallBoxes, width: 6, height: 2, x: 0, y : yHeight, static: true}
      cardWrappedList.push(smallBoxesWrapped);
    
    } else {
        //When no data display spinner
        return(
            <div>
                <SpinnerComponent />
            </div>
        )
    }

    //Normal render (No error)
    return(
        <div className="ml-44 mr-44">
            {user !== undefined && user !== null && (user.userType === "Admin" || user.userType === "RootAdmin") &&
                <div className="m-3 rounded-sm  p-3 text-yellow-700 bg-red-200">
                    <form className="flex flex-col">
                        <div className="flex flex-row gap-3">
                            <label> edit mode:</label>
                            <input type="checkbox" name="editMode" onChange={() => setEditMode(!editMode)}/> 
                        </div>
                       
                    </form>
                    <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={(e) => saveDescription()}> save changes</button>
                </div>
               
            }
            <GridLayout cards={cardWrappedList} />
        </div>      
    )
}