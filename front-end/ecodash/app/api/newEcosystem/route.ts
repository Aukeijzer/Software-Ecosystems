import { NextResponse , NextRequest} from "next/server";

export async function POST(req: NextRequest) {
    console.log("Getting topic taxonomy");
    //Get topic taxonomy from body
    //This is a formdata object with taxonmy as key and the taxonomy file as value
    //Parse the formdata object
    const data = await req.formData();
    console.log(data);
    //Read through json file

    //I have not checked if these files have data in them
    //I dont want to implement reading the file here
    //Just send to backend
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