import { authOptions } from "@/utils/authOptions";
import { getServerSession } from "next-auth";
import { NextResponse , NextRequest} from "next/server";
import { getToken } from "next-auth/jwt";

/**
 * Handles the POST request for saving edits.
 * 
 * @param req - The NextRequest object representing the incoming request.
 *              ecosystem: string - The ecosystem to edit.
 *              description: string - The new description of the ecosystem.
 * @param res - The NextResponse object representing the outgoing response.
 * @returns A NextResponse object indicating the success or failure of the request.
 */
export async function POST(req: NextRequest, res: NextResponse) {
    //Get session
    const token = await getToken({req});

    //Check if user is admin
    if (!token || (token.userType !== "Admin" && token.userType !== "RootAdmin")) {
        return(new NextResponse("Unauthorized", {status: 401}));
    }
    //Get data
    var data = await req.json();
    
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