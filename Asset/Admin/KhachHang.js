let currentIndex = -1; // Lưu chỉ số của khách hàng đang sửa xoá
let danhSachKhachHang = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersKhachHang/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachKhachHang = data;
            getAllkhachHang();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách khách hàng từ API:', error);
        });
}

function hienDialog(index) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    currentIndex = index;

    // Điền thông tin khách hàng vào form 
    const khachHang = danhSachKhachHang[currentIndex];

    document.getElementById('tenkh').value = khachHang.tenKhachHang;
    document.getElementById('gioitinh').value = khachHang.gioiTinh;
    document.getElementById('sdt').value = khachHang.soDienThoai;
    document.getElementById('email').value = khachHang.email;
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    document.getElementById('tenkh').value = '';
    document.getElementById('gioitinh').value = '';
    document.getElementById('sdt').value = '';
    document.getElementById('email').value = '';
    currentIndex = -1;
}

function getAllkhachHang() {
    const tbody = document.querySelector('#BangKhachHang tbody');
    tbody.innerHTML = '';

    danhSachKhachHang.forEach((khachHang, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${khachHang.tenKhachHang}</td>
            <td>${khachHang.gioiTinh}</td>
            <td>${khachHang.soDienThoai}</td>
            <td>${khachHang.email}</td>
            <td>${khachHang.taiKhoan}</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDialog(${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteKhachHang(${index})">
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
    const tenkh = document.getElementById('tenkh').value.trim();
    const gioiTinh = document.getElementById('gioitinh').value;
    const sdt = document.getElementById('sdt').value.trim();
    const email = document.getElementById('email').value;

    const makh = danhSachKhachHang[currentIndex].maKhachHang;
    const taiKhoan = danhSachKhachHang[currentIndex].taiKhoan;

    const khachHang = {
        maKhachHang: makh,
        tenKhachHang: tenkh,
        gioiTinh: gioiTinh,
        soDienThoai: sdt,
        email: email,
        taiKhoan: taiKhoan
    };

    const url = "https://localhost:44383/api/ControllersKhachHang/Update";
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(khachHang)
    })
        .then(response => {
            if (response.ok) {
                dongDialog();
                alert('Sửa thông tin khách hàng thành công!');
                window.location.reload();
            }
        })
        .catch(error => {
            console.log('Error:', error);
            alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
        });

});




function deleteKhachHang(index) {
    if (confirm('Bạn có chắc muốn xóa khách hàng này?')) {
        const taiKhoan = danhSachKhachHang[index].taiKhoan;

        const url = `https://localhost:44383/api/ControllersTaiKhoan/Delete/${taiKhoan}`;
        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Xoá thông tin khách hàng thành công');
                    window.location.reload();
                } else {
                    console.log('Lỗi khi xóa khách hàng');
                    alert('Xoá thông tin khách hàng không thành công!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa khách hàng có tài khoản: ${taiKhoan}.`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getKhachHangByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI();
        return;
    }

    const url = `https://localhost:44383/api/ControllersKhachHang/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachKhachHang = data;
            getAllkhachHang();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm khách hàng:', error);
        });
}

getDataFromAPI();


