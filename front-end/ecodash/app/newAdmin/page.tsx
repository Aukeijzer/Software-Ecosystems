"use client"

import { useSession } from "next-auth/react";
import { ExtendedUser } from "../utils/authOptions";
import Button from "@/components/button";
import { useState } from "react";
import SpinnerComponent from "@/components/spinner";

enum UserType {
    Admin = "Admin",
    RootAdmin = "RootAdmin"
}

export default function newAdminPage(){
    const [userType, setUserType] = useState<UserType>(UserType.Admin);
    const [email, setEmail] = useState<string>("");

    //Check if is rootAdmin
    const { data: session, status } = useSession()
    if(status === "loading"){
        return(
            <div>
                <SpinnerComponent/>
            </div>
        )
    }
    const user = session?.user as ExtendedUser;
    if(!user || (user.userType !== "RootAdmin")){
        return(
            <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
                <h1 className="text-2xl font-bold mb-4">Add new admin</h1>
                <p className="text-gray-600 mb-4">You do not have permission to access this page</p>
            </div>
        )
    }

    
    function isEmail(email : string) {
        var emailFormat = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
        if (email !== '' && email.match(emailFormat)) { return true; }
        
        return false;
    }


    const handleFormSubmit = async (event : any) => {
        event.preventDefault();


        var email = event.target.email.value;
        var validEmail : boolean = isEmail(email);
        if(!validEmail){
            alert("Invalid email");
            return;
        }

        //Prepare api post body
        var apiPostBody = {
            email: event.target.email.value,
            userType: event.target.userType.value
        }
        
        try {
            const response = await fetch('/api/newAdmin', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(apiPostBody),
            });

            if(response.status === 200){
                alert("Admin created successfully");
            } else {
                alert("Error creating admin");
            }
        } catch (error) {
            console.error('An unexpected error happened occurred:', error);
        }
    }

    const handleSearchTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setUserType(event.target.value as UserType);
    }

    const handleSearchTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(event.target.value);
    }


    return(
        <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
            <h1 className="text-2xl font-bold mb-4">Add user as admin</h1>
            <p className="text-gray-600 mb-4">Give a known user admin rights by entering their email</p>
            <form className="space-y-4" onSubmit={handleFormSubmit}>
                <select
                    value={userType}
                    onChange={handleSearchTypeChange}
                    className="mr-2 p-2 border border-gray-300 rounded-md"
                    name="userType"
                >
                    <option value={UserType.Admin}>Admin</option>
                    <option value={UserType.RootAdmin}>RootAdmin</option>
                </select>

                <input
                    type="text"
                    value={email}
                    name="email"
                    onChange={handleSearchTextChange}
                    className="mr-2 p-2 border border-gray-300 rounded-md"
                />
                <Button text={"Submit"} onClick={() => handleFormSubmit} />
            </form>
        </div>
    )

}