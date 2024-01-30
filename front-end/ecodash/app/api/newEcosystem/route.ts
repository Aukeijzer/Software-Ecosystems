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

import { NextResponse , NextRequest} from "next/server";
import { getToken } from "next-auth/jwt";
const stringHash = require("string-hash");

/**
 * Handles the POST request for creating a new ecosystem.
 * 
 * @param req - The NextRequest object containing the request details.
 *              topics: string[] - The topics of the ecosystem.
 *              technologies: string[] - The technologies of the ecosystem.
 *              excluded: string[] - The excluded technologies of the ecosystem.
 *              ecosystemName: string - The name of the ecosystem.
 *              description: string - The description of the ecosystem.
 * @returns A NextResponse object with the response data.
 */
export async function POST(req: NextRequest) {
    //Get session
    const token = await getToken({req});
    
    //Check if user is admin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin")) {
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    //Get data from post body
    const data = await req.json();

    //Hash email and append user to data
    data.email = stringHash(token.email);

    var apiPostBody = {
        topics: data. topics,
        technologies: data.technologies,
        excluded: data.excluded,
        email: stringHash(token.email).toString(),
        ecosystemName: data.ecosystemName,
        description: data.description,
    }

    //Send data to backend
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/CreateEcosystem", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    if (response.status !== 200) {
        throw new Error(response.statusText)
    }

    return new NextResponse("succesfull", {status: 200})
}