import { api } from "@/api/api";
import type AuthInterface from "@/interface/authInterface";

export const Login = async (body: AuthInterface) => {    
    return await api.post('Login', body)
        .then(response => {
           console.log(response)
            if (response.status === 200) {                 
                saveUserToLocalStorage(response.data);
            }

            return response;  
        })
        .catch(error => {
            
            console.error('Erro durante o login:', error);
            throw error;  
        });
}

export const saveUserToLocalStorage = (userData: any) => {    
    localStorage.setItem('token', userData.token);
    localStorage.setItem('user', JSON.stringify(userData.user));
};