let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {};

//không có sp trong giỏ hàng thì hiện giỏ hàng trống
if (Object.keys(GioHang).length === 0) {
    document.getElementById('main').style.display = 'none';
}
else {
    document.getElementById('giohangtrong').style.display = 'none';
}

function updateTamTinh() {
    let tamTinh = 0;

    // Tính tổng tiền tất cả sản phẩm trong giỏ hàng
    for (let item in GioHang) {
        tamTinh += parseFloat(GioHang[item].TongTien);
    }

    const lableTamTinh = document.querySelector('.tamtinh');
    if (lableTamTinh) {
        lableTamTinh.textContent = tamTinh.toLocaleString('vi-VN') + "đ";
    }
}

function SetFile(fileName, container) {
    const url = `https://localhost:44346/api/ControllersTruyen/get-poster/${fileName}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Lỗi khi tải hình ảnh');
            }
            return response.blob();
        })
        .then(blob => {
            const fileUrl = URL.createObjectURL(blob);
            container.setAttribute("src", fileUrl);
        })
        .catch(error => {
            console.log('Đã xảy ra lỗi khi tải hình ảnh:', error);
            container.setAttribute("src", "../../Asset/IMG/default-image.jpg");
        });
}

//hiển thị sản phẩm trong giỏ hàng
function displayCart() {
    let GioHang = JSON.parse(localStorage.getItem('GioHang')) || {};
    let tableBody = document.querySelector('.tableCart tbody');

    if (tableBody != null) {
        tableBody.innerHTML = '';
    }

    for (let item in GioHang) {
        let row = `
            <tr class="cart-product">
                <td>
                    <img id="anhTruyen-${GioHang[item].MaTruyen}" class="cart-product-img" alt="Sản phẩm">
                </td>
                <td>
                    <a class="cart-product-name" href="ChiTietSP.html?maTruyen=${GioHang[item].MaTruyen}" style="cursor: pointer;"><h2>${GioHang[item].TenTruyen}</h2></a>
                    <br>
                    <a style="cursor: pointer;" class="cart-product-remove" data-id="${GioHang[item].MaTruyen}">Xóa</a>
                </td>
                <td class="text-center">
                    <span><h3 class="cart-product-giaban">${GioHang[item].GiaBanTruyen.toLocaleString('vi-VN') + "đ"}</h3></span>
                </td>
                <td>
                    <div class="ArrowInputTypeNumber">
                        <button class="button-down flex-center">
                            <i class="fas fa-chevron-down"></i>
                        </button>
                        <input type="text" class="input-soluong" value="${GioHang[item].SoLuongTruyen}">
                        <button class="button-up flex-center">
                            <i class="fas fa-chevron-up"></i>
                        </button>
                    </div>
                </td>
                <td class="text-center">
                    <span><h3 class="cart-product-tonggia">${GioHang[item].TongTien.toLocaleString('vi-VN') + "đ"}</h3></span>
                </td>
            </tr>`;

        tableBody.insertAdjacentHTML('beforeend', row);
        SetFile(`${GioHang[item].AnhTruyen}`, document.getElementById(`anhTruyen-${GioHang[item].MaTruyen}`));
    }

    // Sự kiện xóa sản phẩm
    let cartRemove = document.querySelectorAll('.cart-product-remove');
    cartRemove.forEach(button => {
        button.addEventListener('click', function () {
            const maTruyen = this.getAttribute('data-id');
            if (maTruyen) {
                delete GioHang[maTruyen];
                localStorage.setItem('GioHang', JSON.stringify(GioHang));
                window.location.reload();
            }
        });
    });

    updateTamTinh();
}

function updateQuantity(inputChange) {
    const cartProduct = inputChange.closest('.cart-product');
    const maTruyenChangeQuantity = cartProduct.querySelector('.cart-product-remove').getAttribute('data-id');

    let soLuongTruyenMoi = parseInt(inputChange.value);


    const tongTienMoi = soLuongTruyenMoi * GioHang[maTruyenChangeQuantity].GiaBanTruyen;
    GioHang[maTruyenChangeQuantity].SoLuongTruyen = soLuongTruyenMoi;
    GioHang[maTruyenChangeQuantity].TongTien = tongTienMoi;

    localStorage.setItem('GioHang', JSON.stringify(GioHang));

    // Cập nhật trực tiếp phần hiển thị tổng tiền của sản phẩm
    const tongTienElement = cartProduct.querySelector('.cart-product-tonggia');
    if (tongTienElement) {
        tongTienElement.textContent = tongTienMoi.toLocaleString('vi-VN') + "đ";
    }

    updateTamTinh();
    updateCartCount();
}

document.addEventListener('change', function (event) {
    if (event.target.classList.contains('input-soluong'))//nếu phần tử kích hoạt sự kiện có class input-soluong thì trả về true
    {
        updateQuantity(event.target);
    }
})

function EventInputTypeNumber() {
    const inputSoLuong = document.querySelectorAll('.input-soluong');
    const buttonUp = document.querySelectorAll('.button-up');
    const buttonDown = document.querySelectorAll('.button-down');

    // Xử lý sự kiện cho các input
    if (inputSoLuong.length > 0) {
        inputSoLuong.forEach(input => {
            input.addEventListener('input', function () {
                input.value = input.value.replace(/[^0-9]/g, '');
            });

            input.addEventListener('keydown', function (event) {
                if (event.key === 'Enter') {
                    input.blur();
                    updateQuantity(input);
                }
            });

            input.addEventListener('blur', function () {
                if (input.value == '') {
                    input.value = 1;
                    updateQuantity(input);
                }
            });
        });
    }

    // Xử lý sự kiện cho buttonUp
    if (buttonUp.length > 0) {
        buttonUp.forEach((button, index) => {
            button.addEventListener('click', function () {
                const input = inputSoLuong[index];
                if (input) {
                    input.value = parseInt(input.value) + 1; // Tăng số lượng
                    updateQuantity(input);
                }
            });
        });
    }

    // Xử lý sự kiện cho buttonDown
    if (buttonDown.length > 0) {
        buttonDown.forEach((button, index) => {
            button.addEventListener('click', function () {
                const input = inputSoLuong[index];
                if (input && parseInt(input.value) > 1) {
                    input.value = parseInt(input.value) - 1; // Giảm số lượng (giới hạn không dưới 1)
                    updateQuantity(input);
                }
            });
        });
    }
}

displayCart();
EventInputTypeNumber();



