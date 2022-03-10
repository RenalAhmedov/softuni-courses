import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {login} from "./requests.js";

const container = document.getElementById('container');

const formLoginSection = document.getElementById('form-login');
formLoginSection.remove();
formLoginSection.style.display = 'block';

const formForLogin = formLoginSection.querySelector('form.text-center');
formForLogin.addEventListener('submit', onLogin);


export function showFormLogin(){
    hideAll();
    container.appendChild(formLoginSection);
    showFooter();
}

function onLogin(e){
    e.preventDefault();
    let info = new FormData(formForLogin);
    let data = {
        email: info.get('email'),
        password:info.get('password'),
    }
    login(data);
}