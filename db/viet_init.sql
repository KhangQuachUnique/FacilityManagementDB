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

-- Chỉ lấy nhân viên kỹ thuật (dùng cho tạo công việc bảo trì)
CREATE PROCEDURE sp_LayNhanVienKyThuat
AS
BEGIN
    SELECT e.MaNhanVien, e.Ten, e.ChucVu, e.MaKhuVuc
    FROM NhanVien e
    WHERE e.ChucVu = N'Nhân Viên Kỹ Thuật';
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

-- Lấy danh sách loại theo khu vực (phục vụ combobox cascading)
CREATE PROCEDURE sp_LayLoaiTheoKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    SELECT DISTINCT l.MaLoai, l.TenLoai
    FROM CoSoVatChat c
    INNER JOIN LoaiCoSoVatChat l ON c.MaLoai = l.MaLoai
    WHERE c.MaKhuVuc = @MaKhuVuc
    ORDER BY l.TenLoai;
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

-- Lọc bảo trì theo khu vực, loại (mở rộng), nhân viên và khoảng ngày
CREATE PROCEDURE sp_LayBaoTriTheoBoLoc_MoRong
        @MaKhuVuc INT = NULL,
        @MaLoai INT = NULL,
        @MaNhanVien INT = NULL,
        @NgayBatDau DATE = NULL,
        @NgayKetThuc DATE = NULL,
        @TrangThai NVARCHAR(50) = NULL
AS
BEGIN
        SELECT m.MaBaoTri,
                     e.Ten AS TenCoSoVatChat,
                     emp.Ten AS TenNhanVien,
                     m.NgayBaoTri,
                     m.ChiPhi,
                     m.MoTa,
                     m.TrangThai
        FROM BaoTriCoSoVatChat m
        JOIN CoSoVatChat e ON m.MaCoSoVatChat = e.MaCoSoVatChat
        JOIN NhanVien emp ON m.MaNhanVien = emp.MaNhanVien
        WHERE (@MaKhuVuc IS NULL OR e.MaKhuVuc = @MaKhuVuc)
            AND (@MaLoai IS NULL OR e.MaLoai = @MaLoai)
            AND (@MaNhanVien IS NULL OR m.MaNhanVien = @MaNhanVien)
            AND (@NgayBatDau IS NULL OR m.NgayBaoTri >= @NgayBatDau)
            AND (@NgayKetThuc IS NULL OR m.NgayBaoTri <= @NgayKetThuc)
            AND (@TrangThai IS NULL OR m.TrangThai = @TrangThai)
        ORDER BY m.NgayBaoTri DESC, m.MaBaoTri DESC;
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

-- =======================================================
-- STORED PROCEDURES CHO BÁO CÁO
-- =======================================================

-- 1. Báo cáo chi phí bảo trì theo tháng (chi tiết)
CREATE PROCEDURE sp_BaoCaoChiPhiBaoTriTheoThang
    @Thang INT,
    @Nam INT
AS
BEGIN
    SELECT 
        c.Ten AS TenCoSoVatChat,
        kv.TenKhuVuc,
        bt.NgayBaoTri,
        bt.ChiPhi,
        nv.Ten AS TenNhanVien,
        bt.MoTa,
        bt.TrangThai
    FROM BaoTriCoSoVatChat bt
    INNER JOIN CoSoVatChat c ON bt.MaCoSoVatChat = c.MaCoSoVatChat
    INNER JOIN KhuVuc kv ON c.MaKhuVuc = kv.MaKhuVuc
    INNER JOIN NhanVien nv ON bt.MaNhanVien = nv.MaNhanVien
    WHERE MONTH(bt.NgayBaoTri) = @Thang 
      AND YEAR(bt.NgayBaoTri) = @Nam
    ORDER BY bt.NgayBaoTri DESC, bt.ChiPhi DESC;
END;
GO

-- 2. Báo cáo thiết bị theo trạng thái
CREATE PROCEDURE sp_BaoCaoThietBiTheoTrangThai
AS
BEGIN
    SELECT 
        TrangThai,
        COUNT(*) AS SoLuong
    FROM CoSoVatChat
    GROUP BY TrangThai
    ORDER BY TrangThai;
