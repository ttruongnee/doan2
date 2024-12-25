create database DoAn2API
go
use DoAn2API

create table TaiKhoan (
    TaiKhoan nvarchar(50) primary key,
    MatKhau nvarchar(30) not null,
    Quyen nvarchar(50) not null,
)
go

create table NhanVien (
    MaNhanVien nvarchar(50) primary key,
    TenNhanVien nvarchar(50) not null,
	GioiTinh nvarchar(5),
    QueQuan nvarchar(150),
    SoDienThoai CHAR(10) not null,
    Email varchar(50) not null,
	TaiKhoan nvarchar(50) not null,
	foreign key (TaiKhoan) references TaiKhoan(TaiKhoan) on delete cascade
)
go

create table KhachHang (
    MaKhachHang nvarchar(50) primary key,
    TenKhachHang nvarchar(50) not null,
	GioiTinh nvarchar(5),
    SoDienThoai CHAR(10) not null,
    Email varchar(50) not null,
	TaiKhoan nvarchar(50) not null,
	foreign key (TaiKhoan) references TaiKhoan(TaiKhoan) on delete cascade
)
go

create table TheLoai (
    MaTheLoai nvarchar(50) primary key,
    TenTheLoai nvarchar(50) not null,
    MoTa nvarchar(max)  
)

go

create table Truyen (
    MaTruyen nvarchar(50) primary key,
    AnhTruyen nvarchar(MAX),
    TenTruyen nvarchar(100) not null,
	ISBN nvarchar(20) not null,
    TacGia nvarchar(50) not null,
	DoiTuong nvarchar(100) not null,
	SoTrang int not null,
	DinhDang nvarchar(50) not null,
	TrongLuong nvarchar(20) not null,
    MaTheLoai nvarchar(50),
    SoLuong int not null,
    GiaGoc float not null,
    PhanTramGiamGia float not null default 0 CHECK (PhanTramGiamGia >= 0 AND PhanTramGiamGia <= 100),
    GiaBan as (GiaGoc * ((100 - PhanTramGiamGia) / 100)),
    foreign key (MaTheLoai) references TheLoai(MaTheLoai) on delete set null
)
go

create table GiamGia (
    MaGiamGia varchar(15) primary key,
    NgayBatDau date not null,
    NgayKetThuc date not null,
    PhanTramGiamGia int not null
)
go

create table DonHang (
    MaDonHang nvarchar(50) primary key,
    MaKhachHangDatHang nvarchar(50),
	MaNhanVienXuLyDonHang nvarchar(50),
    NgayDatHang date not null,
	DiaChiGiaoHang nvarchar(max) not null,
    TrangThai nvarchar(100) not null,
	PhuongThucThanhToan nvarchar(100) not null,
    MaGiamGia varchar(15),
	TongTien float,
    foreign key (MaKhachHangDatHang) references KhachHang(MaKhachHang),
	foreign key (MaNhanVienXuLyDonHang) references NhanVien(MaNhanVien),
    foreign key (MaGiamGia) references GiamGia(MaGiamGia) on delete set null 
)
go

create table ChiTietDonHang ( 
    MaCTDonHang nvarchar(50) primary key,
    MaDonHang nvarchar(50) not null,
    MaTruyen nvarchar(50),
    SoLuong int not null,
    foreign key (MaDonHang) references DonHang(MaDonHang) on delete cascade,
    foreign key (MaTruyen) references Truyen(MaTruyen) on delete set null
)
go


create table GioHang (
    MaGioHang nvarchar(50) primary key,
    MaKhachHang nvarchar(50) not null,
    TongTien float not null,
    foreign key (MaKhachHang) references KhachHang(MaKhachHang) on delete cascade
)
go

create table ChiTietGioHang (
	MaCTGioHang nvarchar(50) primary key,
	MaGioHang nvarchar(50) not null,
	MaTruyen nvarchar(50) null,
    SoLuong int not null,
    foreign key (MaGioHang) references GioHang(MaGioHang) on delete cascade,
    foreign key (MaTruyen) references Truyen(MaTruyen) on delete set null
)
go


