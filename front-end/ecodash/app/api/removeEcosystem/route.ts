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

    //Check if user is admin and check if ecosystem is owned by user
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin") || !data.userEcosystems.includes(data.ecosystem)){
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

    const messages : any = await response.json();
    return new NextResponse(JSON.stringify(messages), {status: 200})
}