import SessionReducer from './SessionReducer';
import OpenSnackbarReducer from './OpenSnackbarReducer';


export const MainReducer = ({userSession, openSnackbar}, action)=>{
    return{
        userSession : SessionReducer(userSession, action),
        openSnackbar : OpenSnackbarReducer(openSnackbar, action)
    }
}