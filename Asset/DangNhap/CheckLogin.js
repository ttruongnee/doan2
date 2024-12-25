const btnDangXuat = document.querySelector('.dangxuat');
if (btnDangXuat) {
    btnDangXuat.addEventListener('click', async () => {
        await CreateGioHang();
        localStorage.removeItem("userLogin");
        updateCartCount();
        checkLogin();
    });
}

async function getKhachHangByUser(user) {
    const url = `https://localhost:44346/api/ControllersKhachHang/GetNameByUser/${user}`;

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await response.json();
        return data[0].tenKhachHang;

    } catch (error) {
        console.log("Lỗi khi lấy tên khách hàng bằng user");
        return null;
    }

}


async function checkLogin() {
    const userLogin = localStorage.getItem("userLogin");

    if (userLogin) {
        document.getElementById('LoginTrue').style.display = 'flex';
        document.getElementById('NameUserLogin').innerHTML = await getKhachHangByUser(userLogin);
        document.getElementById('LoginFalse').style.display = 'none';
    }
    else {
        document.getElementById('LoginTrue').style.display = 'none';
        document.getElementById('LoginFalse').style.display = 'flex';
    }
}

checkLogin();

