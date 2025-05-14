
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

const gridOptions = {
    columnDefs: [
        { headerName: "تصویر", field: "imageUrl", sortable: true, filter: true , cellClass:"logo", cellRenderer:(params)=> `<img src="${params.value}">`},
        { headerName: "نام کتاب", field: "name", sortable: true, filter: true },
        { headerName: "شناسه", field: "id", sortable: true, filter: true },
        { headerName: "شابک", field: "isbn", sortable: true, filter: true },
        { headerName: "دسته", field: "categoryName", sortable: true, filter: true },
        { headerName: "نویسنده", field: "author", sortable: true, filter: true },
        { headerName: "ناشر", field: "publisherName", sortable: true, filter: true },
        { headerName: "توضیحات", field: "description", sortable: true, filter: true },
    ],
   
    defaultColDef: {
        flex: 1,
        minWidth: 100,
        resizable: true
    },
    enableRtl: true,
    animateRows: true,
    rowModelType: 'serverSide',
    serverSideDatasource: datasource,
    pagination: true,
    paginationPageSize: 10,
}

const gridDiv = document.querySelector('#agGrid');
let gridApi ;


// Initialize Grid
document.addEventListener('DOMContentLoaded', () => {
    gridApi = agGrid.createGrid(gridDiv, gridOptions)
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

function onSuccess() {
    gridApi.refreshServerSide();
}

function onFailure() {
    alert("Error!");
}
