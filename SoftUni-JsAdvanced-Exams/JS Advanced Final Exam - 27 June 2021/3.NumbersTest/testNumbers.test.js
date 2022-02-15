const { expect } = require('chai');
const { assert } = require('chai');
const testNumbers = require('./testNumbers');

describe('NumbersTest', () => {
    //FIRST FUNCTION TEST---
    it('inputs ARENT numbers', () => {
        expect(testNumbers.sumNumbers('asd', '1')).to.be.an('undefined');
        expect(testNumbers.sumNumbers('1', 'asd')).to.be.an('undefined');
        expect(testNumbers.sumNumbers('{}}', '[]')).to.be.an('undefined');
        expect(testNumbers.sumNumbers('', '-')).to.be.an('undefined');
    });
    it('inputs are numbers', () => {
        expect(testNumbers.sumNumbers(1, 2)).to.be.equal('3.00');
    });

    //SECOND FUNCTION TEST----
    it('inputs ARE NOT numbers', () => {
        expect(() => { testNumbers.numberChecker('ss') }).to.throw('The input is not a number!');
        expect(() => { testNumbers.numberChecker('{}}') }).to.throw('The input is not a number!');
        expect(() => { testNumbers.numberChecker('[]]') }).to.throw('The input is not a number!');
        expect(() => { testNumbers.numberChecker('-') }).to.throw('The input is not a number!');
        expect(() => { testNumbers.numberChecker('~') }).to.throw('The input is not a number!');
       
    });
    it('inputs EVEN or ODD numbers', () => {
        expect(testNumbers.numberChecker(2)).to.be.equal('The number is even!');
        expect(testNumbers.numberChecker(3)).to.be.equal('The number is odd!');
    });

    //THIRD FUNCTION TEST---
    it('array sum', () => {
        let array = [2, 2, 2];
        expect(testNumbers.averageSumArray(array)).to.be.equal(2);
    });
    
  
 
})