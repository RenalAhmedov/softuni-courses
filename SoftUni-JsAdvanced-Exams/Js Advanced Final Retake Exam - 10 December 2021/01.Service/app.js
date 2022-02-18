window.addEventListener('load', solve);

function solve() {
    let descriptionInputFieldElement = document.getElementById('description');
    let clientNameEInputFieldElement = document.getElementById('client-name');
    let clientPhoneInputFieldElement = document.getElementById('client-phone');

    let typeOfProductElementValue = document.getElementById('type-product');
    let receivedOrdersField = document.getElementById('received-orders')
    let finishedRepairingElement = document.getElementById('completed-orders');
    let completedOrders = document.getElementById('completed-orders');

    const clearBtn = document.querySelector('.clear-btn');

    let btnSubmit = document.querySelector('form button');
    btnSubmit.addEventListener('click', create)

    function create(ev) {
        ev.preventDefault();
        let clientPhone = clientPhoneInputFieldElement;
        if (descriptionInputFieldElement.value != "" && clientNameEInputFieldElement.value != "" && clientPhoneInputFieldElement.value != "") {

            let div = document.createElement('div');
            div.classList = 'container'
            let productTypeToRepairH2 = document.createElement("h2");
            let clientInformationH3 = document.createElement("h3");
            let descriptionOfTheProblemh4 = document.createElement("h4");

            let buttonStart = document.createElement("button");
            buttonStart.classList = "start-btn";
            buttonStart.textContent = "Start repair"
            buttonStart.addEventListener("click", onStart)

            let buttonFinish = document.createElement("button");
            buttonFinish.classList = "finish-btn"
            buttonFinish.textContent = "Finish repair";
            buttonFinish.addEventListener("click", onDelete)

            clearBtn.addEventListener('click', clear);

            productTypeToRepairH2.textContent = `Product type for repair: ${typeOfProductElementValue.value}`;
            clientInformationH3.textContent = `Client Information: ${clientNameEInputFieldElement.value}, ${clientPhone.value}`;
            descriptionOfTheProblemh4.textContent = `Description of the problem: ${descriptionInputFieldElement.value}`

            receivedOrdersField.appendChild(productTypeToRepairH2)
            receivedOrdersField.appendChild(clientInformationH3)
            receivedOrdersField.appendChild(descriptionOfTheProblemh4)
            receivedOrdersField.appendChild(buttonStart)
            receivedOrdersField.appendChild(buttonFinish)

            descriptionInputFieldElement.value = '';
            clientNameEInputFieldElement.value = '';
            clientPhoneInputFieldElement.value = '';

            function onStart(ev) {
                buttonStart.disabled = true;
                buttonFinish.disabled = false;
            }
            function onDelete() {
                finishedRepairingElement.appendChild(productTypeToRepairH2)
                finishedRepairingElement.appendChild(clientInformationH3)
                finishedRepairingElement.appendChild(descriptionOfTheProblemh4)
            }
            function clear() {
                completedOrders.innerHTML = `<h2>Completed orders:</h2><img src="./style/img/completed-order.png"><button class="clear-btn">Clear</button>`;
            }
        }
    }
}