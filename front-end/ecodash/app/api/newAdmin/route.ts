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
    console.log(data)

    console.log(token.email)
    console.log(stringHash(data.email).toString())
    
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

    console.log(apiPostBody)

    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/users/updatepermissions", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    console.log(response)

    if(response.status == 200){
        const messages : any = await response.json();
        return new NextResponse(JSON.stringify(messages), {status: 200})
    } else {
        return new NextResponse("Error", {status: 500})
    }
  
}