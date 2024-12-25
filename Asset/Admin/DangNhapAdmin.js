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
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách tài khoản từ API:', error);
        });
}

document.getElementById('main-form').addEventListener('submit', function (event) {
    event.preventDefault();

    const taiKhoan = document.getElementById('taikhoan').value.trim();
    const matKhau = document.getElementById('matkhau').value.trim();

    const TaiKhoan = {
        taiKhoan: taiKhoan,
        matKhau: matKhau
    };

    const url = "https://localhost:44383/api/ControllersTaiKhoan/DangNhapAdmin";
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(TaiKhoan)
    })
        .then(response => response.json())
        .then(data => {
            if (data === true) {
                alert('Đăng nhập thành công!');
                sessionStorage.setItem("adminLogin", taiKhoan);
                window.location.href = "TrangChu.html";
            } else {
                alert('Tài khoản hoặc mật khẩu không đúng!');
            }
        })
        .catch(error => {
            console.log('Lỗi khi đăng nhập:', error);
            alert('Có lỗi xảy ra trong quá trình đăng nhập, vui lòng thử lại!');
        });
});

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