----------------------------------CHỨC NĂNG BẢNG NHÂN VIÊN----------------------------------------------------
--get all
create proc sp_GetAllNhanVien
as
begin
	select * from NhanVien
end
go

--sửa thông tin nhân viên
create proc sp_UpdateNhanVien
    @MaNhanVien nvarchar(50),
    @TenNhanVien nvarchar(50),
	@GioiTinh nvarchar(5),
    @QueQuan nvarchar(150),
    @SoDienThoai CHAR(10),
    @Email varchar(50)
as
begin
    update NhanVien
    set
        TenNhanVien = @TenNhanVien,
		GioiTinh = @GioiTinh,
        QueQuan = @QueQuan,
        SoDienThoai = @SoDienThoai,
        Email = @Email
    WHERE MaNhanVien = @MaNhanVien
end
go

--xoá nhân viên
create proc sp_DeleteNhanVien
	@MaNhanVien nvarchar(50)
as
begin
	update DonHang
    set MaNhanVienXuLyDonHang = null
    where MaNhanVienXuLyDonHang = @MaNhanVien;

	delete NhanVien where MaNhanVien = @MaNhanVien
end
go

--tìm kiếm nhân viên theo tên nhân viên
create proc sp_SearchNhanVienByName
	@TenNhanVien nvarchar(50)
as
begin
	select * from NhanVien
	where TenNhanVien like '%' + @TenNhanVien + '%'
end 
go


--lấy tên nhân viên theo tài khoản
create proc sp_GetNhanVienByUser
	@User nvarchar(50)
as
begin
	select * from NhanVien
	where TaiKhoan like '%' + @User + '%'
end 
go



----------------------------------CHỨC NĂNG BẢNG KHÁCH HÀNG----------------------------------------------------
--get all
create proc sp_GetAllKhachHang
as
begin
	select * from KhachHang
end
go

--sửa thông tin khách hàng
create proc sp_UpdateKhachHang
	@MaKhachHang nvarchar(50),
    @TenKhachHang nvarchar(50),
	@GioiTinh nvarchar(5),
    @SoDienThoai CHAR(10),
    @Email varchar(50)
as
begin
    update KhachHang
    set
        TenKhachHang = @TenKhachHang,
		GioiTinh = @GioiTinh,
        SoDienThoai = @SoDienThoai,
        Email = @Email
    WHERE MaKhachHang = @MaKhachHang
end
go

--xoá khách hàng
create proc sp_DeleteKhachHang
	@MaKhachHang nvarchar(50)
as
begin
	update DonHang
    set MaKhachHangDatHang = null
    where MaKhachHangDatHang = @MaKhachHang;

	delete KhachHang where MaKhachHang = @MaKhachHang
end
go


--tìm kiếm khách hàng theo tên khách hàng
create proc sp_SearchKhachHangByName
	@TenKhachHang nvarchar(50)
as
begin
	select * from KhachHang
	where TenKhachHang like '%' + @TenKhachHang + '%'
end 
go


--lấy tên khách hàng theo tài khoản
create proc sp_GetNameByUser
	@User nvarchar(50)
as
begin
	select * from KhachHang
	where TaiKhoan like '%' + @User + '%'
end 
go


----------------------------------CHỨC NĂNG BẢNG TÀI KHOẢN----------------------------------------------------
--get all tài khoản 
create proc sp_GetAllTaiKhoan
as
begin 
	select
		case
			when tk.Quyen in (N'Nhân viên', N'Quản lý') then nv.TenNhanVien
			when tk. Quyen = N'Khách hàng' then kh.TenKhachHang
		end as TenNguoiDung,
		tk.TaiKhoan, 
		MatKhau, 
		Quyen 
		from TaiKhoan tk left join NhanVien nv on tk.TaiKhoan = nv.TaiKhoan
						left join KhachHang kh on tk.TaiKhoan = kh.TaiKhoan
end
go

--get email nhân viên
create proc sp_GetEmailNhanVien
as
begin 
	select Email from NhanVien
end
go

--get email khách hàng
create proc sp_GetEmailKhachHang
as
begin 
	select Email from KhachHang
end
go

