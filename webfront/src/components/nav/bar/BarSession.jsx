import React from 'react';
import { Toolbar, IconButton, Typography, Button, Avatar } from '@material-ui/core';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles((theme)=>({
    desktopSection: {
        display: 'none',
        [theme.breakpoints.up('md')] :{
            display: 'flex'
        }
    },
    mobileSection : {
        display: 'flex',
        [theme.breakpoints.up('md')] :{
            display: 'none'
        }
    },
    grow:{
        flexGrow: 1
    },
    avatarSize : {
        width:40,
        height:40 
    }
}))

const BarSession = () => {
    const classes = useStyles();
    return (
        <Toolbar>
            <IconButton>
                <i className='material-icons'>menu</i>
            </IconButton>
            <Typography variant='h6'>Cursos Breves</Typography>
            <div className={classes.grow}></div>


            <div className={classes.desktopSection}>
                <Button color='inherit'>Salir</Button>
                <Button color='inherit'>{"Nombre de usuario"}</Button>
                <Avatar></Avatar>
            </div>


            <div className={classes.mobileSection}>
                <IconButton color='inherit'>
                    <i className='material-icons'>more_vert</i>
                </IconButton>
            </div>


        </Toolbar>
    );
};

export default BarSession;