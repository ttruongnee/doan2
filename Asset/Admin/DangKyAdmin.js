let danhSachTaiKhoan = [];
let emailNhanVien = [];

function getEmailNhanVienFromAPI() {
    const url = "https://localhost:44383/api/ControllersTaiKhoan/Get-EmailNhanVien";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            emailNhanVien = data;
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách email từ API:', error);
        });
}

function getTaiKhoanFromAPI() {
    const url = "https://localhost:44383/api/ControllersTaiKhoan/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            danhSachTaiKhoan = data;
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách tài khoản từ API:', error);
        });
}

document.getElementById('main-form').addEventListener('submit', function (event) {
    event.preventDefault();
    const taiKhoan = document.getElementById('taikhoan').value.trim();
    const matKhau = document.getElementById('matkhau').value.trim();
    const quyen = document.getElementById('quyen').value;
    const hoTen = document.getElementById('hoten').value.trim();
    const gioiTinh = document.getElementById('gioitinh').value;
    const queQuan = document.getElementById('quequan').value.trim();
    const soDienThoai = document.getElementById('sdt').value.trim();
    const email = document.getElementById('email').value.trim();

    // Kiểm tra tài khoản đã tồn tại
    let taiKhoanTonTai = false;
    danhSachTaiKhoan.forEach(taikhoan => {
        if (taikhoan.taiKhoan === taiKhoan) {
            taiKhoanTonTai = true; // Đánh dấu tài khoản tồn tại
        }
    });

    if (taiKhoanTonTai) {
        alert('Tên tài khoản đã tồn tại trong hệ thống, vui lòng chọn tên tài khoản khác.');
        return;
    }

    // Kiểm tra email đã tồn tại
    let EmailTonTai = false;
    emailNhanVien.forEach(emailnv => {
        if (emailnv.email === email) {
            EmailTonTai = true; // Đánh dấu email tồn tại
        }
    });

    if (EmailTonTai) {
        alert('Email đã tồn tại trong hệ thống, vui lòng chọn một email khác.');
        return;
    }

    const TaiKhoan = {
        taiKhoan: taiKhoan,
        matKhau: matKhau,
        quyen: quyen,
        tenNguoiDung: hoTen,
        gioiTinh: gioiTinh,
        queQuan: queQuan,
        soDienThoai: soDienThoai,
        email: email
    };
    console.log(TaiKhoan);

    const url = "https://localhost:44383/api/ControllersTaiKhoan/Create";
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(TaiKhoan)
    })
        .then(response => {
            if (response.ok) {
                alert('Đăng ký thành công!');
                window.location.reload();
            } else {
                throw new Error('Đăng ký tài khoản thất bại');
            }
        })
        .catch(error => {
            console.log('Lỗi khi đăng ký tài khoản:', error);
            alert('Có lỗi xảy ra trong quá trình đăng ký tài khoản, vui lòng thử lại!');
        });
});

getTaiKhoanFromAPI();
getEmailNhanVienFromAPI();

const inputmk = document.getElementById("matkhau");
const eyeOpen = document.querySelector(".fa-eye");
const eyeClose = document.querySelector(".fa-eye-slash");

eyeOpen.addEventListener("click", function () {
    eyeOpen.classList.add("hidden");
    eyeClose.classList.remove("hidden");
    inputmk.setAttribute("type", "password")
})

eyeClose.addEventListener("click", function () {
    eyeOpen.classList.remove("hidden");
    eyeClose.classList.add("hidden");
    inputmk.setAttribute("type", "text")
})