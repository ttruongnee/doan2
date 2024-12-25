// Khai báo danh sách thể loại
let dsTheLoai = [];

// Hàm lấy dữ liệu thể loại từ API
function getDataFromAPI() {
    const url = "https://localhost:44346/api/ControllersTheLoai/Get-All";
    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
        .then((response) => response.json())
        .then((data) => {
            dsTheLoai = data;
            getAllTheLoai();
        })
        .catch((error) => {
            console.error("Lỗi khi lấy danh sách thể loại từ API:", error);
        });
}

function getAllTheLoai() {
    //lấy thẻ ul ở màn tablet pc
    const tablet_pc = document.querySelector(".danhmucsanpham .list-box ul");

    //lấy thẻ ở màn mobile
    const mobile = document.querySelector(".nav_mobile-list ul");

    if (tablet_pc) tablet_pc.innerHTML = "";
    if (mobile) mobile.innerHTML = "";

    //thêm thẻ tất cả sản phẩm
    const allProduct = `
        <a class="danhmuc-a" href="./DanhMucSP.html?maTheLoai=get-all">
            <li class="list-box-item">
                <i class="fas fa-book" style="font-size: 16px; margin-right: 4px;"></i>
                Tất cả sản phẩm
            </li>
        </a>`;
    if (tablet_pc) tablet_pc.insertAdjacentHTML("beforeend", allProduct);
    if (mobile) mobile.insertAdjacentHTML("beforeend", allProduct);

    //tạo ds thể loại từ api
    dsTheLoai.forEach((theLoai) => {
        const categoryHTML = `
            <a class="danhmuc-a" href="./DanhMucSP.html?maTheLoai=${theLoai.maTheLoai}">
                <li class="list-box-item">
                    <i class="fas fa-book" style="font-size: 16px; margin-right: 4px;"></i>
                    ${theLoai.tenTheLoai}
                </li>
            </a>`;
        if (tablet_pc) tablet_pc.insertAdjacentHTML("beforeend", categoryHTML);
        if (mobile) mobile.insertAdjacentHTML("beforeend", categoryHTML);
    });
}

getDataFromAPI();
