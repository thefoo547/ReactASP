import React, { useState } from 'react';
import {ThemeProvider as MuiThemeProvider} from '@material-ui/core/styles';
import Theme from './theme/Theme'
import { Grid } from '@material-ui/core';
import UserSignin from './components/secure/UserSignin';
import UserLogin from './components/secure/UserLogin';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import AppNavbar from './components/nav/AppNavbar';

import { useStateValue } from './context/Store';

function App() {
  const [{sessionUser}, dispatch] = useStateValue();
  const [startApp, setStartApp] = useState(false);

  return (
    <Router>
      <MuiThemeProvider theme={Theme}>
        <AppNavbar/>

        
        <Grid container>
          <Switch>
            <Route exact path='/auth/login' component={UserLogin}/>
            <Route exact path='/auth/register' component={UserSignin}/>
          </Switch>
        </Grid>
      </MuiThemeProvider>
    </Router>
  );
}

export default App;
