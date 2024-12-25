const trangs = document.querySelectorAll('.trang');

function TrangChon(trang) {
    document.querySelector('.trangchon').classList.remove('trangchon')
    trang.classList.add('trangchon')
}

trangs.forEach((trang) => {
    trang.addEventListener('click', () => { TrangChon(trang) });
})


