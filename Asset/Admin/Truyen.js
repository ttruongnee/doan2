let isEditing = false; // Xác định đang chỉnh sửa hay thêm mới
let currentIndex = -1; // Lưu chỉ số của thể loại nếu đang sửa
let danhSachTruyen = [];
let danhSachTheLoai = [];

function BindDataCombobox(danhSachTheLoai, selectId) {
    const selectElement = document.getElementById(selectId);

    danhSachTheLoai.forEach((theLoai) => {
        const option = document.createElement('option');
        option.value = theLoai.maTheLoai;
        option.text = theLoai.tenTheLoai;

        selectElement.appendChild(option);
    });
}

function getDataFromAPI() {
    const url = "https://localhost:44383/api/ControllersTruyen/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTruyen = data;
            getAllTruyen();
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách truyện từ API:', error);
        });

}


function getDataFromAPITheLoai() {
    const url = "https://localhost:44383/api/ControllersTheLoai/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTheLoai = data;
            BindDataCombobox(danhSachTheLoai, 'theloai');
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách thể loại từ API:', error);
        });

}

function hienDialog(isEditMode = false, index = -1) {
    document.getElementById('dialogOverlay').style.display = 'flex';
    isEditing = isEditMode;
    currentIndex = index;

    if (isEditing) {
        document.getElementById('dialogTitle').innerText = 'Sửa truyện';
        // Điền thông tin thể loại vào form nếu đang sửa
        const truyen = danhSachTruyen[currentIndex];

        SetFile(truyen.anhTruyen, document.getElementById('img-preview'));
        document.getElementById('anhtruyen').value = truyen.anhTruyen;
        document.getElementById('tentruyen').value = truyen.tenTruyen;
        document.getElementById('isbn').value = truyen.isbn;
        document.getElementById('tacgia').value = truyen.tacGia;
        document.getElementById('doituong').value = truyen.doiTuong;
        document.getElementById('sotrang').value = truyen.soTrang;
        document.getElementById('dinhdang').value = truyen.dinhDang;
        document.getElementById('trongluong').value = truyen.trongLuong;
        document.getElementById('theloai').value = truyen.maTheLoai;
        document.getElementById('soluong').value = truyen.soLuong;
        document.getElementById('giagoc').value = truyen.giaGoc;
        document.getElementById('phantramgiamgia').value = truyen.phanTramGiamGia;
    } else {
        document.getElementById('dialogTitle').innerText = 'Thêm truyện';
        document.getElementById('img-preview').src = '../../Asset/IMG/default-image.jpg';
        document.getElementById('anhtruyenFile').value = '';
        document.getElementById('tentruyen').value = '';
        document.getElementById('isbn').value = '';
        document.getElementById('tacgia').value = '';
        document.getElementById('doituong').value = '';
        document.getElementById('sotrang').value = '';
        document.getElementById('dinhdang').value = '';
        document.getElementById('trongluong').value = '';
        document.getElementById('soluong').value = '';
        document.getElementById('giagoc').value = '';
        document.getElementById('phantramgiamgia').value = '';
    }
}
document.getElementById('anhtruyenFile').onchange = function (evt) {
    var tgt = evt.target;
    var files = tgt.files;

    if (FileReader && files && files.length) {
        var fr = new FileReader();
        fr.onload = function () {
            document.getElementById('img-preview').src = fr.result;
        }
        fr.readAsDataURL(files[0]);
    }
}

function dongDialog() {
    document.getElementById('dialogOverlay').style.display = 'none';
    isEditing = false;
    currentIndex = -1;
}