--đăng nhập admin
create proc sp_DangNhapAdmin
	@TaiKhoan nvarchar(50),
	@MatKhau nvarchar(30)
as
begin 
	select 1 from TaiKhoan
	where TaiKhoan = @TaiKhoan and MatKhau = @MatKhau and Quyen in (N'Nhân viên', N'Quản lý')
end
go

--đăng nhập khách hàng
create proc sp_DangNhapUser
	@TaiKhoan nvarchar(50),
	@MatKhau nvarchar(30)
as
begin 
	select 1 from TaiKhoan
	where TaiKhoan = @TaiKhoan and MatKhau = @MatKhau and Quyen = N'Khách hàng'
end
go

--thêm tài khoản
create proc sp_CreateTaiKhoan
    @TaiKhoan nvarchar(50),
    @MatKhau nvarchar(30),
    @Quyen nvarchar(50),
    @TenNguoiDung nvarchar(50),
    @GioiTinh nvarchar(5),
    @SoDienThoai char(10),
    @Email varchar(50),
    @QueQuan nvarchar(150) = null -- Dùng cho nhân viên
as
begin
    insert into TaiKhoan (TaiKhoan, MatKhau, Quyen)
    values (@TaiKhoan, @MatKhau, @Quyen)

    if @Quyen in (N'Nhân viên', N'Quản lý')
    begin
        insert into NhanVien (MaNhanVien, TenNhanVien, GioiTinh, QueQuan, SoDienThoai, Email, TaiKhoan)
        values (NEWID(), @TenNguoiDung, @GioiTinh, @QueQuan, @SoDienThoai, @Email, @TaiKhoan)
    end

    else if @Quyen = N'Khách hàng'
    begin
        insert into KhachHang (MaKhachHang, TenKhachHang, GioiTinh, SoDienThoai, Email, TaiKhoan)
        values (NEWID(), @TenNguoiDung, @GioiTinh, @SoDienThoai, @Email, @TaiKhoan)
    end
end
go

--đổi mật khẩu
create procedure sp_UpdateMatKhau
    @Email nvarchar(50),
    @MatKhau nvarchar(30)
as
begin
    update TaiKhoan
    set
        MatKhau = @MatKhau
		where TaiKhoan = (
			select TaiKhoan
			from NhanVien
			where Email	= @Email)
end
go

--xoá tài khoản
create proc sp_DeleteTaiKhoan
	@TaiKhoan nvarchar(50)
as
begin
	declare @MaNguoiDung nvarchar(50)
	declare @Quyen nvarchar(50)

	--xác định quyền của tài khoản để tìm mã nv hoặc kh
	select @Quyen = Quyen 
	from TaiKhoan
	where TaiKhoan = @TaiKhoan

	--xác định mã nv hoặc kh để xoá
	if @Quyen in (N'Nhân viên', N'Quản lý')
	begin 
		select @MaNguoiDung = MaNhanVien 
		from NhanVien
		where TaiKhoan = @TaiKhoan

		exec sp_DeleteNhanVien @MaNguoiDung
	end
	else if @Quyen = N'Khách hàng' 
	begin 
		select @MaNguoiDung = MaKhachHang
		from KhachHang
		where TaiKhoan = @TaiKhoan

		exec sp_DeleteKhachHang @MaNguoiDung
	end

	--xoá tài khoản
	delete TaiKhoan where TaiKhoan = @TaiKhoan
end
go

-- Tìm kiếm tài khoản 
create proc sp_SearchTaiKhoanByName
@TenNguoiDung nvarchar(50)
as
begin 
	select
		case
			when tk.Quyen in (N'Nhân viên', N'Quản lý') then nv.TenNhanVien
			when tk. Quyen = N'Khách hàng' then kh.TenKhachHang
		end as TenNguoiDung,
		tk.TaiKhoan, 
		MatKhau, 
		Quyen 
		from TaiKhoan tk left join NhanVien nv on tk.TaiKhoan = nv.TaiKhoan
						left join KhachHang kh on tk.TaiKhoan = kh.TaiKhoan
		where (nv.TenNhanVien is not null and nv.TenNhanVien like '%' + @TenNguoiDung + '%')
			or (kh.TenKhachHang is not null and kh.TenKhachHang like '%' + @TenNguoiDung + '%')
