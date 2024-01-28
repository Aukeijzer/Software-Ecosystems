import { NextRequest, NextResponse } from "next/server";
import { getToken } from "next-auth/jwt";


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

    console.log(postBody)
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