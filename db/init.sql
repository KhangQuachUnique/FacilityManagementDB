-- Chú thích: Phần tạo database. Kiểm tra nếu database chưa tồn tại trước khi tạo.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FacilityManagementDB')
BEGIN
    CREATE DATABASE FacilityManagementDB;
END
GO

-- Chú thích: Sử dụng database mới tạo.
USE FacilityManagementDB;
GO

-- Chú thích: Phần tạo bảng roles. Bảng lưu trữ thông tin vai trò của nhân viên.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE roles (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(50) NOT NULL,
        description NVARCHAR(255),
        created_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT UQ_roles_name UNIQUE (name)
    );
END
GO

-- Chú thích: Phần tạo bảng employees. Bảng lưu trữ thông tin nhân viên với cấu trúc mới.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[employees]') AND type in (N'U'))
BEGIN
    CREATE TABLE employees (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(100) NOT NULL,
        phone NVARCHAR(20),
        email NVARCHAR(100) UNIQUE,
        address NVARCHAR(255) NULL,
        birth_date DATE NULL,
        role_id INT NOT NULL,
        hire_date DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
        salary DECIMAL(12,2) CHECK (salary >= 0),
        is_active BIT DEFAULT 1,
        created_at DATETIME DEFAULT GETDATE(),
        updated_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_employees_role_id FOREIGN KEY (role_id) REFERENCES roles(id),
        CONSTRAINT CHK_employees_email CHECK (email LIKE '%@%.%')
    );
END
GO

-- Chú thích: Trigger cập nhật updated_at trong bảng employees
CREATE OR ALTER TRIGGER trg_update_employees_updated_at
ON employees
AFTER UPDATE
AS
BEGIN
    UPDATE employees
    SET updated_at = GETDATE()
    FROM employees e
    INNER JOIN inserted i ON e.id = i.id;
END
GO

-- Chú thích: Phần tạo bảng areas. Bảng lưu trữ thông tin các khu vực.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[areas]') AND type in (N'U'))
BEGIN
    CREATE TABLE areas (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(100) NOT NULL,
        description NVARCHAR(255),
        created_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT UQ_areas_name UNIQUE (name)
    );
END
GO

-- Chú thích: Phần tạo bảng equipment. Bảng lưu trữ thông tin thiết bị.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[equipment]') AND type in (N'U'))
BEGIN
    CREATE TABLE equipment (
        id INT IDENTITY(1,1) PRIMARY KEY,
        area_id INT NOT NULL,
        name NVARCHAR(100) NOT NULL,
        type NVARCHAR(50) NOT NULL,
        purchase_date DATE NOT NULL,
        status NVARCHAR(20) NOT NULL CHECK (status IN ('Operational', 'Under Maintenance', 'Out of Service')),
        created_at DATETIME DEFAULT GETDATE(),
        updated_at DATETIME,
        CONSTRAINT FK_equipment_areas FOREIGN KEY (area_id) REFERENCES areas(id),
        CONSTRAINT CHK_purchase_date CHECK (purchase_date <= GETDATE() AND purchase_date >= '2000-01-01')
    );
END
GO

-- Chú thích: Phần tạo bảng equipment_maintenance. Bảng lưu trữ lịch sử bảo trì.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[equipment_maintenance]') AND type in (N'U'))
BEGIN
    CREATE TABLE equipment_maintenance (
        id INT IDENTITY(1,1) PRIMARY KEY,
        equipment_id INT NOT NULL,
        employee_id INT NOT NULL,
        maintenance_date DATE NOT NULL,
        description NVARCHAR(255),
        cost DECIMAL(10,2) DEFAULT 0,
        maintenance_type NVARCHAR(20) NOT NULL CHECK (maintenance_type IN ('Routine', 'Repair', 'Inspection')),
        status NVARCHAR(20) NOT NULL CHECK (status IN ('Completed', 'Pending', 'In Progress')),
        next_maintenance_date DATE,
        created_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_maintenance_equipment FOREIGN KEY (equipment_id) REFERENCES equipment(id) ON DELETE CASCADE,
        CONSTRAINT FK_maintenance_employees FOREIGN KEY (employee_id) REFERENCES employees(id),
        CONSTRAINT CHK_maintenance_date CHECK (maintenance_date <= GETDATE() AND maintenance_date >= '2000-01-01'),
        CONSTRAINT CHK_next_maintenance_date CHECK (next_maintenance_date >= maintenance_date OR next_maintenance_date IS NULL)
    );
END
GO

