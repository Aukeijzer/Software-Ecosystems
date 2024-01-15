import { ecosystemDTO } from "@/app/interfaces/DTOs/ecosystemDTO";
import { NextRequest, NextResponse } from "next/server"


/**
 * Retrieves the home page data from the backend API.
 * @param req - The NextRequest object representing the incoming request.
 * @returns A NextResponse object containing the response data.
 * @throws Error if the response status is 500.
 */
export async function GET(req: NextRequest){
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + '/ecosystems', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
 
    const  messages : ecosystemDTO[] = await response.json()
    return new NextResponse(JSON.stringify(messages), {status: 200});
}

export const dynamic = "force-dynamic";