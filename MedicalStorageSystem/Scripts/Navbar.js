document.addEventListener("DOMContentLoaded", () => {
    let currentPage = window.location.pathname.split("/").pop(); 

    document.querySelectorAll(".nav li a").forEach((link) => {
        let menuPage = link.getAttribute("href").split("/").pop(); 

        if (currentPage === menuPage) {
            link.parentElement.classList.add("active"); 
        }
    });
});