END;
GO

-- 3. Báo cáo giá trị tài sản theo khu vực (chi tiết từng thiết bị)
CREATE PROCEDURE sp_BaoCaoGiaTriTaiSanTheoKhuVuc
    @MaKhuVuc INT
AS
BEGIN
    SELECT 
        c.Ten AS TenCoSoVatChat,
        l.TenLoai,
        c.TrangThai,
        c.Gia
    FROM CoSoVatChat c
    INNER JOIN LoaiCoSoVatChat l ON c.MaLoai = l.MaLoai
    WHERE c.MaKhuVuc = @MaKhuVuc
    ORDER BY c.Gia DESC, c.Ten;
END;
GO

-- 4. Báo cáo thiết bị cần bảo trì
CREATE PROCEDURE sp_BaoCaoThietBiCanBaoTri
AS  
BEGIN
    SELECT 
        c.Ten AS TenCoSoVatChat,
        kv.TenKhuVuc,
        l.TenLoai,
        c.TrangThai,
        c.Gia,
        -- Lấy ngày bảo trì gần nhất (nếu có)
        (SELECT TOP 1 bt1.NgayBaoTri 
         FROM BaoTriCoSoVatChat bt1 
         WHERE bt1.MaCoSoVatChat = c.MaCoSoVatChat 
         ORDER BY bt1.NgayBaoTri DESC) AS NgayBaoTri,
        -- Lấy trạng thái bảo trì gần nhất (nếu có)
        (SELECT TOP 1 bt2.TrangThai 
         FROM BaoTriCoSoVatChat bt2 
         WHERE bt2.MaCoSoVatChat = c.MaCoSoVatChat 
         ORDER BY bt2.NgayBaoTri DESC) AS TrangThaiBaoTri,
        -- Thêm cột cho ORDER BY
        CASE c.TrangThai 
            WHEN N'Hỏng' THEN 1 
            WHEN N'Đang Bảo Trì' THEN 2 
            ELSE 3 
        END AS ThuTuUuTien
    FROM CoSoVatChat c
    INNER JOIN KhuVuc kv ON c.MaKhuVuc = kv.MaKhuVuc
    INNER JOIN LoaiCoSoVatChat l ON c.MaLoai = l.MaLoai
    WHERE c.TrangThai IN (N'Hỏng', N'Đang Bảo Trì')
       OR EXISTS (
           SELECT 1 FROM BaoTriCoSoVatChat bt3 
           WHERE bt3.MaCoSoVatChat = c.MaCoSoVatChat 
             AND bt3.TrangThai = N'Chưa Hoàn Thành'
       )
    ORDER BY ThuTuUuTien, c.Gia DESC;
END;
GO

-- 5. Thống kê tổng quan hệ thống
CREATE PROCEDURE sp_ThongKeTongQuan
AS
BEGIN
    SELECT 
        'Thiết Bị' AS LoaiThongKe,
        COUNT(*) AS TongSo,
        SUM(Gia) AS TongGiaTri
    FROM CoSoVatChat
    
    UNION ALL
    
    SELECT 
        'Khu Vực' AS LoaiThongKe,
        COUNT(*) AS TongSo,
        0 AS TongGiaTri
    FROM KhuVuc
    
    UNION ALL
    
    SELECT 
        'Nhân Viên' AS LoaiThongKe,
        COUNT(*) AS TongSo,
        0 AS TongGiaTri
    FROM NhanVien
    
    UNION ALL
    
    SELECT 
        'Bảo Trì Tháng Này' AS LoaiThongKe,
        COUNT(*) AS TongSo,
        SUM(ChiPhi) AS TongGiaTri
    FROM BaoTriCoSoVatChat
    WHERE MONTH(NgayBaoTri) = MONTH(GETDATE()) 
      AND YEAR(NgayBaoTri) = YEAR(GETDATE());
END;
GO

-- 6. Top thiết bị chi phí bảo trì cao nhất
CREATE PROCEDURE sp_TopThietBiChiPhiBaoTriCao
    @SoLuong INT = 10
