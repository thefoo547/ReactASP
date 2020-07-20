import React from 'react';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import style from '../tools/Style';

const UserLogin = ()=>{
    return(
        <Container maxWidth='xs'>
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.icon}/>
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Login de Usuario
                </Typography>
                <form style={style.form}>
                    <TextField variant='outlined' label='Ingrese nombre de usuario' name='username' fullWidth margin='normal'/>
                    <TextField type='password' variant='outlined' label='Ingrese contraseña' name='password' fullWidth margin='normal'/>
                    <Button type='submit' fullWidth variant='contained' color='primary' style={style.submit}>Ingresar</Button>
                </form>
            </div>

        </Container>
    )
}

export default UserLogin;