describe('Math Enforcer Tests', () => {

    it('adds 5 to the number that is passed to the addFive method', () => {

        expect(mathEnforcer.addFive(3)).to.equal(8);
        expect(mathEnforcer.addFive(-3)).to.equal(2);
        expect(mathEnforcer.addFive(0)).to.equal(5);
        expect(mathEnforcer.addFive(3.3)).to.equal(8.3);

    })

    it('addFive method returns undefined if given a string or object as an arguement', () => {

        expect(mathEnforcer.addFive('3')).to.equal(undefined);
        expect(mathEnforcer.addFive('blahsf')).to.equal(undefined);
        expect(mathEnforcer.addFive({})).to.equal(undefined);

    })

    it('subtracts 10 to the number that is passed to the subtractTen method', () => {

        expect(mathEnforcer.subtractTen(10)).to.equal(0);
        expect(mathEnforcer.subtractTen(5)).to.equal(-5);
        expect(mathEnforcer.subtractTen(5.5)).to.equal(-4.5);
        expect(mathEnforcer.subtractTen(10.5)).to.equal(0.5);
        expect(mathEnforcer.subtractTen(-10.5)).to.equal(-20.5);

    })

    it('subtractTen returns undefined if given a string or object as an arguement', () => {

        expect(mathEnforcer.subtractTen('3')).to.equal(undefined);
        expect(mathEnforcer.subtractTen('blahsf')).to.equal(undefined);
        expect(mathEnforcer.subtractTen({})).to.equal(undefined);

    })

    it('sums the two given parameters properly if numbers', () => {

        expect(mathEnforcer.sum(3, 5)).to.equal(8);
        expect(mathEnforcer.sum(3.3, 5.5)).to.equal(8.8);
        expect(mathEnforcer.sum(-3.3, 5.5)).to.equal(2.2);
        expect(mathEnforcer.sum(-3.3, -5.5)).to.equal(-8.8);

    })

    it('sum returns undefined if one or two of the parameters are not number', () => {

        expect(mathEnforcer.sum('3', '9')).to.equal(undefined);
        expect(mathEnforcer.sum({}, 9)).to.equal(undefined);
        expect(mathEnforcer.sum('3', 9)).to.equal(undefined);
        expect(mathEnforcer.sum(3, '9')).to.equal(undefined);

    })
})