import { deletePet, getPetById } from '../api/data.js';
import { html } from '../lib.js';
import { getUserData } from '../util.js';


const detailsTemplate = (pet, isOwner, onDelete) => html`
<section id="detailsPage">
    <div class="details">
        <div class="animalPic">
            <img src=${pet.image}>
        </div>
        <div>
            <div class="animalInfo">
                <h1>Name: ${pet.name}</h1>
                <h3>Breed: ${pet.breed}</h3>
                <h4>Age: ${pet.age}</h4>
                <h4>Weight: ${pet.weight}</h4>
                <h4 class="donation">Donation: 0$</h4>
            </div>
            <div class="actionBtn">
                ${petsControlTemplate(pet, isOwner, onDelete)}
            </div>
        </div>
    </div>
</section>
</main>`;

const petsControlTemplate = (pet, isOwner, onDelete) => {
    if (isOwner) {
        return html`
        <a href="/edit/${pet._id}" class="button">Edit</a>
        <a @click=${onDelete} href="javascript:void(0)" class="button">Delete</a>`;
    } else {
        return null;
    }
};

/*
<!-- Only for registered user and creator of the pets-->

      <!-- (Bonus Part) Only for no creator and user
     <a href="#" class="donate">Donate</a> -->
*/



export async function detailsPage(ctx) {
    const pet = await getPetById(ctx.params.id);

    const userData = getUserData();
    const isOwner = userData && userData.id == pet._ownerId;
    ctx.render(detailsTemplate(pet, isOwner, onDelete));

    async function onDelete() {
        const choice = confirm('Are you sure, you want to delete this pet?');

        if (choice) {
            await deletePet(ctx.params.id);
            ctx.page.redirect('/');
        }
    }

}