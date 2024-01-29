import { NextAuthOptions, User } from "next-auth";
import GoogleProvider from "next-auth/providers/google";
import GitHubProvider from "next-auth/providers/github"
const stringHash = require("string-hash");


//Set cookie prefix and secure cookies based on environment
const useSecureCookies = process.env.NEXTAUTH_URL!.startsWith("https://");
const cookiePrefix = useSecureCookies ? "__Secure-": "";
const hostName = new URL(process.env.NEXTAUTH_URL!).hostname;

export interface ExtendedUser extends User {
    userType: string;
    ecosystems: string[];
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
                const data = await fetchUserData(user.id, user.email!);
                console.log(data);
                token.id = user.id;
                token.userType = data.userType;
                token.ecosystems = data.ecosystems;
            
            }
            //token.userType = "Admin"
            return token;
        },
        /**
         * Callback function for manipulating the session object.
         */
        async session({ session, token }) {
            if (session.user) {
                const user = session.user as ExtendedUser;
                user.userType = token.userType as string;
                user.ecosystems = token.ecosystems as string[];
                user.id = token.id as string;
            }
            return session;
        }
    }
};

/**
 * Fetches the isAdmin status for a given user.
 * @param userId - The ID of the user.
 * @param username - The username of the user
 * @returns A Promise that resolves to string of userType (Options are: User, Admin, RootAdmin).
 */

async function fetchUserData(userId: string, username: string) {
    //Hash username
    const apiPostBody = {
        id: userId,
        userName: stringHash(username).toString()
    }
    console.log(apiPostBody);

    //Send data to backend
    const response = await fetch(process.env.NEXT_PUBLIC_BACKEND_ADRESS! + '/users/loginrequest', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(apiPostBody)
    });
    console.log(response);
   
    enum userType{
        "User",
        "Admin",
        "RootAdmin"
    }
    const convertedResponse = await response.json();
    let userTypeResult = convertedResponse.userType;
    let enumType = userType[userTypeResult];
    console.log(enumType);
    return  {userType: enumType, ecosystems: convertedResponse.ecosystems}
}