AS
BEGIN
    SELECT TOP (@SoLuong)
        c.Ten AS TenCoSoVatChat,
        kv.TenKhuVuc,
        l.TenLoai,
        c.TrangThai,
        SUM(bt.ChiPhi) AS TongChiPhiBaoTri,
        COUNT(bt.MaBaoTri) AS SoLanBaoTri,
        AVG(bt.ChiPhi) AS ChiPhiTrungBinh,
        MAX(bt.NgayBaoTri) AS LanBaoTriGanNhat
    FROM CoSoVatChat c
    INNER JOIN KhuVuc kv ON c.MaKhuVuc = kv.MaKhuVuc
    INNER JOIN LoaiCoSoVatChat l ON c.MaLoai = l.MaLoai
    INNER JOIN BaoTriCoSoVatChat bt ON c.MaCoSoVatChat = bt.MaCoSoVatChat
    GROUP BY c.MaCoSoVatChat, c.Ten, kv.TenKhuVuc, l.TenLoai, c.TrangThai
    ORDER BY TongChiPhiBaoTri DESC;
END;
GO

-- 7. Báo cáo thiết bị có tổng chi phí bảo trì vượt 50% giá trị (để cân nhắc thay thế)
CREATE PROCEDURE sp_BaoCaoThietBiChiPhiBaoTriVuot50PhanTram
AS
BEGIN
    SELECT 
        c.MaCoSoVatChat,
        c.Ten AS TenCoSoVatChat,
        kv.TenKhuVuc,
        l.TenLoai,
        c.TrangThai,
        c.Gia,
        SUM(bt.ChiPhi) AS TongChiPhiBaoTri,
        (SUM(bt.ChiPhi) / c.Gia) * 100 AS PhanTramChiPhi,
        COUNT(bt.MaBaoTri) AS SoLanBaoTri,
        MAX(bt.NgayBaoTri) AS LanBaoTriGanNhat
    FROM CoSoVatChat c
    INNER JOIN KhuVuc kv ON c.MaKhuVuc = kv.MaKhuVuc
    INNER JOIN LoaiCoSoVatChat l ON c.MaLoai = l.MaLoai
    INNER JOIN BaoTriCoSoVatChat bt ON c.MaCoSoVatChat = bt.MaCoSoVatChat
    GROUP BY c.MaCoSoVatChat, c.Ten, kv.TenKhuVuc, l.TenLoai, c.TrangThai, c.Gia
    HAVING SUM(bt.ChiPhi) > 0.5 * c.Gia
    ORDER BY (SUM(bt.ChiPhi) / c.Gia) DESC;
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

-- Chèn dữ liệu mẫu mở rộng

-- 1. Chèn dữ liệu vào bảng KhuVuc
-- Các khu vực trong cửa hàng bán sách offline
INSERT INTO KhuVuc (TenKhuVuc)
VALUES 
    (N'Quầy Bán Hàng'),
    (N'Kho Sách'),
    (N'Khu Vực Đọc Sách'),
    (N'Văn Phòng'),
    (N'Quầy Trưng Bày Sách Mới'),
    (N'Khu Sách Thiếu Nhi'),
    (N'Khu Sách Văn Học'),
    (N'Khu Sách Kỹ Năng'),
    (N'Khu Cafe Sách'),
    (N'Khu Sự Kiện');
GO

