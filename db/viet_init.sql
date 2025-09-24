-- Tệp: CreateDatabase.sql

-- Chạy tập lệnh này trong SQL Server Management Studio để tạo cơ sở dữ liệu và tất cả các đối tượng.

-- Tạo Cơ Sở Dữ Liệu
CREATE DATABASE QuanLyCoSoVatChatDB;
GO

USE QuanLyCoSoVatChatDB;
GO

-- Tạo Các Bảng

-- Bảng Vai Trò
CREATE TABLE VaiTro (
    MaVaiTro INT PRIMARY KEY IDENTITY(1,1),
    TenVaiTro NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Bảng Người Dùng
CREATE TABLE NguoiDung (
    MaNguoiDung INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash NVARCHAR(255) NOT NULL,
    MaVaiTro INT NOT NULL FOREIGN KEY REFERENCES VaiTro(MaVaiTro),
    HoatDong BIT NOT NULL DEFAULT 1
);
GO

-- Bảng Khu Vực
CREATE TABLE KhuVuc (
    MaKhuVuc INT PRIMARY KEY IDENTITY(1,1),
    TenKhuVuc NVARCHAR(100) NOT NULL
);
GO

-- Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNhanVien INT PRIMARY KEY IDENTITY(1,1),
    Ten NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50),
    MaKhuVuc INT FOREIGN KEY REFERENCES KhuVuc(MaKhuVuc)
);
GO

-- Bảng Loại Cơ Sở Vật Chất
CREATE TABLE LoaiCoSoVatChat (
    MaLoai INT PRIMARY KEY IDENTITY(1,1),
    TenLoai NVARCHAR(100) NOT NULL
);
GO

-- Bảng Cơ Sở Vật Chất
CREATE TABLE CoSoVatChat (
    MaCoSoVatChat INT PRIMARY KEY IDENTITY(1,1),
    Ten NVARCHAR(100) NOT NULL,
    MaLoai INT NOT NULL FOREIGN KEY REFERENCES LoaiCoSoVatChat(MaLoai),
    MaKhuVuc INT NOT NULL FOREIGN KEY REFERENCES KhuVuc(MaKhuVuc),
    TrangThai NVARCHAR(50) NOT NULL, -- Ví dụ: 'Hoạt Động', 'Đang Bảo Trì', 'Hỏng'
    Gia DECIMAL(18,2) NOT NULL
);
GO

-- Bảng Bảo Trì Cơ Sở Vật Chất
CREATE TABLE BaoTriCoSoVatChat (
    MaBaoTri INT PRIMARY KEY IDENTITY(1,1),
    MaCoSoVatChat INT NOT NULL FOREIGN KEY REFERENCES CoSoVatChat(MaCoSoVatChat),
    MaNhanVien INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien),
    NgayBaoTri DATE NOT NULL,
    ChiPhi DECIMAL(18,2) NOT NULL,
    MoTa NVARCHAR(MAX)
);
GO

-- Bảng Nhật Ký Trạng Thái Cơ Sở Vật Chất
CREATE TABLE NhatKyTrangThaiCoSoVatChat (
    MaNhatKy INT PRIMARY KEY IDENTITY(1,1),
    MaCoSoVatChat INT NOT NULL FOREIGN KEY REFERENCES CoSoVatChat(MaCoSoVatChat),
    TrangThai NVARCHAR(50) NOT NULL,
    NgayThayDoi DATETIME NOT NULL DEFAULT GETDATE(),
    NguoiThayDoi INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNhanVien)
);
GO

-- Thủ Tục Lưu Trữ cho CRUD

-- Vai Trò
CREATE PROCEDURE sp_ThemVaiTro
    @TenVaiTro NVARCHAR(50)
AS
BEGIN
    INSERT INTO VaiTro (TenVaiTro) VALUES (@TenVaiTro);
END
GO

CREATE PROCEDURE sp_CapNhatVaiTro
    @MaVaiTro INT,
    @TenVaiTro NVARCHAR(50)
AS
BEGIN
    UPDATE VaiTro SET TenVaiTro = @TenVaiTro WHERE MaVaiTro = @MaVaiTro;
END
GO

CREATE PROCEDURE sp_XoaVaiTro
    @MaVaiTro INT
AS
BEGIN
    DELETE FROM VaiTro WHERE MaVaiTro = @MaVaiTro;
END
GO

CREATE PROCEDURE sp_LayTatCaVaiTro
AS
BEGIN
    SELECT * FROM VaiTro;
END
GO

CREATE PROCEDURE sp_LayVaiTroTheoID
    @MaVaiTro INT
AS
BEGIN
    SELECT * FROM VaiTro WHERE MaVaiTro = @MaVaiTro;
