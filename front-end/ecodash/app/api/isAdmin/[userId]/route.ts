import { NextApiRequest,  NextApiResponse } from 'next';
import { NextResponse, NextRequest } from 'next/server';

/**
 * Handles the GET request to check if a user is an admin.
 * @param request - The NextApiRequest object.
 * @param context - The context object containing the params.
 * @returns A Promise that resolves to a NextResponse object with the isAdmin property indicating if the user is an admin.
 */

// Array of admin usernames
const admins: string[] = ['109254255966692302362', 'admin2', 'admin3'];
//userId, email, userAccount
export async function GET(req : NextRequest, context: { params : any }) : Promise<NextResponse<{isAdmin: boolean}>>{
   // if(!context.params){
    //    return NextResponse.json({ isAdmin: false }, {status: 403});
    //}
    
    const { userId } = context.params;
    console.log("HALLOOO?")
    // Check if the user is an admin
    const isAdmin = admins.includes(userId as string);
    if (isAdmin) {
        //Return status 200 if the user is an admin
        return NextResponse.json({ isAdmin: true }, {status: 200});
    } else {
        //Return status 403 if the user is not an admin
        return NextResponse.json({ isAdmin: false }, {status: 403});
    }
}
