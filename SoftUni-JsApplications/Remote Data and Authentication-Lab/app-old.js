const baseUrl = 'http://localhost:3030'

window.addEventListener('load', () => {
    console.log('loaded')
    const guestElement = document.querySelector('.guest');
    console.log(guestElement);
    guestElement.style.display = 'block';

    fetch(`${baseUrl}/jsonstore/cookbook/recipes`)
        .then(res => res.json())
        .then(recipes => {
            renderRecipes(Object.values(recipes));
        });
});

function renderRecipes(recipes) {
    const mainElement = document.querySelector('main');

    mainElement.innerHTML = '';

    recipes.forEach(x => {
        mainElement.appendChild(createRecipe(x));
    });
}

function createRecipe(recipe) {
    let recipeElement = document.createElement('article');
    recipeElement.classList.add('preview');

    recipeElement.addEventListener('click', () => {
        fetch(`${baseUrl}/jsonstore/cookbook/details/${recipe._id}`)
            .then(res => res.json())
            .then(details => {
                // Problem: need to remove event listeners
                const mainElement = document.querySelector('main');
                mainElement.innerHTML = '';

                mainElement.appendChild(renderDetailedRecipe(details))
            });
    });

    // WARNING XSS!!!
    recipeElement.innerHTML = `
        <div class="title">
            <h2>${recipe.name}</h2>
        </div>
        <div class="small">
            <img src="${recipe.img}">
        </div>
    `;

    return recipeElement;
}

function renderDetailedRecipe(details) {
    let recipeElement = document.createElement('article');

    recipeElement.innerHTML = `
        <article>
            <h2>${details.name}</h2>
            <div class="band">
                <div class="thumb">
                    <img src="${details.img}">
                </div>
                <div class="ingredients">
                    <h3>Ingredients:</h3>
                    <ul>
                        ${details.ingredients.map(x => `<li>${x}</li>`).join('')}
                    </ul>
                </div>
            </div>
            <div class="description">
                <h3>Preparation:</h3>
                ${details.steps.map(x => `<p>${x}</p>`).join('')}
            </div>
        </article>
    `;

    return recipeElement;
}
