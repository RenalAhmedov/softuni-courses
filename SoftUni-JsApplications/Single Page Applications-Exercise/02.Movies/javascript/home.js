import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {getMovies} from "./requests.js";
import {showMovieDetails} from "./movieDetails.js";
import {showAddMovie} from "./addMovie.js";

const container = document.getElementById('container');

const homePageBanner = document.getElementById('home-page');
homePageBanner.remove();
homePageBanner.style.display = 'block';

const h1Movies = document.querySelector('h1.text-center');
h1Movies.remove();

const btnAddMovie = document.getElementById('add-movie-button');
btnAddMovie.remove();
btnAddMovie.style.display = 'block';
btnAddMovie.addEventListener('click', showAddMovie);

const movieSection = document.getElementById('movie');
movieSection.remove();
movieSection.style.display = 'block';

const movieBox = movieSection.querySelector('div.card-deck.d-flex.justify-content-center');
movieBox.addEventListener('click', (e) => {
    e.preventDefault();
    let target = e.target;
    if (target.tagName === 'BUTTON'){
        target = target.parentNode;
    }
    if(target.tagName === 'A'){
        const movieId = target.getAttribute('id');
        showMovieDetails(movieId);
    }

})

export async function showHome(){
    hideAll();
    container.appendChild(homePageBanner);
    container.appendChild(h1Movies);
    if (window.localStorage.getItem('movieUser')){
        container.appendChild(btnAddMovie);
    }

    const requestMovies = await getMovies();
    movieBox.textContent = ''
    Object.values(requestMovies).map(createMovieCard);
    container.appendChild(movieSection);
    showFooter();
}

function createMovieCard(movie){
    const element = `
<div class="card mb-4">
    <img class="card-img-top" src="${movie.img}"
         alt="Card image cap" width="400">
        <div class="card-body">
            <h4 class="card-title">${movie.title}</h4>
        </div>
        <div class="card-footer">
            <a href="#" id="${movie._id}">
                <button type="button" class="btn btn-info">Details</button>
            </a>
        </div>
</div>
`
    movieBox.innerHTML += element;
}