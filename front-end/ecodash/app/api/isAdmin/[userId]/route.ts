import { NextApiRequest, NextApiResponse } from 'next';
import { NextResponse } from 'next/server';

// Array of admin usernames
const admins: string[] = ['109254255966692302362', 'admin2', 'admin3'];

const isAdminHandler = (req: NextApiRequest, res: NextApiResponse) => {
    console.log(req.query)
    const { userId } = req.query;

    // Check if the user is an admin
    const isAdmin = admins.includes(userId as string);
    console.log("test")
    if (isAdmin) {
        res.status(200).json({ isAdmin: true });
    } else {
        res.status(403).json({ isAdmin: false });
    }
    return res;
}
export { isAdminHandler as POST}


export async function GET(request : NextApiRequest, context: { params : any }) : Promise<NextResponse<{isAdmin: boolean}>>{
    const { userId } = context.params;
    console.log(userId);
    // Check if the user is an admin
    const isAdmin = admins.includes(userId as string);
    console.log(isAdmin)
    if (isAdmin) {
        return NextResponse.json({ isAdmin: true }, {status: 200});
    } else {
        return NextResponse.json({ isAdmin: false }, {status: 403});
    }
}