end
go 

--lấy tài khoản từ tên người dùng
create proc sp_GetQuyenByID
	@TaiKhoan nvarchar(50)
as
begin 
	select * from TaiKhoan where TaiKhoan = @TaiKhoan
end
go

----------------------------------CHỨC NĂNG BẢNG THỂ LOẠI----------------------------------------------------
--get all thể loại
create proc sp_GetAllTheLoai
as
begin 
	select * from TheLoai
end
go

--thêm thể loại
create proc sp_CreateTheLoai
	@MaTheLoai nvarchar(50),
    @TenTheLoai nvarchar(50),
    @MoTa nvarchar(MAX) = null  
as
begin 
	insert into TheLoai(MaTheLoai, TenTheLoai, MoTa)
	values(@MaTheLoai, @TenTheLoai, @MoTa)
end
go

--sửa thể loại
create procedure sp_UpdateTheLoai
    @MaTheLoai nvarchar(50),
    @TenTheLoai nvarchar(50),
    @MoTa nvarchar(max)
as
begin
    update TheLoai
    set
        TenTheLoai = @TenTheLoai,
        MoTa = @MoTa
    where MaTheLoai = @MaTheLoai
end
go

--xoá thể loại
create proc sp_DeleteTheLoai
	@MaTheLoai nvarchar(50)
as
begin
	delete TheLoai where MaTheLoai = @MaTheLoai
end
go

--tìm kiếm thể loại
create proc sp_SearchTheLoaiByName
	@TenTheLoai nvarchar(50)
as
begin
	select * from TheLoai 
	where TenTheLoai like '%' + @TenTheLoai + '%'
end
go


----------------------------------CHỨC NĂNG BẢNG TRUYỆN----------------------------------------------------
--getall truyện
create proc sp_GetAllTruyen
as
begin
	select 
    MaTruyen,
    AnhTruyen,
    TenTruyen,
	ISBN,
    TacGia,
	DoiTuong,
	SoTrang,
	DinhDang,
	TrongLuong,
	t.MaTheLoai,
    tl.TenTheLoai,
    SoLuong,
    GiaGoc,
    PhanTramGiamGia,
    GiaBan
	from Truyen t left join TheLoai tl on t.MaTheLoai = tl.MaTheLoai
end
go

--thêm truyện
create proc sp_CreateTruyen
	@MaTruyen nvarchar(50),
    @AnhTruyen nvarchar(MAX) = null,
    @TenTruyen nvarchar(100),
	@ISBN nvarchar(20),
    @TacGia nvarchar(50),
	@DoiTuong nvarchar(100),
	@SoTrang int,
	@DinhDang nvarchar(50),
	@TrongLuong nvarchar(20),
    @MaTheLoai nvarchar(50),
    @SoLuong int,
    @GiaGoc float,
    @PhanTramGiamGia float
as
begin 
	insert into Truyen(MaTruyen, AnhTruyen, TenTruyen, ISBN, TacGia, DoiTuong, SoTrang, DinhDang, TrongLuong, MaTheLoai, SoLuong, Giagoc, PhanTramGiamGia)
	values(@MaTruyen, @AnhTruyen, @TenTruyen, @ISBN, @TacGia, @DoiTuong, @SoTrang, @DinhDang, @TrongLuong, @MaTheLoai, @SoLuong, @Giagoc, @PhanTramGiamGia)
end
go

--sửa truyện
create procedure sp_UpdateTruyen
	@MaTruyen nvarchar(50),
    @AnhTruyen nvarchar(MAX),
    @TenTruyen nvarchar(100),
	@ISBN nvarchar(20),
    @TacGia nvarchar(50),
	@DoiTuong nvarchar(100),
	@SoTrang int,
	@DinhDang nvarchar(50),
	@TrongLuong nvarchar(20),
    @MaTheLoai nvarchar(50),
    @SoLuong int,
    @GiaGoc float,
    @PhanTramGiamGia float
