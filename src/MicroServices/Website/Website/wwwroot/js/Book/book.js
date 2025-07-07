let gridApi;
var isAdmin = $("#isAdmin").val();

const datasource = {
    getRows(params) {
        params.request;
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
        { headerName: "تصویر", field: "imageUrl", sortable: true, filter: true, cellClass: "logo", cellRenderer: (params) => `<img src="${params.value}">`, maxWidth: 100 },
        { headerName: "نام کتاب", field: "title", sortable: true, filter: true },
        { headerName: "شناسه", field: "id", sortable: true, filter: true },
        { headerName: "شابک", field: "isbn", sortable: true, filter: true },
        { headerName: "دسته", field: "category", sortable: true, filter: true },
        { headerName: "نویسنده", field: "author", sortable: true, filter: true },
        { headerName: "ناشر", field: "publisher", sortable: true, filter: true },
        { headerName: "توضیحات", field: "description", sortable: true, filter: true },
        { headerName: "عملیات", sortable: false, filter: false, cellClass: "operation", cellRenderer: (params) => operationComponent(params.data.id) }
    ],


    defaultColDef: {
        flex: 1,
        minWidth: 60,
        resizable: true
    },
    rowStyle: { display: "flex", alignItems: "center", justifyContent: "center",backGroundColor:'reds' },
    rowHeight: 60,
    enableRtl: true,
    animateRows: true,
    cellStyle: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center'
    },
    headerStyle: {
        justifyContent: "center",
        display: 'flex'
    },
    rowModelType: 'serverSide',
    serverSideDatasource: datasource,
    pagination: true,
    paginationPageSize: 10,
    getRowId: params => params.data.id,
}
reserveBook = (id) => {

    $.ajax({
        url: `/Home/RentBook`,
        type: 'Post',
        data: {

            BookId: id,
            UserId: 0
        },
        success: function (data) {

        },
        error: function (err) {
            console.error(err);
        }
    })
}
deleteBook = bookId => {
    $("#deleteBook input[name='bookId']").val(bookId);
    $("#deleteBook").submit();

}


let operationComponent = (id) => {
    let html = "";
    if (isAdmin == "true") {
        html = `
<button class="btn btn-sm"><i class="fa fa-pencil" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" onclick="reserveBook('${id}')"><i class="fa fa-bookmark" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" onclick = "deleteBook('${id}')"><i class="fa fa-trash" aria-hidden="true"></i></i>
</button>
`;
    }
    else {
        html = `

<button class="btn btn-sm" onclick="reserveBook('${id}')"><i class="fa fa-bookmark" aria-hidden="true"></i>
</button>

`;
    }
    return html;
}


const gridDiv = document.querySelector('#agGrid');
document.addEventListener('DOMContentLoaded', () => {
    gridApi = agGrid.createGrid(gridDiv, gridOptions)
});

openFile = () => {
    document.getElementById("image").click();
}

onSuccessDeleteBook = (data) => {
    gridApi.refreshServerSide();
    Swal.fire({
        html: 'کتاب با شناسه' + ` <b>${data}</b> ` + 'حذف شد.',
        icon: 'success',
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        showCancelButton: false,
        confirmButtonText: "باشه"
    })
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

onSuccess = (data) => {
    gridApi.refreshServerSide();
    Swal.fire({
        title: 'کتاب مورد نظر با موفقیت اضافه شد.',
        icon: 'success',
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        showCancelButton: false,
        confirmButtonText: "باشه"
    })
}

onFailure = () => {
    alert("Error!");
}

onSuccessFilterBook = (data) => {
    gridApi.setGridOption('rowData', []);
    gridApi.setGridOption('rowData', data);
}

document.getElementById("searchButton").addEventListener('click', () => {
    let arr = $("#searchForm").serializeArray();
    let formObject = arr.reduce((obj, item) => { obj[item.name] = item.value ?? ""; return obj }, {})

    gridApi.setServerSideDatasource({
        getRows(params) {
            params.request;
            $.ajax({
                url: "/Home/Search",
                type: "Post",
                data: {
                    ...formObject
                },
                headers: {
                    'RequestVerificationToken': $('#searchForm input[name="__RequestVerificationToken"]').val()
                },
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
})
});
