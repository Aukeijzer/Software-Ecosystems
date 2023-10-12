//"use client";
//This component is rendered client-side because we need to change the navbar depending on the site we are on
// This is not currently implemented :) because we have no links to click on
import NavLogo from "./navLogo"

export default function NavBar(){
    return(
        <nav className="bg-blue-300 border-gray-200 max-w" data-cy="navBar">
            <NavLogo />
        </nav>
    )
}