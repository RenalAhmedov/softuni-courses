function solve(array = []) {
    array.sort((a, b) => a.length - b.length || a.localeCompare(b));
    for(let word of array) {
        console.log(word);
    }
}
solve(['alpha', 
'beta', 
'gamma'])
