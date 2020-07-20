import React, { useState } from 'react';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../tools/Style'
import Swal from 'sweetalert2';
import { SigninUser } from '../../actions/UserAction';


const UserSignin = () => {

    const [user, setUser] = useState({
        name: "",
        lastName: "",
        email: "",
        password: "",
        password2: "",
        username: ""
    });

    const addValues = e=>{
        const {name, value} = e.target;
        setUser(prev=>({
            ...prev,
            [name] :  value
        }));
    };

    const registerUser = e =>{
        e.preventDefault();
        SigninUser(user).then(response=>{
            console.log(response);
            if(response.status === 200){
                Swal.fire('Hell yea', 'xd');
                window.localStorage.setItem('breve_sec_token', response.data.token);
            }
            else{
                Swal.fire('Error', 'Chingaste master'+response.status,'error');
            }
        });
        
    }

    return(
        <Container component='main' maxWidth='md' justify='center'>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Registro de usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={6}>
                            <TextField name='name' value={user.name} onChange={addValues} variant='outlined' fullWidth label='Ingrese su nombre'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='lastName' value={user.lastName} onChange={addValues} variant='outlined' fullWidth label='Ingrese su apellido'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='email' value={user.email} onChange={addValues} type='email' variant='outlined' fullWidth label='Ingrese su correo'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='username' value={user.username} onChange={addValues} variant='outlined' fullWidth label='Ingrese un nombre de usuario' helperText='Ejemplo {}'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='password' value={user.password} onChange={addValues} type='password' variant='outlined' fullWidth label='Ingrese una contraseña'/>
                        </Grid>
                        <Grid item xs={12} md={6}>
                            <TextField name='password2' value={user.password2} onChange={addValues} type='password' variant='outlined' fullWidth label='Confirme la contraseña'/>
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12} md={6}>
                            <Button type='submit' fullWidth variant='contained' color='primary' size='large' onClick={registerUser} style={style.submit}>
                                Registrarme
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    )
}

export default UserSignin;