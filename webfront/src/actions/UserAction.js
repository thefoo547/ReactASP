import HttpRequest from '../services/HttpRequest';

export const SigninUser = user=>{
    return new Promise( (resolve, eject) => {
        HttpRequest.post('/User/signup', user).then(response=>{
            resolve(response);
        });
    });
}