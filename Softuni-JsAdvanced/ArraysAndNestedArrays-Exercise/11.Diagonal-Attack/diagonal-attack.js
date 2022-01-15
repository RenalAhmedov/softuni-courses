function diagonalAttack(array = []) {
    let matrix = [];
    for(let i = 0; i < array.length; i++){
        let splitStringArray = array[i].split(' ');
        matrix[i] = [];
        for(let j = 0; j < splitStringArray.length; j++){
            matrix[i][j] = Number(splitStringArray[j]);
        }
    }
    let leftDiagonalSum = 0;
    let rightDiagonalSum = 0;
    for(let i = 0; i < matrix.length; i++){
        for(let j = 0; j < matrix[i].length; j++){
            if(i === j){
                leftDiagonalSum += matrix[i][j];
            }
            if((i+j) === matrix.length - 1){
                rightDiagonalSum += matrix[i][j];
            }
        }
    }
    if(leftDiagonalSum === rightDiagonalSum){
        for(let i = 0; i < matrix.length; i++){
            for(let j = 0; j < matrix[i].length; j++){
                if((i === j) || (i+j) === matrix.length - 1){
                    continue;
                }
                matrix[i][j] = leftDiagonalSum;
            }
        }
    }
    for(let row of matrix){
        console.log(row.join(' '));
    }
}