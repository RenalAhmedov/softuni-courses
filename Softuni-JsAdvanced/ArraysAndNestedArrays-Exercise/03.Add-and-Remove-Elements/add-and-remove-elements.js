function solve(array = []) {
    let initialValue = 1;
    let arrayToPrint = [];
    for(let command of array) {
        switch(command) {
            case 'add':
                arrayToPrint.push(initialValue);
                break;
            case 'remove':
                arrayToPrint.pop();
                break;
        }
        initialValue++;
    }
    if(arrayToPrint.length === 0) {
        console.log('Empty');
        return;
    }
    for(let element of arrayToPrint) {
        console.log(element);
    }
}
solve(['add', 
'add', 
'add', 
'add']);
solve(['add', 
'add', 
'remove', 
'add', 
'add']);
solve(['remove', 
'remove', 
'remove']);