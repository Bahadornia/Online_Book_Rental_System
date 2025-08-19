let gridApi;

const dataSource = {
    getRows(params) {
        params.request;
        $.ajax({
            url: "/Home/GetAll",
            type: "POST",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(params.request),
            success: function (data) {
                params.success({
                    rowData: data.rows,
                    rowCount: data.totalCount
                });

            },
            error: function (err) {
                console.error(err);
                params.fail();
            }
        });
    }
};
const columnsDef = [
    { headerName: "تصویر", field: "imageUrl", sortable: true, filter: true, cellClass: "logo", cellRenderer: (params) => `<img src="${params.value}">`, maxWidth: 100 },
    { headerName: "نام کتاب", field: "title", sortable: true, filter: true },
    { headerName: "شناسه", field: "id", sortable: true, filter: true },
    { headerName: "شابک", field: "isbn", sortable: true, filter: true },
    { headerName: "دسته", field: "category", sortable: true, filter: true },
    { headerName: "نویسنده", field: "author", sortable: true, filter: true },
    { headerName: "ناشر", field: "publisher", sortable: true, filter: true },
    { headerName: "توضیحات", field: "description", sortable: true, filter: true },
    { headerName: "عملیات", sortable: false, filter: false, cellClass: "operation", cellRenderer: (params) => operationComponent(params.data.id) }
];

let gridObject = new Grid(dataSource, columnsDef);

gridApi = gridObject.drawGrid();

function operationComponent(id) {
    html = `
       <button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="ویرایش"><i class="fa fa-pencil" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="رزرو" onclick="reserveBook('${id}')"><i class="fa fa-bookmark" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="حذف" onclick = "deleteBook('${id}')"><i class="fa fa-trash" aria-hidden="true"></i></i>
</button>
`;

    return html;
}
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