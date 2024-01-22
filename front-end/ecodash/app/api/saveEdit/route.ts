import { NextResponse , NextRequest} from "next/server";

export async function POST(req: NextRequest) {
    //Get description from body
    const data = await req.json();
    
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/descriptionupdate", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
    return new NextResponse("succesful", {
        status: 200,
        
    })
}