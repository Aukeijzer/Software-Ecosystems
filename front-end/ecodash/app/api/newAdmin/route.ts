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

import { getToken } from "next-auth/jwt";
import { NextRequest, NextResponse } from "next/server";
const stringHash = require("string-hash");

export async function POST(req: NextRequest){
    //Get session 
    const token = await getToken({req});
    
    //Check if user is RootAdmin
    if (!token || token.userType !== "RootAdmin") {
        return(new NextResponse("Unauthorized", {status: 401}));
    }

    const data = await req.json();
    
    var userEnum = 0;
    if(data.userType == "Admin"){
        userEnum = 1;
    } else if(data.userType == "RootAdmin"){
        userEnum = 2;
    }

    const apiPostBody = {
        userName: stringHash(data.email).toString(),
        userType: userEnum
    }

    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/users/updatepermissions", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    if (response.status !== 200) {
        throw new NextResponse(`Internal server error: ${response.text}`, {status: 500})
    }

    return new NextResponse("succesfull", {status: 200})
}