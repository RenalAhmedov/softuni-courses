import {showFooter} from "./common.js";
import {hideAll} from "./common.js";
import {signUp} from "./requests.js";

const container = document.getElementById('container');

const formSignUpSection = document.getElementById('form-sign-up');
const form = formSignUpSection.querySelector('form');
form.addEventListener('submit', onSubmit);
formSignUpSection.remove();
formSignUpSection.style.display = 'block';

export function showSignUp(){
    hideAll();
    container.appendChild(formSignUpSection);
    showFooter();
}


function onSubmit(e){
    e.preventDefault();
    const formData = new FormData(form);
    const data = {
        email: formData.get('email').trim(),
        password: formData.get('password').trim()
    }
    if(data.password !== formData.get('repeatPassword') || data.password === ''){
        alert('Passwords did NOT match or EMPTY');
    }
    if(data.email === ''){
        alert('Email can NOT be EMPTY');
    }

    signUp(data);

}