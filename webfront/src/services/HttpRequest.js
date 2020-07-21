import axios from "axios";

axios.defaults.baseURL = 'https://localhost:44382/api';
axios.interceptors.request.use(config=>{
    const token = window.localStorage.getItem('breve_sec_token');
    if(token){
        config.headers.Authorization = `Bearer ${token}`;
        return config;
    }
    return config;
}, error => {
    return Promise.reject(error);
})


const MakeRequest = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url),
}

export default MakeRequest;