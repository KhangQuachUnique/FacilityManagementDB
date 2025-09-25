-- Tệp: CreateDatabase.sql
-- Mô tả: Tạo cơ sở dữ liệu QuanLyCoSoVatChatDB và các đối tượng liên quan
-- Hướng dẫn: Chạy tập lệnh này trong SQL Server Management Studio

-- Tạo Cơ Sở Dữ Liệu
CREATE DATABASE QuanLyCoSoVatChatDB;
GO

USE QuanLyCoSoVatChatDB;
GO

-- Tạo Các Bảng

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
    TrangThai NVARCHAR(50) NOT NULL CHECK ( TrangThai IN (N'Hoạt Động', N'Đang Bảo Trì', N'Hỏng', N'Ngừng Hoạt Động')),
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
    MoTa NVARCHAR(MAX),
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chưa Hoàn Thành' CHECK ( TrangThai IN (N'Chưa Hoàn Thành', N'Hoàn Thành', N'Quá Hạn'))
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

-- Thủ Tục Lưu Trữ (Stored Procedures)

-- Khu Vực
CREATE PROCEDURE sp_ThemKhuVuc
    @TenKhuVuc NVARCHAR(100)
AS
BEGIN
    INSERT INTO KhuVuc (TenKhuVuc) VALUES (@TenKhuVuc);
END;
GO

CREATE PROCEDURE sp_CapNhatKhuVuc
    @MaKhuVuc INT,
    @TenKhuVuc NVARCHAR(100)
AS
BEGIN
    UPDATE KhuVuc SET TenKhuVuc = @TenKhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END;
GO

CREATE PROCEDURE sp_XoaKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    DELETE FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END;
GO

CREATE PROCEDURE sp_LayTatCaKhuVuc
AS
BEGIN
    SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc;
END;
GO

CREATE PROCEDURE sp_LayKhuVucTheoID
    @MaKhuVuc INT
AS
BEGIN
    SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc;
END;
GO

-- Nhân Viên
CREATE PROCEDURE sp_ThemNhanVien
    @Ten NVARCHAR(100),
    @ChucVu NVARCHAR(50),
    @MaKhuVuc INT
AS
BEGIN
    INSERT INTO NhanVien (Ten, ChucVu, MaKhuVuc) VALUES (@Ten, @ChucVu, @MaKhuVuc);
END;
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
END;
GO

CREATE PROCEDURE sp_XoaNhanVien
    @MaNhanVien INT
AS
BEGIN
    DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien;
END;
GO

CREATE PROCEDURE sp_LayTatCaNhanVien
AS
BEGIN
    SELECT e.MaNhanVien, e.Ten, e.ChucVu, a.TenKhuVuc 
    FROM NhanVien e 
    LEFT JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc;
END;
GO

CREATE PROCEDURE sp_LayNhanVienTheoID
    @MaNhanVien INT
AS
BEGIN
    SELECT e.MaNhanVien, e.Ten, e.ChucVu, a.TenKhuVuc 
    FROM NhanVien e 
    LEFT JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE e.MaNhanVien = @MaNhanVien;
END;
GO

-- Loại Cơ Sở Vật Chất
CREATE PROCEDURE sp_ThemLoaiCoSoVatChat
    @TenLoai NVARCHAR(100)
AS
BEGIN
    INSERT INTO LoaiCoSoVatChat (TenLoai) VALUES (@TenLoai);
END;
GO

CREATE PROCEDURE sp_CapNhatLoaiCoSoVatChat
    @MaLoai INT,
    @TenLoai NVARCHAR(100)
AS
BEGIN
    UPDATE LoaiCoSoVatChat SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai;
END;
GO

CREATE PROCEDURE sp_XoaLoaiCoSoVatChat
    @MaLoai INT
AS
BEGIN
    DELETE FROM LoaiCoSoVatChat WHERE MaLoai = @MaLoai;
END;
GO