-- Chú thích: Phần tạo bảng maintenance_schedules. Bảng lưu trữ lịch bảo trì định kỳ.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[maintenance_schedules]') AND type in (N'U'))
BEGIN
    CREATE TABLE maintenance_schedules (
        id INT IDENTITY(1,1) PRIMARY KEY,
        equipment_id INT NOT NULL,
        schedule_type NVARCHAR(20) NOT NULL CHECK (schedule_type IN ('Daily', 'Weekly', 'Monthly', 'Yearly')),
        start_date DATE NOT NULL,
        interval_days INT NOT NULL CHECK (interval_days > 0),
        description NVARCHAR(255),
        created_at DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_schedules_equipment FOREIGN KEY (equipment_id) REFERENCES equipment(id) ON DELETE CASCADE,
        CONSTRAINT CHK_start_date CHECK (start_date >= '2000-01-01')
    );
END
GO

-- Chú thích: Trigger cập nhật updated_at trong bảng equipment
CREATE OR ALTER TRIGGER trg_update_equipment_updated_at
ON equipment
AFTER UPDATE
AS
BEGIN
    UPDATE equipment
    SET updated_at = GETDATE()
    FROM equipment e
    INNER JOIN inserted i ON e.id = i.id;
END
GO

-- Chú thích: Trigger kiểm tra và tạo bản ghi bảo trì từ maintenance_schedules khi INSERT/UPDATE
CREATE OR ALTER TRIGGER trg_auto_create_maintenance
ON maintenance_schedules
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @equipment_id INT, @start_date DATE, @interval_days INT, @description NVARCHAR(255);
    DECLARE @employee_id INT;

    -- Lấy thông tin từ bản ghi vừa thêm/cập nhật
    SELECT @equipment_id = equipment_id, @start_date = start_date, @interval_days = interval_days, @description = description
    FROM inserted;

    -- Kiểm tra thiết bị tồn tại và ở trạng thái hợp lệ
    IF EXISTS (SELECT 1 FROM equipment WHERE id = @equipment_id AND status IN ('Operational', 'Under Maintenance'))
    BEGIN
        DECLARE @next_maintenance_date DATE = DATEADD(DAY, @interval_days, @start_date);
        
        -- Kiểm tra xem đã có bản ghi bảo trì nào cho chu kỳ này chưa
        IF NOT EXISTS (
            SELECT 1 FROM equipment_maintenance 
            WHERE equipment_id = @equipment_id 
            AND maintenance_date >= @start_date 
            AND maintenance_date <= @next_maintenance_date
        )
        BEGIN
            -- Chọn nhân viên Active ngẫu nhiên
            SELECT TOP 1 @employee_id = id 
            FROM employees 
            WHERE is_active = 1 
            ORDER BY NEWID();

            IF @employee_id IS NOT NULL AND @next_maintenance_date <= GETDATE()
            BEGIN
                INSERT INTO equipment_maintenance (equipment_id, employee_id, maintenance_date, description, cost, maintenance_type, status, next_maintenance_date)
                VALUES (@equipment_id, @employee_id, GETDATE(), @description, 0, 'Routine', 'Pending', DATEADD(DAY, @interval_days, GETDATE()));
            END
        END
    END
END
GO

-- Chú thích: Stored Procedure kiểm tra lịch trình và tạo bản ghi bảo trì định kỳ
CREATE OR ALTER PROCEDURE sp_check_and_create_maintenance
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @equipment_id INT, @start_date DATE, @interval_days INT, @description NVARCHAR(255);
    DECLARE @employee_id INT, @next_maintenance_date DATE;

    -- Cursor để duyệt qua các lịch trình
    DECLARE schedule_cursor CURSOR FOR
    SELECT equipment_id, start_date, interval_days, description
    FROM maintenance_schedules
    WHERE DATEADD(DAY, interval_days, start_date) <= GETDATE();

    OPEN schedule_cursor;
    FETCH NEXT FROM schedule_cursor INTO @equipment_id, @start_date, @interval_days, @description;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Kiểm tra thiết bị tồn tại và ở trạng thái hợp lệ
        IF EXISTS (SELECT 1 FROM equipment WHERE id = @equipment_id AND status IN ('Operational', 'Under Maintenance'))
        BEGIN
            SET @next_maintenance_date = DATEADD(DAY, @interval_days, @start_date);

            -- Kiểm tra xem đã có bản ghi bảo trì nào cho chu kỳ này chưa
            IF NOT EXISTS (
                SELECT 1 FROM equipment_maintenance 
                WHERE equipment_id = @equipment_id 
                AND maintenance_date >= @start_date 
                AND maintenance_date <= @next_maintenance_date
            )
            BEGIN
                -- Chọn nhân viên Active ngẫu nhiên
                SELECT TOP 1 @employee_id = id 
                FROM employees 
                WHERE is_active = 1 
                ORDER BY NEWID();

                IF @employee_id IS NOT NULL
                BEGIN
                    -- Thêm bản ghi bảo trì
                    INSERT INTO equipment_maintenance (equipment_id, employee_id, maintenance_date, description, cost, maintenance_type, status, next_maintenance_date)
                    VALUES (@equipment_id, @employee_id, GETDATE(), @description, 0, 'Routine', 'Pending', DATEADD(DAY, @interval_days, GETDATE()));

                    -- Cập nhật start_date để chuyển sang chu kỳ tiếp theo
                    UPDATE maintenance_schedules
                    SET start_date = DATEADD(DAY, @interval_days, @start_date)
                    WHERE equipment_id = @equipment_id AND start_date = @start_date;
                END
            END
        END
        FETCH NEXT FROM schedule_cursor INTO @equipment_id, @start_date, @interval_days, @description;
    END

    CLOSE schedule_cursor;
    DEALLOCATE schedule_cursor;
