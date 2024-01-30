/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

"use client"

import { useSession } from "next-auth/react";
import { ExtendedUser } from "@/utils/authOptions";
import Button from "@/components/button";
import { useState } from "react";
import SpinnerComponent from "@/components/spinner";
import { useRouter } from "next/navigation";

enum UserType {
    Admin = "Admin",
    RootAdmin = "RootAdmin"
}
/**
 * Represents the NewAdminPage component.
 * This component allows a root admin to add a new admin by entering their email.
 * 
 */
export default function NewAdminPage(){
    const [userType, setUserType] = useState<UserType>(UserType.Admin);
    const [email, setEmail] = useState<string>("");

    const router = useRouter();
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
    /**
     * Checks if a given string is a valid email address.
     * 
     * @param email - The email address to validate.
     * @returns True if the email is valid, false otherwise.
     */
    function isEmail(email: string) {
        var emailFormat = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
        if (email !== '' && email.match(emailFormat)) { return true; }
        
        return false;
    }
     
    /**
    * Handles the form submission event.
    * 
    * @param event - The form submission event.
    */
    const handleFormSubmit = async (event : any) => {
        //Prevent default form submit
        event.preventDefault();

        //Get email and validate
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

                alert("Succesfullly created admin");
                router.push("/");
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