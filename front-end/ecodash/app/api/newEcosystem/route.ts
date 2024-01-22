import { NextResponse , NextRequest} from "next/server";
import { getToken } from "next-auth/jwt";

export async function POST(req: NextRequest) {
    //Get session
    const token = await getToken({req});

    //Check if user is admin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin")) {
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    
    const data = await req.formData();


    console.log(data);

    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/newEcosystem", {
        method: 'POST',
        body: data
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }

    const messages : any = await response.json();


    return new NextResponse(JSON.stringify(messages), {status: 200})
}