END
GO

-- Người Dùng
CREATE PROCEDURE sp_ThemNguoiDung
    @TenDangNhap NVARCHAR(50),
    @MatKhauHash NVARCHAR(255),
    @MaVaiTro INT,
    @HoatDong BIT
AS
BEGIN
    INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, MaVaiTro, HoatDong) 
    VALUES (@TenDangNhap, @MatKhauHash, @MaVaiTro, @HoatDong);
END
GO

CREATE PROCEDURE sp_CapNhatNguoiDung
    @MaNguoiDung INT,
    @TenDangNhap NVARCHAR(50),
    @MatKhauHash NVARCHAR(255),
    @MaVaiTro INT,
    @HoatDong BIT
AS
BEGIN
    UPDATE NguoiDung SET TenDangNhap = @TenDangNhap, MatKhauHash = @MatKhauHash, 
    MaVaiTro = @MaVaiTro, HoatDong = @HoatDong WHERE MaNguoiDung = @MaNguoiDung;
END
GO

CREATE PROCEDURE sp_XoaNguoiDung
    @MaNguoiDung INT
AS
BEGIN
    DELETE FROM NguoiDung WHERE MaNguoiDung = @MaNguoiDung;
END
GO

CREATE PROCEDURE sp_LayTatCaNguoiDung
AS
BEGIN
    SELECT u.*, r.TenVaiTro FROM NguoiDung u JOIN VaiTro r ON u.MaVaiTro = r.MaVaiTro;
END
GO

CREATE PROCEDURE sp_LayNguoiDungTheoID
    @MaNguoiDung INT
AS
BEGIN
    SELECT u.*, r.TenVaiTro FROM NguoiDung u JOIN VaiTro r ON u.MaVaiTro = r.MaVaiTro 
    WHERE u.MaNguoiDung = @MaNguoiDung;
END
GO

CREATE PROCEDURE sp_LayNguoiDungTheoTenDangNhap
    @TenDangNhap NVARCHAR(50)
AS
BEGIN
    SELECT u.*, r.TenVaiTro FROM NguoiDung u JOIN VaiTro r ON u.MaVaiTro = r.MaVaiTro 
    WHERE u.TenDangNhap = @TenDangNhap;
END
GO

-- Khu Vực
CREATE PROCEDURE sp_ThemKhuVuc
    @TenKhuVuc NVARCHAR(100)
AS
BEGIN
    INSERT INTO KhuVuc (TenKhuVuc) VALUES (@TenKhuVuc);
END
GO

CREATE PROCEDURE sp_CapNhatKhuVuc
    @MaKhuVuc INT,
    @TenKhuVuc NVARCHAR(100)
AS
BEGIN
    UPDATE KhuVuc SET TenKhuVuc = @TenKhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END
GO

CREATE PROCEDURE sp_XoaKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    DELETE FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END
GO

CREATE PROCEDURE sp_LayTatCaKhuVuc
AS
BEGIN
    SELECT * FROM KhuVuc;
END
GO

CREATE PROCEDURE sp_LayKhuVucTheoID
    @MaKhuVuc INT
AS
BEGIN
    SELECT * FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END
GO

-- Nhân Viên
CREATE PROCEDURE sp_ThemNhanVien
    @Ten NVARCHAR(100),
    @ChucVu NVARCHAR(50),
    @MaKhuVuc INT
AS
BEGIN
    INSERT INTO NhanVien (Ten, ChucVu, MaKhuVuc) VALUES (@Ten, @ChucVu, @MaKhuVuc);
END
GO

CREATE PROCEDURE sp_CapNhatNhanVien
    @MaNhanVien INT,
    @Ten NVARCHAR(100),
    @ChucVu NVARCHAR(50),
    @MaKhuVuc INT
AS
BEGIN
    UPDATE NhanVien SET Ten = @Ten, ChucVu = @ChucVu, MaKhuVuc = @MaKhuVuc 
    WHERE MaNhanVien = @MaNhanVien;
END
GO

CREATE PROCEDURE sp_XoaNhanVien
    @MaNhanVien INT
AS
BEGIN
    DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien;
END
GO

CREATE PROCEDURE sp_LayTatCaNhanVien
AS
BEGIN
    SELECT e.*, a.TenKhuVuc FROM NhanVien e LEFT JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc;
END
GO

-- Loại Cơ Sở Vật Chất
CREATE PROCEDURE sp_ThemLoaiCoSoVatChat
    @TenLoai NVARCHAR(100)
AS
BEGIN
    INSERT INTO LoaiCoSoVatChat (TenLoai) VALUES (@TenLoai);
END
GO

