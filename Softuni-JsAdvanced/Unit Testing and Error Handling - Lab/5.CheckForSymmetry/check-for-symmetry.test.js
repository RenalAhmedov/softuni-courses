const expect = require('chai').expect;
const assert = require('chai').assert;
const isSymmetric  = require('./check-for-symmetry')

describe('Check for Symmetry Tests', () => {

    it('returns false for non-array arguement', () => {

        expect(isSymmetric(3)).to.be.false;
        expect(isSymmetric('abcba')).to.be.false;
        expect(isSymmetric('a')).to.be.false;

    })

    it('returns true if array is symmetric with numbers', () => {

        expect(isSymmetric([1,2,3,2,1])).to.be.true;
        expect(isSymmetric([1,2,2,1])).to.be.true;

    })

    it('returns true if array is symmetric with strings', () => {

        expect(isSymmetric(['a','b','b','a'])).to.be.true;
        expect(isSymmetric(['a','b','c','b','a'])).to.be.true;

    })

    it('returns false if array is not symmetric', () => {

        expect(isSymmetric(1,2,1,2)).to.be.false;
        expect(isSymmetric(['1', '2', '1', '2'])).to.be.false;

    })

    it('returns false if array is symmetric but with mixed data types', () => {

        expect(isSymmetric([1,2,2,'1'])).to.be.false;

    })
})