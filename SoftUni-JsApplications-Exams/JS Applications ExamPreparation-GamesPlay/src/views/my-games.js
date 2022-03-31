import { allGames } from '../api/data.js';
import { html } from '../lib.js';
import { getUserData } from '../util.js';


const myGamesTemplate = (games) => html`
<section id="catalog-page">
    <h1>All Games</h1>
    ${games.length == 0 
      ? html` <h3 class="no-articles">No articles yet</h3>`
      : html`${games.map(gamePreview)};   
    </div>`}    
    
</section>`;

const gamePreview = (game) => html`
<div class="allGames">
        <div class="allGames-info">
            <img src=${game.imageUrl}>
            <h6>${game.category}</h6>
            <h2>${game.title}</h2>
            <a href="/details/${game._id}" class="details-button">Details</a>
        </div>`;


export async function myGamePage(ctx) {
    const userData = getUserData();
    if(userData){
        const games = await allGames(userData.id);
        ctx.render(myGamesTemplate(games));       
    }else{
        const games = await allGames();
        ctx.render(myGamesTemplate(games));
    }
}