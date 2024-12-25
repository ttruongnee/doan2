function SetFile(fileName, container) {
    const url = `https://localhost:44346/api/ControllersTruyen/get-poster/${fileName}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Lỗi khi tải hình ảnh');
            }
            return response.blob();
        })
        .then(blob => {
            const fileUrl = URL.createObjectURL(blob);
            container.setAttribute("src", fileUrl);
        })
        .catch(error => {
            console.log('Đã xảy ra lỗi khi tải hình ảnh:', error);
            container.setAttribute("src", "../../Asset/IMG/default-image.jpg");
        });
}

function getTruyenByID() {
    const urlParams = new URLSearchParams(window.location.search);
    const maTruyen = urlParams.get('maTruyen');

    const url = `https://localhost:44346/api/ControllersTruyen/SearchByID/${maTruyen}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(truyen => {
            document.querySelector('.theloai').innerHTML = truyen[0].tenTheLoai;
            document.querySelector('.theloai').href = `DanhMucSP.html?maTheLoai=${truyen[0].maTheLoai}`;
            document.querySelector('.namelink').innerHTML = truyen[0].tenTruyen;
            document.querySelector('.main-product-name').innerHTML = truyen[0].tenTruyen;
            document.querySelector('.main-product-giaban').innerHTML = truyen[0].giaBan.toLocaleString('vi-VN') + 'đ';
            document.querySelector('.main-product-giagoc').innerHTML = truyen[0].giaGoc.toLocaleString('vi-VN') + 'đ';
            document.getElementById('tietkiem').innerHTML = `(Bạn đã tiết kiệm được ${(truyen[0].giaGoc - truyen[0].giaBan).toLocaleString('vi-VN')}đ)`;
            document.getElementById('isbn').innerHTML = truyen[0].isbn;
            document.getElementById('tacgia').innerHTML = truyen[0].tacGia;
            document.getElementById('doituong').innerHTML = truyen[0].doiTuong;
            document.getElementById('sotrang').innerHTML = truyen[0].soTrang;
            document.getElementById('dinhdang').innerHTML = truyen[0].dinhDang;
            document.getElementById('trongluong').innerHTML = truyen[0].trongLuong;
            document.getElementById('theloai').innerHTML = truyen[0].tenTheLoai;
            document.getElementById('theloai').href = `DanhMucSP.html?maTheLoai=${truyen[0].maTheLoai}`;
            // Cập nhật hình ảnh
            SetFile(`${truyen[0].anhTruyen}`, document.querySelector('.main-product-img'))
            document.querySelector('.main-product-img').dataset.linkimg = truyen[0].anhTruyen;
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm truyện:', error);
        });
}

getTruyenByID();