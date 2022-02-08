const {expect} = require('chai');
const StringBuilder = require('./stringBuilder');

describe('String Builder Tests', () => {

    //constructors
    it('is instantiated with a string', () => {

        const sb = new StringBuilder('pavkata');
        expect(sb.toString()).to.equal('pavkata');
        expect(sb._stringArray).to.deep.equal(['p', 'a', 'v', 'k', 'a', 't', 'a']);
        expect(sb._stringArray.length).to.equal(7);
        expect(typeof sb).to.equal('object');
        expect(sb).to.be.instanceOf(StringBuilder);

    })

    it('is instantiated with an empty constructor', () => {

        const sb = new StringBuilder();
        expect(sb.toString()).to.equal('');

    })

    it('should return empty array if given parameter is undefined', () => {

        const sb = new StringBuilder(undefined);
        expect(sb._stringArray).to.deep.equal([]);

    })

    it('throws error when invalid type parameters are passed to constructor', () => {

        expect(() => { new StringBuilder(3)}).to.throw('Argument must be a string');
        expect(() => { new StringBuilder([])}).to.throw('Argument must be a string');
        expect(() => { new StringBuilder({})}).to.throw('Argument must be a string');
        expect(() => { new StringBuilder(false)}).to.throw('Argument must be a string');

    })

    //append
    it('appends the passed parameter correctly', () => {

        const sb = new StringBuilder('asd');
        sb.append('qwer');
        expect(sb.toString()).to.equal('asdqwer');

    })

    it('throws error when invalid parameter is passed/append', () => {

        const sb = new StringBuilder('asd');
        expect(() => { sb.append(3) }).to.throw('Argument must be a string');
        expect(() => { sb.append([])}).to.throw('Argument must be a string');
        expect(() => { sb.append({})}).to.throw('Argument must be a string');
        expect(() => { sb.append(false)}).to.throw('Argument must be a string');

    })

    it('works in combination with other methods/append', () => {

        const sb = new StringBuilder('asd');
        sb.prepend('boom ');
        sb.insertAt('qwer', 1); // bqweroom asd
        expect(sb.toString()).to.equal('bqweroom asd');

    })

    //prepend
    it('prepends the passed parameter properly', () => {

        const sb = new StringBuilder('asd');
        sb.prepend('qwer');
        expect(sb.toString()).to.equal('qwerasd');

    })

    it('throws error when invalid parameter is passed/prepend', () => {

        const sb = new StringBuilder('asd');
        expect(() => { sb.prepend(3) }).to.throw('Argument must be a string');
        expect(() => { sb.prepend({}) }).to.throw('Argument must be a string');
        expect(() => { sb.prepend([]) }).to.throw('Argument must be a string');
        expect(() => { sb.prepend(false) }).to.throw('Argument must be a string');

    })

    it('works in combination with other methods/prepend',() => {

        const sb = new StringBuilder('asd');
        sb.append('qwer');
        sb.prepend('ty');
        expect(sb.toString()).to.equal('tyasdqwer');

    })

    //insertAt
    it('inserts At given index properly', () => {

        const sb = new StringBuilder('asd');
        sb.insertAt('qwer', 1);
        expect(sb.toString()).to.equal('aqwersd');

    })

    it('throws error when invalid parameter is passed/insertAt', () => {

        const sb = new StringBuilder('asd');
        expect(() => { sb.insertAt(3, 1) }).to.throw('Argument must be a string');
        expect(() => { sb.insertAt({}, 1) }).to.throw('Argument must be a string');
        expect(() => { sb.insertAt([], 1) }).to.throw('Argument must be a string');
        expect(() => { sb.insertAt(false, 1) }).to.throw('Argument must be a string');

    })

    it('works in combination with other methods/insertAt', () => {

        const sb = new StringBuilder('asd');
        sb.append('qwer');
        sb.prepend('123');
        sb.insertAt('123', 0);
        expect(sb.toString()).to.equal('123123asdqwer');

    })

    //remove
    it('removes a substring properly', () => {

        const sb = new StringBuilder('asd');
        sb.remove(1, 1);
        expect(sb.toString()).to.equal('ad');

    })

    it('works in combination with other methods/remove', () => {

        const sb = new StringBuilder('asd');
        sb.append('qwer');
        sb.prepend('ty');
        sb.insertAt('for', 2);
        sb.remove(5, 3);
        expect(sb.toString()).to.equal('tyforqwer');

    })
})