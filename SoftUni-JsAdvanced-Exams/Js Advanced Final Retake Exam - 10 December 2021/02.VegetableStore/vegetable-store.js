class VegetableStore {
    constructor(owner, location) {
        this.owner = owner;
        this.location = location;
        this.availableProducts = [];
    }
    loadingVegetables(vegetables) {
        let added = [];
        vegetables.forEach((el) => {
            let [type, quantity, price] = el.split(' ');
            quantity = Number(quantity);
            price = Number(price);

            let found = this.availableProducts.find(el => el.type === type);
            if (found) {
                found.quantity += quantity;

                if (price > found.price) {
                    found.price = price;
                }
            } else {
                this.availableProducts.push({ type, quantity, price });
                added.push(type);
            }
        })
        return `Successfully added ${added.join(", ")}`;
    };
    buyingVegetables(selectedProducts) {
        let totalPrice = 0;
        
        selectedProducts.forEach((el) => {
            let [type, quantity] = el.split(' ');
            quantity = Number(quantity)

            let found = this.availableProducts.find(el => el.type === type);
            if (!found) {
                throw new Error(`${type} is not available in the store, your current bill is $${totalPrice.toFixed(2)}.`)
            }
            if (quantity > found.quantity) {
                throw new Error(`The quantity ${quantity} for the vegetable ${type} is not available in the store, your current bill is $${totalPrice.toFixed(2)}.`)
            }

            totalPrice += found.price * quantity;
            found.quantity -= quantity;
        });
        return `Great choice! You must pay the following amount $${totalPrice.toFixed(2)}.`
    }
    rottingVegetable(type, quantity) {
        let found = this.availableProducts.find(el => el.type === type)

        if (!found) {
            throw new Error(`${type} is not available in the store.`);
        }

        if (found.quantity < quantity) {
            found.quantity = 0;
            return `The entire quantity of the ${type} has been removed.`;
        }

        found.quantity -= quantity
        return `Some quantity of the ${type} has been removed.`
    }
    revision (){
        let result = [];
        result.push('Available vegetables:') 

        this.availableProducts
        .sort((a, b) => a.price - b.price)
        .forEach(x => result.push(`${x.type}-${x.quantity}-$${x.price}`));

        result.push(`The owner of the store is ${this.owner}, and the location is ${this.location}.`)

        return result.join('\n');
    }
}



let vegStore = new VegetableStore("Jerrie Munro", "1463 Pette Kyosheta, Sofia");
console.log(vegStore.loadingVegetables(["Okra 2.5 3.5", "Beans 10 2.8", "Celery 5.5 2.2", "Celery 0.5 2.5"]));
console.log(vegStore.rottingVegetable("Okra", 1));
console.log(vegStore.rottingVegetable("Okra", 2.5));
console.log(vegStore.buyingVegetables(["Beans 8", "Celery 1.5"]));
console.log(vegStore.revision());