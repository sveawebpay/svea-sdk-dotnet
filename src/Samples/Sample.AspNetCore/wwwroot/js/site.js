// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "table-row-group") {
            content.style.display = "none";
        } else {
            content.style.display = "table-row-group";
        }
    });
}

var updateSettings = function(element) {
    var form = element.closest('form');
    form.find('[type="hidden"]:first').val(element.html());
    form.submit();
};