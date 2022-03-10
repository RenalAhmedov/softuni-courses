import {goToPost} from "./viewPost.js";
const btnHome = document.querySelector('header nav a ');
btnHome.addEventListener('click', loadPosts);
const body = document.querySelector('div.container');
const mainPost = document.querySelector('main');
const form = document.querySelector('form');
form.addEventListener('submit', onSubmit);
const btnCancel = form.querySelector('button.cancel');
btnCancel.addEventListener('click', () => {
    form.reset()
});
const topicContainer = document.querySelector('div.topic-title');
topicContainer.addEventListener('click', onClick)
console.log('Start');
mainPost.remove();

export async function loadPosts(){
    body.appendChild(mainPost);
    const req = await fetch('http://localhost:3030/jsonstore/collections/myboard/posts');
    const serverPosts = await req.json();
    topicContainer.innerHTML = '';
    Object.values(serverPosts).forEach((postData) => {
        createTopic(postData);
    })
}


function createTopic(postData){
    topicContainer.innerHTML +=`
<div class="topic-container">
    <div class="topic-name-wrapper">
        <div class="topic-name">
            <a href="#" id = ${postData._id} class="normal">
                <h2>${postData.title}</h2>
            </a>
            <div class="columns">
                <div>
                    <p>Date: <time>${postData.date}</time></p>
                    <div class="nick-name">
                        <p>Username: <span>${postData.username}</span></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
`
}

async function onSubmit(e) {
    e.preventDefault();
    const formData = new FormData(form);
    let date = new Date();
    const data = {
        title: formData.get('topicName').trim(),
        username: formData.get('username').trim(),
        post: formData.get('postText').trim(),
        date: date.getFullYear()+'-'+(date.getMonth()+1)+'-'+date.getDate()+'T'+date.getHours()+'-'
            +date.getMinutes()+'-'+date.getSeconds(),
        comments: [],
    }

    try {
        const req = await fetch('http://localhost:3030/jsonstore/collections/myboard/posts', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        })

        if(req.ok === false){
            const serverError = await req.json();
            throw new Error(serverError.message);
        }
        form.reset();
        loadPosts();
    }catch(err){
        alert(err.message);
    }
}

function onClick(e){
    e.preventDefault();
    let target = e.target
    if (target.tagName === 'H2'){
        target = target.parentElement;
    }
    if (target.tagName === 'A'){
        const postID = target.getAttribute('id');
        goToPost(postID);
    }
}