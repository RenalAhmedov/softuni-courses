function solve(obj = {}){ 
    let calc = 0;

    if (obj.dizziness) {
        calc = 0.1 * obj.weight * obj.experience
        obj.levelOfHydrated += calc;
        obj.dizziness == false;
    }
    return obj;
}

console.log(solve({ weight: 80,
    experience: 1,
    levelOfHydrated: 0,
    dizziness: true }));
