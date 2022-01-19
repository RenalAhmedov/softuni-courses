function solve(inputArr){ 
    let productCatalogue = {};
    for (let i = 0; i < inputArr.length; i++) {
        [productName, productPrice] = inputArr[i].split(' : ')
        productPrice = Number(productPrice);
        let initial = productName[0].toUpperCase();

        if (productCatalogue[initial] === undefined) {
            productCatalogue[initial] = {};      
        }

        productCatalogue[initial][productName] = productPrice;
    }
    let result =[];
    let initialSorted = Object.keys(productCatalogue).sort((a, b) => a.localeCompare(b));
    for (const key of initialSorted) {
       let products = Object.entries(productCatalogue[key]).sort((a, b) => a[0].localeCompare(b[0]));
       result.push(key)
       let productAsString = products.map(x => `  ${x[0]}: ${x[1]}`).join('\n');
       result.push(productAsString);
    }

    return result.join('\n');
}

console.log(solve(['Appricot : 20.4',
'Fridge : 1500',
'TV : 1499',
'Deodorant : 10',
'Boiler : 300',
'Apple : 1.25',
'Anti-Bug Spray : 15',
'T-Shirt : 10']
));