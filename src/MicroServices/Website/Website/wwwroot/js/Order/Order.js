let gridApi;

const datasource = {
    getRows(params) {
        params.request;
        $.ajax({
            url: "/Order/GetAll",
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
        { headerName: "شناسه سفارش", field: "orderId", maxWidth: 150 },
        { headerName: "شناسه کتاب", field: "bookId", sortable: true, filter: true },
        { headerName: "نام کتاب", field: "bookTitle", sortable: true, filter: true },
        { headerName: "شابک", field: "isbn", sortable: true, filter: true },
        { headerName: "تاریخ امانت", field: "orderDate", sortable: true, filter: true },
        { headerName: "تاریخ عودت", field: "description", sortable: true, filter: true },
        { headerName: "تعداد دفعات تمدید شده", field: "description", sortable: true, filter: true },
        { headerName: "وضعیت", field: "statusString", sortable: true, filter: true },
        { headerName: "عملیات", sortable: false, filter: false, cellClass: "operation", cellRenderer: (params) => operationComponent(params.data.id) }
    ],


    defaultColDef: {
        flex: 1,
        minWidth: 60,
        resizable: true
    },
    rowStyle: { display: "flex", alignItems: "center", justifyContent: "center", backGroundColor: 'reds' },
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
    Swal.fire({
        html: 'آیا از حذف کتاب با شناسه' + ` <b>${bookId}</b> ` + 'مطمئن هستید.',
        icon: 'warning',
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        showCancelButton: true,
        confirmButtonText: "بله",
        cancelButtonText: "لغو",
    }).then((result) => {
        if (result.isConfirmed) {
            $("#deleteBook input[name='bookId']").val(bookId);
            $("#deleteBook").submit();
        };
    });
}


let operationComponent = (id) => {
    let html = "";
    if (isAdmin == "true" || true) {
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


