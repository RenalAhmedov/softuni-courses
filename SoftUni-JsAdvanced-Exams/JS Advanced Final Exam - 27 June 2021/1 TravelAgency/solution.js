window.addEventListener('load', solution);

function solution() {
  let fullNameElement = document.getElementById('fname');
  let emailElement = document.getElementById('email');
  let phoneNumberElement = document.getElementById('phone');
  let adressElement = document.getElementById('address');
  let postalCodeElement = document.getElementById('code');
  let blockElement = document.getElementById('block');

  let btnSubmit = document.getElementById('submitBTN');
  let btnEdit = document.getElementById('editBTN');
  let btnContinue = document.getElementById('continueBTN');



  btnSubmit.addEventListener('click', submitDetails)
  function submitDetails(ev) {
    ev.preventDefault();

    let li1 = document.createElement('li');
    li1.textContent = `Full Name: ${fullNameElement.value}`
  
    let li2 = document.createElement('li');
    li2.textContent = `Email: ${emailElement.value}`
  
    let li3 = document.createElement('li');
    li3.textContent = `Phone Number: ${phoneNumberElement.value}`
  
    let li4 = document.createElement('li');
    li4.textContent = `Address: ${adressElement.value}`
  
    let li5 = document.createElement('li');
    li5.textContent = `Postal Code: ${postalCodeElement.value}`

    if (!fullNameElement.value == "" && !emailElement.value == "") {
      document.querySelector('ul').appendChild(li1);
      document.querySelector('ul').appendChild(li2);
      document.querySelector('ul').appendChild(li3);
      document.querySelector('ul').appendChild(li4);
      document.querySelector('ul').appendChild(li5);

      btnEdit.disabled = false;
      btnContinue.disabled = false;
      btnSubmit.disabled = true;
      
      fullNameElement.value = "";
      emailElement.value = "";
      phoneNumberElement.value = "";
      adressElement.value = "";
      postalCodeElement.value = "";

    }

  }

  btnEdit.addEventListener('click', editDetails)
  function editDetails(ev) {
    ev.preventDefault();
    let infoArr = Array.from(document.getElementsByTagName('li'))
    let inputArr = Array.from(document.getElementsByTagName('input'))
 
    for (let i = 0; i < infoArr.length; i++) {
 
      inputArr[i].value = infoArr[i].textContent.split(': ')[1];
    }
 
    document.querySelector('ul').innerHTML = ""

    btnEdit.disabled = true;
    btnContinue.disabled = true;
    btnSubmit.disabled = false;
 
  }

  btnContinue.addEventListener('click', continueDetails)
  function continueDetails(ev){ 
    document.querySelector('#block').innerHTML =
     "<h3>Thank you for your reservation!</h3>"

     editBtnEl.disabled = true;
     continueBtnEl.disabled = true;
     submitBtnEl.disabled = false;
  }
  
}

