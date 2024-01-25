import { NextResponse , NextRequest} from "next/server";
import { getToken } from "next-auth/jwt";
import { readFile } from "fs";
import { buffer } from "stream/consumers";
import path from "path";
import { fileURLToPath } from "url";
export async function POST(req: NextRequest) {
    //Get session
    const token = await getToken({req});
    
    //Check if user is admin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin")) {
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    //Get data from post body
    const data = await req.json();

    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/newEcosystem", {
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

    return new NextResponse(JSON.stringify(messages), {status: 200})
}