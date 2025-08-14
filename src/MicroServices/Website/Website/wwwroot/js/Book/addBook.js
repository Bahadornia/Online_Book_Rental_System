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

$("#publishers").select2({
    placeholder: "ناشر را انتخاب کنید",
    theme: "bootstrap4",
    allowClear: false,
    ajax: {
        url: "/api/publisher",
        data: function (params) {
            var query =
            {
                
            };
            return query;
        },
        contentType: "application/json; charset=utf-8",
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item.name,
                        text: item.name
                    };
                }),
            };
        }
    }
});