END
GO

-- Chú thích: Phần tạo Functions
CREATE OR ALTER FUNCTION fn_equipment_total_cost (@equipment_id INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @total_cost DECIMAL(10,2);
    SELECT @total_cost = SUM(cost)
    FROM equipment_maintenance
    WHERE equipment_id = @equipment_id;
    RETURN ISNULL(@total_cost, 0);
END
GO

CREATE OR ALTER FUNCTION fn_equipment_status_count (@status NVARCHAR(20))
RETURNS INT
AS
BEGIN
    DECLARE @count INT;
    SELECT @count = COUNT(*)
    FROM equipment
    WHERE status = @status;
    RETURN @count;
END
GO

CREATE OR ALTER FUNCTION fn_area_equipment_count (@area_id INT)
RETURNS INT
AS
BEGIN
    DECLARE @count INT;
    SELECT @count = COUNT(*)
    FROM equipment
    WHERE area_id = @area_id;
    RETURN @count;
END
GO

CREATE OR ALTER FUNCTION fn_avg_maintenance_interval (@equipment_id INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @avg_days DECIMAL(10,2);
    SELECT @avg_days = AVG(DATEDIFF(DAY, LAG(maintenance_date) OVER (ORDER BY maintenance_date), maintenance_date) * 1.0)
    FROM equipment_maintenance
    WHERE equipment_id = @equipment_id;
    RETURN ISNULL(@avg_days, 0);
END
GO

-- Chú thích: Phần tạo Stored Procedures
CREATE OR ALTER PROCEDURE sp_add_equipment
    @area_id INT,
    @name NVARCHAR(100),
    @type NVARCHAR(50),
    @purchase_date DATE,
    @status NVARCHAR(20)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM equipment WHERE area_id = @area_id AND name = @name)
    BEGIN
        RAISERROR ('Tên thiết bị đã tồn tại trong khu vực này.', 16, 1);
        RETURN;
    END
    INSERT INTO equipment (area_id, name, type, purchase_date, status)
    VALUES (@area_id, @name, @type, @purchase_date, @status);
END
GO

CREATE OR ALTER PROCEDURE sp_update_equipment_status
    @equipment_id INT,
    @new_status NVARCHAR(20)
AS
BEGIN
    IF @new_status NOT IN ('Operational', 'Under Maintenance', 'Out of Service')
    BEGIN
        RAISERROR ('Trạng thái không hợp lệ.', 16, 1);
        RETURN;
    END
    UPDATE equipment
    SET status = @new_status
    WHERE id = @equipment_id;
END
GO

CREATE OR ALTER PROCEDURE sp_schedule_maintenance
    @equipment_id INT,
    @employee_id INT,
    @maintenance_date DATE,
    @description NVARCHAR(255),
    @cost DECIMAL(10,2),
    @maintenance_type NVARCHAR(20),
    @status NVARCHAR(20),
    @next_maintenance_date DATE
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM equipment WHERE id = @equipment_id)
    BEGIN
        RAISERROR ('Thiết bị không tồn tại.', 16, 1);
        RETURN;
    END
    IF NOT EXISTS (SELECT 1 FROM employees WHERE id = @employee_id AND is_active = 1)
    BEGIN
        RAISERROR ('Nhân viên không tồn tại hoặc không hoạt động.', 16, 1);
        RETURN;
    END
    INSERT INTO equipment_maintenance (equipment_id, employee_id, maintenance_date, description, cost, maintenance_type, status, next_maintenance_date)
    VALUES (@equipment_id, @employee_id, @maintenance_date, @description, @cost, @maintenance_type, @status, @next_maintenance_date);
