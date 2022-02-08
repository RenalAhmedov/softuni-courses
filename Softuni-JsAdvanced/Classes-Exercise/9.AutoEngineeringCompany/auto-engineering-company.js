function autoEngineeringCompany(cars) {
    
    const carOutputMap = new Map();

    cars.forEach(car => {
        
        const [brand, model, producedCars] = car.split(' | ');

        if (!carOutputMap.has(brand)) {

            carOutputMap.set(brand, {});
        
        }

        if (!carOutputMap.get(brand)[model]) {
            
            carOutputMap.get(brand)[model] = 0;

        }

        carOutputMap.get(brand)[model] += Number(producedCars);

    });

    for (const [key, value] of carOutputMap) {
        
        let lastPrintedBrand = '';

        if (lastPrintedBrand !== key) {
            
            console.log(`${key}`);
            lastPrintedBrand = key;

        }

        for (const key in value) {
            
            console.log(`###${key} -> ${value[key]}`);

        }

    }

}