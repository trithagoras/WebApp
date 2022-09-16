import Cookies from "js-cookie";
import { Button, Card, CardMedia, CardActions } from '@mui/material';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useEffect, useRef, useState } from "react";
import ThumbUpIcon from '@mui/icons-material/ThumbUp';
import ThumbDownIcon from '@mui/icons-material/ThumbDown';
import './style.css'
import ImageDto from "../dtos/imageDto";

async function logout() {
    await axios.post('/api/auth/logout', {}, {
        withCredentials: true
    });
}

function MainApp() {
    const navigate = useNavigate();
    const [image, setImage] = useState<ImageDto>();
    const [value, setValue] = useState<number>();

    const isFirstRender = useRef(true)

    const getImage = () => {
        axios.get('/api/app/image', {
            withCredentials: true
        }).then(res => {
            setImage(res.data);
        });
    }

    const getLiked = () => {
        axios.get(`/api/app/liked?imageId=${image?.imageId}`, {
            withCredentials: true
        }).then(res => {
            setValue(res.data);
        })
    }

    const interactImage = (value: number) => {
        axios.post("/api/app/image", {
            imageId: image?.imageId,
            value: value
        }, {
            withCredentials: true
        }).then(() => {
            getImage();
        })
    }

    useEffect(() => {
        if (isFirstRender.current) {
            isFirstRender.current = false;
            return;
        }

        getImage();
    }, []);

    useEffect(() => {
        if (image === undefined) {
            return;
        }

        getLiked();
    }, [image]);

    return <div id="app-content">
        <div style={{ display: 'flex', justifyContent: 'space-between', paddingBottom: 10 }}>
            <h2>Main App</h2>
            <Button sx={{ maxHeight: 50 }} variant="contained" onClick={async () => await logout().then(() => navigate('/login', {replace: true}))}>Logout</Button>
        </div>
        <Card sx={{ maxWidth: 400 }}>
            <CardMedia
                component="img"
                height="300"
                image={image?.imageBase64}
            />
            <CardActions style={{ display: 'flex', justifyContent: 'space-between', paddingBottom: 10 }}>
                { (value! < 1)
                    ? 
                    <Button onClick={() => interactImage(1)} size="small" startIcon={<ThumbUpIcon />}>I like this image</Button> 
                    :
                    <Button onClick={() => getImage()} variant="contained" size="small" startIcon={<ThumbUpIcon />}>I still like this image</Button> 
                }
                { (value! > -1)
                    ? 
                    <Button onClick={() => interactImage(-1)} size="small" endIcon={<ThumbDownIcon />}>I don't like this image</Button>
                    :
                    <Button onClick={() => getImage()} variant="contained" size="small" endIcon={<ThumbDownIcon />}>I still don't like this image</Button>
                }
                
            </CardActions>
        </Card>
    </div>;
}

export default MainApp;