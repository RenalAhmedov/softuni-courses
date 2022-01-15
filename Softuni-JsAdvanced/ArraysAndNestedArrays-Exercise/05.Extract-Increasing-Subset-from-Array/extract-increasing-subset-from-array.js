function solve(array = []) {
    let finalArray = [array[0]];

    array.reduce((accumulator, currentValue) => {
        if(currentValue >= finalArray[finalArray.length - 1]) {
            finalArray.push(currentValue);
        }
    });

    return finalArray;
}
console.log(solve([1, 
    3, 
    8, 
    4, 
    10, 
    12, 
    3, 
    2, 
    24]));
console.log(solve([1, 
    2, 
    3,
    4]));
console.log(solve([20, 
    3, 
    2, 
    15,
    6, 
    1]));