import { NextResponse , NextRequest} from "next/server";

export async function POST(req: NextRequest) {
    //Get description from body
    const data = await req.json();
    const { description, ecosystem } = data;

    console.log("Updating description of ecosystem " + ecosystem + " to " + description + ".");
    var apiPostBody = { 
        description: description,
        ecosystem: ecosystem
    }
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/descriptionupdate", {
        method: 'POST',
        headers: {
            'Content-Type': 'text/plain;charset=UTF-8',
        },
        body: JSON.stringify(apiPostBody)
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
    const  messages  = await response.json()
    //const  messages  = await req.json()
    //console.log(messages);
    return new NextResponse(messages, {
        status: 200,
        
    })
}