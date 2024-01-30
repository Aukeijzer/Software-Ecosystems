/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

import { ecosystemDTO } from "@/interfaces/DTOs/ecosystemDTO"
import { NextRequest, NextResponse } from "next/server"

/**
 * Handles GET request from homepage
 * @param req 
 * @returns all available ecosystems as ecosystemDTO[]
 */
export async function GET(req: NextRequest){
    const response : Response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS + '/ecosystems', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
    }
 
    const  messages : ecosystemDTO[] = await response.json()
    return new NextResponse(JSON.stringify(messages), {status: 200});
}

export const dynamic = "force-dynamic";