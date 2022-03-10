import {showHome} from "./home.js";
import {showAddMovie} from "./addMovie.js";
import {showMovieDetails} from "./movieDetails.js";
import {showEditMovie} from "./editMovie.js";
import {showFormLogin} from "./login.js";
import {showSignUp} from "./signUp.js";
import {showFooter} from "./common.js";
import {logout} from "./requests.js";



const container = document.getElementById('container');
const navBar = document.querySelector('nav.navbar');
navBar.remove();
navBar.style.display = 'flex';
const rawLinks = Array.from(navBar.querySelectorAll('a'));
const links = {
    'movies': rawLinks[0],
    'welcome': rawLinks[1],
    'logout': rawLinks[2],
    'login': rawLinks[3],
    'register': rawLinks[4],
}

links.movies.addEventListener('click', showHome);
links.logout.addEventListener('click', logout);
links.login.addEventListener('click', showFormLogin);
links.register.addEventListener('click', showSignUp);

export function showNavBar() {
    if (window.localStorage.getItem('movieUser')) {
        const userEmail = JSON.parse(window.localStorage.getItem('movieUser')).email;
        links.welcome.textContent = `Welcome, ${userEmail}`
        links.login.style.display = 'none';
        links.logout.style.display = 'block';
        links.register.style.display = 'none';
    } else {
        links.welcome.textContent = '';
        links.login.style.display = 'block';
        links.logout.style.display = 'none';
        links.register.style.display = 'block';
    }
    container.appendChild(navBar);
}