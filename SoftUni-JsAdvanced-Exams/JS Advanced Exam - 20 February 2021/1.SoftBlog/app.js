function solve(){
   let articleSectionElement =  document.querySelector('.site-content main section')
   let createBtn = document.getElementsByTagName('button')[0]
   let [authorEl, titleEl, categoryEl] = document.querySelectorAll('form p input');
   let contentEl = document.querySelector('form p textarea');
   let archiveListElement = document.querySelector('.archive-section ol')
 
   let create = (e) => {
      e.preventDefault()
 
      let newArticle = document.createElement('article')
 
      let articleTitle = document.createElement('h1');
      articleTitle.textContent = titleEl.value;
 
      let articleCategoryPar = document.createElement('p');
      articleCategoryPar.textContent = 'Category: '
      let categoryStrong = document.createElement('strong');
      categoryStrong.textContent = categoryEl.value;
      articleCategoryPar.appendChild(categoryStrong);
 
      let articleCreatorPar = document.createElement('p');
      articleCreatorPar.textContent = 'Creator: '
      let creatorStrong = document.createElement('strong');
      creatorStrong.textContent = authorEl.value;
      articleCreatorPar.appendChild(creatorStrong);
 
      let contentPar = document.createElement('p');
      contentPar.textContent = contentEl.value;
 
      let btnsDiv = document.createElement('div');
      btnsDiv.classList.add('buttons')
      let delBtn = document.createElement('button');
      delBtn.className = 'btn delete';
      delBtn.textContent = 'Delete'
      delBtn.addEventListener('click', deleteFunc)
      btnsDiv.appendChild(delBtn);
      let archiveBtn = document.createElement('button');
      archiveBtn.className = 'btn archive';
      archiveBtn.textContent = 'Archive'
      archiveBtn.addEventListener('click', archive)
      btnsDiv.appendChild(archiveBtn);
 
 
      newArticle.appendChild(articleTitle);
      newArticle.appendChild(articleCategoryPar)
      newArticle.appendChild(articleCreatorPar)
      newArticle.appendChild(contentPar)
      newArticle.appendChild(btnsDiv)
 
      articleSectionElement.appendChild(newArticle)
 
      contentEl.value = '';
      authorEl.value = '';
      titleEl.value = '';
      categoryEl.value = '';
   }
   let deleteFunc = (e) => {
      e.currentTarget.parentNode.parentNode.remove();
   }
   let archive = (e) => {
      let title = e.currentTarget.parentNode.parentNode.querySelector('h1').textContent
      let newListItem = document.createElement('li');
      newListItem.textContent = title;
      archiveListElement.appendChild(newListItem)
 
      Array.from(archiveListElement.getElementsByTagName("li"))
           .sort((a, b) => a.textContent.localeCompare(b.textContent))
           .forEach(li => archiveListElement.appendChild(li));
 
      e.currentTarget.parentNode.parentNode.remove();
   }
   createBtn.addEventListener('click', create)
}