-- 2. Chèn dữ liệu vào bảng NhanVien
-- Thêm nhiều nhân viên với các vai trò khác nhau trong cửa hàng
INSERT INTO NhanVien (Ten, ChucVu, MaKhuVuc)
VALUES 
    (N'Nguyễn Thị Mai', N'Quản Lý Cửa Hàng', 4), -- Văn Phòng
    (N'Trần Văn Hùng', N'Thu Ngân', 1), -- Quầy Bán Hàng
    (N'Lê Minh Tuấn', N'Nhân Viên Kỹ Thuật', 2), -- Kho Sách
    (N'Phạm Thị Lan', N'Thu Ngân', 1), -- Quầy Bán Hàng
    (N'Hoàng Văn Nam', N'Nhân Viên Kỹ Thuật', 2), -- Kho Sách
    (N'Nguyễn Văn An', N'Nhân Viên Bán Hàng', 5), -- Quầy Trưng Bày Sách Mới
    (N'Trần Thị Hoa', N'Nhân Viên Bán Hàng', 6), -- Khu Sách Thiếu Nhi
    (N'Lê Thị Hồng', N'Nhân Viên Bán Hàng', 7), -- Khu Sách Văn Học
    (N'Vũ Minh Đức', N'Nhân Viên Bán Hàng', 8), -- Khu Sách Kỹ Năng
    (N'Đỗ Thị Thảo', N'Nhân Viên Phục Vụ', 9), -- Khu Cafe Sách
    (N'Nguyễn Quang Huy', N'Nhân Viên Sự Kiện', 10), -- Khu Sự Kiện
    (N'Phan Thị Ngọc', N'Nhân Viên Bán Hàng', 6), -- Khu Sách Thiếu Nhi
    (N'Trần Văn Long', N'Nhân Viên Kho', 2), -- Kho Sách
    (N'Lê Thị Mai Anh', N'Nhân Viên Bán Hàng', 7), -- Khu Sách Văn Học
    (N'Nguyễn Thị Thu', N'Nhân Viên Phục Vụ', 9), -- Khu Cafe Sách
    (N'Võ Văn Tâm', N'Nhân Viên Kỹ Thuật', 2), -- Kho Sách
    (N'Bùi Thị Hương', N'Nhân Viên Bán Hàng', 8), -- Khu Sách Kỹ Năng
    (N'Đinh Văn Khải', N'Nhân Viên Sự Kiện', 10); -- Khu Sự Kiện
GO

-- 3. Chèn dữ liệu vào bảng LoaiCoSoVatChat
-- Các loại cơ sở vật chất trong cửa hàng bán sách
INSERT INTO LoaiCoSoVatChat (TenLoai)
VALUES 
    (N'Kệ Sách'),
    (N'Máy Tính'),
    (N'Máy In Hóa Đơn'),
    (N'Đèn Chiếu Sáng'),
    (N'Bàn Ghế'),
    (N'Máy Quét Mã Vạch'),
    (N'Máy Lạnh'),
    (N'Loa Âm Thanh'),
    (N'Màn Hình Hiển Thị'),
    (N'Tủ Kính Trưng Bày'),
    (N'Máy Pha Cà Phê'),
    (N'Quạt'),
    (N'Bảng Hiệu'),
    (N'Camera An Ninh'),
    (N'Máy Chiếu');
GO

