// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



// Initialize Grid

agGrid.LicenseManager
    .setLicenseKey("DownloadDevTools_COM_NDEwMjM0NTgwMDAwMA==59158b5225400879a12a96634544f5b6");

export class AppGrid {

    static Create(gridOptions) {
        return new AppGrid(gridOptions).init(gridOptions);
    }
    init(gridOptions) {
        let gridApi;
        const gridDiv = document.querySelector('#agGrid');
        document.addEventListener('DOMContentLoaded', () => {
            gridApi = agGrid.createGrid(gridDiv, gridOptions)
        });
        return gridApi;
    }
}

