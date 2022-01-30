function solution(number) {
    function add(a, b) {
        return a + b;
    }
    
    return add.bind(this, number);
    // return (n) => add(number, n); 
}
