CREATE DATABASE Project_64130980
GO

USE Project_64130980
GO

-- 1) Bảng LOẠI SẢN PHẨM
CREATE TABLE LoaiSanPham (
    MaLoaiSP NVARCHAR(10) PRIMARY KEY,
    TenLoaiSP NVARCHAR(100) NOT NULL
);

-- 2) Bảng SẢN PHẨM
CREATE TABLE SanPham (
    MaSP NVARCHAR(10) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
	SoLuong INT,
    DonViTinh NVARCHAR(50) NOT NULL,
    AnhSanPham NVARCHAR(255) DEFAULT '/Images/default.png',
    DonGia DECIMAL(18, 2) NOT NULL,
    MaLoaiSP NVARCHAR(10) NOT NULL,
    FOREIGN KEY (MaLoaiSP) REFERENCES LoaiSanPham(MaLoaiSP)
	ON UPDATE CASCADE
	ON DELETE CASCADE
);

-- 3) Bảng KHÁCH HÀNG
CREATE TABLE KhachHang (
    MaKH NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
	GioiTinh BIT DEFAULT(1),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    SoDienThoai NVARCHAR(15) NOT NULL,
    DiaChi NVARCHAR(255) NOT NULL,
);

-- 4) Bảng NHÂN VIÊN
CREATE TABLE NhanVien (
    MaNV NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
	GioiTinh BIT DEFAULT(1),
    SoDienThoai NVARCHAR(15) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    QuyenSuDung TINYINT NOT NULL
);

-- 5) Bảng GIỎ HÀNG
CREATE TABLE GioHang (
    SoHD NVARCHAR(10) PRIMARY KEY,
    NgayDatHang DATETIME NOT NULL,
    MaKH NVARCHAR(10) NOT NULL,
    MaNVGiaoHang NVARCHAR(10) NOT NULL,
	TongTien DECIMAL(18, 2) NOT NULL DEFAULT 0,
	LoaiThanhToan BIT DEFAULT(1),
    TinhTrang TINYINT NOT NULL
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
        ON DELETE CASCADE,
    FOREIGN KEY (MaNVGiaoHang) REFERENCES NhanVien(MaNV)
        ON DELETE CASCADE
);

-- 6) Bảng CHI TIẾT GIỎ HÀNG
CREATE TABLE ChiTietGioHang (
	MaCTGH INT PRIMARY KEY IDENTITY(1,1),
    SoHD NVARCHAR(10) NOT NULL,
    MaSP NVARCHAR(10) NOT NULL,
    SoLuong INT NOT NULL,
    DonGiaBan DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (SoHD) REFERENCES GioHang(SoHD)
	ON DELETE CASCADE,
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
	ON DELETE CASCADE
);

GO

INSERT INTO LoaiSanPham (MaLoaiSP, TenLoaiSP) VALUES
(N'LSP001', N'Đồ chơi trẻ em'),
(N'LSP002', N'Đồ chơi giáo dục'),
(N'LSP003', N'Đồ chơi mô hình'),
(N'LSP004', N'Đồ chơi điện tử'),
(N'LSP005', N'Đồ chơi gỗ'),
(N'LSP006', N'Đồ chơi ngoài trời'),
(N'LSP007', N'Đồ chơi nghệ thuật'),
(N'LSP008', N'Đồ chơi thông minh'),
(N'LSP009', N'Đồ chơi sáng tạo'),
(N'LSP010', N'Đồ chơi thể thao');
GO

