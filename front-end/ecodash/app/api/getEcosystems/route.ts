import { ecosystemDTO } from "@/app/interfaces/DTOs/ecosystemDTO"
import { NextRequest, NextResponse } from "next/server"


export async function GET(req: NextRequest){
    
    console.log("Getting all ecosystems.")
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    if (response.status === 500) {
        throw new Error(response.statusText)
    }
    const  messages : ecosystemDTO[] = await response.json()
    //const  messages  = await req.json()
    console.log(messages);

    return new NextResponse(JSON.stringify(messages), {status: 200});
}