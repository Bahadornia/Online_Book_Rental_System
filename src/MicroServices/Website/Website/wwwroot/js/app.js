    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub/OrderNotification", { headers: { "X-CSRF": "1" } })
        .withAutomaticReconnect()
        .build();

    connection.onclose(err => console.error("Connection closed:", err));
    connection.on("SendOrderCreatedNotification", (message) => {
        console.log(message)
    });

    connection.on("SendOrderCreatedNotification", (message) => {
        console.log(message);
        var temp = JSON.parse(message[0].payload);
        document.getElementById("notification-badge").textContent = temp.length;
    });

    connection.start()
        .then(() => console.log("Connected on hub"))
        .catch(err => console.error(err.toString()));


agGrid.LicenseManager
    .setLicenseKey("DownloadDevTools_COM_NDEwMjM0NTgwMDAwMA==59158b5225400879a12a96634544f5b6");
window.App = window.App || {};

document.getElementById("logoutButton").addEventListener('click', () => {
    $("#logoutForm").submit();
})


$("#persian").on('click', () => {

    $.ajax({
        url: "/Home/SetUserCulture",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Culture: $("#persian").val(),
            Theme:""
        }),
        headers: {
            "RequestVerificationToken": $("input[name=__RequestVerificationToken]").val()
        },
        success: function () {
            window.location.reload();
        },
        error: function () {
           
        }
    })
});

$("#english").on('click', () => {

    $.ajax({
        url: "/Home/SetUserCulture",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Culture: $("#english").val(),
            Theme: ""
        }),
        headers: {
            "RequestVerificationToken": $("input[name=__RequestVerificationToken]").val()
        },
        success: function () {
            window.location.reload();
        },
        error: function () {
          
        }
    })

})
