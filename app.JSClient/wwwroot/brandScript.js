let Brands = [];
let connection = null;
let BrandIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:55149/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });

    connection.on("BrandUpdated", (user, message) => {
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
    await fetch('http://localhost:55149/brands')
        .then(x => x.json())
        .then(y => {
            Brands = y;
            //console.log(actors);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    Brands.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.brandId + "</td><td>" + t.brandName + "</td><td>" +
            `<button type="button" onclick="remove(${t.brandId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.brandId})">Update</button>` +
            "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:55149/Brands/' + id, {
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
    document.getElementById('brandNametoupdate').value = Brands.find(t => t['brandId'] == id)['brandName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    brandIdToUpdate = id;
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('brandNametoupdate').value;
    fetch('http://localhost:55149/Brands', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name, brandId: brandIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('brandName').value;
    fetch('http://localhost:55149/brands', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function CountCarsforBrand() {
    let brandId = document.getElementById('brandId').value; 
    fetch(`http://localhost:55149/Stat/CountCarsForBrand/${brandId}`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(data => {
            console.log('Count of Cars for Brand:', data);
            document.getElementById('carForBrandCount').value = data;
        })
        .catch(error => {
            console.error('Error:', error);
            document.getElementById('carForBrandCount').value = 0; 
        });
}
function FindBrandWithMostCars() {
    fetch('http://localhost:55149/Stat/FindBrandWithMostCars', {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.text())
        .then(data => {
            console.log('Response Data:', data);
            document.getElementById('mostCarsResult').value = data; x
        })
        .catch(error => {
            console.error('Error:', error);
        });
    getdata();
}