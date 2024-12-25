const checkSizeMobile = document.querySelectorAll('.checkSizeMobile');
function checkScreenSize() {
    checkSizeMobile.forEach(item => {
        if (window.innerWidth > 520) {
            item.classList.remove('c-6');
            item.classList.add('c-4');
        }
        else {
            item.classList.remove('c-4');
            item.classList.add('c-6');
        }
    })
}

window.addEventListener('resize', checkScreenSize);
window.addEventListener('load', checkScreenSize);

