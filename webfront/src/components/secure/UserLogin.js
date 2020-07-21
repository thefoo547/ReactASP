import React, { useState } from 'react';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import style from '../tools/Style';
import { LoginUser } from '../../actions/UserAction';
import Swal from 'sweetalert2';

const UserLogin = ()=>{
    const [user, setUser] = useState({
        email: "",
        password: ""
    });

    const updUser = e =>{
        const {name, value} = e.target;
        setUser(prev=>({
            ...prev,
            [name] : value
        }));
    }

    const loginUser = e =>{
        e.preventDefault();
        LoginUser(user).then(res=>{
            if(res.status === 200){
                
                Swal.fire("Bienvenido");
                window.localStorage.setItem('breve_sec_token', res.data.token);
            }
        });
    }

    return(
        <Container maxWidth='xs'>
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.icon}/>
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Login de Usuario
                </Typography>
                <form style={style.form} onSubmit={loginUser}>
                    <TextField type='email' variant='outlined' value={user.email} onChange={updUser} label='Ingrese nombre de usuario' name='email' on fullWidth margin='normal'/>
                    <TextField type='password' value={user.password} onChange={updUser} variant='outlined' label='Ingrese contraseña' name='password' fullWidth margin='normal'/>
                    <Button type='submit' fullWidth variant='contained' color='primary' style={style.submit}>Ingresar</Button>
                </form>
            </div>

        </Container>
    )
}

export default UserLogin;