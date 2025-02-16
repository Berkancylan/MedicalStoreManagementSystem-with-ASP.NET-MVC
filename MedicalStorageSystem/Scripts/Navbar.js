document.addEventListener("DOMContentLoaded", () => {
    let currentPage = window.location.pathname.split("/").pop() || "AnaSayfa"; 

    let foundActive = false; 

    document.querySelectorAll(".nav li a").forEach((link) => {
        let menuPage = link.getAttribute("href").split("/").pop();

        if (currentPage === menuPage) {
            link.parentElement.classList.add("active");
            foundActive = true; 
        }
    });

    if (!foundActive) {
        document.querySelector(".nav li:first-child").classList.add("active");
    }
});
function setActive(clickedItem) {
    
    document.querySelectorAll(".nav li").forEach((item) => item.classList.remove("active"));
    clickedItem.classList.add("active");

}