function SetFile(fileName, container) {
    const url = `https://localhost:44383/api/ControllersTruyen/get-poster/${fileName}`;

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


function getAllTruyen() {
    const tbody = document.querySelector('#BangTruyen tbody');
    tbody.innerHTML = '';

    danhSachTruyen.forEach((truyen, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `<tr>
            <td><img id='poster-list-${truyen.maTruyen}' src="" alt="Ảnh truyện" width="100px"></td>
            <td>${truyen.tenTruyen}</td>
            <td>${truyen.tacGia}</td>
            <td>${truyen.tenTheLoai}</td>
            <td>${truyen.soLuong} quyển</td>
            <td>${truyen.giaGoc.toLocaleString('vi-VN')}đ</td>
            <td>${truyen.phanTramGiamGia}%</td>
            <td>${truyen.giaBan.toLocaleString('vi-VN')}đ</td>
            <td>
                <div class="flex-center">
                    <button onclick="hienDialog(true, ${index})">
                        <i class="fas fa-edit" style="color: #D51C24;"></i>
                    </button>
                    <button onclick="deleteTruyen(${index})">
                        <i class="fas fa-trash-alt" style="color: #D51C24;"></i>
                    </button>
                </div>
            </td>
        `;
        tbody.appendChild(row);
        SetFile(`${truyen.anhTruyen}`, document.getElementById(`poster-list-${truyen.maTruyen}`));
    });
}

function uploadImage(anhTruyen) {
    const anh = new FormData();
    anh.append("img", anhTruyen);

    fetch("https://localhost:44383/api/ControllersTruyen/UploadImage", {
        method: 'POST',
        body: anh
    })
        .then(response => {
            if (response) {
                window.location.reload();
            }
        })
        .catch(error => {
            console.log('Lỗi khi upload ảnh:', error);
        });
}

function saveTruyen() {
    // Kiểm tra xem người dùng có chọn ảnh mới không
    const anhTruyen = document.getElementById('anhtruyenFile').files[0];
    let tenAnh = '';
    if (anhTruyen) {
        tenAnh = anhTruyen.name;  // Nếu người dùng chọn ảnh mới, lấy tên ảnh
    } else {
        // Nếu không chọn ảnh mới, giữ lại ảnh cũ
        tenAnh = document.getElementById('anhtruyen').value;
    }
    const tenTruyen = document.getElementById('tentruyen').value.trim();
    const isbn = document.getElementById('isbn').value.trim();
    const tacGia = document.getElementById('tacgia').value.trim();
    const doiTuong = document.getElementById('doituong').value.trim();
    const soTrang = document.getElementById('sotrang').value.trim();
    const dinhDang = document.getElementById('dinhdang').value.trim();
    const trongLuong = document.getElementById('trongluong').value.trim();
    const maTheLoai = document.getElementById('theloai').value.trim();
    const soLuong = document.getElementById('soluong').value.trim();
    const giaGoc = document.getElementById('giagoc').value.trim();
    const phanTramGiamGia = document.getElementById('phantramgiamgia').value.trim();

    if (!tenTruyen || !isbn || !tacGia || !doiTuong || !soTrang || !dinhDang || !trongLuong || !maTheLoai || !soLuong || !giaGoc || !phanTramGiamGia) {
        alert('Vui lòng nhập đầy đủ thông tin!');
        return;
    }


    // Nếu đang sửa thể loại (isEditing = true)
    if (isEditing) {
        const maTruyen = danhSachTruyen[currentIndex].maTruyen;

        const truyen = {
            maTruyen: maTruyen,
            anhTruyen: tenAnh,
            tenTruyen: tenTruyen,
            iSBN: isbn,
            tacGia: tacGia,
            doiTuong: doiTuong,
            soTrang: soTrang,
            dinhDang: dinhDang,
            trongLuong: trongLuong,
            maTheLoai: maTheLoai,
            soLuong: soLuong,
            giaGoc: giaGoc,
            phanTramGiamGia: phanTramGiamGia
        };


        const url = "https://localhost:44383/api/ControllersTruyen/Update";
        fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(truyen)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    uploadImage(anhTruyen);
                    alert('Sửa thông tin truyện thành công!');
                }
            })
            .catch(error => {
                console.log('Error:', error);
                alert('Có lỗi xảy ra trong quá trình cập nhật, vui lòng thử lại!');
            });
    } else {
        // Tạo object không bao gồm mã truyện khi thêm mới
        const truyenMoi = {
            anhTruyen: tenAnh,
            tenTruyen: tenTruyen,
            iSBN: isbn,
            tacGia: tacGia,
            doiTuong: doiTuong,
            soTrang: soTrang,
            dinhDang: dinhDang,
            trongLuong: trongLuong,
            maTheLoai: maTheLoai,
            soLuong: soLuong,
            giaGoc: giaGoc,
            phanTramGiamGia: phanTramGiamGia
        };
        console.log(JSON.stringify(truyenMoi));


        const url = "https://localhost:44383/api/ControllersTruyen/Create";
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(truyenMoi)
        })
            .then(response => {
                if (response.ok) {
                    dongDialog();
                    uploadImage(anhTruyen);
                    alert('Thêm truyện thành công!');
                }
            })
            .catch(error => {
                console.log('Lỗi khi thêm truyện mới:', error);
                alert('Có lỗi xảy ra trong quá trình thêm mới, vui lòng thử lại!');
            });
    }
}





function deleteTruyen(index) {
    if (confirm('Bạn có chắc muốn xóa thể loại này?')) {
        const maTruyen = danhSachTruyen[index].maTruyen;
        const url = `https://localhost:44383/api/ControllersTruyen/Delete/${maTruyen}`;

        fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (response.ok) {
                    getDataFromAPI();
                    getAllTruyen();
                    alert('Xoá truyện thành công!');
                } else {
                    console.log('Lỗi khi xóa truyện: ', error);
                }
            })
            .catch(error => {
                console.log(`Lỗi khi xóa truyện mã: ${maTruyen}`, error);
                alert('Có lỗi xảy ra trong quá trình xóa, vui lòng thử lại!');
            });
    }
}

function getTruyenByName() {
    const search = document.getElementById('searchInput').value.trim();

    if (search === '') {
        getDataFromAPI(); // Lấy lại toàn bộ danh sách nếu thanh tìm kiếm trống
        return;
    }

    const url = `https://localhost:44383/api/ControllersTruyen/Search/${search}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTruyen = data;
            getAllTruyen();

        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm truyện:', error);
        });
}


getDataFromAPI();
getDataFromAPITheLoai();