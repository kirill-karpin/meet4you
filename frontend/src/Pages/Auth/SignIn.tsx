import { useForm } from "react-hook-form";
import { api } from "../../lib/fetch-api";
import { useNavigate } from "react-router-dom";
import { useState} from "react";
export interface LogInParams {
    login: string;
    password: string;
    remember: boolean;
}

export interface LogInRepsponse {
    token?: string;
    refreshToken?: string;
    result?: string
}
export default function SignIn()  {
    const { handleSubmit, register} = useForm<LogInParams>();
    const navigate = useNavigate();
    const [error, setError] = useState<string | null>(null);
    
    
    
    const onSubmit = handleSubmit(async (data) => {
       const {result,  token } = await api.fetch<LogInRepsponse>({
           url: "http://localhost:5128/api/auth/sign-in",
           method:  "POST",  body: data
       });
       if (token) {
           api.setToken(token);
           navigate('/');
       } else {
           if (result)
            setError(result);
       }
      
    })
    
    return <>
        <h3>SignIn</h3>

        <form action=""  onSubmit={onSubmit}>
            {(error) ?  (<p> {error} </p>) : (<></>)} 
            <input type="text"  id="login"  {...register("login")}/>
            <input  type="password"  id="password"  {...register("password")} />
            <button type="submit">
                Sign In
            </button>
        </form>
    </>;
}
