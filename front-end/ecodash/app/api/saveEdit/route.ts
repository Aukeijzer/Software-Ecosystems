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
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
    return new NextResponse("succesful", {
        status: 200,
        
    })
}