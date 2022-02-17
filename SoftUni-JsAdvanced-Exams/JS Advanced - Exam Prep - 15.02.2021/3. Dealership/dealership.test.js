const { expect } = require('chai');
const dealership = require('./dealership');

describe('NumberOperationsTests', () => {
    //FIRST FUNCTION TEST---
    it('Discount  to work', () => {
        expect(dealership.newCarCost(`Audi A6 4K`, 40000)).to.be.equal(20000);
    });
    it('Discount not to work', () => {
        expect(dealership.newCarCost(`BMW M5 10K`, 100000)).to.be.equal(100000);
    });
    
    //SECOND FUNCTION TEST--
    it('Carequipment testing ', () => {
        expect(dealership.carEquipment(['rims', 'seats', 'podgrev', 'kojensalon', 'stereo'], [2, 3])).to.be.eql(['podgrev', 'kojensalon']);
        expect(dealership.carEquipment([], [2, 3])).to.be.eql([undefined, undefined]);
    });
    
    //THIRD FUNCTION TEST--
    it('Should make no discount for low category', () => {
        expect(dealership.euroCategory(2)).to.be.equal('Your euro category is low, so there is no discount from the final price!');
    });

    it('Should make a discount for a high category', () => {
        expect(dealership.euroCategory(4)).to.be.equal('We have added 5% discount to the final price: 14250.');
    });

})