import { getToken } from "next-auth/jwt";
import { NextRequest, NextResponse } from "next/server";

export async function POST(req: NextRequest){
    //Get session 
    const token = await getToken({req});
    
    //Check if user is RootAdmin
    if (!token || token.userType !== "RootAdmin") {
        return(new NextResponse("Unauthorized", {status: 401}));
    }

    const data = await req.json();
    console.log(data)

    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/newAdmin", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
    const messages : any = await response.json();
    return new NextResponse(JSON.stringify(messages), {status: 200})
}