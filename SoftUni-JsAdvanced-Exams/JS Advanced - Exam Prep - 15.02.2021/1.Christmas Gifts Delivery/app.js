function solution() {
    let giftInput = document.querySelector('div.container section.card:nth-of-type(1) input');
    let addGiftButton = document.querySelector('div.container section.card:nth-of-type(1) button');
 
    addGiftButton.addEventListener('click', (e) =>{
        let ulListOfGifts = document.querySelector('div.container section.card:nth-of-type(2) ul');
 
        let liGift = document.createElement('li');
        let sendButton = document.createElement('button');
        let discardButton = document.createElement('button');
 
        liGift.className = 'gift';
        sendButton.id = 'sendButton';
        discardButton.id = 'discardButton';
        sendButton.textContent = 'Send';
        discardButton.textContent = 'Discard';
        
        liGift.textContent = giftInput.value;
        liGift.appendChild(sendButton);
        liGift.appendChild(discardButton);
        ulListOfGifts.appendChild(liGift);
 
        let allGifts = document.querySelectorAll('div.container section.card:nth-of-type(2) ul li');
 
        Array.from(allGifts)
            .sort((a,b) => a.textContent.localeCompare(b.textContent))
            .forEach(li => ulListOfGifts.appendChild(li));
 
        giftInput.value = '';
 
        sendButton.addEventListener('click', (e) =>{
            let ulSentGifts = document.querySelector('div.container section.card:nth-of-type(3) ul');
 
            ulSentGifts.appendChild(e.currentTarget.parentElement);
            sendButton.remove();
            discardButton.remove();
        })
 
        discardButton.addEventListener('click', (e) =>{
            let ulDiscardedGifts = document.querySelector('div.container section.card:nth-of-type(4) ul');
 
            ulDiscardedGifts.appendChild(e.currentTarget.parentElement);
            sendButton.remove();
            discardButton.remove();
        })
    })
}