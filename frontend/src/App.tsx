import {Link, LoaderFunctionArgs, Outlet, RouterProvider, createBrowserRouter, redirect } from 'react-router-dom';
import './App.css';
import SignIn from './Pages/Auth/SignIn';
import PublicPage from './Pages/PublicPage';
import Profile from './Pages/Profile/Profile';
import { authProvider } from './lib/auth-provider';
import Registration from './Pages/Auth/Registration';
import UsersList from './Pages/Profile/UsersList';
import ProfileEdit from './Pages/Profile/ProfileEdit';


const router = createBrowserRouter([
    {
        id: "root",
        path: "/",
        Component: Layout,
        children: [
            {
                index: true,
                Component: PublicPage,
            },
            {
                path: "sign-in",
                Component: SignIn,
            },
            {
                path: "registration",
                Component: Registration,
            },
            {
                path: "profile",
                action: loginAction,
                Component: Profile,
            },
            {
                path: "profile/edit",
                Component: ProfileEdit,
            },
            {
                path: "users",
                Component: UsersList,
            },
        ],
    },
    {
        path: "/logout",
        async action() {
            
        },
    },
]);

function Layout() {
    return (
        <div>
            <h1>Meet4You</h1>
            
            <h2>Best, daiting social network, ever!</h2>
            
            <ul>
                <li><Link to="/">Public page</Link></li>
                <li><Link to="/sign-in">Sign-in page</Link></li>
                <li><Link to="/registration">Registration page</Link></li>
                <li><Link to="/profile">Profile page</Link></li>
                <li><Link to="/profile/edit">Profile edit page</Link></li>
                <li><Link to="/users">Users list</Link></li>
            </ul>
            
            <Outlet/>
        </div>
    );
}


function App() {
    return (
        <RouterProvider router={router} fallbackElement={<p>Initial Load...</p>} />
    );
}


async function loginAction({ request }: LoaderFunctionArgs) {
    let formData = await request.formData();
    let username = formData.get("username") as string | null;
   
    // Validate our form inputs and return validation errors via useActionData()
    if (!username) {
        return {
            error: "You must provide a username to log in",
        };
    }

    // Sign in and redirect to the proper destination if successful.
    try {
        await authProvider.signin(username);
    } catch (error) {
        // Unused as of now but this is how you would handle invalid
        // username/password combinations - just like validating the inputs
        // above
        return {
            error: "Invalid login attempt",
        };
    }

    let redirectTo = formData.get("redirectTo") as string | null;
    return redirect(redirectTo || "/");
}
export default App;
