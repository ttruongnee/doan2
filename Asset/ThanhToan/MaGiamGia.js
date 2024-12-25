document.getElementById('giamgia').addEventListener('submit', function (event) {
    event.preventDefault();
    const input_magiamgia = document.getElementById('magiamgia').value.trim();
    let phantramgiamgia = 0;

    const url = `https://localhost:44346/api/ControllersGiamGia/ApplyMaGiamGia/${input_magiamgia}`;
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                document.querySelector('.fail-apply').style.display = 'none';
                document.querySelector('.success-apply').style.display = 'block';
                document.querySelector('.success-apply').innerHTML = `Áp dụng mã giảm giá ${input_magiamgia.toUpperCase()} thành công!`;

                //gán tạm mã giảm giá vào local để lưu vào thông tin đơn hàng nếu thanh toán đơn hàng
                localStorage.setItem('maGiamGia', input_magiamgia.toUpperCase().trim());

                phantramgiamgia = data[0].phanTramGiamGia;
                const tongTienMoi = getTongTien() * (1 - (phantramgiamgia / 100));
                document.querySelector('.product-discount').innerHTML = `${(getTongTien() - tongTienMoi).toLocaleString('vi-VN')}đ`;
                document.querySelector('.product-tongtien').innerHTML = `${tongTienMoi.toLocaleString('vi-VN')}đ`;
            }
            else {
                document.querySelector('.success-apply').style.display = 'none';
                document.querySelector('.fail-apply').style.display = 'block';
                document.querySelector('.product-discount').innerHTML = '0đ';
                document.querySelector('.product-tongtien').innerHTML = `${getTongTien().toLocaleString('vi-VN')}đ`;

                localStorage.removeItem('maGiamGia');
            }
        })
        .catch(error => {
            console.log('Lỗi khi áp dụng mã giảm giá:', error);
        });
});

document.getElementById('magiamgia').addEventListener('input', function () {
    const input_magiamgia = document.getElementById('magiamgia').value.trim();
    if (input_magiamgia === '') {
        document.querySelector('.fail-apply').style.display = 'none';
        document.querySelector('.success-apply').style.display = 'none';
        return;
    }

    const url = `https://localhost:44346/api/ControllersGiamGia/ApplyMaGiamGia/${input_magiamgia}`;
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.length > 0) {
                document.querySelector('.fail-apply').style.display = 'none';
            }
            else {
                document.querySelector('.fail-apply').style.display = 'block';
                document.querySelector('.fail-apply').innerHTML = `Mã giảm giá ${input_magiamgia.toUpperCase()} không tồn tại!`

            }
        })
        .catch(error => {
            console.log('Lỗi khi áp dụng mã giảm giá:', error);
        });
});

function getTongTien() {
    let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {};
    let tongtien = 0;

    // Tính tổng tiền tất cả sản phẩm trong giỏ hàng
    for (let item in GioHang) {
        tongtien += parseFloat(GioHang[item].TongTien);
    }
    return tongtien;
}
