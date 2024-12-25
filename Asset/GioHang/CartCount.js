function updateCartCount() {
    let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {}
    let count = 0

    for (let item in GioHang) {
        count += GioHang[item].SoLuongTruyen
    }

    let SLGioHang = document.querySelectorAll("#giohang .soluong")  //có 2 biến hiển thị, 1 ở pc tablet 1 ở mobile
    if (SLGioHang) {
        SLGioHang.forEach(phanTuSoLuong => {
            phanTuSoLuong.textContent = count < 10 ? count : "9+"
        })
    }
}

updateCartCount();