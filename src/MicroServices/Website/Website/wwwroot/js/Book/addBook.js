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

$("#publishers-select").select2({
    placeholder: "ناشر را انتخاب کنید",
    theme: "bootstrap4",
    minimumResultsForSearch: -1,
    delay:250,
    minimumInputLength: 1,
    
    allowClear: false,
    ajax: {
        url: "/api/publisher/getAll",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var query =
            {

            };
            return query;
        },
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
    },
    tags:true

});