as
begin 
    update Truyen
    set
        AnhTruyen = @AnhTruyen,
        TenTruyen = @TenTruyen,
        ISBN = @ISBN,
        TacGia = @TacGia,
        DoiTuong = @DoiTuong,
        SoTrang = @SoTrang,
        DinhDang = @DinhDang,
        TrongLuong = @TrongLuong,
        MaTheLoai = @MaTheLoai,
        SoLuong = @SoLuong,
        Giagoc = Giagoc,
        PhanTramGiamGia =@PhanTramGiamGia
    where 
        MaTruyen = @MaTruyen
end
go

--xoá truyện
create proc sp_DeleteTruyen
	@MaTruyen nvarchar(50)
as
begin 
	delete Truyen where MaTruyen = @MaTruyen
end 
go

-- Tìm kiếm bằng mã truyện
create proc sp_SearchByID
    @MaTruyen nvarchar(50)
as
begin
    select 
        MaTruyen,
        AnhTruyen,
        TenTruyen,
        ISBN,
        TacGia,
        DoiTuong,
        SoTrang,
        DinhDang,
        TrongLuong,
        t.MaTheLoai,
        tl.TenTheLoai,
        SoLuong,
        GiaGoc,
        PhanTramGiamGia,
        GiaBan
    from Truyen t
    LEFT JOIN TheLoai tl on t.MaTheLoai = tl.MaTheLoai
    where MaTruyen = @MaTruyen
end
go

-- Tìm kiếm bằng tên truyện hoặc tên thể loại
create proc sp_SearchByName
    @TimKiem nvarchar(200) 
as
begin
    select 
        MaTruyen,
        AnhTruyen,
        TenTruyen,
        ISBN,
        TacGia,
        DoiTuong,
        SoTrang,
        DinhDang,
        TrongLuong,
        t.MaTheLoai,
        tl.TenTheLoai,
        SoLuong,
        GiaGoc,
        PhanTramGiamGia,
        GiaBan
    from Truyen t
    LEFT JOIN TheLoai tl on t.MaTheLoai = tl.MaTheLoai
    where TenTruyen LIKE '%' + @TimKiem + '%' 
        OR tl.TenTheLoai LIKE '%' + @TimKiem + '%'
end
go

--tìm kiếm truyện theo tên thể loại
create proc sp_GetTruyenByTheLoai
	@TenTheLoai nvarchar(200) = null
as
begin
	select 
    MaTruyen,
    AnhTruyen,
    TenTruyen,
	ISBN,
    TacGia,
	DoiTuong,
	SoTrang,
	DinhDang,
	TrongLuong,
	t.MaTheLoai,
    tl.TenTheLoai,
    SoLuong,
    GiaGoc,
    PhanTramGiamGia,
    GiaBan
	from Truyen t left join TheLoai tl on t.MaTheLoai = tl.MaTheLoai
    WHERE (tl.TenTheLoai is null or tl.TenTheLoai like '%' + @TenTheLoai + '%')
	or (tl.MaTheLoai is null or tl.MaTheLoai like '%' + @TenTheLoai + '%')
end
go
exec sp_GetTruyenByTheLoai @TenTheLoai = ' '
----------------------------------CHỨC NĂNG BẢNG GIẢM GIÁ----------------------------------------------------
--getall mã giảm giá
create proc sp_GetAllGiamGia
as
begin 
	select * from GiamGia
end
go 

--áp mã giảm giá( lấy ra mã giảm giá người dùng nhập nếu có)
create proc sp_ApplyMaGiamGia
@MaGiamGia varchar(15)
as
begin 
	select * from GiamGia where MaGiamGia = @MaGiamGia
end
go 

--thêm mã giảm giá
create proc sp_CreateGiamGia
	@MaGiamGia varchar(15),
    @NgayBatDau date,
    @NgayKetThuc date,
    @PhanTramGiamGia int
as
begin
	insert into GiamGia(MaGiamGia, NgayBatDau, NgayKetThuc, PhanTramGiamGia)
	values(@MaGiamGia, @NgayBatDau, @NgayKetThuc, @PhanTramGiamGia)
end
go


--sửa mã giảm giá
create proc sp_updateGiamGia
	@MaGiamGia varchar(15),
    @NgayBatDau date = null,
    @NgayKetThuc date = null,
    @PhanTramGiamGia int = null
