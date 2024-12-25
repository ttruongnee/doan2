let fromEmail = document.getElementById("form-email");
let fromOTP = document.getElementById("form-otp");
let fromResetPassword = document.getElementById("form-reset-password");
let emailNhanVien = [];

function getDataFromAPI() {
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

fromEmail.addEventListener("submit", (event) => {
    event.preventDefault();
    const email = document.getElementById("email").value.trim();
    let emailExists = false;

    emailNhanVien.forEach(item => {
        if (item.email === email) {
            emailExists = true;
        }
    });

    if (emailExists) {
        fetch("https://localhost:44383/api/ControllersTaiKhoan/SendOTP", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(email),
        })
            .then(response => {
                if (response.ok) {
                    alert("Mã OTP đã được gửi đến email của bạn.");
                    document.getElementById("form-email").style.display = "none";
                    document.getElementById("form-otp").style.display = "block";
                } else {
                    return response.text().then(error => { throw new Error(error); });
                }
            })
            .catch(error => {
                console.error("Có lỗi xảy ra:", error);
                alert("Lỗi: " + error.message);
            });
    }
    else {
        alert(`Không tồn tại tài khoản có email là "${email}". Vui lòng thử lại!`);
    }
});

fromOTP.addEventListener("submit", (event) => {
    event.preventDefault();
    const otp = document.getElementById("otp").value.trim();

    fetch("https://localhost:44383/api/ControllersTaiKhoan/VerifyOTP", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(otp),
    })
        .then(response => {
            if (response.ok) {
                alert("Xác nhận thành công! Bạn có thể đặt lại mật khẩu.");
                document.getElementById("form-otp").style.display = "none";
                document.getElementById("form-reset-password").style.display = "block";
            } else {
                return response.text().then(error => { throw new Error(error); });
            }
        })
        .catch(error => {
            console.error("Có lỗi xảy ra:", error);
            alert("Lỗi: " + error.message);
        });
});

fromResetPassword.addEventListener("submit", (event) => {
    event.preventDefault();
    const email = document.getElementById("email").value.trim();
    const newPassword = document.getElementById("new-password").value.trim();

    const newUser = {
        email: email,
        matKhau: newPassword
    };

    fetch("https://localhost:44383/api/ControllersTaiKhoan/UpdateMatKhau", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newUser)
    })
        .then(response => {
            if (response.ok) {
                alert("Đặt lại mật khẩu thành công! Bạn có thể đăng nhập lại.");
                window.location.href = "DangNhapAdmin.html";
            } else {
                return response.text().then(error => { throw new Error(error); });
            }
        })
        .catch(error => {
            console.error("Có lỗi xảy ra:", error);
            alert("Lỗi: " + error.message);
        });
});

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

getDataFromAPI();
