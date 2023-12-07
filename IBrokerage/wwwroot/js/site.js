// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', () => {
    let sidebar = document.querySelector(".sidebar");
    let contentWrapper = document.querySelector("main");
    let hamburgerMenu = document.querySelector(".hamburger");
    let navLinks = document.querySelectorAll(".nav-link");
    let navIcons = document.querySelectorAll(".nav-icon");
    let navText = document.querySelectorAll(".nav-text");
    let collapseBtn = document.querySelectorAll(".collapse-toggle");
    let collapseIcon = document.querySelectorAll(".collapse-icon");
    let collapseItem = document.querySelectorAll(".collapse-item");
    let sidebarTitle = document.querySelectorAll(".sidebar-title");
    let navBrandSm = document.querySelector(".navbrand-sm");
    let navBrandLg = document.querySelector(".navbrand-lg");


    collapseBtn.forEach((c, i) => {
        collapseItem[i].classList.add("collapsed");
        let isRotated = false;
        c.addEventListener("click", () => {
            if (collapseItem[i].classList.contains("collapsed")) {
                collapseItem[i].classList.remove("collapsed");
                isRotated = true;
            }
            else {
                collapseItem[i].classList.add("collapsed");
                isRotated = false;
            }
            collapseIcon[i].style.transform = `rotate(${isRotated ? 90 : 0}deg)`;
        })
    })


    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            // Remove the "active" class from all links
            navLinks.forEach(link => link.classList.remove('active'));

            // Add the "active" class to the clicked link
            link.classList.add('active');
        });
    });

    let toggleSidebar = function () {
        hamburgerMenu.firstElementChild.classList.replace("bi-list", "bi-arrow-right");
        if (sidebar.classList.contains("expand-sidebar")) {
            sidebar.classList.replace("expand-sidebar", "not-expanded")
            contentWrapper.style.marginLeft = "80px";
            navBrandLg.classList.toggle("d-none");
            navBrandSm.classList.toggle("d-none");

            navLinks.forEach(l => {
                l.classList.add("justify-content-center");
            })

            navIcons.forEach(i => {
                i.classList.add("me-0");
            })

            navText.forEach(i => {
                i.classList.toggle("d-none");
            })

            collapseIcon.forEach(ci => {
                ci.classList.toggle("d-none");
            })

            collapseItem.forEach(i => {
                i.classList.toggle("d-none");
            })

            sidebarTitle.forEach(t => {
                t.classList.toggle("d-none");
            })
        }
        else {
            hamburgerMenu.firstElementChild.classList.replace("bi-arrow-right", "bi-list");
            sidebar.classList.replace("not-expanded", "expand-sidebar");
            contentWrapper.style.marginLeft = "260px";
            navBrandLg.classList.toggle("d-none");
            navBrandSm.classList.toggle("d-none");

            navLinks.forEach(l => {
                l.classList.remove("justify-content-center");
            })

            navIcons.forEach(i => {
                i.classList.remove("me-0");
            })

            navText.forEach(i => {
                i.classList.toggle("d-none");
            })

            collapseIcon.forEach(ci => {
                ci.classList.toggle("d-none");
            })

            collapseItem.forEach(I => {
                I.classList.toggle("d-none");
            })

            sidebarTitle.forEach(t => {
                t.classList.toggle("d-none");
            })
        }
    }

    hamburgerMenu.addEventListener("click", toggleSidebar);
});




