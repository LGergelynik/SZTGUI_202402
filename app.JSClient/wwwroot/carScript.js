let Car = [];
let connenction = null;
let carIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55149/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getdata();
    });

    connection.on("CarDeleted", (user, message) => {
        getdata();
    });

    connection.on("CarUpdated", (user, message) => {
        getdata();
    });
    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:55149/car')
        .then(x => x.json())
        .then(y => {
            Car = y;
            //console.log(actors);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Car.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.carId + "</td><td>" + t.model + "</td><td>" +
            `<button type="button" onclick="remove(${t.carId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.carId})">Update</button>` +
            /*`<button type="button" onclick="countRentalEvents(${t.carId})">Count</button>` +*/
            "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:55149/car/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function showupdate(id)
{
    document.getElementById('modeltoupdate').value = Car.find(t => t['carId'] == id)['model'];
    document.getElementById('updateformdiv').style.display = 'flex';
    carIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('modeltoupdate').value;
    fetch('http://localhost:55149/car', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { model: name, carId: carIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('model').value;
    fetch('http://localhost:55149/car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { model: name })
    })
        .then(response => response)
         .then(data => {console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function getMostPopularBrand() {
    fetch('http://localhost:55149/Stat/GetMostPopularBrand', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.text())
        .then(data => {
            console.log('Response Data:', data); 
            document.getElementById('popularBrandResult').value = data;
        })
        .catch(error => {
            console.error('Error:', error);
        });
    getdata();
}
function countRentalEvents() {
    let carId = document.getElementById('carId').value;
    fetch(`http://localhost:55149/Stat/CountRentalEvents/${carId}`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(data => {
            console.log('Count of Rental Events for Car:', data);
            document.getElementById('rentalEventsCount').value = data; 
        })
        .catch(error => {
            console.error('Error:', error);
            document.getElementById('rentalEventsCount').value = 0;
        });
}