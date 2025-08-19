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


function operationComponent(id) {
    html = `
     
<button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="رزرو" onclick="reserveBook('${id}')"><i class="fa fa-bookmark" aria-hidden="true"></i>
</button>

`;

    return html;
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
                        rowCount: data.totalCount
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