as
begin 
	update GiamGia
    set
		NgayBatDau = ISNULL(@NgayBatDau, NgayBatDau),
		 NgayKetThuc = ISNULL(@NgayKetThuc, NgayKetThuc),
        PhanTramGiamGia = ISNULL(@PhanTramGiamGia, PhanTramGiamGia)
	where MaGiamGia = @MaGiamGia
end
go

--xoá mã giảm giá
create proc sp_DeleteGiamGia
	@MaGiamGia varchar(15)
as
begin 
	delete GiamGia where MaGiamGia = @MaGiamGia
end
go

--tìm kiếm mã giảm giá theo thời gian
create proc sp_SearchMaGiamGiaBydateTime
	@NgayBatDau date,
	@NgayKetThuc date
as
begin 
	
	select * from GiamGia
	where NgayBatDau <= @NgayKetThuc 
	and NgayKetThuc >= @NgayBatDau
end 
go

----------------------------------CHỨC NĂNG BẢNG ĐƠN HÀNG----------------------------------------------------
--getall đơn hàng 
create proc sp_GetAllDonHang
as
begin 
    select 
        dh.MaDonHang,
        dh.MaKhachHangDatHang,
        kh.TenKhachHang,
        dh.MaNhanVienXuLyDonHang,
        nv.TenNhanVien,
        dh.NgayDatHang,
		dh.DiaChiGiaoHang,
        dh.TrangThai,
        dh.PhuongThucThanhToan,
		dh.MaGiamGia,
        dh.TongTien
    from DonHang dh
    left join KhachHang kh on dh.MaKhachHangDatHang = kh.MaKhachHang
    left join NhanVien nv on dh.MaNhanVienXuLyDonHang = nv.MaNhanVien
end
go

--get chi tiết đơn hàng 
create proc sp_GetChiTietDonHangByID
@MaDonHang nvarchar(50)
as
begin
	select 
    ct.MaCTDonHang,
    ct.MaDonHang,
    ct.MaTruyen,
	t.AnhTruyen,
	t.TenTruyen,
	t.GiaBan,
    ct.SoLuong,
	t.GiaBan,
    (ct.SoLuong * t.GiaBan) as ThanhTien
	from ChiTietDonHang ct
	join Truyen t on ct.MaTruyen = t.MaTruyen
	where MaDonHang = @MaDonHang
end
go 

--thêm đơn hàng
alter proc sp_CreateDonHang
    @MaDonHang nvarchar(50),
    @MaKhachHangDatHang nvarchar(50),
    @MaNhanVienXuLyDonHang nvarchar(50) = NULL,
    @NgayDatHang date,
	@DiaChiGiaoHang nvarchar(max),
    @TrangThai nvarchar(100),
    @PhuongThucThanhToan nvarchar(100),
    @MaGiamGia nvarchar(15) = NULL,
	@TongTien float,
    @listjson_chitiet nvarchar(MAX)
AS
BEGIN
    --thêm đơn hàng
    insert into DonHang (MaDonHang, MaKhachHangDatHang, MaNhanVienXuLyDonHang, NgayDatHang, DiaChiGiaoHang, TrangThai, PhuongThucThanhToan, MaGiamGia, TongTien)
    values (@MaDonHang, @MaKhachHangDatHang, @MaNhanVienXuLyDonHang, @NgayDatHang, @DiaChiGiaoHang, @TrangThai, @PhuongThucThanhToan, @MaGiamGia, @TongTien);

    --thêm chi tiết đơn hàng
    if (@listjson_chitiet IS NOT NULL)
		begin
			insert into ChiTietDonHang (MaCTDonHang, MaDonHang, MaTruyen, SoLuong)
			select 
				json_value(p.value, '$.maCTDonHang'), 
				@MaDonHang, 
				json_value(p.value, '$.maTruyen'), 
				json_value(p.value, '$.soLuong')
			from openjson(@listjson_chitiet) as p;

			 -- trừ số lượng trong bảng Truyen
			update Truyen
			set SoLuong = t.SoLuong - ct.SoLuong
			from 
				Truyen t join 
				(select 
					json_value(p.value, '$.maTruyen') as MaTruyen, 
					json_value(p.value, '$.soLuong') as SoLuong
				from openjson(@listjson_chitiet) as p)as ct
			on t.MaTruyen = ct.MaTruyen;
		end
	select '';
	end
