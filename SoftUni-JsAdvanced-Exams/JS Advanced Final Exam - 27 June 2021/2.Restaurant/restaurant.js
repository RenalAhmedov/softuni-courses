class Restaurant {
    constructor(budget) {
        this.budgetMoney = Number(budget);
        this.menu = {};
        this.stockProducts = {};
        this.history = [];
    }

    loadProducts(productArr) {
        for (let strSequence of productArr) {
            let [product, quantity, price] = strSequence.split(' ');
            quantity = Number(quantity);
            price = Number(price);

            if (price <= this.budgetMoney) {
                this.budgetMoney -= price;
                this.history.push(`Successfully loaded ${quantity} ${product}`);

                if (!this.stockProducts.hasOwnProperty(product)) {
                    this.stockProducts[product] = 0;
                }
                this.stockProducts[product] += quantity;
            } else {
                this.history.push(
                    `There was not enough money to load ${quantity} ${product}`
                );
            }
        }

        return this.history.join('\n');
    }

    addToMenu(meal, productArr, price) {
        let neededProducts = [];

        for (let strSequence of productArr) {
            let [product, quantity] = strSequence.split(' ');
            quantity = Number(quantity);

            neededProducts.push([product, quantity]);
        }

        if (this.menu.hasOwnProperty(meal)) {
            return `The ${meal} is already in the our menu, try something different.`;
        }

        this.menu[meal] = { products: neededProducts, price };
        let mealCount = Object.keys(this.menu).length;

        if (mealCount == 1) {
            return `Great idea! Now with the ${meal} we have 1 meal in the menu, other ideas?`;
        } else if (!mealCount || mealCount >= 2) {
            return `Great idea! Now with the ${meal} we have ${mealCount} meals in the menu, other ideas?`;
        }
    }

    showTheMenu() {
        let menuEntries = Object.entries(this.menu);
        let mealCount = menuEntries.length;

        if (!mealCount) {
            return 'Our menu is not ready yet, please come later...';
        }

        let stringArr = [];
        for (let [meal, mealInfoObj] of menuEntries) {
            stringArr.push(`${meal} - $ ${mealInfoObj.price}`);
        }

        return stringArr.join('\n');
    }

    makeTheOrder(meal) {
        if (!this.menu.hasOwnProperty(meal)) {
            return `There is not ${meal} yet in our menu, do you want to order something else?`;
        }

        for (let meal in this.menu) {
            let products = this.menu[meal].products;
            for (let [product, quantity] of products) {
                if (!this.stockProducts.hasOwnProperty(product)) {
                    return `For the time being, we cannot complete your order (${meal}), we are very sorry...`;
                }

                if (this.stockProducts[product] < quantity) {
                    return `For the time being, we cannot complete your order (${meal}), we are very sorry...`;
                }
            }

            products.forEach(([product, quantity]) => {
                this.stockProducts[product] - quantity;
            });

            this.budgetMoney += this.menu[meal].price;
            return `Your order (${meal}) will be completed in the next 30 minutes and will cost you ${this.menu[meal].price}.`;
        }
    }
}
let kitchen = new Restaurant(1000);
console.log(kitchen.loadProducts(['Banana 10 5', 'Banana 20 10', 'Strawberries 50 30', 'Yogurt 10 10', 'Yogurt 500 1500', 'Honey 5 50']));
console.log(kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
console.log(kitchen.showTheMenu());
console.log(kitchen.makeTheOrder('frozenYogurt'));