INSERT INTO SanPham (MaSP, TenSP, MoTa, SoLuong, DonViTinh, AnhSanPham, DonGia, MaLoaiSP) VALUES
(N'SP001', N'Lego Classic', N'Bộ xếp hình Lego cơ bản', 100, N'Hộp', N'/Images/default.png', 500000, N'LSP001'),
(N'SP002', N'Rubik 3x3', N'Khối lập phương trí tuệ', 200, N'Cái', N'/Images/default.png', 150000, N'LSP008'),
(N'SP003', N'Xe điều khiển từ xa', N'Xe địa hình điều khiển từ xa', 50, N'Chiếc', N'/Images/default.png', 1200000, N'LSP004'),
(N'SP004', N'Búp bê Barbie', N'Búp bê thời trang Barbie', 120, N'Hộp', N'/Images/default.png', 700000, N'LSP001'),
(N'SP005', N'Đàn Piano gỗ', N'Đàn piano nhỏ bằng gỗ', 80, N'Cái', N'/Images/default.png', 850000, N'LSP005'),
(N'SP006', N'Bảng học chữ cái', N'Bảng học chữ cái và số', 150, N'Bộ', N'/Images/default.png', 300000, N'LSP002'),
(N'SP007', N'Bộ tô màu', N'Bộ dụng cụ tô màu sáng tạo', 100, N'Bộ', N'/Images/default.png', 250000, N'LSP007'),
(N'SP008', N'Robot lắp ráp', N'Robot thông minh có thể lắp ráp', 60, N'Hộp', N'/Images/default.png', 1500000, N'LSP009'),
(N'SP009', N'Xếp hình 3D', N'Mô hình xếp hình 3D độc đáo', 90, N'Bộ', N'/Images/default.png', 600000, N'LSP003'),
(N'SP010', N'Cầu trượt mini', N'Cầu trượt cho trẻ em', 30, N'Bộ', N'/Images/default.png', 2000000, N'LSP006'),
(N'SP011', N'Xe trượt scooter', N'Xe trượt cho trẻ em năng động', 50, N'Cái', N'/Images/default.png', 1200000, N'LSP006'),
(N'SP012', N'Bộ cờ vua', N'Cờ vua bằng gỗ cao cấp', 200, N'Bộ', N'/Images/default.png', 400000, N'LSP005'),
(N'SP013', N'Mô hình xe hơi', N'Mô hình xe hơi cổ điển', 70, N'Hộp', N'/Images/default.png', 500000, N'LSP003'),
(N'SP014', N'Bóng rổ mini', N'Bộ chơi bóng rổ mini trong nhà', 150, N'Bộ', N'/Images/default.png', 800000, N'LSP010'),
(N'SP015', N'Gấu bông Teddy', N'Thú nhồi bông gấu Teddy', 300, N'Cái', N'/Images/default.png', 450000, N'LSP001'),
(N'SP016', N'Bộ lắp ghép sáng tạo', N'Dụng cụ lắp ghép nhiều mẫu', 120, N'Bộ', N'/Images/default.png', 550000, N'LSP009'),
(N'SP017', N'Đèn ngủ ngôi sao', N'Đèn ngủ hình ngôi sao cho trẻ', 200, N'Cái', N'/Images/default.png', 300000, N'LSP008'),
(N'SP018', N'Lều cắm trại trẻ em', N'Lều nhỏ cho bé chơi trong nhà', 50, N'Bộ', N'/Images/default.png', 1500000, N'LSP006'),
(N'SP019', N'Máy bay mô hình', N'Mô hình máy bay chiến đấu', 100, N'Cái', N'/Images/default.png', 700000, N'LSP003'),
(N'SP020', N'Bộ đồ chơi nhà bếp', N'Dụng cụ nhà bếp cho trẻ', 80, N'Bộ', N'/Images/default.png', 350000, N'LSP001'),
(N'SP021', N'Đĩa bay UFO', N'Đồ chơi đĩa bay phát sáng', 150, N'Cái', N'/Images/default.png', 200000, N'LSP010'),
(N'SP022', N'Thảm chơi đa năng', N'Thảm chơi cho bé nhiều chức năng', 60, N'Tấm', N'/Images/default.png', 1000000, N'LSP002'),
(N'SP023', N'Bộ cờ tỷ phú', N'Trò chơi cờ tỷ phú bản lớn', 90, N'Bộ', N'/Images/default.png', 450000, N'LSP007'),
(N'SP024', N'Xếp gỗ Jenga', N'Trò chơi xếp gỗ cân bằng', 120, N'Bộ', N'/Images/default.png', 300000, N'LSP005'),
(N'SP025', N'Cá sấu kẹp tay', N'Trò chơi cá sấu vui nhộn', 150, N'Cái', N'/Images/default.png', 150000, N'LSP008'),
(N'SP026', N'Mô hình khủng long', N'Mô hình khủng long nhựa', 80, N'Cái', N'/Images/default.png', 400000, N'LSP003'),
(N'SP027', N'Súng nước', N'Đồ chơi súng nước cỡ lớn', 200, N'Cái', N'/Images/default.png', 250000, N'LSP010'),
(N'SP028', N'Ghép tranh gỗ', N'Tranh gỗ nhiều mảnh ghép', 100, N'Bộ', N'/Images/default.png', 550000, N'LSP005'),
(N'SP029', N'Bộ xây dựng kỹ thuật', N'Dụng cụ xây dựng kỹ thuật nhỏ', 60, N'Bộ', N'/Images/default.png', 1500000, N'LSP009'),
(N'SP030', N'Xe lửa đồ chơi', N'Xe lửa chạy pin cho trẻ', 90, N'Hộp', N'/Images/default.png', 1200000, N'LSP004');
GO

