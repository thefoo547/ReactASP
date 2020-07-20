import React from 'react';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../tools/Style';

const UserProfile = ()=>{
    return(
        <Container component='main' maxWidth='md' justify='center'>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>Perfil de Usuario</Typography>
            </div>
            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={6}>
                        <TextField name='name' variant='outlined' fullWidth label='Nombres'/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name='lastname' variant='outlined' fullWidth label='Apellidos'/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name='email' variant='outlined' fullWidth label='Correo'/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name='username' variant='outlined' fullWidth label='Nombre de usuario'/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name='password' type='password' variant='outlined' fullWidth label='Contraseña'/>
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name='password2' type='password' variant='outlined' fullWidth label='Confirme la contraseña'/>
                    </Grid>
                </Grid>
                <Grid container justify='center'>
                    <Grid item xs={12} md={6}>
                        <Button color='primary' type='submit' fullWidth variant='contained' size='large' style={style.submit}>Guardar Cambios</Button>
                    </Grid>
                </Grid>
            </form>
        </Container>      
    );
}

export default UserProfile;