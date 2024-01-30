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

import LayoutEcosystem from "@/components/layoutEcosystem";
import { Metadata } from "next";
//This page has a dynamic path. Meaning you can put everything after /ecosytem/.... and it will go to that site and pass the .... as props to the page

interface ecosystemPageProps{
    params: { ecosystem : string},
}

//This whole block has functions that are defined by Next. Dont change any of the spelling! this will break things
// Do not enable params that are not defined as static
export const dynamicParams = true;
// Define static params
export function generateStaticParams() {
    return ["agriculture", "artificial-intelligence", "quantum", "gaming"].map(ecosystem => ({ ecosystem }));
}                           
//Set title 
export function generateMetadata({params: {ecosystem}} : ecosystemPageProps): Metadata {
    return {
        title: ecosystem + " data page"
    }
}

export default function ecosystemPage({params: {ecosystem}}: ecosystemPageProps){
    return( 
        <div className="h-full">
            <LayoutEcosystem ecosystem={ecosystem} />
        </div>
       
    )
}