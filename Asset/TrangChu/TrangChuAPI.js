let danhSachTruyen = [];
let danhSachTheLoai = [];

function getDataFromAPI() {
    const url = "https://localhost:44346/api/ControllersTruyen/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            danhSachTruyen = data;
        })
        .catch(error => {
            console.log('Lỗi khi lấy danh sách truyện từ API:', error);
        });
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

function editTenTruyen(str) {
    const isMobile = window.innerWidth <= 740;

    const maxLength = isMobile ? 30 : 42;

    if (str.length > maxLength) {
        return str.substring(0, maxLength).trim() + "...";
    }
    return str;
}

function getTruyenByCount(containerId, danhSachTruyen, soluong) {
    const listItemContainer = document.getElementById(containerId); //lấy thẻ chứa ds

    if (!danhSachTruyen || danhSachTruyen.length === 0) {
        listItemContainer.innerHTML = '<p>Không có dữ liệu truyện để hiển thị.</p>';
        return;
    }

    const row = document.createElement('div');
    row.className = 'row';
    row.style.width = '100%';
    listItemContainer.appendChild(row);

    for (let i = 0; i < danhSachTruyen.length && i < soluong; i++) {
        const truyen = danhSachTruyen[i];

        const col = document.createElement('div');
        col.className = 'col l-2-4 m-3 c-4 checkSizeMobile';

        col.innerHTML = `
            <div class="product">
                <a href="ChiTietSP.html?maTruyen=${truyen.maTruyen}" class="a-black">
                    <div class="product-img">
                        <img id='poster-list-${truyen.maTruyen}' class="product-img" src="" alt="${truyen.tenTruyen}">
                    </div>
                    <h4 class="product-name">
                        ${editTenTruyen(truyen.tenTruyen)}
                    </h4>
                    <div class="product-price">
                        <span class="current-price"><b>${truyen.giaBan.toLocaleString('vi-VN')}₫</b></span>
                        <span class="original-price"><s><b>${truyen.giaGoc.toLocaleString('vi-VN')}₫</b></s></span>
                    </div>
                </a>
            </div>
        `;

        row.appendChild(col);

        SetFile(`${truyen.anhTruyen}`, document.getElementById(`poster-list-${truyen.maTruyen}`));
    }

    checkScreenSize();
}

function createXemThem(classname, truyen) {
    const row = document.createElement('div');
    row.className = 'row';
    row.innerHTML =
        `<a class="col l-12 m-12 c-12 a-red xemthem" href="DanhMucSP.html?maTheLoai=${truyen.maTheLoai}">
                    <span>Xem thêm >></span>
                </a>`
    document.querySelector(`.${classname}`).appendChild(row);
}

function getTruyenByTheLoai(tenTheLoai, callback) {
    if (tenTheLoai === '') {
        callback([]);
        return;
    }

    const url = `https://localhost:44346/api/ControllersTruyen/GetTruyenByTheLoai/${tenTheLoai}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            callback(data);
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm truyện:', error);
            callback([]);
        });
}



function checkScreenSize() {
    const checkSizeMobile = document.querySelectorAll('.checkSizeMobile');
    checkSizeMobile.forEach(item => {
        if (window.innerWidth > 520) {
            item.classList.remove('c-6');
            item.classList.add('c-4');
        }
        else {
            item.classList.remove('c-4');
            item.classList.add('c-6');
        }
    })
}

window.addEventListener('resize', checkScreenSize);
window.addEventListener('load', checkScreenSize);

