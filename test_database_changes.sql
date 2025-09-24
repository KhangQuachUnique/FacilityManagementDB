-- Test script to verify database changes
USE QuanLyCoSoVatChatDB;
GO

PRINT 'Testing database schema changes...'

-- Test 1: Verify TrangThai column exists in BaoTriCoSoVatChat with check constraints
PRINT 'Test 1: Checking BaoTriCoSoVatChat table structure...'
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'BaoTriCoSoVatChat' AND COLUMN_NAME = 'TrangThai';

-- Test 2: Verify check constraints on CoSoVatChat
PRINT 'Test 2: Checking CoSoVatChat check constraints...'
SELECT 
    cc.CONSTRAINT_NAME,
    cc.CHECK_CLAUSE
FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc
JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu ON cc.CONSTRAINT_NAME = cu.CONSTRAINT_NAME
WHERE cu.TABLE_NAME = 'CoSoVatChat';

-- Test 3: Verify check constraints on BaoTriCoSoVatChat
PRINT 'Test 3: Checking BaoTriCoSoVatChat check constraints...'
SELECT 
    cc.CONSTRAINT_NAME,
    cc.CHECK_CLAUSE
FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc
JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu ON cc.CONSTRAINT_NAME = cu.CONSTRAINT_NAME
WHERE cu.TABLE_NAME = 'BaoTriCoSoVatChat';

-- Test 4: Test inserting a maintenance record and verify trigger works
PRINT 'Test 4: Testing trigger functionality...'

-- First, check current status of equipment with ID 1
PRINT 'Current status of equipment ID 1:'
SELECT MaCoSoVatChat, Ten, TrangThai FROM CoSoVatChat WHERE MaCoSoVatChat = 1;

-- Insert a new maintenance record
INSERT INTO BaoTriCoSoVatChat (MaCoSoVatChat, MaNhanVien, NgayBaoTri, ChiPhi, MoTa, TrangThai)
VALUES (1, 1, GETDATE(), 100000, N'Test bảo trì trigger', N'Chưa Hoàn Thành');

-- Check if equipment status was automatically updated to 'Đang Bảo Trì'
PRINT 'Status after inserting maintenance record:'
SELECT MaCoSoVatChat, Ten, TrangThai FROM CoSoVatChat WHERE MaCoSoVatChat = 1;

-- Test 5: Test completion trigger
PRINT 'Test 5: Testing completion trigger...'

-- Get the ID of the maintenance record we just inserted
DECLARE @MaBaoTri INT;
SELECT TOP 1 @MaBaoTri = MaBaoTri FROM BaoTriCoSoVatChat WHERE MaCoSoVatChat = 1 ORDER BY MaBaoTri DESC;

-- Mark maintenance as completed
UPDATE BaoTriCoSoVatChat SET TrangThai = N'Hoàn Thành' WHERE MaBaoTri = @MaBaoTri;

-- Check if equipment status was automatically updated back to 'Hoạt Động'
PRINT 'Status after completing maintenance:'
SELECT MaCoSoVatChat, Ten, TrangThai FROM CoSoVatChat WHERE MaCoSoVatChat = 1;

-- Clean up test data
DELETE FROM BaoTriCoSoVatChat WHERE MaBaoTri = @MaBaoTri;

PRINT 'All tests completed successfully!'