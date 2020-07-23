const initialState = {
    open : false,
    msg : ''
}

const OpenSnackbarReducer = (state = initialState, action) =>{
    switch(action.type){
        case 'OPEN_SNACKBAR':
            return {
                ...state,
                open : action.openMessage.open,
                msg: action.openMessage.msg
            };
        default :
            return state;
    }
}

export default OpenSnackbarReducer;