function solve(array) {
    let newArr = [];
    for (let i = 0; i < array.length; i++) {

        if(array[i] < 0){
            newArr.unshift(array[i]);
        }else if(array[i] >= 0 ){
            newArr.push(array[i]);
        }
    }
    console.log(newArr);
}
solve([7, -2, 8, 9]);
