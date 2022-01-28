// function addItem() {
//     let itemText = document.getElementById('newItemText');
//     let itemValue = document.getElementById('newItemValue');
//     let menuElement = document.getElementById('menu');
//     let optionElement = document.createElement('option')

//     let text = itemText.value;
//     let textVal = itemValue.value;

//     optionElement.textContent = text.value;
//     optionElement.value = textVal;
//     menuElement.appendChild(optionElement);
//     itemText = '';
//     itemValue = '';
// }


function addItem() {
    
    const itemText = document.getElementById('newItemText');
    const itemValue = document.getElementById('newItemValue');
    const text = itemText.value;
    const value = itemValue.value;

    const optionElement = document.createElement('option');
    optionElement.textContent = text;
    optionElement.value = value;

    document.getElementById('menu').appendChild(optionElement);

    itemText.value = '';
    itemValue.value = '';

}