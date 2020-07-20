import React from 'react';
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider';
import Theme from './theme/Theme'
import { TextField, Button } from '@material-ui/core';
import UserSignin from './components/secure/UserSignin';
import UserLogin from './components/secure/UserLogin';
import UserProfile from './components/secure/UserProfile';

function App() {
  return (
    <MuiThemeProvider theme={Theme}>
      <UserSignin/>
    </MuiThemeProvider>
  );
}

export default App;
