import { html, render } from './node_modules/lit-html/lit-html.js'

const studentRow = (student) => html`
<tr class=${student.match ? 'select'  : '' }>
   <td>${student.item.firstName}</td>
   <td>${student.item.email}</td>
   <td>${student.item.course}</td>
</tr>`;

const input = document.getElementById('searchField');
document.getElementById('searchBtn').addEventListener('click', onSearch);
let students;
start();

async function start() {
   const res = await fetch('http://localhost:3030/jsonstore/advanced/table');
   const data = await res.json();
   students = Object.values(data).map(s => ({item:s, match:false}));

   update();
}

function update() {
   render(students.map(studentRow), document.querySelector('tbody'));
}

// compare input with all data fields
// mark matching items and update students data
function onSearch() {
   let text = input.value.trim().toLocaleLowerCase();
   for( let student of students){
      student.match = Object.values(student.item).some(v =>text && v.toLocaleLowerCase().includes(text));
   }
   input.value = '';
   update();
}