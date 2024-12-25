const urlParamfind = new URLSearchParams(window.location.search);
const tenTruyen = urlParamfind.get('tenTruyen');

document.getElementById('header-find').addEventListener('submit', function (event) {
    event.preventDefault();

    let input = document.querySelector('.input-find').value;
    window.location.href = `DanhMucSP.html?tenTruyen=${input}`;
})

if (tenTruyen) {
    getTruyenByTenTruyen(`${tenTruyen}`, function (listTruyen) {
        getTruyenByCount('main-truyen', listTruyen, 9999);
    });
    document.querySelector('.banner').style.display = 'none';
    document.querySelector('.input-find').value = tenTruyen;
}


function getTruyenByTenTruyen(tentruyen, callback) {
    if (tentruyen === '') {
        callback([]);
        return;
    }

    const url = `https://localhost:44346/api/ControllersTruyen/SearchByName/${tentruyen}`;

    fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            callback(data);
            document.getElementById('tendanhmuc').innerHTML = `Sản phẩm tìm kiếm với "${tentruyen}"`;
        })
        .catch(error => {
            console.log('Lỗi khi tìm kiếm truyện:', error);
            callback([]);
        });
}



