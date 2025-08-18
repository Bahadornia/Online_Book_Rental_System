class ButtonRenderer {
    init(params) {
        this.eGui = document.createElement('button');
        this.eGui.className = 'btn btn-sm btn-primary';
        this.eGui.innerText = 'Click me';

        this.eGui.setAttribute('title', 'Click to perform action');
        this.eGui.setAttribute('data-bs-toggle', 'tooltip');

        // Add your click logic here
        this.eGui.addEventListener('click', () => {
            alert('Button clicked!');
        });

        // Initialize Bootstrap tooltip
        setTimeout(() => {
            new bootstrap.Tooltip(this.eGui);
        }, 0);
    }

    getGui() {
        return this.eGui;
    }

    destroy() {
        // Optional: cleanup
        bootstrap.Tooltip.getInstance(this.eGui)?.dispose();
    }
}
let gridApi;
var isAdmin = $("#isAdmin").val();

const datasource = {
    getRows(params) {
        console.log(params.request);
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
const fa = {
    Page: "صفحه",
    page: "صفحه",
    from: "از",
    to: "به",
    of: "از",
    Size: "سایز",
    pageSize: 'اندازه صفحه',
    pageLastRowUnknown: '?',
    nextPage: 'صفحه بعد',
    lastPage: 'آخرین صفحه',
    firstPage: 'اولین صفحه',
    previousPage: 'صفحه قبلی',
    pageSizeSelectorLabel: 'اندازه صفحه:',
    ariaPageSizeSelectorLabel: 'اندازه صفحه',

}
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

    localeTextFunc: (key, defaultValue) => {
        // Peek keys during dev:
        // console.log('locale key:', key, 'default:', defaultValue);
        if (key === 'Page Size') return 'اندازه صفحه';
        return fa[pageSize] ?? defaultValue;
    },
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
    paginationPageSize: 25,
    cacheBlockSize: 25,
    localeText: fa,
    getRowId: params => params.data.id,
    onFirstDataRendered: (event) => {

        const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
        [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
    },
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


function operationComponent(id) {
    let html = "";
    if (isAdmin == "true" || true) {
        html = `
       <button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="ویرایش"><i class="fa fa-pencil" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="رزرو" onclick="reserveBook('${id}')"><i class="fa fa-bookmark" aria-hidden="true"></i>
</button>
<button class="btn btn-sm" data-bs-toggle="tooltip" data-bs-placement="top" data-bs-title="حذف" onclick = "deleteBook('${id}')"><i class="fa fa-trash" aria-hidden="true"></i></i>
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
