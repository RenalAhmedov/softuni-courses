const { expect } = require('chai');
const numberOperations = require('./03. Number Operations_Resources');

describe('NumberOperationsTests', () => {
    //FIRST FUNCTION TEST---
    it('pow number act correct', () => {
        expect(numberOperations.powNumber(2)).to.be.equal(4);
    });

    //SECOND FUNCTION TEST----
    it('Input is not a number', () => {
        expect(() => { numberOperations.numberChecker('~') }).to.throw('The input is not a number!');    
    });
    it('Input is less than 100', () => {
        expect(numberOperations.numberChecker(99)).to.be.equal('The number is lower than 100!');   
    });
    it('Input is bigger or equal to 100', () => {
        expect(numberOperations.numberChecker(101)).to.be.equal('The number is greater or equal to 100!');
        expect(numberOperations.numberChecker(100)).to.be.equal('The number is greater or equal to 100!');  
    });

    //THIRD FUNCTION TEST---
    it('should return new array', () => {
        expect(numberOperations.sumArrays([1,2,5],[1,2,3])).to.eql([2,4,8])
    });
    it('should return new array', () => {
        expect(numberOperations.sumArrays([1,2,5,2,5],[1,2,3])).to.eql([2,4,8,2,5])
    });

    it('should return new array', () => {
        expect(numberOperations.sumArrays([1,2,5],[1,2,3,5,6])).to.eql([2,4,8,5,6])
    });

})