const fromDatHang = document.getElementById('main-form');

fromDatHang.addEventListener('submit', async (event) => {
    event.preventDefault();

    await CreateDonHang();
    localStorage.removeItem('maGiamGia');

})

//thêm đơn hàng vào database
async function CreateDonHang() {
    deleteGioHangAPI();
    const localGioHang = localStorage.getItem('GioHang');
    if (!localGioHang) {
        alert('Thông tin truyện trong giỏ hàng trống.');
        return;
    }

    //lấy thông tin đơn hàng cần
    const maKhachHang = await getMaKhachHang();

    const now = new Date();
    const DateNow = `${now.getFullYear()}-${(now.getMonth() + 1).toString().padStart(2, '0')}-${now.getDate().toString().padStart(2, '0')}`;

    const diachicuthe = document.getElementById('diachicuthe').value.trim();
    const ward = document.getElementById('ward').value.trim();
    const district = document.getElementById('district').value.trim();
    const city = document.getElementById('city').value.trim();

    const gioHangData = JSON.parse(localGioHang);
    const listjson_chitiet = [];
    const tongTientext = document.querySelector('.product-tongtien').textContent.trim().replace(/[^0-9]/g, '');
    const tongTien = parseInt(tongTientext);

    for (const key in gioHangData) { //key ở đây là mã truyện quản lý từng chi tiết giỏ hàng
        if (gioHangData.hasOwnProperty(key)) {
            const item = gioHangData[key];
            listjson_chitiet.push({
                maTruyen: item.MaTruyen,
                soLuong: item.SoLuongTruyen,
            });
        }
    }

    const DonHang = {
        maKhachHangDatHang: maKhachHang,
        ngayDatHang: DateNow,
        diaChiGiaoHang: `${diachicuthe}, ${ward}, ${district}, ${city}`,
        trangThai: 'Chờ xác nhận',
        phuongThucThanhToan: document.querySelector('input[name="phuongthuc"]:checked').value,
        maGiamGia: localStorage.getItem('maGiamGia'),
        tongTien: tongTien,
        listjson_chitiet: listjson_chitiet
    };

    try {
        const response = await fetch("https://localhost:44346/api/ControllersDonHang/Create", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(DonHang)
        });

        if (response.ok) {
            console.log('Đơn hàng đã được thêm thành công.');
            alert('Đặt hàng thành công, quay lại trang chủ để tiếp tục mua sắm.');
            window.location.href = 'TrangChu.html';
            localStorage.removeItem("GioHang");
        } else {
            console.error('Lỗi khi thêm đơn hàng:', response.status);
            alert('Đặt hàng thất bại, vui lòng thử lại.');
        }
    } catch (error) {
        console.log('Lỗi khi thêm đơn hàng:', error);
        alert('Có lỗi xảy ra trong quá trình thêm đơn hàng, vui lòng thử lại!');
    }
}

async function deleteGioHangAPI() {
    let maGioHang = localStorage.getItem('userLogin');
    if (!maGioHang) {
        console.log("Không có giỏ hàng để xóa.");
        return;
    }

    const url = `https://localhost:44346/api/ControllersGioHang/Delete/${maGioHang}`;

    try {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            console.log("Xóa giỏ hàng thành công.");
        } else {
            console.log("Xóa giỏ hàng thất bại:", response.status);
        }
    } catch (error) {
        console.log("Lỗi khi xóa giỏ hàng:", error);
    }
}

async function getMaKhachHang() {
    let taiKhoan = localStorage.getItem('userLogin');
    if (!taiKhoan) {
        return null;
    }
    console.log(taiKhoan);

    const url = `https://localhost:44346/api/ControllersTaiKhoan/Get-MaKhachHang/${taiKhoan}`;
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const data = await response.json();
        if (data && data.length > 0) {
            return data[0].maKhachHang;
        } else {
            console.log('Không tìm thấy mã khách hàng.');
            return null;
        }
    } catch (error) {
        console.log('Lỗi khi lấy mã khách hàng:', error);
        return null;
    }
}