CREATE PROCEDURE sp_CapNhatLoaiCoSoVatChat
    @MaLoai INT,
    @TenLoai NVARCHAR(100)
AS
BEGIN
    UPDATE LoaiCoSoVatChat SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai;
END
GO

CREATE PROCEDURE sp_XoaLoaiCoSoVatChat
    @MaLoai INT
AS
BEGIN
    DELETE FROM LoaiCoSoVatChat WHERE MaLoai = @MaLoai;
END
GO

CREATE PROCEDURE sp_LayTatCaLoaiCoSoVatChat
AS
BEGIN
    SELECT * FROM LoaiCoSoVatChat;
END
GO

CREATE PROCEDURE sp_LayLoaiCoSoVatChatTheoID
    @MaLoai INT
AS
BEGIN
    SELECT * FROM LoaiCoSoVatChat WHERE MaLoai = @MaLoai;
END
GO

-- Cơ Sở Vật Chất
CREATE PROCEDURE sp_ThemCoSoVatChat
    @Ten NVARCHAR(100),
    @MaLoai INT,
    @MaKhuVuc INT,
    @TrangThai NVARCHAR(50),
    @Gia DECIMAL(18,2)
AS
BEGIN
    INSERT INTO CoSoVatChat (Ten, MaLoai, MaKhuVuc, TrangThai, Gia) 
    VALUES (@Ten, @MaLoai, @MaKhuVuc, @TrangThai, @Gia);
END
GO

CREATE PROCEDURE sp_CapNhatCoSoVatChat
    @MaCoSoVatChat INT,
    @Ten NVARCHAR(100),
    @MaLoai INT,
    @MaKhuVuc INT,
    @TrangThai NVARCHAR(50),
    @Gia DECIMAL(18,2)
AS
BEGIN
    UPDATE CoSoVatChat SET Ten = @Ten, MaLoai = @MaLoai, MaKhuVuc = @MaKhuVuc, TrangThai = @TrangThai, 
    Gia = @Gia WHERE MaCoSoVatChat = @MaCoSoVatChat;
END
GO

CREATE PROCEDURE sp_XoaCoSoVatChat
    @MaCoSoVatChat INT
AS
BEGIN
    DELETE FROM CoSoVatChat WHERE MaCoSoVatChat = @MaCoSoVatChat;
END
GO

CREATE PROCEDURE sp_LayTatCaCoSoVatChat
AS
BEGIN
    SELECT e.*, t.TenLoai, a.TenKhuVuc FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc;
END
GO

CREATE PROCEDURE sp_LayCoSoVatChatTheoBoLoc
    @MaKhuVuc INT = NULL,
    @MaLoai INT = NULL,
    @TrangThai NVARCHAR(50) = NULL
AS
BEGIN
    SELECT e.*, t.TenLoai, a.TenKhuVuc FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc
    WHERE (@MaKhuVuc IS NULL OR e.MaKhuVuc = @MaKhuVuc)
    AND (@MaLoai IS NULL OR e.MaLoai = @MaLoai)
    AND (@TrangThai IS NULL OR e.TrangThai = @TrangThai);
END
GO

CREATE PROCEDURE sp_LayCoSoVatChatTheoID
    @MaCoSoVatChat INT
AS
BEGIN
    SELECT e.*, t.TenLoai, a.TenKhuVuc FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc
    WHERE e.MaCoSoVatChat = @MaCoSoVatChat;
END
GO

-- Bảo Trì Cơ Sở Vật Chất
CREATE PROCEDURE sp_ThemBaoTri
    @MaCoSoVatChat INT,
    @MaNhanVien INT,
    @NgayBaoTri DATE,
    @ChiPhi DECIMAL(18,2),
    @MoTa NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO BaoTriCoSoVatChat (MaCoSoVatChat, MaNhanVien, NgayBaoTri, ChiPhi, MoTa) 
    VALUES (@MaCoSoVatChat, @MaNhanVien, @NgayBaoTri, @ChiPhi, @MoTa);
END
GO

CREATE PROCEDURE sp_CapNhatBaoTri
    @MaBaoTri INT,
    @MaCoSoVatChat INT,
    @MaNhanVien INT,
    @NgayBaoTri DATE,
    @ChiPhi DECIMAL(18,2),
    @MoTa NVARCHAR(MAX)
AS
BEGIN
    UPDATE BaoTriCoSoVatChat SET MaCoSoVatChat = @MaCoSoVatChat, MaNhanVien = @MaNhanVien, 
    NgayBaoTri = @NgayBaoTri, ChiPhi = @ChiPhi, MoTa = @MoTa 
    WHERE MaBaoTri = @MaBaoTri;
END
GO

