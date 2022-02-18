const { expect } = require('chai');
const companyAdministration = require('./companyAdministration');

describe('companyAdministration tests', () => {
    //FIRST FUNCTION TEST---
    it('Should throw an error if the given string is not `Programmer`', () => {
        expect(() => companyAdministration.hiringEmployee('some', 'programmer', 10)).to.throw(Error, `We are not looking for workers for this position.`);
        expect(() => companyAdministration.hiringEmployee('some', 'some', 10)).to.throw(Error, `We are not looking for workers for this position.`);
    });
    it('Should not approve employee if the years of experience are < 3', () => {
        expect(companyAdministration.hiringEmployee('s', 'Programmer', 0)).to.equal(`s is not approved for this position.`);
        expect(companyAdministration.hiringEmployee('s', 'Programmer', 2)).to.equal(`s is not approved for this position.`);
    });
    it('Should approve employee if the years of experience are >= 3', () => {
        expect(companyAdministration.hiringEmployee('s', 'Programmer', 3)).to.equal(`s was successfully hired for the position Programmer.`);
        expect(companyAdministration.hiringEmployee('s', 'Programmer', 50)).to.equal(`s was successfully hired for the position Programmer.`);
    });

    //SECOND FUNCTION TEST--
    it('calculate salary testing if number is different', () => {
        expect(() => { companyAdministration.calculateSalary(-1) }).to.throw(`Invalid hours`);  
        expect(() => { companyAdministration.calculateSalary('asd') }).to.throw(`Invalid hours`);  
        expect(() => { companyAdministration.calculateSalary({}) }).to.throw(`Invalid hours`);  
        expect(() => { companyAdministration.calculateSalary([]) }).to.throw(`Invalid hours`);   
        expect(() => { companyAdministration.calculateSalary() }).to.throw(`Invalid hours`);   
        expect(() => { companyAdministration.calculateSalary('') }).to.throw(`Invalid hours`);   
        expect(() => { companyAdministration.calculateSalary(false) }).to.throw(`Invalid hours`);  
        expect(() => { companyAdministration.calculateSalary(undefined) }).to.throw(`Invalid hours`);  

    });
    it('if the hours are above 160 + salary 1k', () => {
        expect(companyAdministration.calculateSalary(200)).to.be.equal(4000);
        expect(companyAdministration.calculateSalary(150)).to.be.equal(2250);
        expect(companyAdministration.calculateSalary(0)).to.be.equal(0);
    });

    //THIRD FUNCTION TEST--
    it('testing the fire function the ARRAY and the index', () => {
        expect(() => { companyAdministration.firedEmployee({}, 1) }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee([1,2,3], 1.2) }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee([2,4,6], -1) }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee([1,2,3], 10) }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee([1,2,3], '10') }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee() }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee('') }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee(false) }).to.throw(`Invalid input`);
        expect(() => { companyAdministration.firedEmployee(undefined) }).to.throw(`Invalid input`);
    });
    it('testing the fire function behavior', () => {
        expect(companyAdministration.firedEmployee([1,2,3,4,5], 2)).to.be.equal('1, 2, 4, 5');  
        expect(companyAdministration.firedEmployee([1,2,3,4,5], 4)).to.be.equal('1, 2, 3, 4');  
    });
})