getDataFromAPI();

//hiển thị lên trang chủ
getTruyenByTheLoai('Tháng sách Kim Đồng', function (listTruyen) {
    getTruyenByCount('thangsachkimdong', listTruyen, 5);
    createXemThem('thangsachkimdong', listTruyen[0]);
});

getTruyenByTheLoai('Sách mới', function (listTruyen) {
    getTruyenByCount('sachmoi', listTruyen, 5);
    createXemThem('sachmoi', listTruyen[0]);
});

getTruyenByTheLoai('Combo', function (listTruyen) {
    getTruyenByCount('combo', listTruyen, 5);
    createXemThem('combo', listTruyen[0]);
});

getTruyenByTheLoai('Manga - Comic', function (listTruyen) {
    getTruyenByCount('mangacomic', listTruyen, 5);
    createXemThem('mangacomic', listTruyen[0]);
});

getTruyenByTheLoai('Doraemon', function (listTruyen) {
    getTruyenByCount('doraemon', listTruyen, 5);
    createXemThem('doraemon', listTruyen[0]);
});

getTruyenByTheLoai('Wingsbooks', function (listTruyen) { //callback được chạy sau khi getTruyenByTheLoai đã được chạy xong và sử dụng data của hàm đó
    getTruyenByCount('wingsbooks', listTruyen, 5);
    createXemThem('wingsbooks', listTruyen[0]);
});

getTruyenByTheLoai('Siêu ưu đãi', function (listTruyen) {
    getTruyenByCount('sieuuudai', listTruyen, 5);
    createXemThem('sieuuudai', listTruyen[0]);
});


