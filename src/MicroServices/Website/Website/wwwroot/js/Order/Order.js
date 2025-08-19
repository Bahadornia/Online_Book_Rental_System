let gridApi;

const dataSource = {
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

const columnsDef = [
    { headerName: "شناسه سفارش", field: "orderId", maxWidth: 150 },
    { headerName: "شناسه کتاب", field: "bookId", sortable: true, filter: true },
    { headerName: "نام کتاب", field: "bookTitle", sortable: true, filter: true },
    { headerName: "شابک", field: "isbn", sortable: true, filter: true },
    { headerName: "تاریخ امانت", field: "orderDate", sortable: true, filter: true },
    { headerName: "تاریخ عودت", field: "description", sortable: true, filter: true },
    { headerName: "تعداد دفعات تمدید شده", field: "description", sortable: true, filter: true },
    { headerName: "وضعیت", field: "statusString", sortable: true, filter: true },
    { headerName: "عملیات", sortable: false, filter: false, cellClass: "operation", cellRenderer: (params) => operationComponent(params.data.id) }
];


let gridObject = new Grid(dataSource, columnsDef);

gridApi = gridObject.drawGrid();


    
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

