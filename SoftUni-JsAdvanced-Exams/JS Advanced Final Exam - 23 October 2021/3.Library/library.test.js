const { expect } = require('chai');
 
const { assert } = require('chai');
 
const library = require('./library');

describe('calcPriceOfBook Tests', () => {
    //firstMethod--------
    it('Should throw an error if first arg is not a string', () => {
        expect(() => { library.calcPriceOfBook(0, 1) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook([], 1) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook({}, 1) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook(null, 1) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook(undefined, 1) }).to.throw('Invalid input');
    });
    it('Should throw an error if second arg is not a number', () => {
        expect(() => { library.calcPriceOfBook('ss', {}) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook('ss', []) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook('ss', undefined) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook('ss', null) }).to.throw('Invalid input');
        expect(() => { library.calcPriceOfBook('ss', 'string') }).to.throw('Invalid input');
    });

    it('Should return 10 if year <= 1980', () => {
        expect(library.calcPriceOfBook('Harry Potter', 1979)).to.be.equal('Price of Harry Potter is 10.00');
        expect(library.calcPriceOfBook('Harry Potter', 1980)).to.be.equal('Price of Harry Potter is 10.00');
    });

    it('Should return 20 if year > 1980', () => {
        expect(library.calcPriceOfBook('Harry Potter', 1981)).to.be.equal('Price of Harry Potter is 20.00');
    });

    //secondMethod---------
    it('Should throw an error if first arg length is equal to 0', () => {
        expect(() => { library.findBook(booksArr = [], 'Harry Potter') }).to.throw('No books currently available');
    });

    it('Should return the found book', () => {
        expect(library.findBook(booksArr = ['Troy', '1984', 'Harry Potter'], 'Harry Potter')).to.be.equal('We found the book you want.');
    });

    it('Should NOT return the found book', () => {
        expect(library.findBook(booksArr = ['Troy', '1984', 'Harry Potter'], 'Spiderman')).to.be.equal('The book you are looking for is not here!');
    });

    //thirdMethod---------
    it('Should throw error if countBooks is not a number', () => {
        expect(() => { library.arrangeTheBooks(24.24) }).to.throw('Invalid input');
        expect(() => { library.arrangeTheBooks(-1) }).to.throw('Invalid input');
    });

    it('Should return OKAY if availableSpace is bigger than countBooks', () => {
        expect(library.arrangeTheBooks(40)).to.be.equal('Great job, the books are arranged.');
    });

    it('Should return negative answer if not enough space for books', () => {
        expect(library.arrangeTheBooks(41)).to.be.equal('Insufficient space, more shelves need to be purchased.');
    });



})