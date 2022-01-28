function create(input) {
   let parentElement = document.getElementById('content');
   let elements = input;
   
   for (let i = 0; i < elements.length; i++) {
      let div = document.createElement('div');
      let p = document.createElement('p');
      let text = document.createTextNode(elements[i]); // give me input textnode in the let text

      div.appendChild(p)
      p.appendChild(text);
      p.style.display = 'none';
      div.addEventListener('click', showParagraph);
      parentElement.appendChild(div);  
   }
   function showParagraph(event){
      event.target.children[0].style.display = 'inline';
   }
}