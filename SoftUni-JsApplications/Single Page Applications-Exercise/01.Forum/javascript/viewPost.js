import {loadPosts} from "./loadPosts.js";

const mainPost = document.querySelector('main');
const body = document.querySelector('div.container');
export async function goToPost(id) {
    mainPost.remove();
    body.innerHTML = '';
    let mainSpecificPost = document.createElement('MAIN');
    body.appendChild(mainSpecificPost);
    const btnHome = document.querySelector('header nav a ');
    btnHome.addEventListener('click', () => {
        mainSpecificPost.remove();
        loadPosts();
    });
    const req = await fetch('http://localhost:3030/jsonstore/collections/myboard/posts/' + id, {
        method: 'get',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    const postData = await req.json();
    const thePost = `
<div class="comment">
    <div class="header">
        <img src="static/profile.png" alt="avatar">
        <p><span>${postData.username}</span> posted on <time>${postData.date}</time></p>
        <p class="post-content">${postData.post}</p>
    </div>
</div>`
    mainSpecificPost.innerHTML += thePost;
    const commentHeader = mainSpecificPost.querySelector('div.comment');
    if(postData.comments.length > 0){
        postData.comments.forEach((comment)=> {viewComment(comment)})
    }


    function viewComment(comment){
        commentHeader.innerHTML += `
<div id="user-comment">
    <div className="topic-name-wrapper">
        <div className="topic-name">
            <p><strong>${comment.username}</strong> commented on <time>${comment.date}</time></p>
            <div className="post-content">
                <p>${comment.comment}</p>
            </div>
        </div>
    </div>
</div>`
    }

    const form = document.createElement('FORM');
    const inputComment = document.createElement('TEXTAREA');
    inputComment.setAttribute('name', 'comment');
    inputComment.setAttribute('placeholder', 'Comment');
    const inputUsername = document.createElement('INPUT');
    inputUsername.setAttribute('name', 'username');
    inputUsername.setAttribute('placeholder', 'username');
    const btnSubmit = document.createElement('BUTTON');
    btnSubmit.textContent = 'SUBMIT';
    form.appendChild(inputComment);
    form.appendChild(inputUsername);
    form.appendChild(btnSubmit);
    document.querySelector('div.comment').appendChild(form);

    form.addEventListener('submit', sentComment);

    async function sentComment(e){
        e.preventDefault();
        const date = new Date();
        const dataForm = new FormData(form);
        const data = {
            username: dataForm.get('username'),
            comment: dataForm.get('comment'),
            date: date.getFullYear()+'-'+(date.getMonth()+1)+'-'+date.getDate()+'T'+date.getHours()+'-'
                +date.getMinutes()+'-'+date.getSeconds(),
        }
        console.log(postData);
        postData.comments.push(data);
        const reqComment = await fetch('http://localhost:3030/jsonstore/collections/myboard/posts/' + id,{
            method:'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(postData)
        })
        goToPost(id);
    }

}