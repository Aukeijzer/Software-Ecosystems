import { NextResponse , NextRequest} from "next/server";
import { getToken } from "next-auth/jwt";
const stringHash = require("string-hash");

export async function POST(req: NextRequest) {
    //Get session
    const token = await getToken({req});
    
    //Check if user is admin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin")) {
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    //Get data from post body
    const data = await req.json();

    //Hash email and append user to data
    data.email = stringHash(token.email);

    var apiPostBody = {
        topics: data. topics,
        technologies: data.technologies,
        excluded: data.excluded,
        email: stringHash(token.email).toString(),
        ecosystemName: data.ecosystemName,
    
        description: data.description,
    }

    console.log(apiPostBody);

    //Send data to backend
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + "/ecosystems/CreateEcosystem", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    console.log(response)

    if (response.status !== 200) {
        throw new Error(response.statusText)
    }


    const messages : any = await response.json();

    return new NextResponse(JSON.stringify(messages), {status: 200})
}