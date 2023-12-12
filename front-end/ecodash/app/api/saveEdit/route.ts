import { NextResponse , NextRequest} from "next/server";
type ResponseData = {
    description: string
}
export async function POST(req: NextRequest) {
    //Get description from body
    console.log("ahlloas?")
    //const  messages  = await req.json()
    //console.log(messages);
    return new NextResponse("hoi", {
        status: 200,
    })
}