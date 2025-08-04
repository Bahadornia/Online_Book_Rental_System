agGrid.LicenseManager
    .setLicenseKey("DownloadDevTools_COM_NDEwMjM0NTgwMDAwMA==59158b5225400879a12a96634544f5b6");
window.App = window.App || {};

document.getElementById("logoutButton").addEventListener('click', () => {
    $("#logoutForm").submit();
})
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7101/OrderNotification")
    .withAutomaticReconnect()
    .build();
connection.onclose(err => console.error("Connection closed:", err));
connection.on("SendOrderCreatedNotification", (message) => {
    console.log(message)
});

connection.on("SendOverDuetedOrderNotification", (message) => {
    console.log(message);
    var temp = JSON.parse(message[0].payload);
    document.getElementById("notification-badge").textContent = temp.length;
});

connection.start()
    .then(() => console.log("Connected on hub"))
    .catch(err => console.error(err.toString()));
//setUpSingalRConnection();
//    function setUpSingalRConnection() {

       

//    };
