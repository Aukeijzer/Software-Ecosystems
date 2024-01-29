
import { ecosystemDTO } from "@/app/interfaces/DTOs/ecosystemDTO";
import { NextRequest, NextResponse } from "next/server"

/**
 * Handles GET request from homepage
 * @param req 
 * @returns all available ecosystems as ecosystemDTO[]
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