// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function copyElement(id) {
    // Get the text field
    var copyText = document.getElementById(id).textContent;
    console.log(copyText);
    navigator.clipboard.writeText(copyText);
}
