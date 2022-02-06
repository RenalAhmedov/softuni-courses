const lookupChar = require('./char-lookup');
const { assert } = require('chai');

describe('Char Lookup Tests', () => {

    it('returns undefined if given arguements are not valid types', () => {

        expect(lookupChar(4, 4)).to.equal(undefined);
        expect(lookupChar('blah blah', '4')).to.equal(undefined);
        expect(lookupChar(3,'dasdf')).to.equal(undefined);

    })

    it('returns Incorrect index when index is out of range', () => {

        expect(lookupChar('sdfgh', -1)).to.equal('Incorrect index');
        expect(lookupChar('sdfgh', 5)).to.equal('Incorrect index');

    })

    it('returns undefined when given floating number as an arguement', () => {

        expect(lookupChar('sdg', 1.1)).to.equal(undefined);
        expect(lookupChar('sdg', 2.99)).to.equal(undefined);

    })

    it('works perfectly fine with proper arguements', () => {

        expect(lookupChar('asd', 1)).to.equal('s');
        expect(lookupChar('asd', 2)).to.equal('d');
        expect(lookupChar('asd', 0)).to.equal('a');

    })
})