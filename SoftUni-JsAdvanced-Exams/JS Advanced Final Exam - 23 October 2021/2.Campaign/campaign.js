class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer
        this.location = location
        this.priceForCamp = { "child": 150, "student": 300, "collegian": 500 }
        this.listOfParticipants = []
        this.namesArr = [];
    }
 
    registerParticipant(name, condition, money) {
        if (!this.priceForCamp.hasOwnProperty(condition)) {
            throw new Error("Unsuccessful registration at the camp.")
        }
        this.namesArr = [];
        for (const obj of this.listOfParticipants) {
            this.namesArr.push(obj.name)
        }
        if (this.namesArr.includes(name)) {
            return `The ${name} is already registered at the camp.`
        }
 
        if (money < this.priceForCamp[condition]) {
            return `The money is not enough to pay the stay at the camp.`
        } else {
            this.listOfParticipants.push({
                name: name,
                condition: condition,
                power: 100,
                wins: 0,
            })
            return `The ${name} was successfully registered.`
        }
    }
 
    unregisterParticipant(name) {
        this.namesArr = [];
        for (const obj of this.listOfParticipants) {
            this.namesArr.push(obj.name)
 
            if (obj.name == name) {
                let index = this.listOfParticipants.indexOf(obj)
                this.listOfParticipants.splice(index, 1)
                return `The ${name} removed successfully.`
            }
        }
        if (!this.namesArr.includes(name)) {
            throw new Error(`The ${name} is not registered in the camp.`)
        }
    }
 
    timeToPlay(typeOfGame, participant1, participant2) {
        if (participant2 == undefined) {
            this.namesArr = [];
            for (const obj of this.listOfParticipants) {
 
                this.namesArr.push(obj.name)
            }
            if (!this.namesArr.includes(participant1)) {
                throw new Error(`Invalid entered name/s.`)
            }
            let participant = this.listOfParticipants.find(el => el.name == participant1)
            participant.power += 20;
            return `The ${participant1} successfully completed the game ${typeOfGame}.`
 
        } else {
            this.namesArr = [];
            for (const obj of this.listOfParticipants) {
 
                this.namesArr.push(obj.name)
            }
            if (!this.namesArr.includes(participant1)) {
                throw new Error(`Invalid entered name/s.`)
            }
            if (!this.namesArr.includes(participant2)) {
                throw new Error(`Invalid entered name/s.`)
            }
            let first = this.listOfParticipants.find(el => el.name == participant1)
            let second = this.listOfParticipants.find(el => el.name == participant2)
 
            if (first.condition != second.condition) {
                throw new Error(`Choose players with equal condition.`)
            }
 
            if (first.power > second.power) {
                first.wins += 1;
                return `The ${participant1} is winner in the game ${typeOfGame}.`
            } else if (first.power < second.power) {
                second.wins += 1;
                return `The ${participant2} is winner in the game ${typeOfGame}.`
            } else {
                return `There is no winner.`
            }
 
        }
 
    }
 
    toString() {
        let output = []
        let sorted = this.listOfParticipants.sort((obj1, obj2) => obj2.wins - obj1.wins)
        let string = `${this.organizer} will take ${this.listOfParticipants.length} participants on camping to ${this.location}`
 
 
        sorted.forEach(obj => {
            output.push(`${obj.name} - ${obj.condition} - ${obj.power} - ${obj.wins}`);
        })
        output.unshift(string)
        return output.join("\n")
 
    }
 
 
}