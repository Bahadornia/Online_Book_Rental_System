const gridOptions = {
    columnDefs: [
        { headerName: "شناسه", field: "id", sortable: true, filter: true },
        { headerName: "نام کتاب", field: "name", sortable: true, filter: true },
        { headerName: "شابک", field: "isbn", sortable: true, filter: true },
        { headerName: "دسته", field: "categoryName", sortable: true, filter: true },
        { headerName: "نویسنده", field: "author", sortable: true, filter: true },
        { headerName: "ناشر", field: "publisherName", sortable: true, filter: true },
        { headerName: "توضیحات", field: "description", sortable: true, filter: true },
        { headerName: "تصویر", field: "image", sortable: true, filter: true }
    ],
    defaultColDef: {
        flex: 1,
        minWidth: 100,
        resizable: true
    },
    enableRtl: true,
    animateRows: true,
    rowModelType: 'serverSide',
    serverSideStoreType: 'partial', // enables pagination
    pagination: true,
    paginationPageSize: 10,
    theme: "legacy"
}

const datasource = {
    getRows(params) {
        const request = params.request;
        $.ajax({
            url: "/Home/GetAll",
            type: "Get",
            success: function (data) {
                params.success({
                    rowData: data.rows,
                    rowCount: data.total
                });
            },
            error: function (err) {
                console.error(err);
                params.fail();
            }
        });
    }
};
// Initialize Grid
document.addEventListener('DOMContentLoaded', () => {
    const gridDiv = document.querySelector('#agGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource);
});

function openFile() {
    document.getElementById("image").click();
}

function previewImage(input) {
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