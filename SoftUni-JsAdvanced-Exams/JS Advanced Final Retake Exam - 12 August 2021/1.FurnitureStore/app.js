window.addEventListener('load', solve);
 
function solve() {
    let model = document.getElementById('model');
    let year = document.getElementById('year');
    let description = document.getElementById('description');
    let price = document.getElementById('price');
 
    let btnAdd = document.getElementById('add');
 
    let tbody = document.getElementById('furniture-list');
 
    btnAdd.addEventListener('click', addFurniture)
 
    function addFurniture(ev) {
        ev.preventDefault();
 
        let yearNumb = Number(year.value);
        let priceNumb = Number(price.value).toFixed(2);
        if (model.value && description.value && yearNumb > 0 && priceNumb > 0) {
 
            let trEl = document.createElement('tr');
            trEl.className = 'info';
 
            let tdModel = document.createElement('td');
            tdModel.textContent = model.value;
            trEl.appendChild(tdModel);
 
            let tdPrice = document.createElement('td');
            tdPrice.textContent = `${priceNumb}`
            trEl.appendChild(tdPrice);
 
            let td = document.createElement('td');
            let moreBtn = document.createElement('button');
            moreBtn.className = 'moreBtn';
            moreBtn.textContent = 'More Info';
            td.appendChild(moreBtn);
 
            let buyBtn = document.createElement('button');
            buyBtn.className = 'buyBtn';
            buyBtn.textContent = 'Buy it';
            td.appendChild(buyBtn);
 
            trEl.appendChild(td);
            tbody.appendChild(trEl);
 
            let trHide = document.createElement('tr');
            trHide.className = 'hide';
 
            let tdYear = document.createElement('td');
            tdYear.textContent = `Year: ${yearNumb}`
            trHide.appendChild(tdYear);
 
            let tdDiscription = document.createElement('td');
            tdDiscription.setAttribute('colspan', '3');
            tdDiscription.textContent = `Description: ${description.value}`;
            trHide.appendChild(tdDiscription);
 
            tbody.appendChild(trHide);
 
            model.value = '';
            year.value = '';
            description.value = '';
            price.value = '';
 
        }
    }
 
    tbody.addEventListener('click', onClick)
 
    function onClick(ev) {
        ev.preventDefault();
        let eventTarget = ev.target;
 
        if (eventTarget.textContent == 'More Info') {
            let tbodyEl = eventTarget.parentElement.parentElement.parentElement;
            tbodyEl.lastChild.style.display = 'contents';
            eventTarget.textContent = 'Less Info'
        } else if (eventTarget.textContent == 'Less Info') {
            let tbodyEl = eventTarget.parentElement.parentElement.parentElement;
            tbodyEl.lastChild.style.display = '';
            eventTarget.textContent = 'More Info'
        } else {
            let tdTotalPrice = document.querySelector('.total-price');
 
            let cuuSum = Number(tdTotalPrice.textContent);
 
            let currPrice = (eventTarget.parentElement.parentElement.children[1]);
 
            let sumChear = Number(currPrice.textContent);
 
 
            tdTotalPrice.textContent = Number(sumChear + cuuSum).toFixed(2);
 
            eventTarget.parentElement.parentElement.remove()
        }
    }
}