-- 4. Chèn dữ liệu vào bảng CoSoVatChat
-- Thêm nhiều cơ sở vật chất ở các khu vực khác nhau
INSERT INTO CoSoVatChat (Ten, MaLoai, MaKhuVuc, TrangThai, Gia)
VALUES 
    -- Khu Vực Quầy Bán Hàng (MaKhuVuc = 1)
    (N'Máy Tính Quầy Thu Ngân 1', 2, 1, N'Hoạt Động', 12000000.00),
    (N'Máy Tính Quầy Thu Ngân 2', 2, 1, N'Hoạt Động', 12000000.00),
    (N'Máy In Hóa Đơn EPSON 1', 3, 1, N'Đang Bảo Trì', 3500000.00),
    (N'Máy In Hóa Đơn EPSON 2', 3, 1, N'Hoạt Động', 3500000.00),
    (N'Máy Quét Mã Vạch 1', 6, 1, N'Hoạt Động', 1500000.00),
    (N'Máy Quét Mã Vạch 2', 6, 1, N'Hỏng', 1500000.00),
    (N'Đèn LED Quầy Bán Hàng', 4, 1, N'Hoạt Động', 500000.00),
    
    -- Khu Vực Kho Sách (MaKhuVuc = 2)
    (N'Kệ Sách Kim Loại 1', 1, 2, N'Hoạt Động', 1800000.00),
    (N'Kệ Sách Kim Loại 2', 1, 2, N'Hoạt Động', 1800000.00),
    (N'Kệ Sách Kim Loại 3', 1, 2, N'Hỏng', 1800000.00),
    (N'Quạt Công Nghiệp Kho', 12, 2, N'Hoạt Động', 800000.00),
    (N'Camera An Ninh Kho', 14, 2, N'Hoạt Động', 2000000.00),
    
    -- Khu Vực Đọc Sách (MaKhuVuc = 3)
    (N'Kệ Sách Gỗ Cao Cấp 1', 1, 3, N'Hoạt Động', 2500000.00),
    (N'Kệ Sách Gỗ Cao Cấp 2', 1, 3, N'Hoạt Động', 2500000.00),
    (N'Bàn Ghế Đọc Sách 1', 5, 3, N'Hoạt Động', 1500000.00),
    (N'Bàn Ghế Đọc Sách 2', 5, 3, N'Hoạt Động', 1500000.00),
    (N'Đèn LED Khu Đọc Sách 1', 4, 3, N'Hoạt Động', 500000.00),
    (N'Đèn LED Khu Đọc Sách 2', 4, 3, N'Hỏng', 500000.00),
    
    -- Khu Vực Văn Phòng (MaKhuVuc = 4)
    (N'Bàn Gỗ Văn Phòng 1', 5, 4, N'Hỏng', 1500000.00),
    (N'Bàn Gỗ Văn Phòng 2', 5, 4, N'Hoạt Động', 1500000.00),
    (N'Máy Tính Văn Phòng 1', 2, 4, N'Hoạt Động', 10000000.00),
    (N'Máy Tính Văn Phòng 2', 2, 4, N'Đang Bảo Trì', 10000000.00),
    (N'Máy Lạnh Văn Phòng', 7, 4, N'Hoạt Động', 8000000.00),
    
    -- Quầy Trưng Bày Sách Mới (MaKhuVuc = 5)
    (N'Tủ Kính Trưng Bày 1', 10, 5, N'Hoạt Động', 3000000.00),
    (N'Tủ Kính Trưng Bày 2', 10, 5, N'Hoạt Động', 3000000.00),
    (N'Đèn LED Trưng Bày', 4, 5, N'Hoạt Động', 600000.00),
    
    -- Khu Sách Thiếu Nhi (MaKhuVuc = 6)
    (N'Kệ Sách Thiếu Nhi 1', 1, 6, N'Hoạt Động', 2000000.00),
    (N'Kệ Sách Thiếu Nhi 2', 1, 6, N'Hoạt Động', 2000000.00),
    (N'Bàn Ghế Thiếu Nhi', 5, 6, N'Hoạt Động', 1200000.00),
    
    -- Khu Sách Văn Học (MaKhuVuc = 7)
    (N'Kệ Sách Văn Học 1', 1, 7, N'Hoạt Động', 2200000.00),
    (N'Kệ Sách Văn Học 2', 1, 7, N'Hoạt Động', 2200000.00),
    (N'Đèn LED Văn Học', 4, 7, N'Hoạt Động', 500000.00),
    
    -- Khu Sách Kỹ Năng (MaKhuVuc = 8)
    (N'Kệ Sách Kỹ Năng 1', 1, 8, N'Hoạt Động', 2000000.00),
    (N'Kệ Sách Kỹ Năng 2', 1, 8, N'Hỏng', 2000000.00),
    
    -- Khu Cafe Sách (MaKhuVuc = 9)
    (N'Máy Pha Cà Phê 1', 11, 9, N'Hoạt Động', 15000000.00),
    (N'Máy Pha Cà Phê 2', 11, 9, N'Đang Bảo Trì', 15000000.00),
    (N'Bàn Ghế Cafe 1', 5, 9, N'Hoạt Động', 2000000.00),
    (N'Bàn Ghế Cafe 2', 5, 9, N'Hoạt Động', 2000000.00),
    (N'Máy Lạnh Cafe', 7, 9, N'Hoạt Động', 9000000.00),
    
    -- Khu Sự Kiện (MaKhuVuc = 10)
    (N'Máy Chiếu Sự Kiện', 15, 10, N'Hoạt Động', 12000000.00),
    (N'Loa Âm Thanh Sự Kiện', 8, 10, N'Hoạt Động', 3000000.00),
    (N'Bàn Ghế Sự Kiện 1', 5, 10, N'Hoạt Động', 1500000.00),
    (N'Bàn Ghế Sự Kiện 2', 5, 10, N'Hỏng', 1500000.00),
    (N'Bảng Hiệu Sự Kiện', 13, 10, N'Hoạt Động', 1000000.00);
