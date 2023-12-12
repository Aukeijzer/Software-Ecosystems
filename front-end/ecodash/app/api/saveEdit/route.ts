import { NextApiRequest, NextApiResponse } from "next";
import { NextResponse } from "next/server";
type ResponseData = {
    description: string
}
export async function POST(req: Request) {
    //Get description from body
    console.log("ahlloas?")
    //const  messages  = await req.json()
    //console.log(messages);
    return new Response("hoi", {
        status: 200,
    })
}