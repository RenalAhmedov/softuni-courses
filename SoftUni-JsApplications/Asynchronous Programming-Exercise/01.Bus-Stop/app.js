async function getInfo() {

    const stopNameEl = document.getElementById('stopName');
    const timetableEl = document.getElementById('buses');
    const submitButton = document.getElementById('submit');

    const stopId = document.getElementById('stopId').value;
    const url = `http://localhost:3030/jsonstore/bus/businfo/${stopId}`;

    try {
        stopNameEl.textContent = 'Loading...';
        timetableEl.replaceChildren();
        submitButton.disabled = true;

        const res = await fetch(url);

        if (res.status !== 200) {
            throw new Error('Stop ID not found!');
        }
        const data = await res.json();

        stopNameEl.textContent = data.name;

        Object.entries(data.buses).forEach(b => {
            const liElement = document.createElement('li');
            liElement.textContent = `Bus ${b[0]} arrives in ${b[1]} minutes`;
            timetableEl.appendChild(liElement);
        });
        submitButton.disabled = false;

    } catch (error) {
        stopNameEl.textContent = 'Error';
    }

}