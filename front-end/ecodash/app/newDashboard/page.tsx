"use client";
import { useSession } from "next-auth/react";
import { ExtendedUser } from "../utils/authOptions";
import Button from "@/components/button";
import SpinnerComponent from "@/components/spinner";
/**
 * Represents the page component for creating a new dashboard.
 */
export default function NewDashboardPage(){
   
    //Check if isAdmin
    const { data: session, status } = useSession()
    const user = session?.user as ExtendedUser;
    if(status === "loading"){
        return(
            <div>
                <SpinnerComponent/>
            </div>
        )
    }
    
    if(!user || (user.userType !== "Admin" && user.userType !== "RootAdmin")){
        return(
            <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
                <h1 className="text-2xl font-bold mb-4">Create a new dashboard</h1>
                <p className="text-gray-600 mb-4">You do not have permission to access this page</p>
            </div>
        )
    }
    
    interface inputFileDTO {
        topics: string[],
        technologies: string[],
        excludedTopics: string[]
    }


    /**
     * Handles the form submission event.
     * 
     * @param event - The form submission event.
     * @returns {Promise<void>}
     */
    const handleFormSubmit = async (event : any) => {
        //Prevent default form submit
        event.preventDefault();

        //Get ecosystem .json file
        const fileTaxonomyInput : HTMLInputElement | null = document.querySelector('input[name="ecosystemJSON"]');
        if(fileTaxonomyInput === null || fileTaxonomyInput.files === null){
            alert("File taxonomy input not found")
            throw new Error("File taxonomy input not found");
        }   
        const fileTaxonomy = fileTaxonomyInput.files[0];

        //Read file
        var ecosystemInformation : inputFileDTO  | null = null;
        const fileReader = new FileReader();
        fileReader.readAsText(fileTaxonomy, "UTF-8");
        fileReader.onload = async function (e) {
            console.log(e.target?.result);
            const content = e.target?.result as string;
            console.log(content);
            if(content === null){
                alert("Error reading file");
                throw new Error("Error reading file");
            }
            try {
                ecosystemInformation = JSON.parse(content) as inputFileDTO;
                console.log(ecosystemInformation);
                
                //Prepare api post body
                 var apiPostBody = {
                    topics: (ecosystemInformation as inputFileDTO)?.topics,
                    technologies: (ecosystemInformation as inputFileDTO)?.technologies,
                    excludedTopics: (ecosystemInformation as inputFileDTO)?.excludedTopics,
                    ecosystem: event.target.ecosystem.value,
                    description: event.target.description.value
                }
            } catch(err){
                throw new Error("Error parsing file");
            }

            console.log("api post body incoming")
            console.log(apiPostBody)
            //send to node backend
            try {
                const response = await fetch('/api/newEcosystem', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(apiPostBody),
                });
                if (response.ok) {
                    // Handle success
                    const data = await response.json();
                    console.log(data);
                    //Alert user that ecosystem was created
                    alert("Ecosystem created");
                } else {
                    // Handle error
                    throw new Error("Error in response");
                }
            } catch (error ) {
                throw new Error("Error in fetch");
            }
            
        }
    }

    return(
        <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
            <h1 className="text-2xl font-bold mb-4">Create a new dashboard</h1>
            <p className="text-gray-600 mb-4">Text here explaining the steps to create a new dashboard</p>
            <div className="mt-4">
                <h2 className="text-lg font-bold mb-2">JSON Structure:</h2>
                <pre className="bg-gray-100 p-4 rounded-md">
    {
    `{   
    "topics": [],
    "technologies": [],
    "bannedTopics": []
}`}
            </pre>
        </div>
            <form className="space-y-4" onSubmit={handleFormSubmit}>
                <div>
                    <label className="block text-sm font-medium text-gray-700">Ecosystem name</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="text" name="ecosystem" />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700">Description</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="text" name="description" />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700"> Ecosystem json </label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="file" name="ecosystemJSON" accept=".json" multiple={false} />
                </div>

                <Button text={"Submit"} onClick={() => handleFormSubmit} />
            </form>
        </div>
    )
}