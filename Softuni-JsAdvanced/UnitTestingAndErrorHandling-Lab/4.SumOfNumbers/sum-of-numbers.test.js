const expect = require('chai').expect;
const sum = require('./sum-of-numbers')

describe('Sum of Numbers', () => {
    it('should return the sum of the numbers in the array', () =>{
        let numbers = [1,2,3,4,5];
        let expectedSum = 15;

        let actualSum = sum(numbers);

        expect(actualSum).to.equal(expectedSum)

    })

    it('should return the sum with negative number in the array', () =>{
        let numbers = [1,2,3,4,-5];
        let expectedSum = 5;

        let actualSum = sum(numbers);

        expect(actualSum).to.equal(expectedSum)

    })

    it('should return the zero when only zeros are given', () =>{
        let numbers = [0,0,0,0,0];
        let expectedSum = 0;

        let actualSum = sum(numbers);

        expect(actualSum).to.equal(expectedSum)

    })
})