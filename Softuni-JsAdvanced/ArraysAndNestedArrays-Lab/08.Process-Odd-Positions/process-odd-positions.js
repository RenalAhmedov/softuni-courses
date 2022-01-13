function solve(array){ 
    let newArr = [];
    
    for (let i = 1; i < array.length; i += 2) {
        newArr.push(array[i] * 2);
        
    }
    newArr.reverse();
    return newArr.join(' ');
}
// console.log(solve([10, 15, 20, 25]));
// console.log(solve([3, 0, 10, 4, 7, 3]));

