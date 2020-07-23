import React from 'react';
import { AppBar } from '@material-ui/core';
import BarSession from './bar/BarSession';


const AppNavbar = () => {
    return(
        <AppBar position='sticky'>
            <BarSession/>
        </AppBar>
    );
}

export default AppNavbar;