CREATE PROCEDURE sp_LayTatCaLoaiCoSoVatChat
AS
BEGIN
    SELECT MaLoai, TenLoai FROM LoaiCoSoVatChat;
END;
GO

CREATE PROCEDURE sp_LayLoaiCoSoVatChatTheoID
    @MaLoai INT
AS
BEGIN
    SELECT MaLoai, TenLoai FROM LoaiCoSoVatChat WHERE MaLoai = @MaLoai;
END;
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
END;
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
    UPDATE CoSoVatChat 
    SET Ten = @Ten, MaLoai = @MaLoai, MaKhuVuc = @MaKhuVuc, TrangThai = @TrangThai, Gia = @Gia 
    WHERE MaCoSoVatChat = @MaCoSoVatChat;
END;
GO

CREATE PROCEDURE sp_XoaCoSoVatChat
    @MaCoSoVatChat INT
AS
BEGIN
    DELETE FROM CoSoVatChat WHERE MaCoSoVatChat = @MaCoSoVatChat;
END;
GO

CREATE PROCEDURE sp_LayTatCaCoSoVatChat
AS
BEGIN
    SELECT e.MaCoSoVatChat, e.Ten, e.MaLoai, t.TenLoai, e.MaKhuVuc, a.TenKhuVuc, e.TrangThai, e.Gia 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc;
END;
GO

CREATE PROCEDURE sp_LayCoSoVatChatTheoBoLoc
    @MaKhuVuc INT = NULL,
    @MaLoai INT = NULL,
    @TrangThai NVARCHAR(50) = NULL
AS
BEGIN
    SELECT e.MaCoSoVatChat, e.Ten, e.MaLoai, t.TenLoai, e.MaKhuVuc, a.TenKhuVuc, e.TrangThai, e.Gia 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE (@MaKhuVuc IS NULL OR e.MaKhuVuc = @MaKhuVuc)
    AND (@MaLoai IS NULL OR e.MaLoai = @MaLoai)
    AND (@TrangThai IS NULL OR e.TrangThai = @TrangThai);
END;
GO

CREATE PROCEDURE sp_LayCoSoVatChatTheoID
    @MaCoSoVatChat INT
AS
BEGIN
    SELECT e.MaCoSoVatChat, e.Ten, e.MaLoai, t.TenLoai, e.MaKhuVuc, a.TenKhuVuc, e.TrangThai, e.Gia 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE e.MaCoSoVatChat = @MaCoSoVatChat;
END;
GO

-- Bảo Trì Cơ Sở Vật Chất
CREATE PROCEDURE sp_ThemBaoTri
    @MaCoSoVatChat INT,
    @MaNhanVien INT,
    @NgayBaoTri DATE,
    @ChiPhi DECIMAL(18,2),
    @MoTa NVARCHAR(MAX),
    @TrangThai NVARCHAR(50) = N'Chưa Hoàn Thành'
AS
BEGIN
    INSERT INTO BaoTriCoSoVatChat (MaCoSoVatChat, MaNhanVien, NgayBaoTri, ChiPhi, MoTa, TrangThai) 
    VALUES (@MaCoSoVatChat, @MaNhanVien, @NgayBaoTri, @ChiPhi, @MoTa, @TrangThai);
END;
GO

CREATE PROCEDURE sp_CapNhatBaoTri
    @MaBaoTri INT,
    @MaCoSoVatChat INT,
    @MaNhanVien INT,
    @NgayBaoTri DATE,
    @ChiPhi DECIMAL(18,2),
    @MoTa NVARCHAR(MAX),
    @TrangThai NVARCHAR(50)
AS
BEGIN
    UPDATE BaoTriCoSoVatChat 
    SET MaCoSoVatChat = @MaCoSoVatChat, MaNhanVien = @MaNhanVien, NgayBaoTri = @NgayBaoTri, 
        ChiPhi = @ChiPhi, MoTa = @MoTa, TrangThai = @TrangThai 
    WHERE MaBaoTri = @MaBaoTri;
END;
GO

CREATE PROCEDURE sp_XoaBaoTri
    @MaBaoTri INT
