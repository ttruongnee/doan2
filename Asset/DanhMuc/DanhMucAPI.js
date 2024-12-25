const urlParamtl = new URLSearchParams(window.location.search);
const maTheLoai = urlParamtl.get('maTheLoai');

if (maTheLoai) {
    getTruyenByTheLoai(`${maTheLoai}`, function (listTruyen) {
        getTruyenByCount('main-truyen', listTruyen, 9999);
        if (maTheLoai === 'get-all') {
            document.getElementById('tendanhmuc').innerHTML = "TẤT CẢ SẢN PHẨM";
            document.getElementById('title-top').innerHTML = "TẤT CẢ SẢN PHẨM";
            document.getElementById('title-link-mini').innerHTML = "/ Tất cả sản phẩm";
        }
        else {
            document.getElementById('tendanhmuc').innerHTML = listTruyen[0].tenTheLoai;
            document.getElementById('title-top').innerHTML = listTruyen[0].tenTheLoai;;
            document.getElementById('title-link-mini').innerHTML = `/ ${listTruyen[0].tenTheLoai}`;
        }
    });
}

