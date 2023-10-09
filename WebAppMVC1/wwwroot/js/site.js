// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let tableBody = document.querySelector("tbody");
tableBody.addEventListener("click", (e) => {
    let target = e.target.closest("tr[user-id]");
    if (target && target.getAttribute("user-id")) {
        let userId = target.getAttribute("user-id");
        document.getElementById("super-edit").setAttribute("href", "/OrganisationUsers/Edit/" + userId);
        document.getElementById("super-details").setAttribute("href", "/OrganisationUsers/Details/" + userId);
        document.getElementById("super-delete").setAttribute("href", "/OrganisationUsers/Delete/" + userId);
    }
});