let isEditing = false; // Xác định đang chỉnh sửa hay thêm mới
let currentIndex = -1; // Lưu chỉ số của thể loại nếu đang sửa
let danhSachTheLoai = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersTheLoai/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachTheLoai = data;
            getAllTheLoai();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách thể loại từ API:', error);
        });
}

function hienDialog(isEditMode = false, index = -1) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    isEditing = isEditMode;
    currentIndex = index;

    if (isEditing) {
        document.getElementById('dialogTitle').innerText = 'Sửa thể loại truyện';
        // Điền thông tin thể loại vào form nếu đang sửa
        const theLoai = danhSachTheLoai[currentIndex];

        document.getElementById('tenloai').value = theLoai.tenTheLoai;
        document.getElementById('mota').value = theLoai.moTa;
    } else {
        document.getElementById('dialogTitle').innerText = 'Thêm thể loại truyện';
        document.getElementById('tenloai').value = '';
        document.getElementById('mota').value = '';
    }
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    document.getElementById('tenloai').value = '';
    document.getElementById('mota').value = '';
    isEditing = false;
    currentIndex = -1;
}

function getAllTheLoai() {
    const tbody = document.querySelector('#BangTheLoai tbody');
    tbody.innerHTML = '';

    danhSachTheLoai.forEach((theLoai, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${theLoai.tenTheLoai}</td>
            <td>${theLoai.moTa}</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDialog(true, ${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteTheLoai(${index})">
                        <i class="fas fa-trash-alt" style="color: #D51C24;"></i>
                    </button>
                </div>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function saveTheLoai() {
    const tenLoai = document.getElementById('tenloai').value.trim();
    const moTa = document.getElementById('mota').value.trim();

    // Kiểm tra nếu tên loại trống
    if (tenLoai === '') {
        alert('Vui lòng nhập tên thể loại!');
        return;
    }

    // Nếu đang sửa thể loại (isEditing = true)
    if (isEditing) {
        const maLoai = danhSachTheLoai[currentIndex].maTheLoai;

        const theLoai = {
            maTheLoai: maLoai,
            tenTheLoai: tenLoai,
            moTa: moTa
        };

        const url = "https://localhost:44383/api/ControllersTheLoai/Update";
        fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(theLoai)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    alert('Sửa thông tin thể loại truyện thành công!');
                    window.location.reload();
                }
            })
            .catch(error => {
                console.log('Error:', error);
                alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
            });
    } else {
        // Tạo object không bao gồm mã thể loại khi thêm mới
        const theLoaiMoi = {
            tenTheLoai: tenLoai,
            moTa: moTa
        };

        const url = "https://localhost:44383/api/ControllersTheLoai/Create";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(theLoaiMoi)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    alert('Thêm thể loại thành công!');
                    window.location.reload();
                } else {
                    throw new Error('Thêm thất bại');
                }
            })
            .catch(error => {
                console.log('Lỗi khi thêm thể loại mới:', error);
                alert('Có lỗi xảy ra trong quá trình thêm mới, vui lòng thử lại!');
            });
    }
}





function deleteTheLoai(index) {
    if (confirm('Bạn có chắc muốn xóa thể loại này?')) {
        const maTheLoai = danhSachTheLoai[index].maTheLoai;
        const url = `https://localhost:44383/api/ControllersTheLoai/Delete/${maTheLoai}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Xoá thông tin thể loại thành công!');
                    window.location.reload();
                } else {
                    console.log('Lỗi khi xóa thể loại');
                    alert('Xoá thông tin thể loại không thành công!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa thể loại mã: ${maTheLoai}`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getTheLoaiByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI();
        return;
    }

    const url = `https://localhost:44383/api/ControllersTheLoai/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTheLoai = data;
            getAllTheLoai();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm thể loại:', error);
        });
}

getDataFromAPI();


