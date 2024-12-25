let currentIndex = -1; // Lưu chỉ số của đơn hàng đang sửa xoá
let danhSachDonHang = [];
let danhSachNhanVien = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersDonHang/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachDonHang = data;
            getAllDonHang();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách đơn hàng từ API:', error);
        });
}

function getNhanVienFromAPI() {
    const url = "https://localhost:44383/api/ControllersNhanVien/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachNhanVien = data;
            BindDataCombobox(danhSachNhanVien, 'nhanvien');
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách nhân viên từ API:', error);
        });
}

function BindDataCombobox(danhSachNhanVien, selectId) {
    const selectElement = document.getElementById(selectId);

    danhSachNhanVien.forEach((nv) => {
        const option = document.createElement('option');
        option.value = nv.maNhanVien;
        option.text = nv.tenNhanVien;

        selectElement.appendChild(option);
    });
}

function hienDialog(index) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    currentIndex = index;

    // Điền thông tin nhân viên vào form 
    const donHang = danhSachDonHang[currentIndex];

    if (donHang.tenNhanVien) {
        document.getElementById('nhanvien').value = donHang.maNhanVienXuLyDonHang;
    }
    document.getElementById('trangthai').value = donHang.trangThai;
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    document.getElementById('nhanvien').value = '';
    document.getElementById('trangthai').value = '';
    currentIndex = -1;
}

function convertToDateFormat(isoDate) {
    const date = new Date(isoDate);
    const ngay = date.getDate().toString().padStart(2, '0');
    const thang = (date.getMonth() + 1).toString().padStart(2, '0'); //trong js tháng bắt đầu từ 0 :)) 
    const nam = date.getFullYear();

    const formattedDate = `${ngay}/${thang}/${nam}`;

    return formattedDate;
}

function getAllDonHang() {
    const tbody = document.querySelector('#BangDonHang tbody');
    tbody.innerHTML = '';

    danhSachDonHang.forEach((donHang, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${donHang.tenKhachHang}</td>
            <td>${donHang.tenNhanVien}</td>
            <td>${convertToDateFormat(donHang.ngayDatHang)}</td>
            <td>${donHang.diaChiGiaoHang}</td>
            <td>${donHang.trangThai}</td>
            <td>${donHang.phuongThucThanhToan}</td>
            <td>${donHang.tongTien.toLocaleString('vi-VN')}đ</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDetailDialog(${index})">
                        <i class="fas fa-info" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="hienDialog(${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteDonHang(${index})">
                        <i class="fas fa-trash-alt" style="color: #D51C24;"></i>
                    </button>
                </div>
            </td>
        `;
        tbody.appendChild(row);
    });
}

document.getElementById('main-form').addEventListener('submit', function (event) {
    event.preventDefault();
    const maDonHang = danhSachDonHang[currentIndex].maDonHang;
    const nhanVien = document.getElementById('nhanvien').value;
    const trangThai = document.getElementById('trangthai').value;

    const donHang = {
        maDonHang: maDonHang,
        maNhanVienXuLyDonHang: nhanVien,
        trangThai: trangThai
    };

    console.log(JSON.stringify(donHang));

    const url = "https://localhost:44383/api/ControllersDonHang/Update";
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(donHang)
    })
        .then(response => {
            if (response.ok) {
                dongDialog();
                alert('Cập nhật thông tin đơn hàng thành công!');
                window.location.reload();
            }
        })
        .catch(error => {
            console.log('Error:', error);
            alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
        });

});





function deleteDonHang(index) {
    if (confirm('Bạn có chắc muốn xóa đơn hàng này?')) {
        const maDonHang = danhSachDonHang[index].maDonHang;
        const url = `https://localhost:44383/api/ControllersDonHang/Delete/${maDonHang}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Xoá thông tin đơn hàng thành công!');
                    window.location.reload();
                } else {
                    console.log('Lỗi khi xóa đơn hàng');
                    alert('Xoá thông tin đơn hàng không thành công!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa đơn hàng mã: ${maTheLoai}`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getDonHangByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI();
        return;
    }

    const url = `https://localhost:44383/api/ControllersDonHang/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachDonHang = data;
            getAllDonHang();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm đơn hàng:', error);
        });
}

getDataFromAPI();
getNhanVienFromAPI();

// ------------------------------------------------------------------------------------
function hienDetailDialog(index) {
    const donHang = danhSachDonHang[index]; // Lấy đơn hàng dựa trên index
    const chiTietContainer = document.getElementById('chiTietDonHangContainer');
    chiTietContainer.innerHTML = ''; // Xóa nội dung cũ

    // Gọi API lấy chi tiết đơn hàng
    const url = `https://localhost:44383/api/ControllersDonHang/Get-CTDonHangByID/${donHang.maDonHang}`;
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            data.forEach(chiTiet => {
                const chiTietItem = document.createElement('div');
                chiTietItem.classList.add('chi-tiet-item');
                chiTietItem.innerHTML = `
                    <img id='poster-list-${chiTiet.maTruyen}' src="" width="100px">
                    <div class="chi-tiet-info">
                        <p><strong>${chiTiet.tenTruyen}</strong></p>
                        <p>Giá: ${chiTiet.giaBan.toLocaleString('vi-VN')}đ</p>
                        <p>Số lượng: ${chiTiet.soLuong}</p>
                    </div>
                `;
                chiTietContainer.appendChild(chiTietItem);
                SetFile(`${chiTiet.anhTruyen}`, document.getElementById(`poster-list-${chiTiet.maTruyen}`));
            });

            // Hiển thị dialog
            document.getElementById('dialogDetailOverlay').style.display = 'flex';
        })
        .catch(error => {
            console.log('Lỗi khi tải chi tiết đơn hàng:', error);
            alert('Không thể hiển thị chi tiết đơn hàng.');
        });
}

function dongDetailDialog() {
    document.getElementById('dialogDetailOverlay').style.display = 'none';
}

function SetFile(fileName, container) {
    const url = `https://localhost:44383/api/ControllersTruyen/get-poster/${fileName}`;

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

