let isEditing = false; // Xác định đang chỉnh sửa hay thêm mới
let currentIndex = -1; // Lưu chỉ số của mã giảm giá nếu đang sửa
let danhSachMaGiamGia = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersGiamGia/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachMaGiamGia = data;
            getAllMaGiamGia();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách mã giảm giá từ API:', error);
        });
}

function hienDialog(isEditMode = false, index = -1) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    isEditing = isEditMode;
    currentIndex = index;

    if (isEditing) {
        document.getElementById('dialogTitle').innerText = 'Sửa mã giảm giá';
        // Điền thông tin mã giảm giá vào form nếu đang sửa
        const MaGiamGia = danhSachMaGiamGia[currentIndex];

        document.getElementById('magiamgia').value = MaGiamGia.maGiamGia;
        document.getElementById('ngaybatdau').value = new Date(MaGiamGia.ngayBatDau).toISOString().split('T')[0];
        document.getElementById('ngayketthuc').value = new Date(MaGiamGia.ngayBatDau).toISOString().split('T')[0];
        document.getElementById('phantramgiamgia').value = MaGiamGia.phanTramGiamGia;
    } else {
        document.getElementById('dialogTitle').innerText = 'Thêm mã giảm giá';
        document.getElementById('magiamgia').value = '';
        document.getElementById('ngaybatdau').value = '';
        document.getElementById('ngayketthuc').value = '';
        document.getElementById('phantramgiamgia').value = '';
    }
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    document.getElementById('magiamgia').value = '';
    document.getElementById('ngaybatdau').value = '';
    document.getElementById('ngayketthuc').value = '';
    document.getElementById('phantramgiamgia').value = '';
    isEditing = false;
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


function getAllMaGiamGia() {
    const tbody = document.querySelector('#BangMaGiamGia tbody');
    tbody.innerHTML = '';

    danhSachMaGiamGia.forEach((MaGiamGia, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${MaGiamGia.maGiamGia}</td>
            <td>${convertToDateFormat(MaGiamGia.ngayBatDau)}</td>
            <td>${convertToDateFormat(MaGiamGia.ngayKetThuc)}</td>
            <td>${MaGiamGia.phanTramGiamGia}%</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDialog(true, ${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteMaGiamGia(${index})">
                        <i class="fas fa-trash-alt" style="color: #D51C24;"></i>
                    </button>
                </div>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function saveMaGiamGia() {
    const maGiamGia = document.getElementById('magiamgia').value.trim();
    const ngayBatDau = document.getElementById('ngaybatdau').value;
    const ngayKetThuc = document.getElementById('ngayketthuc').value;
    const phanTramGiamGia = document.getElementById('phantramgiamgia').value;
    console.log(maGiamGia, ngayBatDau, ngayKetThuc, phanTramGiamGia);


    if (!maGiamGia || !ngayBatDau || !ngayKetThuc || !phanTramGiamGia) {
        alert('Vui lòng nhập đầy đủ thông tin!');
        return;
    }

    //check mã trùng
    let check = false;
    danhSachMaGiamGia.forEach(item => {
        if (item.maGiamGia === maGiamGia) {
            check = true;
        }
    });

    if (check) {
        alert('Mã giảm giá đã tồn tại, vui lòng chọn mã khác!');
        return;  // Dừng hàm nếu mã giảm giá trùng
    }

    //check ngày 
    if (new Date(ngayBatDau) > new Date(ngayKetThuc)) {
        alert('Vui lòng chọn khoảng thời gian hợp lệ !');
        return;
    }

    if (phanTramGiamGia < 0 || phanTramGiamGia > 100) {
        alert('Vui lòng nhập phần trăm giảm giá hợp lệ!');
        return;
    }

    const MaGiamGia = {
        maGiamGia: maGiamGia,
        ngayBatDau: ngayBatDau,
        ngayKetThuc: ngayKetThuc,
        phanTramGiamGia: phanTramGiamGia
    };

    // Nếu đang sửa mã giảm giá (isEditing = true)
    if (isEditing) {
        const url = "https://localhost:44383/api/ControllersGiamGia/Update";
        fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(MaGiamGia)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    alert('Sửa thông tin mã giảm giá thành công!');
                    window.location.reload();
                }
            })
            .catch(error => {
                console.log('Error:', error);
                alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
            });
    }
    else {
        const url = "https://localhost:44383/api/ControllersGiamGia/Create";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(MaGiamGia)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    alert('Thêm mã giảm giá thành công!');
                    window.location.reload();
                } else {
                    throw new Error('Thêm thất bại');
                }
            })
            .catch(error => {
                console.log('Lỗi khi thêm mã giảm giá mới:', error);
                alert('Có lỗi xảy ra trong quá trình thêm mới, vui lòng thử lại!');
            });
    }
}


function deleteMaGiamGia(index) {
    if (confirm('Bạn có chắc muốn xóa mã giảm giá này?')) {
        const maGiamGia = danhSachMaGiamGia[index].maGiamGia;
        const url = `https://localhost:44383/api/ControllersGiamGia/Delete/${maGiamGia}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Xoá thông tin mã giảm giá thành công!');
                    window.location.reload();
                } else {
                    console.log('Lỗi khi xóa mã giảm giá');
                    alert('Xoá thông tin mã giảm giá không thành công!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa mã giảm giá: ${maGiamGia}`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

document.getElementById('search-date').addEventListener('submit', function (event) {
    event.preventDefault();
    const startDateSearch = document.getElementById('startDateSearch').value;
    const endDateSearch = document.getElementById('endDateSearch').value;

    //check ngày 
    if (new Date(startDateSearch) > new Date(endDateSearch)) {
        alert('Vui lòng chọn khoảng thời gian hợp lệ !');
        return;
    }

    const url = `https://localhost:44383/api/ControllersGiamGia/Search?NgayBatDau=${encodeURIComponent(startDateSearch)}&NgayKetThuc=${encodeURIComponent(endDateSearch)}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachMaGiamGia = data;
            getAllMaGiamGia();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm mã giảm giá:', error);
        });
});

getDataFromAPI();


