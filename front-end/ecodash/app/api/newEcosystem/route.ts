import { NextResponse , NextRequest} from "next/server";

export async function POST(req: NextRequest) {
    console.log("Getting topic taxonomy");
    //Get topic taxonomy from body
    //This is a formdata object with taxonmy as key and the taxonomy file as value
    //Parse the formdata object
    const data = await req.formData();
    console.log(data);
    //Read through json file
    var file = data.get('taxonomy') as File;
    if(file){
        console.log(file.name);

    }

    return new NextResponse("succesful", {status: 200})
}