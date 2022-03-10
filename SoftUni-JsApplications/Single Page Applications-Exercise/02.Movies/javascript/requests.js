import {showHome} from "./home.js";
import {showMovieDetails} from "./movieDetails.js";

export async function getMovies() {
    const req = await fetch('http://localhost:3030/data/movies');
    return await req.json();
}

export async function getMovieInfo(id) {
    const req = await fetch('http://localhost:3030/data/movies/' + id);
    return await req.json();
}

export async function createMovie(data) {
    const token = JSON.parse(window.localStorage.getItem('movieUser')).accessToken;
    const req = await fetch('http://localhost:3030/data/movies', {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token,
        },
        body: JSON.stringify(data)
    })
    showHome();
}

export async function updateMovie(data, movieId) {
    const token = JSON.parse(window.localStorage.getItem('movieUser')).accessToken;
    const req = await fetch('http://localhost:3030/data/movies/' + movieId, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token,
        },
        body: JSON.stringify(data)
    })
    showHome();
}

export async function deleteMovie(movieId) {
    const token = JSON.parse(window.localStorage.getItem('movieUser')).accessToken;
    const req = await fetch('http://localhost:3030/data/movies/' + movieId, {
        method: 'delete',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token
        },
    });
    showHome();
}


export async function getLikes(movieId) {
    const req = await fetch(`http://localhost:3030/data/likes?where=movieId%3D%22${movieId}%22&distinct=_ownerId&count`, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    return req.json();
}

export async function haveYouLiked(movieId) {
    const UserInfo = JSON.parse(window.localStorage.getItem('movieUser'));
    if (!UserInfo) {
        return [];
    }
    const token = UserInfo.accessToken;
    const userId = UserInfo.id;
    const req = await fetch(`http://localhost:3030/data/likes?where=movieId%3D%22${movieId}%22%20and%20_ownerId%3D%22${userId}%22`, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token,
        }
    })
    return await req.json();
}

export async function giveLike(movieId) {
    const token = JSON.parse(window.localStorage.getItem('movieUser')).accessToken;
    const req = await fetch(`http://localhost:3030/data/likes`, {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token,
        },
        body: JSON.stringify({movieId: movieId})
    })
    showMovieDetails(movieId);
}

export async function revokeLike(likedID, movieId) {
    const token = JSON.parse(window.localStorage.getItem('movieUser')).accessToken;
    const req = await fetch(`http://localhost:3030/data/likes/` + likedID, {
        method: 'delete',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': token,
        }
    })
    showMovieDetails(movieId);
}


export async function login(data) {
    try {
        const req = await fetch('http://localhost:3030/users/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        })

        if (req.ok === false) {
            const error = await req.json();
            console.log(error)
            throw new Error(error.message);
        }

        const respond = await req.json();
        window.localStorage.setItem('movieUser', JSON.stringify({
            accessToken: respond.accessToken,
            id: respond._id,
            email: respond.email,
        }))
        showHome();
    } catch (err) {
        alert(err.message);
    }
}

export async function signUp(data) {
    try {
        const req = await fetch('http://localhost:3030/users/register', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        })
        if (req.ok === false){
            const result = await req.json();
            throw new Error(result.message);
        }

        login(data);
    }catch(err){
        alert(err.message)
    }

}

export async function logout() {
    const req = await fetch('http://localhost:3030/users/logout', {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
            'X-Authorization': JSON.parse(window.localStorage.getItem('movieUser')).accessToken,
        },
    })
    window.localStorage.removeItem('movieUser');
    showHome();
}