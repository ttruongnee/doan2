document.getElementById('main-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const taiKhoan = document.getElementById('taikhoan').value.trim();
    const matKhau = document.getElementById('matkhau').value.trim();

    const TaiKhoan = {
        taiKhoan: taiKhoan,
        matKhau: matKhau
    };

    const url = "https://localhost:44346/api/ControllersTaiKhoan/DangNhapUser";
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(TaiKhoan)
        });

        const data = await response.json();

        if (data === true) {
            alert('Đăng nhập thành công!');
            localStorage.setItem("userLogin", taiKhoan);

            await GetGioHangFromAPI();
            window.location.href = "TrangChu.html";
        } else {
            alert('Tài khoản hoặc mật khẩu không đúng!');
        }
    } catch (error) {
        console.log('Lỗi khi đăng nhập:', error);
        alert('Có lỗi xảy ra trong quá trình đăng nhập, vui lòng thử lại!');
    }
});

async function getGioHangByUser(maGioHang) {
    const url = `https://localhost:44346/api/ControllersGioHang/GetGioHangByUser/${maGioHang}`;
    try {
        const response = await fetch(url);
        const gioHang = await response.json();

        return gioHang;
    } catch (error) {
        console.error("Lỗi khi tải giỏ hàng từ API:", error);
        return {}; //trả về giỏ hàng trống nếu lỗi
    }
}

async function GetGioHangFromAPI() {
    localStorage.removeItem('GioHang');
    const maGioHang = localStorage.getItem('userLogin');
    const gioHang = await getGioHangByUser(maGioHang); //lấy giỏ hàng từ api

    if (Object.keys(gioHang).length === 0) {
        localStorage.setItem('GioHang', JSON.stringify({})); //lưu giỏ hàng trống
        return;
    }

    let GioHang = {};
    for (let item of gioHang) {
        for (let ct of item.listjson_chitiet) {
            const { maTruyen, tenTruyen, soLuong } = ct;

            try {
                const truyenInfo = await fetch(`https://localhost:44346/api/ControllersTruyen/SearchByID/${maTruyen}`).then(response => response.json());
                const { anhTruyen, giaBan } = truyenInfo[0] || {};
                GioHang[maTruyen] = {
                    'MaTruyen': maTruyen,
                    'TenTruyen': tenTruyen,
                    'SoLuongTruyen': soLuong,
                    'AnhTruyen': anhTruyen,
                    'GiaBanTruyen': giaBan,
                    'TongTien': soLuong * giaBan
                };
            } catch (error) {
                console.log(`Lỗi khi lấy thông tin truyện ${tenTruyen}:`, error);
            }
        }
    }

    localStorage.setItem('GioHang', JSON.stringify(GioHang));
}

const inputmk = document.getElementById("matkhau");
const eyeOpen = document.querySelector(".fa-eye");
const eyeClose = document.querySelector(".fa-eye-slash");

eyeOpen.addEventListener("click", function () {
    eyeOpen.classList.add("hidden");
    eyeClose.classList.remove("hidden");
    inputmk.setAttribute("type", "password");
});

eyeClose.addEventListener("click", function () {
    eyeOpen.classList.remove("hidden");
    eyeClose.classList.add("hidden");
    inputmk.setAttribute("type", "text");
});
