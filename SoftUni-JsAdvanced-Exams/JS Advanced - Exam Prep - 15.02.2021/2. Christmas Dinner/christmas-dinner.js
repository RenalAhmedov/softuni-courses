class ChristmasDinner {
    constructor(budget) {
        if (budget < 0) {
            throw Error('The budget cannot be a negative number');
        }
 
        this.budget = budget;
        this.dishes = [];
        this.products = [];
        this.guests = {};
    }
 
    shopping(products) {
        const [product, price] = products;
        if (this.budget < price) {
            throw Error('Not enough money to buy this product');
        }
 
        this.budget -= price;
        this.products.push(product);
        return `You have successfully bought ${product}!`
    }
 
    recipes(recipe) {
        const { recipeName, productsList } = recipe;
 
        for (const product of productsList) {
            if (!this.products.includes(product)) {
                throw Error('We do not have this product');
            }
        }
 
        this.dishes.push(recipe);
        return `${recipeName} has been successfully cooked!`;
    }
 
    inviteGuests(guest, dish) {
        const recipes = this.dishes.map(item => item.recipeName);
        if (!recipes.includes(dish)) {
            throw Error('We do not have this dish');
        }
 
        if (this.guests.hasOwnProperty(guest)) {
            throw Error('This guest has already been invited');
        }
 
        this.guests[guest] = dish;
        return `You have successfully invited ${guest}!`;
    }
 
    showAttendance() {
        const guests = Object.entries(this.guests);
        let result = [];
        for (const item of guests) {
            const [guest, dish] = item;
            const index = this.dishes.findIndex(x => x.recipeName === dish);
            result.push(`${guest} will eat ${dish}, which consists of ${this.dishes[index].productsList.join(', ')}`);
        }
 
        return result.join('\n');
    }
}

let dinner = new ChristmasDinner(300);

dinner.shopping(['Salt', 1]);
dinner.shopping(['Beans', 3]);
dinner.shopping(['Cabbage', 4]);
dinner.shopping(['Rice', 2]);
dinner.shopping(['Savory', 1]);
dinner.shopping(['Peppers', 1]);
dinner.shopping(['Fruits', 40]);
dinner.shopping(['Honey', 10]);

dinner.recipes({
    recipeName: 'Oshav',
    productsList: ['Fruits', 'Honey']
});
dinner.recipes({
    recipeName: 'Folded cabbage leaves filled with rice',
    productsList: ['Cabbage', 'Rice', 'Salt', 'Savory']
});
dinner.recipes({
    recipeName: 'Peppers filled with beans',
    productsList: ['Beans', 'Peppers', 'Salt']
});

dinner.inviteGuests('Ivan', 'Oshav');
dinner.inviteGuests('Petar', 'Folded cabbage leaves filled with rice');
dinner.inviteGuests('Georgi', 'Peppers filled with beans');

console.log(dinner.showAttendance());
