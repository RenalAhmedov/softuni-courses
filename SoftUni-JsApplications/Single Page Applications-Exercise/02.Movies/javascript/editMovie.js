import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {updateMovie} from "./requests.js";

const container = document.getElementById('container');
const editMovieSection = document.getElementById('edit-movie');
const editForm = editMovieSection.querySelector('form');
editMovieSection.remove();
editMovieSection.style.display = 'block';


export function showEditMovie(movieInfo){
    console.log(movieInfo);
    hideAll();
    editForm.querySelector('input[name="title"]').value = movieInfo.title;
    editForm.querySelector('textarea[name="description"]').value = movieInfo.description;
    editForm.querySelector('input[name="imageUrl"]').value = movieInfo.img;
    container.appendChild(editMovieSection);
    showFooter();

    editForm.addEventListener('submit', onSubmit);
    function onSubmit(e){
        e.preventDefault();
        const sentData = new FormData(editForm);
        const data = {
            title: sentData.get('title'),
            description: sentData.get('description'),
            img: sentData.get('imageUrl'),
        }
        updateMovie(data, movieInfo._id);
    }
}