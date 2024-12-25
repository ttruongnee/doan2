const adminLogin = sessionStorage.getItem("adminLogin");
let currentIndex = -1; // Lưu chỉ số của nhân viên đang sửa xoá
let danhSachNhanVien = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersNhanVien/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachNhanVien = data;
            getAllNhanVien();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách nhân viên từ API:', error);
        });
}

function hienDialog(index) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    currentIndex = index;

    // Điền thông tin nhân viên vào form 
    const nhanVien = danhSachNhanVien[currentIndex];

    document.getElementById('tennv').value = nhanVien.tenNhanVien;
    document.getElementById('gioitinh').value = nhanVien.gioiTinh;
    document.getElementById('quequan').value = nhanVien.queQuan;
    document.getElementById('sdt').value = nhanVien.soDienThoai;
    document.getElementById('email').value = nhanVien.email;
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    document.getElementById('tennv').value = '';
    document.getElementById('gioitinh').value = '';
    document.getElementById('quequan').value = '';
    document.getElementById('sdt').value = '';
    document.getElementById('email').value = '';
    currentIndex = -1;
}

function getAllNhanVien() {
    const tbody = document.querySelector('#BangNhanVien tbody');
    tbody.innerHTML = '';

    danhSachNhanVien.forEach((nhanVien, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${nhanVien.tenNhanVien}</td>
            <td>${nhanVien.gioiTinh}</td>
            <td>${nhanVien.queQuan}</td>
            <td>${nhanVien.soDienThoai}</td>
            <td>${nhanVien.email}</td>
            <td>${nhanVien.taiKhoan}</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDialog(${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteNhanVien(${index})">
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
    const tennv = document.getElementById('tennv').value.trim();
    const gioiTinh = document.getElementById('gioitinh').value;
    const queQuan = document.getElementById('quequan').value.trim();
    const sdt = document.getElementById('sdt').value.trim();
    const email = document.getElementById('email').value;

    const manv = danhSachNhanVien[currentIndex].maNhanVien;
    const taiKhoan = danhSachNhanVien[currentIndex].taiKhoan;

    const nhanVien = {
        maNhanVien: manv,
        tenNhanVien: tennv,
        gioiTinh: gioiTinh,
        queQuan: queQuan,
        soDienThoai: sdt,
        email: email,
        taiKhoan: taiKhoan
    };

    const url = "https://localhost:44383/api/ControllersNhanVien/Update";
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(nhanVien)
    })
        .then(response => {
            if (response.ok) {
                dongDialog();
                alert('Sửa thông tin nhân viên thành công!');
                window.location.reload();
            }
        })
        .catch(error => {
            console.log('Error:', error);
            alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
        });

});




function deleteNhanVien(index) {
    if (confirm('Bạn có chắc muốn xóa nhân viên này?')) {
        const taiKhoan = danhSachNhanVien[index].taiKhoan;
        if (taiKhoan === adminLogin) {
            alert("Đây là nhân viên mà tài khoản đang đăng nhập, không thể xoá!");
            return;
        }

        const url = `https://localhost:44383/api/ControllersTaiKhoan/Delete/${taiKhoan}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    alert('Xoá thông tin nhân viên thành công');
                    window.location.reload();
                } else {
                    console.log('Lỗi khi xóa nhân viên');
                    alert('Xoá thông tin nhân viên không thành công!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa nhân viên có tài khoản: ${taiKhoan}.`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getNhanVienByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI();
        return;
    }

    const url = `https://localhost:44383/api/ControllersNhanVien/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachNhanVien = data;
            getAllNhanVien();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm nhân viên:', error);
        });
}

getDataFromAPI();