INSERT INTO KhachHang (MaKH, HoTen, GioiTinh, Email, SoDienThoai, DiaChi) VALUES
(N'KH001', N'Nguyễn Văn A', 1, N'nguyenvana@gmail.com', N'0901234567', N'123 Đường ABC, Quận 1, TP.HCM'),
(N'KH002', N'Trần Thị B', 0, N'tranthib@gmail.com', N'0902345678', N'456 Đường DEF, Quận 2, TP.HCM'),
(N'KH003', N'Lê Minh C', 1, N'leminhc@gmail.com', N'0903456789', N'789 Đường GHI, Quận 3, TP.HCM'),
(N'KH004', N'Phan Ngọc D', 0, N'phanngocd@gmail.com', N'0904567890', N'321 Đường JKL, Quận 4, TP.HCM');
GO

INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, SoDienThoai, Email, MatKhau, QuyenSuDung) VALUES
(N'NV001', N'Nguyễn Văn E', 1, N'0905678901', N'nguyenve@gmail.com', N'123', 1),
(N'NV002', N'Hoàng Thị F', 0, N'0906789012', N'hoangtf@gmail.com', N'123', 2),
(N'NV003', N'Phạm Minh G', 1, N'0907890123', N'phammg@gmail.com', N'123', 1),
(N'NV004', N'Vũ Ngọc H', 1, N'0908901234', N'vungh@gmail.com', N'123', 0);
GO

INSERT INTO GioHang (SoHD, NgayDatHang, MaKH, MaNVGiaoHang, TongTien, LoaiThanhToan, TinhTrang) VALUES
(N'HD001', N'2023-12-26 10:00:00', N'KH001', N'NV001', 100000, 0, 0),
(N'HD002', N'2024-12-25 09:00:00', N'KH002', N'NV002', 200000, 1, 1),
(N'HD003', N'2024-12-24 11:00:00', N'KH003', N'NV003', 300000, 1, 2),
(N'HD004', N'2024-12-23 13:00:00', N'KH004', N'NV004', 400000, 0, 1);
GO

INSERT INTO ChiTietGioHang (SoHD, MaSP, SoLuong, DonGiaBan) VALUES
(N'HD001', N'SP001', 1, 500000),
(N'HD002', N'SP002', 1, 150000),
(N'HD003', N'SP003', 1, 1200000),
(N'HD004', N'SP004', 1, 700000);
GO

CREATE PROCEDURE SanPham_TimKiem
    @MaSP NVARCHAR(10) = NULL,
    @TenSP NVARCHAR(100) = NULL,
    @MaLoaiSP NVARCHAR(10) = NULL,
    @DonGia DECIMAL(18, 2) = NULL,
    @SoLuong INT = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000)
    SELECT @SqlStr = '
        SELECT * 
        FROM SanPham
        WHERE (1=1)
    '
    
    IF @MaSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (MaSP LIKE ''%' + @MaSP + '%'')
        '
    IF @TenSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (TenSP LIKE ''%' + @TenSP + '%'')
        '
    IF @MaLoaiSP IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (MaLoaiSP LIKE ''%' + @MaLoaiSP + '%'')
        '
    IF @DonGia IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (DonGia LIKE ''%' + @DonGia + '%'')
        '
    IF @SoLuong IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (SoLuong LIKE ''%' + @SoLuong + '%'')
        '
    
    EXEC SP_EXECUTESQL @SqlStr