go

--sửa đơn hàng
alter proc sp_UpdateDonHang
	@MaDonHang nvarchar(50),
	@MaNhanVienXuLyDonHang nvarchar(50),
	@TrangThai nvarchar(100)
as
begin 
	update DonHang
	set
		MaNhanVienXuLyDonHang = @MaNhanVienXuLyDonHang,
		TrangThai = @TrangThai
	where 
		MaDonHang = @MaDonHang
end
go

--tìm kiếm đơn hàng
alter procedure sp_SearchDonHang
    @Search nvarchar(100) = null
as
begin
    select 
        dh.MaDonHang,
        dh.MaKhachHangDatHang,
        kh.TenKhachHang,
        dh.MaNhanVienXuLyDonHang,
        nv.TenNhanVien,
        dh.NgayDatHang,
        dh.DiaChiGiaoHang,
        dh.TrangThai,
        dh.PhuongThucThanhToan,
        dh.MaGiamGia,
        dh.TongTien
    from DonHang dh
    left join KhachHang kh on dh.MaKhachHangDatHang = kh.MaKhachHang
    left join NhanVien nv on dh.MaNhanVienXuLyDonHang = nv.MaNhanVien
    where 
        @Search is null or 
        kh.TenKhachHang like '%' + @Search + '%' or 
        nv.TenNhanVien like '%' + @Search + '%' or
        dh.DiaChiGiaoHang like '%' + @Search + '%'
end;
go

----------------------------------CHỨC NĂNG BẢNG GIỎ HÀNG----------------------------------------------------
 --thêm giỏ hàng
create proc sp_CreateGioHang
    @MaGioHang nvarchar(50),
    @MaKhachHang nvarchar(50),
    @TongTien float,
    @listjson_chitiet nvarchar(MAX)
AS
BEGIN
    --thêm giỏ hàng
    insert into GioHang(MaGioHang, MaKhachHang, TongTien)
    values (@MaGioHang, @MaKhachHang, @TongTien);

    --thêm chi tiết giỏ hàng
    if (@listjson_chitiet IS NOT NULL)
		begin
			insert into ChiTietGioHang(MaCTGioHang, MaGioHang, MaTruyen, SoLuong)
			select 
				json_value(p.value, '$.maCTGioHang'),
				@MaGioHang, 
				json_value(p.value, '$.maTruyen'), 
				json_value(p.value, '$.soLuong')
			from openjson(@listjson_chitiet) as p;
		end
	select '';
	end
go

--lấy mã khách hàng từ tài khoản
create proc sp_GetMaKhachHang
	@TaiKhoan nvarchar(50)
as
begin 
	select MaKhachHang from KhachHang where TaiKhoan = @TaiKhoan
end
go

--xoá giỏ hàng bằng mã giỏ hàng
create proc sp_DeleteGioHang
	@MaGioHang nvarchar(50)
as
begin 
	delete GioHang where MaGioHang = @MaGioHang
end
go


create proc sp_GetGioHangByUser
    @MaGioHang nvarchar(50)
as
begin
    select 
        g.MaGioHang, 
        g.MaKhachHang, 
        g.TongTien,
        (
            select 
                ct.MaCTGioHang,
                ct.MaGioHang,
                ct.MaTruyen,
                ct.SoLuong,
                t.TenTruyen,
                t.GiaBan
            from ChiTietGioHang as ct
            JOIN Truyen t ON ct.MaTruyen = t.MaTruyen
            where ct.MaGioHang = g.MaGioHang
            for json path
        ) as listjson_chitiet
    from GioHang g
    where g.MaGioHang = @MaGioHang;
end
go

delete GioHang
select * from Truyen
select * from KhachHang
select * from GioHang
select * from ChiTietGioHang
select * from DonHang
select * from ChiTietDonHang
select * from TaiKhoan
