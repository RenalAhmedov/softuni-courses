import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {createMovie} from "./requests.js";

const container = document.getElementById('container');
const addMovieSection = document.getElementById('add-movie');
const form = addMovieSection.querySelector('form');
addMovieSection.addEventListener('submit', onSubmit);
addMovieSection.remove();
addMovieSection.style.display = 'block';


export function showAddMovie(){
    hideAll();
    container.appendChild(addMovieSection);
    showFooter();
}

function onSubmit(e){
    e.preventDefault();
    const dataForm = new FormData(form);
    const data = {
        title: dataForm.get('title').trim(),
        description: dataForm.get('description').trim(),
        img: dataForm.get('imageUrl').trim(),
    }
    createMovie(data);
}