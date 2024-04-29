let Rental = [];
let connenction = null;
let rentalIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55149/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RentalCreated", (user, message) => {
        getdata();
    });

    connection.on("RentalDeleted", (user, message) => {
        getdata();
    });

    connection.on("RentalUpdated", (user, message) => {
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
    await fetch('http://localhost:55149/rental')
        .then(x => x.json())
        .then(y => {
            Rental = y;
            //console.log(actors);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Rental.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.rentalId + "</td><td>" + t.rentalDate + "</td><td>" +
            `<button type="button" onclick="remove(${t.rentalId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.rentalId})">Update</button>` +
            "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:55149/rental/' + id, {
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
function showupdate(id) {
    document.getElementById('rentaltoupdate').value = Rental.find(t => t['rentalId'] == id)['rentalDate'];
    document.getElementById('updateformdiv').style.display = 'flex';
    rentalIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('rentaltoupdate').value;
    let formattedName = new Date(name).toISOString();
    fetch('http://localhost:55149/rental', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { rentalDate: formattedName, rentalId: rentalIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('rentalDate').value;
    fetch('http://localhost:55149/rental', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { rentalDate: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function IsThereAnOngoingRental() {
    fetch('http://localhost:55149/Stat/IsThereAnOngoingRental', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.text())
        .then(data => {
            console.log('Response Data:', data);
            document.getElementById('ongoingRentalResult').value = data;
        })
        .catch(error => {
            console.error('Error:', error);
        });
    getdata();
}