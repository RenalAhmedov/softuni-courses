function sortArray(array, criteria) {

    return criteria === 'asc' ? array.sort((a, b) => a - b) : array.sort((a, b) => b - a);

}