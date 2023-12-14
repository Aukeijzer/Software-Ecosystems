import { NextAuthOptions, User } from "next-auth";
import GoogleProvider from "next-auth/providers/google";
import GitHubProvider from "next-auth/providers/github"


const useSecureCookies = process.env.NEXTAUTH_URL!.startsWith("https://");
const cookiePrefix = useSecureCookies ? "__Secure-": "";
const hostName = new URL(process.env.NEXTAUTH_URL!).hostname;
console.log(hostName);

export interface ExtendedUser extends User {
    isAdmin: boolean;
}

/**
 * Configuration options for authentication in the application.
 */
export const authOptions: NextAuthOptions = {
    
    providers: [
        /**
         * Google authentication provider configuration.
         */
        GoogleProvider({
            clientId: process.env.GOOGLE_CLIENT_ID!,
            clientSecret: process.env.GOOGLE_CLIENT_SECRET!,
        }),
        /**
         * GitHub authentication provider configuration.
         */
        GitHubProvider({
            clientId: process.env.GITHUB_CLIENT_ID!,
            clientSecret: process.env.GITHUB_CLIENT_SECRET!
        })
    ],

    session: {
        /**
         * Session strategy configuration.
         */
        strategy: "jwt",
    },
    cookies: {
        sessionToken: {
            /**
             * Name and options for the session token cookie.
             */
            name: `${cookiePrefix}next-auth.session-token`,
            options: {
              httpOnly: true,
              sameSite: 'lax',
              path: '/',
              domain: "."+ hostName,
              secure: useSecureCookies,
            }
          }, 
    },
    callbacks: {
        /**
         * Callback function for manipulating the JWT token.
         */
        async jwt({token , user }) {
            if(user){
                token.id = user.id;
                token.isAdmin = await fetchIsAdmin(user.id, user.email!);
                token.userName = await fetchUserName(user.id);
            }
            return token;
        },
        /**
         * Callback function for manipulating the session object.
         */
        async session({ session, token }) {
            if (session.user) {
                const user = session.user as ExtendedUser;
                // Cast token.isAdmin to boolean
                user.isAdmin = token.isAdmin as boolean;
                user.id = token.id as string;
            }
            return session;
        }
    }
};

/**
 * Fetches the isAdmin status for a given user.
 * @param userId - The ID of the user.
 * @returns A Promise that resolves to a boolean indicating whether the user is an admin or not.
 */

async function fetchIsAdmin(userId: string, username: string) {
    const apiPostBody = {
        id: userId,
        userName: username
    }

    console.log(apiPostBody)
    
    const response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS! + '/users/loginrequest', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(apiPostBody)
    });
   
    const convertedResponse = await response.json();
    console.log("dit is de response")
    console.log(convertedResponse);
    if(convertedResponse.userType == "Admin"){
        return true;
    } else {
        return false;
    }
}

async function fetchUserName(userId: string)
{

}
