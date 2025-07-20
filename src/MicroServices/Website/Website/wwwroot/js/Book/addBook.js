onSuccess = (data) => {
   
}

onFailure = () => {
    alert("Error!");
}


openFile = () => {
    document.getElementById("image").click();
}
previewImage = () => {
    var file = $("input[type=file]").get(0).files[0];
    var name = file.name;
    $("Image").val(name);
    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#previewImg").attr("src", reader.result);
        }

        reader.readAsDataURL(file);
    }
}

