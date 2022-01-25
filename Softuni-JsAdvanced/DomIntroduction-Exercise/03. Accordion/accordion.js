function toggle() {
    let first = document.getElementsByClassName('button')[0].textContent;
    if(first == 'More'){
        document.getElementsByClassName('button')[0].textContent = 'Less';
        document.getElementById('extra').style.display = 'block';
    }else if(first =='Less'){
        document.getElementsByClassName('button')[0].textContent = 'More';
        document.getElementById('extra').style.display = 'none';
    }


}