AS
BEGIN
    DELETE FROM BaoTriCoSoVatChat WHERE MaBaoTri = @MaBaoTri;
END;
GO

CREATE PROCEDURE sp_LayTatCaBaoTri
AS
BEGIN
    SELECT m.MaBaoTri, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien, 
           m.NgayBaoTri, m.ChiPhi, m.MoTa, m.TrangThai 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat 
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien;
END;
GO

CREATE PROCEDURE sp_LayBaoTriTheoBoLoc
    @MaCoSoVatChat INT = NULL,
    @MaNhanVien INT = NULL,
    @NgayBatDau DATE = NULL,
    @NgayKetThuc DATE = NULL,
    @TrangThai NVARCHAR(50) = NULL
AS
BEGIN
    SELECT m.MaBaoTri, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien, 
           m.NgayBaoTri, m.ChiPhi, m.MoTa, m.TrangThai 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat 
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien 
    WHERE (@MaCoSoVatChat IS NULL OR m.MaCoSoVatChat = @MaCoSoVatChat)
    AND (@MaNhanVien IS NULL OR m.MaNhanVien = @MaNhanVien)
    AND (@NgayBatDau IS NULL OR m.NgayBaoTri >= @NgayBatDau)
    AND (@NgayKetThuc IS NULL OR m.NgayBaoTri <= @NgayKetThuc)
    AND (@TrangThai IS NULL OR m.TrangThai = @TrangThai);
END;
GO

CREATE PROCEDURE sp_LayBaoTriTheoID
    @MaBaoTri INT
AS
BEGIN
    SELECT m.MaBaoTri, m.MaCoSoVatChat, m.MaNhanVien,
           e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien, 
           m.NgayBaoTri, m.ChiPhi, m.MoTa, m.TrangThai 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat 
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien 
    WHERE m.MaBaoTri = @MaBaoTri;
END;
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
END;
GO

CREATE PROCEDURE sp_BaoCaoGiaTriCoSoVatChatTheoKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    SELECT a.TenKhuVuc, SUM(e.Gia) AS TongGiaTri 
    FROM CoSoVatChat e 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE e.MaKhuVuc = @MaKhuVuc 
    GROUP BY a.TenKhuVuc;
END;
GO

CREATE PROCEDURE sp_BaoCaoGiaTriCoSoVatChatTheoLoai
    @MaLoai INT
AS
BEGIN
    SELECT t.TenLoai, SUM(e.Gia) AS TongGiaTri 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    WHERE e.MaLoai = @MaLoai 
    GROUP BY t.TenLoai;
END;
GO

