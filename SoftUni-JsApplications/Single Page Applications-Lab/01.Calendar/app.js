import { loadView, displayMonth, displayYear, displayYears } from './view.js';

const validYears = ["2020", "2021", "2022", "2023"];
const validMonths = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

// get sections from DOM
const yearsView = document.getElementById('years');
const years = [...document.getElementsByClassName('monthCalendar')].reduce((a, v) => {
    a[v.id] = v
    return a
}, {})
const months = [...document.getElementsByClassName('daysCalendar')];

// display all years
loadView(yearsView);


//check what is clicked
document.addEventListener('click', (e) => {
    let clicked;
    if (e.target.tagName === 'TD') {
        clicked = e.target.children[0].textContent.trim();
    } else if (e.target.tagName === 'DIV') {
        clicked = e.target.textContent.trim();
    }

    if (validYears.includes(clicked)) {
        displayYear(years, clicked);
    } else if (validMonths.includes(clicked)) {
        const year = document.querySelector('caption').textContent.trim();
        const month = e.target.textContent.trim();
        const monthIndex = validMonths.findIndex(x => x === month);

        if (monthIndex !== -1) {
            displayMonth(months, monthIndex, year);
        }
    }

    if (e.target.tagName == 'CAPTION') {
        let caption = e.target.innerText;
        if (caption.length == 4) {
            displayYears(yearsView)
        } else {
            caption = caption.slice(-4);
            displayYear(years, caption);
        }
    }
})