CREATE PROCEDURE sp_XoaBaoTri
    @MaBaoTri INT
AS
BEGIN
    DELETE FROM BaoTriCoSoVatChat WHERE MaBaoTri = @MaBaoTri;
END
GO

CREATE PROCEDURE sp_LayTatCaBaoTri
AS
BEGIN
    SELECT m.*, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien;
END
GO

CREATE PROCEDURE sp_LayBaoTriTheoBoLoc
    @MaCoSoVatChat INT = NULL,
    @MaNhanVien INT = NULL,
    @NgayBatDau DATE = NULL,
    @NgayKetThuc DATE = NULL
AS
BEGIN
    SELECT m.*, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien
    WHERE (@MaCoSoVatChat IS NULL OR m.MaCoSoVatChat = @MaCoSoVatChat)
    AND (@MaNhanVien IS NULL OR m.MaNhanVien = @MaNhanVien)
    AND (@NgayBatDau IS NULL OR m.NgayBaoTri >= @NgayBatDau)
    AND (@NgayKetThuc IS NULL OR m.NgayBaoTri <= @NgayKetThuc);
END
GO

CREATE PROCEDURE sp_LayBaoTriTheoID
    @MaBaoTri INT
AS
BEGIN
    SELECT m.*, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien
    WHERE m.MaBaoTri = @MaBaoTri;
END
GO

-- Báo Cáo
CREATE PROCEDURE sp_BaoCaoChiPhiBaoTriTheoThangNam
    @Thang INT,
    @Nam INT
AS
BEGIN
    SELECT SUM(ChiPhi) AS TongChiPhi 
    FROM BaoTriCoSoVatChat 
    WHERE MONTH(NgayBaoTri) = @Thang AND YEAR(NgayBaoTri) = @Nam;
END
GO

CREATE PROCEDURE sp_BaoCaoGiaTriCoSoVatChatTheoKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    SELECT SUM(Gia) AS TongGiaTri 
    FROM CoSoVatChat 
    WHERE MaKhuVuc = @MaKhuVuc;
END
GO

CREATE PROCEDURE sp_BaoCaoGiaTriCoSoVatChatTheoLoai
    @MaLoai INT
AS
BEGIN
    SELECT SUM(Gia) AS TongGiaTri 
    FROM CoSoVatChat 
    WHERE MaLoai = @MaLoai;
END
GO

-- Hàm
CREATE FUNCTION fn_TinhTongChiPhiBaoTri (@MaCoSoVatChat INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Tong DECIMAL(18,2);
    SELECT @Tong = SUM(ChiPhi) FROM BaoTriCoSoVatChat WHERE MaCoSoVatChat = @MaCoSoVatChat;
    RETURN ISNULL(@Tong, 0);
END
GO

-- Trigger
CREATE TRIGGER trg_ThayDoiTrangThaiCoSoVatChat
ON CoSoVatChat
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThai)
    BEGIN
        INSERT INTO NhatKyTrangThaiCoSoVatChat (MaCoSoVatChat, TrangThai, NgayThayDoi, NguoiThayDoi)
        SELECT i.MaCoSoVatChat, i.TrangThai, GETDATE(), 1 -- Giả sử NguoiThayDoi là 1 để demo, thay bằng thực tế
        FROM inserted i
        INNER JOIN deleted d ON i.MaCoSoVatChat = d.MaCoSoVatChat
        WHERE i.TrangThai <> d.TrangThai;
    END
END
GO

-- Chèn Dữ Liệu Mẫu để Demo
INSERT INTO VaiTro (TenVaiTro) VALUES ('Quản Trị'), ('Quản Lý'), ('Nhân Viên');
INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, MaVaiTro, HoatDong) 
VALUES ('admin', 'matkhauhash', 1, 1); -- Sử dụng hàm băm phù hợp trong mã
INSERT INTO KhuVuc (TenKhuVuc) VALUES ('Kho'), ('Sàn Bán Hàng'), ('Nhà Vệ Sinh');
INSERT INTO NhanVien (Ten, ChucVu, MaKhuVuc) VALUES ('Nguyễn Văn A', 'Quản Lý', 1);
INSERT INTO LoaiCoSoVatChat (TenLoai) VALUES ('Kệ'), ('Máy Tính'), ('Bồn Rửa'), ('Cửa');
INSERT INTO CoSoVatChat (Ten, MaLoai, MaKhuVuc, TrangThai, Gia) 
VALUES ('Kệ Sách', 1, 1, 'Hoạt Động', 100.00),
       ('Bồn Rửa Tay', 3, 3, 'Hoạt Động', 50.00),
       ('Cửa Chính', 4, 3, 'Hoạt Động', 200.00);
GO
