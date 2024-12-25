async function getNhanVienByUser(user) {
    const url = `https://localhost:44383/api/ControllersNhanVien/GetNhanVienByUser/${user}`;

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await response.json();
        return data[0];
    } catch (error) {
        console.log("Lỗi khi lấy nhân viên bằng user");
        return null;
    }
}

async function getQuyenByUser(user) {
    const url = `https://localhost:44383/api/ControllersTaiKhoan/GetQuyenByID/${user}`;

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await response.json();
        return data[0];
    } catch (error) {
        console.log("Lỗi khi lấy thông tin tài khoản bằng user");
        return null;
    }
}

async function checkLogin() {
    const adminLogin = sessionStorage.getItem("adminLogin");
    const nhanVien = await getNhanVienByUser(adminLogin);
    const taikhoan = await getQuyenByUser(adminLogin);

    if (adminLogin) {
        document.getElementById('NameUserLogin').innerHTML = nhanVien.tenNhanVien;
        document.getElementById('QuyenUserLogin').innerHTML = taikhoan.quyen;

        if (taikhoan.quyen === 'Nhân viên') {
            document.getElementById('nhanvien').style.display = 'none';
            document.getElementById('taikhoan').style.display = 'none';
            document.getElementById('thongke').style.display = 'none';
        }
    }
    else {
        window.location.href = "DangNhapAdmin.html";
    }
}

document.querySelectorAll('.dangxuat').forEach(function (dx) {
    dx.addEventListener("click", function () {
        sessionStorage.removeItem("adminLogin");
        window.location.href = "DangNhapAdmin.html";
    });
});

checkLogin();