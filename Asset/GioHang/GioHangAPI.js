//viết hàm cho checklogin dùng =))

//thêm giỏ hàng vào sql
async function CreateGioHang() {
    deleteGioHang();
    const localGioHang = localStorage.getItem('GioHang');
    if (!localGioHang || Object.keys(JSON.parse(localGioHang)).length === 0) {
        alert('Giỏ hàng trống.');
        return;
    }

    const gioHangData = JSON.parse(localGioHang);
    const listjson_chitiet = [];
    let tongTien = 0;

    //key ở đây là mã truyện quản lý từng chi tiết giỏ hàng
    for (const key in gioHangData) {
        if (gioHangData.hasOwnProperty(key)) {
            const item = gioHangData[key];
            listjson_chitiet.push({
                maTruyen: item.MaTruyen,
                soLuong: item.SoLuongTruyen,
            });
            tongTien += item.TongTien;
        }
    }

    const maKhachHang = await getMaKhachHang();
    const GioHang = {
        maGioHang: localStorage.getItem('userLogin'),
        maKhachHang: maKhachHang,
        tongTien: tongTien,
        listjson_chitiet: listjson_chitiet
    };

    try {
        const response = await fetch("https://localhost:44346/api/ControllersGioHang/Create", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(GioHang)
        });

        if (response.ok) {
            alert('Đã lưu lại giỏ hàng của bạn vào hệ thống!');
            localStorage.removeItem("GioHang");
        } else {
            alert('Thêm giỏ hàng thất bại, vui lòng thử lại.');
        }
    } catch (error) {
        alert('Có lỗi xảy ra trong quá trình thêm giỏ hàng, vui lòng thử lại!');
    }
}

async function deleteGioHang() {
    const maGioHang = localStorage.getItem('userLogin');
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
    const taiKhoan = localStorage.getItem('userLogin');
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
