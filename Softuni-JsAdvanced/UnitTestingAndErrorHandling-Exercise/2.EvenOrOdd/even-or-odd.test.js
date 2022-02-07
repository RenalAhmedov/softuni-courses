const isOddOrEven = require('./even-or-odd');
const { assert } = require('chai');

describe(' isOddOrEven function tests', () => {

    it('should return undefined if paramter is number', () => {
        assert.equal(isOddOrEven(10, undefined));
    })
    it('should return undefined if paramter is object', () => {
        assert.equal(isOddOrEven({}, undefined));
    })
    it('should return undefined if paramter is array', () => {
        assert.equal(isOddOrEven([], undefined));
    })
    it('should return even', () => {
        assert.equal(isOddOrEven('Hi'), 'even');
    })
    it('should return odd', () => {
        assert.equal(isOddOrEven('Hello'), 'odd');
    })


})