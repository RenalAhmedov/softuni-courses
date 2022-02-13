const { expect } = require('chai');
const { assert } = require('chai');
const cinema = require('./cinema');


describe('Cinema Tests', () => {
    //FIRST METHOD TEST---
    it('Movie length is the required Length', () => {
        const arrOfMovies = ["film1", "film2", "film3", "film5"];
        expect(cinema.showMovies(arrOfMovies)).to.be.equal(arrOfMovies.join(", "));
    });
    it('Movie length is NOT the required Length', () => {
        const arrOfMovies = [];
        expect(cinema.showMovies(arrOfMovies)).to.be.equal('There are currently no movies to show.');
    });

    //SECOND METHOD TEST---
    it('Schedule testing for correct behavior', () => {
        expect(cinema.ticketPrice('Premiere')).to.be.equal(12.00);
        expect(cinema.ticketPrice('Normal')).to.be.equal(7.50);
        expect(cinema.ticketPrice('Discount')).to.be.equal(5.50);
    });
    it('Schedule testing for INCORRECT behavior', () => {
        expect(() => { cinema.ticketPrice('ss') }).to.throw('Invalid projection type');
    });

    //THIRD METHOD TEST--
    it('SeatsSwap testing for INCORRECT behavior', () => {
        expect(cinema.swapSeatsInHall(20.50, 21.50)).to.be.equal('Unsuccessful change of seats in the hall.');
        expect(cinema.swapSeatsInHall(-1), -2).to.be.equal('Unsuccessful change of seats in the hall.');
        expect(cinema.swapSeatsInHall(20.50, 20.50)).to.be.equal('Unsuccessful change of seats in the hall.');
    });
    it('SeatsSwap testing for correct behavior', () => {
        assert.equal(cinema.swapSeatsInHall(1, 20), "Successful change of seats in the hall.");
        assert.equal(cinema.swapSeatsInHall(20, 18), "Successful change of seats in the hall.");
    });







})