const expect = require('chai').expect;
const assert = require('chai').assert;
const rgbtohex = require('./rgb-to-hex');

describe('RGB to HEX Tests', () => {

    it('returns undefined if out of range parameters', () => {

        expect(rgbToHexColor(-3,5,196)).to.equal(undefined);
        expect(rgbToHexColor(3,-5,196)).to.equal(undefined);
        expect(rgbToHexColor(3,5,-196)).to.equal(undefined);

        expect(rgbToHexColor(3,5,256)).to.equal(undefined);
        expect(rgbToHexColor(3,256,6)).to.equal(undefined);
        expect(rgbToHexColor(300,5,56)).to.equal(undefined);

    })

    it('returns undefined if parameters are not numbers', () => {

        expect(rgbToHexColor('3',5,196)).to.equal(undefined);
        expect(rgbToHexColor(3,'5',196)).to.equal(undefined);
        expect(rgbToHexColor(3,5,'196')).to.equal(undefined);
        
    })

    it('returns proper hex represantations', () => {

        expect(rgbToHexColor(66, 135, 245)).to.equal('#4287F5');
        expect(rgbToHexColor(5, 10, 18)).to.equal('#050A12');
        expect(rgbToHexColor(79, 9, 23)).to.equal('#4F0917');

        expect(rgbToHexColor(255, 0, 0)).to.equal('#FF0000');
        expect(rgbToHexColor(0, 255, 0)).to.equal('#00FF00');
        expect(rgbToHexColor(0, 0, 255)).to.equal('#0000FF');

    })
})