GO

-- 5. Chèn dữ liệu vào bảng BaoTriCoSoVatChat
-- Thêm nhiều bản ghi bảo trì cho các thiết bị
INSERT INTO BaoTriCoSoVatChat (MaCoSoVatChat, MaNhanVien, NgayBaoTri, ChiPhi, MoTa, TrangThai)
VALUES 
    -- Bảo trì Máy In Hóa Đơn
    (3, 3, '2025-09-20', 300000.00, N'Sửa lỗi máy in không in được hóa đơn', N'Chưa Hoàn Thành'),
    (3, 3, '2025-08-15', 250000.00, N'Thay mực in cho máy in hóa đơn', N'Hoàn Thành'),
    (4, 3, '2025-09-10', 200000.00, N'Vệ sinh máy in hóa đơn', N'Hoàn Thành'),
    
    -- Bảo trì Bàn Gỗ Văn Phòng
    (19, 5, '2025-09-18', 200000.00, N'Thay chân bàn bị gãy', N'Hoàn Thành'),
    (19, 5, '2025-08-20', 150000.00, N'Sơn lại mặt bàn', N'Hoàn Thành'),
    
    -- Bảo trì Kệ Sách
    (13, 3, '2025-09-15', 150000.00, N'Sơn lại kệ sách gỗ', N'Hoàn Thành'),
    (10, 5, '2025-09-22', 300000.00, N'Sửa kệ sách kim loại bị cong', N'Chưa Hoàn Thành'),
    (28, 3, '2025-09-05', 120000.00, N'Vệ sinh kệ sách thiếu nhi', N'Hoàn Thành'),
    
    -- Bảo trì Máy Tính
    (22, 5, '2025-09-17', 500000.00, N'Cài đặt lại phần mềm máy tính văn phòng', N'Chưa Hoàn Thành'),
    (1, 3, '2025-09-01', 400000.00, N'Nâng cấp RAM máy tính quầy thu ngân', N'Hoàn Thành'),
    
    -- Bảo trì Máy Lạnh
    (23, 5, '2025-09-12', 600000.00, N'Vệ sinh và nạp gas máy lạnh văn phòng', N'Hoàn Thành'),
    (40, 3, '2025-09-19', 700000.00, N'Sửa máy lạnh khu cafe', N'Chưa Hoàn Thành'),
    
    -- Bảo trì Máy Quét Mã Vạch
    (6, 3, '2025-09-21', 250000.00, N'Sửa lỗi máy quét mã vạch không nhận diện', N'Chưa Hoàn Thành'),
    (6, 5, '2025-08-25', 200000.00, N'Thay pin máy quét mã vạch', N'Hoàn Thành'),
    
    -- Bảo trì Đèn LED
    (18, 5, '2025-09-14', 100000.00, N'Thay bóng đèn LED khu đọc sách', N'Hoàn Thành'),
    (26, 3, '2025-09-11', 120000.00, N'Thay đèn LED trưng bày', N'Hoàn Thành'),
    
    -- Bảo trì Máy Pha Cà Phê
    (36, 5, '2025-09-23', 800000.00, N'Sửa máy pha cà phê khu cafe', N'Chưa Hoàn Thành'),
    (36, 3, '2025-09-02', 500000.00, N'Vệ sinh máy pha cà phê', N'Hoàn Thành'),
    
    -- Bảo trì Máy Chiếu
    (41, 5, '2025-09-16', 600000.00, N'Sửa lỗi máy chiếu không hiển thị', N'Hoàn Thành'),
    (41, 3, '2025-08-30', 400000.00, N'Vệ sinh máy chiếu sự kiện', N'Hoàn Thành'),
    
    -- Bảo trì Loa Âm Thanh
    (42, 5, '2025-09-20', 300000.00, N'Sửa loa âm thanh sự kiện bị rè', N'Chưa Hoàn Thành'),
    (42, 3, '2025-09-05', 200000.00, N'Vệ sinh loa âm thanh', N'Hoàn Thành');
GO