END
GO

CREATE OR ALTER PROCEDURE sp_get_equipment_history
    @equipment_id INT
AS
BEGIN
    SELECT * FROM equipment_maintenance
    WHERE equipment_id = @equipment_id
    ORDER BY maintenance_date DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_add_maintenance_schedule
    @equipment_id INT,
    @schedule_type NVARCHAR(20),
    @start_date DATE,
    @interval_days INT,
    @description NVARCHAR(255)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM equipment WHERE id = @equipment_id)
    BEGIN
        RAISERROR ('Thiết bị không tồn tại.', 16, 1);
        RETURN;
    END
    IF @schedule_type NOT IN ('Daily', 'Weekly', 'Monthly', 'Yearly')
    BEGIN
        RAISERROR ('Loại lịch không hợp lệ.', 16, 1);
        RETURN;
    END
    INSERT INTO maintenance_schedules (equipment_id, schedule_type, start_date, interval_days, description)
    VALUES (@equipment_id, @schedule_type, @start_date, @interval_days, @description);
END
GO

CREATE OR ALTER PROCEDURE sp_facility_report
AS
BEGIN
    SELECT 
        a.name AS AreaName,
        COUNT(e.id) AS EquipmentCount,
        SUM(dbo.fn_equipment_total_cost(e.id)) AS TotalMaintenanceCost,
        dbo.fn_area_equipment_count(a.id) AS AreaEquipmentCount
    FROM areas a
    LEFT JOIN equipment e ON a.id = e.area_id
    GROUP BY a.name;
END
GO

CREATE OR ALTER PROCEDURE sp_overdue_maintenance
AS
BEGIN
    SELECT 
        e.name AS EquipmentName,
        a.name AS AreaName,
        em.next_maintenance_date AS OverdueDate,
        DATEDIFF(DAY, em.next_maintenance_date, GETDATE()) AS DaysOverdue
    FROM equipment_maintenance em
    JOIN equipment e ON em.equipment_id = e.id
    JOIN areas a ON e.area_id = a.id
    WHERE em.next_maintenance_date < GETDATE() AND em.status != 'Completed'
    ORDER BY em.next_maintenance_date;
END
GO

-- Chú thích: View hiển thị thông tin thiết bị với khu vực và tổng chi phí bảo trì
CREATE OR ALTER VIEW vw_equipment_details
AS
SELECT 
    e.id AS EquipmentID,
    e.name AS EquipmentName,
    a.name AS AreaName,
    e.type AS EquipmentType,
    e.status AS EquipmentStatus,
    dbo.fn_equipment_total_cost(e.id) AS TotalMaintenanceCost
FROM equipment e
JOIN areas a ON e.area_id = a.id;
GO

-- Chú thích: Transaction mẫu để thêm bản ghi bảo trì
BEGIN TRANSACTION;
BEGIN TRY
    DECLARE @equipment_id INT = 1;
    DECLARE @employee_id INT = 1;
    DECLARE @maintenance_date DATE = GETDATE();
    DECLARE @description NVARCHAR(255) = 'Bảo trì mẫu';
    DECLARE @cost DECIMAL(10,2) = 100.00;
    DECLARE @maintenance_type NVARCHAR(20) = 'Routine';
    DECLARE @status NVARCHAR(20) = 'Pending';
    DECLARE @next_maintenance_date DATE = DATEADD(MONTH, 1, GETDATE());

    IF NOT EXISTS (SELECT 1 FROM equipment WHERE id = @equipment_id)
    BEGIN
        RAISERROR ('Thiết bị không tồn tại.', 16, 1);
    END
    IF NOT EXISTS (SELECT 1 FROM employees WHERE id = @employee_id AND is_active = 1)
    BEGIN
        RAISERROR ('Nhân viên không tồn tại hoặc không hoạt động.', 16, 1);
    END

    INSERT INTO equipment_maintenance (equipment_id, employee_id, maintenance_date, description, cost, maintenance_type, status, next_maintenance_date)
    VALUES (@equipment_id, @employee_id, @maintenance_date, @description, @cost, @maintenance_type, @status, @next_maintenance_date);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    PRINT @ErrorMessage;
END CATCH
GO