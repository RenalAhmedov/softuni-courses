function evenPositionElement(array){

    let endArray = '';
    for(let i = 0; i < array.length; i++){
        if(i % 2 == 0){
            endArray += array[i] + ` `;
        }
    }
    console.log(endArray);
}

evenPositionElement(['20', '30', '40', '50', '60'])
