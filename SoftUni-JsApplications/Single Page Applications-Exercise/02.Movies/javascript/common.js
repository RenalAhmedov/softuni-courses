import {showNavBar} from "./nav.js";

const container = document.getElementById('container');
const footerSection = document.querySelector('footer.page-footer');
footerSection.remove();
footerSection.style.display = 'block';

export function showFooter(){
    container.appendChild(footerSection);
}



export function hideAll(){
    while(container.firstChild){
        container.removeChild(container.firstChild);
    }
    showNavBar();
}