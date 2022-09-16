import { TextField, Button } from '@mui/material';
import axios from 'axios';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './style.css';

async function loginUser(username: string | undefined) {
    await axios.post('/api/auth/login', {
        "username": username
    }, {
        withCredentials: true
    }).then(res => {
        console.log(res.data);
    });
}

function Login() {
    const [username, setUserName] = useState<string>();
    const navigate = useNavigate();

    return <div>
        <h1>WebApp</h1>
        <p>Enter a username</p>
        <TextField id="standard-basic" label="Username" variant="standard" onChange={e => setUserName(e.target.value)} />
        <Button variant="contained" onClick={async () => await loginUser(username).then(() => navigate('/', {replace: true}))}>Login</Button>
    </div>;
}

export default Login;