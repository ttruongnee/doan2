const radioButtons = document.querySelectorAll('input[name="phuongthuc"]');
const qrPay = document.getElementById('qr-pay');

radioButtons.forEach(button => {
    button.addEventListener('change', function () {
        if (this.value === 'Chuyển khoản ngân hàng') {
            qrPay.style.display = 'block';
        } else {
            qrPay.style.display = 'none';
        }
    });
});

function displayThanhToan() {
    let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {};
    let listProduct = document.getElementById('list-product');

    if (listProduct != null) {
        listProduct.innerHTML = ''; // Xóa nội dung cũ trong listProduct
    }

    // Tính tổng tiền tạm tính
    let tongtien = 0;

    // Lặp qua các sản phẩm trong đơn hàng
    for (let item in GioHang) {
        tongtien += GioHang[item].SoLuongTruyen * GioHang[item].GiaBanTruyen;

        let product = `
            <div class="product-item">
                <div class="product-img">
                    <img id="anhTruyen-${GioHang[item].MaTruyen}">
                    <span class="soluongsp">${GioHang[item].SoLuongTruyen}</span>
                </div>
                <div class="product-info">
                    <p class="product-name">${GioHang[item].TenTruyen}</p>
                    <p class="product-price">${(GioHang[item].GiaBanTruyen).toLocaleString('vi-VN')}đ</p>
                </div>
            </div>`;

        listProduct.insertAdjacentHTML('beforeend', product);
        SetFile(`${GioHang[item].AnhTruyen}`, document.getElementById(`anhTruyen-${GioHang[item].MaTruyen}`));
    }
    document.querySelector('.product-tamtinh').innerHTML = `${tongtien.toLocaleString('vi-VN')}đ`;
    document.querySelector('.product-tongtien').innerHTML = `${tongtien.toLocaleString('vi-VN')}đ`;
}

function SetFile(fileName, listProduct) {
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
            listProduct.setAttribute("src", fileUrl);
        })
        .catch(error => {
            console.log('Đã xảy ra lỗi khi tải hình ảnh:', error);
            listProduct.setAttribute("src", "../../Asset/IMG/default-image.jpg");
        });
}


// Gọi hàm khi trang được load
displayThanhToan();
