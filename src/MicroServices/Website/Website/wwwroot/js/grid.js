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
class Grid {
    geridApi;
    constructor(dataSource, columnDefs) {
        this.gridOptions = {
            defaultColDef: {
                flex: 1,
                minWidth: 60,
                resizable: true
            },
            rowStyle: { display: "flex", alignItems: "center", justifyContent: "center" },
            rowHeight: 60,
            enableRtl: true,
            animateRows: true,
            serverSideDatasource: dataSource,
            columnDefs: columnDefs,
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
    }
    drawGrid() {
        const gridDiv = document.querySelector('#agGrid');
        document.addEventListener('DOMContentLoaded', () => {
            this.gridApi = agGrid.createGrid(gridDiv, this.gridOptions)
        });
        return this.gridApi;
    }
}