export const initialState = {
    user:{
        fullName: '',
        email: '',
        username: '',
        profilePhoto: ''
    },
    isAuthenticated: false
}

const SessionReducer = (state = initialState, action)=>{
    switch(action.type){
        case "LOGIN":
            return{
                ...state,
                user : action.session,
                isAuthenticated : action.isAuthenticated
            }
        case "LOGOUT":
            return{
                ...state,
                user : action.newUser,
                isAuthenticated : action.isAuthenticated
            }
        default :
            return state;
    }
}

export default SessionReducer;