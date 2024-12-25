const adminLogin = sessionStorage.getItem("adminLogin");
let danhSachTaiKhoan = [];

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersTaiKhoan/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachTaiKhoan = data;
            getAllTaiKhoan();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách tài khoản từ API:', error);
        });
}

function getAllTaiKhoan() {
    const tbody = document.querySelector('#BangTaiKhoan tbody');
    tbody.innerHTML = '';

    danhSachTaiKhoan.forEach((taikhoan, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${taikhoan.tenNguoiDung}</td>
            <td>${taikhoan.taiKhoan}</td>
            <td>${taikhoan.matKhau}</td>
            <td>${taikhoan.quyen}</td>
            <td>
                <div class="flex-center">
                    <button onclick="deleteTaiKhoan(${index})">
                        <i class="fas fa-trash-alt" style="color: #D51C24;"></i>
                    </button>
                </div>
            </td>
        `;
        tbody.appendChild(row);
    });
}

function deleteTaiKhoan(index) {
    if (confirm('Bạn có chắc muốn xóa tài khoản này?')) {
        const taikhoan = danhSachTaiKhoan[index].taiKhoan;
        if (taikhoan === adminLogin) {
            alert("Đây là tài khoản bạn đang sử dụng, không thể xoá!");
            return;
        }
        const url = `https://localhost:44383/api/ControllersTaiKhoan/Delete/${taikhoan}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    window.location.reload();
                    alert('Xoá thông tin tài khoản thành công!');

                } else {
                    console.log('Lỗi khi xóa tài khoản');
                    alert('Xoá thông tin tài khoản thất bại!');
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa tài khoản: ${taikhoan}`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getTaiKhoanByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI();
        return;
    }

    const url = `https://localhost:44383/api/ControllersTaiKhoan/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTaiKhoan = data;
            getAllTaiKhoan();
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm tài khoản:', error);
        });
}

getDataFromAPI()