END
GO

CREATE PROCEDURE NhanVien_TimKiem
    @MaNV NVARCHAR(10) = NULL,
    @HoTen NVARCHAR(100) = NULL,
    @GioiTinh NVARCHAR(3) = NULL,
    @SoDienThoai NVARCHAR(15) = NULL,
    @Email NVARCHAR(100) = NULL,
	@QuyenSuDung NVARCHAR(50) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000)
    SELECT @SqlStr = '
        SELECT * 
        FROM NhanVien
        WHERE (1=1)
    '
    
    IF @MaNV IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (MaNV LIKE ''%' + @MaNV + '%'')
        '
    IF @HoTen IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (HoTen LIKE ''%' + @HoTen + '%'')
        '
	IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (GioiTinh LIKE ''%' + @GioiTinh + '%'')
             '
    IF @SoDienThoai IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (SoDienThoai LIKE ''%' + @SoDienThoai + '%'')
        '
    IF @Email IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (Email LIKE ''%' + @Email + '%'')
        '
    IF @QuyenSuDung IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (QuyenSuDung LIKE ''%' + @QuyenSuDung + '%'')
             '
    EXEC SP_EXECUTESQL @SqlStr
END
GO

CREATE PROCEDURE KhachHang_TimKiem
    @MaKH NVARCHAR(10) = NULL,
    @HoTen NVARCHAR(100) = NULL,
	@GioiTinh NVARCHAR(3) = NULL,
    @Email NVARCHAR(100) = NULL,
    @SoDienThoai NVARCHAR(15) = NULL,
    @DiaChi NVARCHAR(255) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000)
    SELECT @SqlStr = '
        SELECT * 
        FROM KhachHang
        WHERE (1=1)
    '
    
    IF @MaKH IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (MaKH LIKE ''%' + @MaKH + '%'')
        '
    IF @HoTen IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (HoTen LIKE ''%' + @HoTen + '%'')
        '
	IF @GioiTinh IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
             AND (GioiTinh LIKE ''%' + @GioiTinh + '%'')
             '
    IF @Email IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (Email LIKE ''%' + @Email + '%'')
        '
    IF @SoDienThoai IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (SoDienThoai LIKE ''%' + @SoDienThoai + '%'')
        '
    IF @DiaChi IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (DiaChi LIKE ''%' + @DiaChi + '%'')
        '
    
    EXEC SP_EXECUTESQL @SqlStr
END
GO

CREATE PROCEDURE GioHang_TimKiem
    @SoHD NVARCHAR(10) = NULL,
    @MaKH NVARCHAR(10) = NULL,
    @TinhTrang TINYINT = NULL,
    @NgayDatHangFrom DATETIME = NULL,
    @NgayDatHangTo DATETIME = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000)
    SELECT @SqlStr = '
        SELECT * 
        FROM GioHang
        WHERE (1=1)
    '
    
    IF @SoHD IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (SoHD LIKE ''%' + @SoHD + '%'')
        '
    IF @MaKH IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (MaKH LIKE ''%' + @MaKH + '%'')
        '
    IF @TinhTrang IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (TinhTrang = ' + CAST(@TinhTrang AS NVARCHAR) + ')
        '
    IF @NgayDatHangFrom IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (NgayDatHang >= ''' + CAST(@NgayDatHangFrom AS NVARCHAR) + ''')
        '
    IF @NgayDatHangTo IS NOT NULL
        SELECT @SqlStr = @SqlStr + '
            AND (NgayDatHang <= ''' + CAST(@NgayDatHangTo AS NVARCHAR) + ''')
        '
    
    EXEC SP_EXECUTESQL @SqlStr
END