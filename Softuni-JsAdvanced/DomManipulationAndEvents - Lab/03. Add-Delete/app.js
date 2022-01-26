function addItem() {
    let itemsElement = document.getElementById('items');
    let inputElement = document.getElementById('newItemText');

    let liElement = document.createElement('li');
    liElement.textContent = inputElement.value;

    let deleteElement = document.createElement('a');
    deleteElement.href = "#";
    deleteElement.textContent = '[Delete]';
    deleteElement.addEventListener('click', (e) => {
        e.currentTarget.parentNode.remove();
    })

    liElement.appendChild(deleteElement);
    itemsElement.appendChild(liElement);
}