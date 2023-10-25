document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Loaded")
});
window.addEventListener('load', menuFunc);
window.addEventListener('resize', menuFunc);

function toggleMenu() {
    var menu = document.getElementById("navegacion");
    if (menu.style.display === "block") {
        menu.style.display = "none";
        document.body.style.overflow = "auto";
        document.getElementById('header').style.background = 'none';
    } else {
        menu.style.display = "block";
        document.body.style.overflow = "hidden";
        document.getElementById('header').style.background = 'rgba(248, 248, 248, 0.6)';
    }
}
function menuFunc() {
    var menu = document.getElementById("navegacion");
    var menuButton = document.getElementById("menuButton");
    if (window.innerWidth > 666) {
        menu.style.display = "block";
        menuButton.style.display = "none";
        document.getElementById('header').style.background = 'rgba(248, 248, 248, 0.6)';
    } else {
        menu.style.display = "none";
        menuButton.style.display = "block";
        document.getElementById('header').style.background = 'none';
    }
}