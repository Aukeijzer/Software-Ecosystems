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

import { NextRequest, NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";


/**
 * Handles the POST request to remove an ecosystem.
 * 
 * @param req - The NextRequest object representing the incoming request.
 *              ecosystem: string - The ecosystem to remove.
 * @returns A NextResponse object representing the response to the request.
 */
export async function POST(req: NextRequest){
    //Get session
    const token = await getToken({req});

    //Get data from post body
    const data = await req.json();

    //Check if user is admin or rootadmin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin") ){
        return(new NextResponse("Unauthorized", {status: 401}));
    }

    //Check if user is admin of ecosystem
    //Rootadmins can delete each ecosytem
    if(token.userType == "Admin" && !data.userEcosystems.includes(data.ecosystem)){
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    

    //Prepare post body
    var postBody = {
        ecosystem: data.ecosystem,
    }

    //Send data to backend
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/removeEcosystem", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(postBody)
    })

    if (response.status !== 200) {
        throw new Error(response.statusText)
    }

    return new NextResponse("succesfull", {status: 200})
}