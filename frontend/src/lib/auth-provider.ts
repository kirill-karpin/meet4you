interface AuthProvider {
    isAuthenticated: boolean;
    username: null | string;
    signin(username: string): Promise<void>;
    signout(): Promise<void>;
}

/**
 * This represents some generic auth provider API, like Firebase.
 */
export const authProvider: AuthProvider = {
    isAuthenticated: false,
    username: null,
    async signin(username: string) {
        await new Promise((r) => setTimeout(r, 500)); 
        authProvider.isAuthenticated = true;
        authProvider.username = username;
    },
    async signout() {
        await new Promise((r) => setTimeout(r, 500)); 
        authProvider.isAuthenticated = false;
        authProvider.username = "";
    },
};