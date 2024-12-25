let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {};

const btnThem = document.querySelector('.btnThem')
btnThem.addEventListener("click", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const maTruyen = urlParams.get('maTruyen');
    const anhTruyen = document.querySelector('.main-product-img').dataset.linkimg;
    const tenTruyen = document.querySelector('.main-product-name').innerHTML.trim();
    const giaBanTruyen = parseFloat(document.querySelector('.main-product-giaban').textContent.replace(/[^0-9]+/g, ""));
    const soLuongTruyen = parseInt(document.querySelector('.input-soluong').value);
    const tongTien = parseFloat(giaBanTruyen) * soLuongTruyen;

    if (GioHang[maTruyen] != null) {
        GioHang[maTruyen].SoLuongTruyen += soLuongTruyen;

        const tongTienCu = parseFloat(GioHang[maTruyen].TongTien);
        const tongTienMoi = tongTienCu + tongTien;
        GioHang[maTruyen].TongTien = tongTienMoi;
    }
    else {
        GioHang[maTruyen] = {
            'MaTruyen': maTruyen,
            'AnhTruyen': anhTruyen,
            'TenTruyen': tenTruyen,
            'GiaBanTruyen': giaBanTruyen,
            'SoLuongTruyen': soLuongTruyen,
            'TongTien': tongTien
        }
    }
    localStorage.setItem("GioHang", JSON.stringify(GioHang));
    updateCartCount();
});