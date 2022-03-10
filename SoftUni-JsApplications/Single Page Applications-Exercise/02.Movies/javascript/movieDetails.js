import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {getLikes, getMovieInfo, giveLike, haveYouLiked, revokeLike} from "./requests.js";
import {deleteMovie} from "./requests.js";
import {showEditMovie} from "./editMovie.js";

const container = document.getElementById('container');

const movieDetailsSection = document.getElementById('movie-example');
movieDetailsSection.remove();
movieDetailsSection.style.display = 'block';



export async function showMovieDetails(movieId){
    hideAll();
    const [likesCount, movieInfo, hasLiked] = await Promise.all([getLikes(movieId),getMovieInfo(movieId), haveYouLiked(movieId)]);
    movieDetailsSection.innerHTML = `
        <div class="container">
            <div class="row bg-light text-dark">
                <h1>Movie title: ${movieInfo.title}</h1>
                <div class="col-md-8">
                    <img class="img-thumbnail" src="${movieInfo.img}"
                         alt="Movie">
                </div>
                <div class="col-md-4 text-center">
                    <h3 class="my-3 ">Movie Description</h3>
                    <p>${movieInfo.description}</p>
                    <a class="btn btn-danger" btn = "delete" href="#">Delete</a>
                    <a class="btn btn-warning" btn = "edit"  href="#">Edit</a>
                    <a class="btn btn-primary"  btn = "like" href="#">Like</a>
                    <span class="enrolled-span">Liked ${likesCount}</span>
                </div>
            </div>
        </div>`

    const btnEdit = movieDetailsSection.querySelector('a[btn="edit"]');
    btnEdit.addEventListener('click', () => {
        showEditMovie(movieInfo);
    })
    const btnDelete = movieDetailsSection.querySelector('a[btn="delete"]');
    btnDelete.addEventListener('click', () =>{
        deleteMovie(movieInfo._id);
    })
    const btnLike = movieDetailsSection.querySelector('a[btn="like"]');
    if(hasLiked.length > 0){
        btnLike.textContent = 'Revoke Like';
        btnLike.addEventListener('click', () => {revokeLike(hasLiked[0]._id, movieId)});
    }else{
        btnLike.addEventListener('click', () => giveLike(movieId));
    }

    const userLocal = JSON.parse(window.localStorage.getItem('movieUser'));
    if (!userLocal){
        btnLike.style.display = 'none';
    }
    if(!userLocal || userLocal.id !== movieInfo._ownerId){
        btnEdit.style.display = 'none';
        btnDelete.style.display = 'none';
    }
    container.appendChild(movieDetailsSection);
    showFooter();
}