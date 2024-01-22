import { NextRequest, NextResponse } from "next/server";

export async function POST(req: NextRequest){
    //Get variables from POST body
    const data = await req.json();
   
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems", {
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

    return new NextResponse(JSON.stringify(messages), {status: 200,
         headers: {'Access-Control-Allow-Origin': '*'}
    });

}