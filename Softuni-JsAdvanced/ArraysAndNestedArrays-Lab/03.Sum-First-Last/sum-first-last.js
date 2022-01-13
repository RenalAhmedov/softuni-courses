function solve(array){ 
    let firstIndex = Number(array[0]);
    let lastIndex = Number(array.slice(-1));
    
    console.log(firstIndex + lastIndex);

}
solve(['20', '30', '40'])