-- Hàm
CREATE FUNCTION fn_TinhTongChiPhiBaoTri (@MaCoSoVatChat INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Tong DECIMAL(18,2);
    SELECT @Tong = SUM(ChiPhi) FROM BaoTriCoSoVatChat WHERE MaCoSoVatChat = @MaCoSoVatChat;
    RETURN ISNULL(@Tong, 0);
END;
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
        SELECT i.MaCoSoVatChat, i.TrangThai, GETDATE(), 1 
        FROM inserted i 
        INNER JOIN deleted d ON i.MaCoSoVatChat = d.MaCoSoVatChat 
        WHERE i.TrangThai <> d.TrangThai;
    END;
END;
GO

CREATE TRIGGER trg_TuDongCapNhatTrangThaiBaoTri
ON BaoTriCoSoVatChat
AFTER INSERT
AS
BEGIN
    UPDATE CoSoVatChat 
    SET TrangThai = N'Đang Bảo Trì'
    WHERE MaCoSoVatChat IN (SELECT DISTINCT MaCoSoVatChat FROM inserted)
    AND TrangThai != N'Đang Bảo Trì';
END;
GO

CREATE TRIGGER trg_HoanThanhBaoTri
ON BaoTriCoSoVatChat
AFTER UPDATE
AS
BEGIN
    IF UPDATE(TrangThai)
    BEGIN
        UPDATE CoSoVatChat 
        SET TrangThai = N'Hoạt Động'
        WHERE MaCoSoVatChat IN (
            SELECT DISTINCT i.MaCoSoVatChat 
            FROM inserted i 
            INNER JOIN deleted d ON i.MaBaoTri = d.MaBaoTri 
            WHERE i.TrangThai = N'Hoàn Thành' AND d.TrangThai = N'Chưa Hoàn Thành'
        );
    END;
END;
GO

-- Tìm Kiếm
CREATE PROCEDURE sp_TimKiemNhanVienTheoTen
    @TenNhanVien NVARCHAR(255)
AS
BEGIN
    SELECT nv.MaNhanVien, nv.Ten, nv.ChucVu, kv.TenKhuVuc
    FROM NhanVien nv
    LEFT JOIN KhuVuc kv ON nv.MaKhuVuc = kv.MaKhuVuc
    WHERE nv.Ten LIKE N'%' + @TenNhanVien + N'%'
    ORDER BY nv.Ten;
END;
GO

CREATE PROCEDURE sp_TimKiemKhuVucTheoTen
    @TenKhuVuc NVARCHAR(255)
AS
BEGIN
    SELECT MaKhuVuc, TenKhuVuc
    FROM KhuVuc
    WHERE TenKhuVuc LIKE N'%' + @TenKhuVuc + N'%'
    ORDER BY TenKhuVuc;
END;
GO

CREATE PROCEDURE sp_TimKiemLoaiCoSoVatChatTheoTen
    @TenLoai NVARCHAR(255)
AS
BEGIN
    SELECT MaLoai, TenLoai
    FROM LoaiCoSoVatChat
    WHERE TenLoai LIKE N'%' + @TenLoai + N'%'
    ORDER BY TenLoai;
END;
GO

CREATE PROCEDURE sp_TimKiemCoSoVatChatTheoTen
    @TenCoSoVatChat NVARCHAR(255)
AS
BEGIN
    SELECT csvc.MaCoSoVatChat, csvc.Ten, csvc.MaLoai, lcsvc.TenLoai, csvc.MaKhuVuc, kv.TenKhuVuc, csvc.TrangThai, csvc.Gia
    FROM CoSoVatChat csvc
    JOIN LoaiCoSoVatChat lcsvc ON csvc.MaLoai = lcsvc.MaLoai
    JOIN KhuVuc kv ON csvc.MaKhuVuc = kv.MaKhuVuc
    WHERE csvc.Ten LIKE N'%' + @TenCoSoVatChat + N'%'
    ORDER BY csvc.Ten;
END;
GO

CREATE PROCEDURE sp_TimKiemBaoTriTheoTenCoSoVatChat
    @TenCoSoVatChat NVARCHAR(255)
AS
BEGIN
    SELECT bt.MaBaoTri, csvc.Ten AS TenCoSoVatChat, nv.Ten AS TenNhanVien, 
           bt.NgayBaoTri, bt.ChiPhi, bt.MoTa, bt.TrangThai
    FROM BaoTriCoSoVatChat bt
    JOIN CoSoVatChat csvc ON bt.MaCoSoVatChat = csvc.MaCoSoVatChat
    JOIN NhanVien nv ON bt.MaNhanVien = nv.MaNhanVien
    WHERE csvc.Ten LIKE N'%' + @TenCoSoVatChat + N'%'
    ORDER BY bt.NgayBaoTri DESC;
END;
GO

CREATE PROCEDURE sp_TimKiemBaoTriTheoTenNhanVien
    @TenNhanVien NVARCHAR(255)
AS
BEGIN
    SELECT bt.MaBaoTri, csvc.Ten AS TenCoSoVatChat, nv.Ten AS TenNhanVien, 
           bt.NgayBaoTri, bt.ChiPhi, bt.MoTa, bt.TrangThai
    FROM BaoTriCoSoVatChat bt
    JOIN CoSoVatChat csvc ON bt.MaCoSoVatChat = csvc.MaCoSoVatChat
    JOIN NhanVien nv ON bt.MaNhanVien = nv.MaNhanVien
    WHERE nv.Ten LIKE N'%' + @TenNhanVien + N'%'
    ORDER BY bt.NgayBaoTri DESC;
END;
GO

CREATE PROCEDURE sp_LayCoSoVatChatBiHong
AS
BEGIN
    SELECT e.MaCoSoVatChat, e.Ten, e.MaLoai, t.TenLoai, e.MaKhuVuc, a.TenKhuVuc, e.TrangThai, e.Gia 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE e.TrangThai = N'Hỏng'
    ORDER BY e.Ten;
END;
GO

CREATE PROCEDURE sp_HoanThanhBaoTri
    @MaBaoTri INT
AS
BEGIN
    UPDATE BaoTriCoSoVatChat 
    SET TrangThai = N'Hoàn Thành' 
    WHERE MaBaoTri = @MaBaoTri AND TrangThai = N'Chưa Hoàn Thành';
END;
GO

CREATE PROCEDURE sp_LayBaoTriChuaHoanThanh
AS
BEGIN
    SELECT m.MaBaoTri, e.Ten AS TenCoSoVatChat, emp.Ten AS TenNhanVien, 
           m.NgayBaoTri, m.ChiPhi, m.MoTa, m.TrangThai 
    FROM BaoTriCoSoVatChat m 
    JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat 
    JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien 
    WHERE m.TrangThai = N'Chưa Hoàn Thành' 
    ORDER BY m.NgayBaoTri DESC;
END;
GO

CREATE PROCEDURE sp_LayThietBiDangBaoTri
AS
BEGIN
    SELECT e.MaCoSoVatChat, e.Ten, t.TenLoai, a.TenKhuVuc, e.TrangThai, e.Gia 
    FROM CoSoVatChat e 
    JOIN LoaiCoSoVatChat t ON e.MaLoai = t.MaLoai 
    JOIN KhuVuc a ON e.MaKhuVuc = a.MaKhuVuc 
    WHERE e.TrangThai = N'Đang Bảo Trì' 
    ORDER BY e.Ten;
END;
GO

-- Quản lý phân quyền ở mức server

-- 1. Tạo các login cho người dùng
CREATE LOGIN AdminLogin WITH PASSWORD = 'StrongPassword123!';
CREATE LOGIN ThuNganLogin WITH PASSWORD = 'StrongPassword456!';
CREATE LOGIN KyThuatVienLogin WITH PASSWORD = 'StrongPassword789!';
GO

-- 2. Tạo các user trong cơ sở dữ liệu và ánh xạ đến login
USE QuanLyCoSoVatChatDB;
GO

CREATE USER AdminUser FOR LOGIN AdminLogin;
CREATE USER ThuNganUser FOR LOGIN ThuNganLogin;
CREATE USER KyThuatVienUser FOR LOGIN KyThuatVienLogin;
GO

-- 3. Tạo các database roles
CREATE ROLE QuanLyCuaHang;
CREATE ROLE ThuNgan;
CREATE ROLE NhanVienKyThuat;
GO

-- 4. Gán user vào các role
EXEC sp_addrolemember 'QuanLyCuaHang', 'AdminUser';
EXEC sp_addrolemember 'ThuNgan', 'ThuNganUser';
EXEC sp_addrolemember 'NhanVienKyThuat', 'KyThuatVienUser';
GO

-- 5. Phân quyền cho các role

-- Quyền cho Quản Lý Cửa Hàng (toàn quyền trên tất cả bảng và stored procedures)
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO QuanLyCuaHang;
GRANT EXECUTE ON SCHEMA::dbo TO QuanLyCuaHang;
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO NhanVienKyThuat;
GRANT EXECUTE ON SCHEMA::dbo TO NhanVienKyThuat;
GO

-- Thu Ngân KHÔNG được quyền truy cập ứng dụng quản lý cơ sở vật chất
-- Chỉ có QuanLyCuaHang và NhanVienKyThuat mới được phép đăng nhập vào ứng dụng
-- Thu Ngân login sẽ bị từ chối ngay từ đầu
GO

-- Chèn dữ liệu mẫu

-- Chèn dữ liệu vào bảng KhuVuc
INSERT INTO KhuVuc (TenKhuVuc)
VALUES 
    (N'Quầy Bán Hàng'),
    (N'Kho Sách'),
    (N'Khu Vực Đọc Sách'),
    (N'Văn Phòng');
GO

-- Chèn dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (Ten, ChucVu, MaKhuVuc)
VALUES 
    (N'Nguyễn Thị Mai', N'Quản Lý Cửa Hàng', 4), -- Văn Phòng
    (N'Trần Văn Hùng', N'Thu Ngân', 1), -- Quầy Bán Hàng
    (N'Lê Minh Tuấn', N'Nhân Viên Kỹ Thuật', 2); -- Kho Sách
GO

-- Chèn dữ liệu vào bảng LoaiCoSoVatChat
INSERT INTO LoaiCoSoVatChat (TenLoai)
VALUES 
    (N'Kệ Sách'),
    (N'Máy Tính'),
    (N'Máy In Hóa Đơn'),
    (N'Đèn Chiếu Sáng'),
    (N'Bàn Ghế');
GO

-- Chèn dữ liệu vào bảng CoSoVatChat
INSERT INTO CoSoVatChat (Ten, MaLoai, MaKhuVuc, TrangThai, Gia)
VALUES 
    (N'Kệ Sách Gỗ Cao Cấp', 1, 3, N'Hoạt Động', 2500000.00), -- Khu Vực Đọc Sách
    (N'Máy Tính Quầy Thu Ngân', 2, 1, N'Hoạt Động', 12000000.00), -- Quầy Bán Hàng
    (N'Máy In Hóa Đơn EPSON', 3, 1, N'Đang Bảo Trì', 3500000.00), -- Quầy Bán Hàng
    (N'Đèn LED Khu Đọc Sách', 4, 3, N'Hoạt Động', 500000.00), -- Khu Vực Đọc Sách
    (N'Bàn Gỗ Văn Phòng', 5, 4, N'Hỏng', 1500000.00), -- Văn Phòng
    (N'Kệ Sách Kim Loại', 1, 2, N'Hoạt Động', 1800000.00); -- Kho Sách
GO

-- Chèn dữ liệu vào bảng BaoTriCoSoVatChat
INSERT INTO BaoTriCoSoVatChat (MaCoSoVatChat, MaNhanVien, NgayBaoTri, ChiPhi, MoTa, TrangThai)
VALUES 
    (3, 3, '2025-09-20', 300000.00, N'Sửa lỗi máy in không in được hóa đơn', N'Chưa Hoàn Thành'), -- Máy In Hóa Đơn
    (5, 3, '2025-09-18', 200000.00, N'Thay chân bàn bị gãy', N'Hoàn Thành'), -- Bàn Gỗ Văn Phòng
    (1, 3, '2025-09-15', 150000.00, N'Sơn lại kệ sách gỗ', N'Hoàn Thành'); -- Kệ Sách Gỗ
GO

-- Chèn dữ liệu vào bảng NhatKyTrangThaiCoSoVatChat
INSERT INTO NhatKyTrangThaiCoSoVatChat (MaCoSoVatChat, TrangThai, NgayThayDoi, NguoiThayDoi)
VALUES 
    (3, N'Đang Bảo Trì', '2025-09-20 10:00:00', 3), -- Máy In Hóa Đơn
    (5, N'Hỏng', '2025-09-17 14:30:00', 3), -- Bàn Gỗ Văn Phòng
    (5, N'Hoạt Động', '2025-09-18 16:00:00', 3), -- Bàn Gỗ sau khi sửa
    (1, N'Hoạt Động', '2025-09-15 15:00:00', 3); -- Kệ Sách Gỗ
GO