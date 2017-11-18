CREATE DATABASE TuVanLaptop
GO
USE TuVanLaptop
GO 
--SELECT * FROM dbo.Luat WHERE SuKienVT='1' AND GiaiThich='test' AND SukienVP = 'mausac Like N'%Đen%'''
GO
--Tạo bảng nghề nghiệp
CREATE TABLE NgheNghiep(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(50)
)
GO
--Tạo bảng giới tính
CREATE TABLE GioiTinh(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(50) NOT NULL
)
GO
--Tạo bảng mục đích
CREATE TABLE MucDich(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(50)
)
GO
--Tạo bảng hệ điều hành
CREATE TABLE HeDieuHanh(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(200)
)
GO
--Tạo bảng yêu cầu giá tiền
CREATE TABLE YeuCauGiaTien(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(100)
)
GO 
--Tạo bảng sự kiện
CREATE TABLE SuKien(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(200)
)
GO

--Tạo bảng luật
--độ tin cậy ban đầu là 0(chưa đc kiểm chứng), khi người dùng mua độ tin cậy sẽ tăng
CREATE TABLE Luat(
Id INT PRIMARY KEY IDENTITY NOT NULL,
SuKienVT NVARCHAR(200),
SukienVP NVARCHAR(200) ,
GiaiThich NVARCHAR(200),
DoTinCay int DEFAULT 0
)
GO 

--default cho luật
ALTER TABLE dbo.Luat
ADD CONSTRAINT LuatConstraint
DEFAULT 0 FOR DoTinCay
GO 

--Tạo bảng laptop
CREATE TABLE Laptop(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Name NVARCHAR(50),
MoTa NVARCHAR(200),
AnhBia NVARCHAR(MAX),
NgayCapNhat DATETIME,

HangLaptopId INT,
HeDieuHanhId INT,

mausac NVARCHAR(50),
gia DECIMAL(18,0),
--Màn hình(inch):(giá trị: 10.6-12.1-13.3-14-14.1-15-15.4-17)
manhinh FLOAT,
--độ phân giải màn hình: 1 là HD, 0 là không HD
dophangiai BIT,
--cpu : giá trị core i3,i5,i7
cpu NVARCHAR(10),
--Ram:(GB): (giá trị: 2GB,4GB,8GB,16GB)
ram INT,
--Ổ cứng(GB): mặc định là HDD(128,256,512,1024)
ocung INT,
khoiluong FLOAT,
--pin tính theo giờ
pin FLOAT,
cardroi bit
)
GO
ALTER TABLE dbo.Laptop ADD	FOREIGN KEY(HangLaptopId) REFERENCES dbo.HangLapTop(Id)
ALTER TABLE dbo.Laptop ADD	FOREIGN KEY(HeDieuHanhId) REFERENCES dbo.HeDieuHanh(Id)
GO

--default cho laptop
ALTER TABLE dbo.Laptop
ADD CONSTRAINT LaptopConstraint
DEFAULT 'Chưa có mô tả' FOR MoTa
GO 


--thêm bảng khách hàng
CREATE TABLE KhachHang(
MaKH INT PRIMARY KEY IDENTITY NOT NULL,
HoTen NVARCHAR(50),
NgaySinh DATETIME,
DienThoai NVARCHAR(50),
TaiKhoan NVARCHAR(50),
MatKhau NVARCHAR(50),
Email NVARCHAR(50),
DiaChi NVARCHAR(50)

)
GO 

--Thêm bảng đơn hàng
CREATE TABLE DonHang( 
MaDonHang INT PRIMARY KEY IDENTITY NOT NULL,
NgayGiao DATETIME,
NgayDat DATETIME,
TinhTrangGiaoHang BIT,
MaKH INT
)
GO 


--thêm bảng chi tiết đơn hàng
CREATE TABLE ChiTietDonHang(
MaDonHang INT NOT NULL,
MaSanPham INT,
SoLuong INT,
DonGia DECIMAL(18,0),
CONSTRAINT [PK_ChiTietDonHang] PRIMARY KEY CLUSTERED 
(
[MaDonHang] ASC,
[MaSanPham] ASC
)
)
GO 

--Thêm
--bảng login
CREATE TABLE Admin(
 Id INT PRIMARY KEY IDENTITY NOT NULL,
 Username NVARCHAR(50),
 Password BINARY(20)
)
GO

--thêm dữ liệu bảng hệ điều hành
INSERT INTO dbo.HeDieuHanh
        ( Name )
VALUES  ( N'Windows'  -- Name - nvarchar(200) 
) 
GO 
INSERT INTO dbo.HeDieuHanh
        ( Name )
VALUES  ( N'Mac Os X'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.HeDieuHanh
        ( Name )
VALUES  ( N'Linux'  -- Name - nvarchar(200)
          )
GO 

--thêm dữ liệu bảng hãng sản xuất
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'Lenovo'  -- Name - nvarchar(50) 
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'Asus'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'Dell'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'HP'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'MacBook'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'LG'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'MSI'  -- Name - nvarchar(50)
)
GO 
INSERT INTO dbo.HangLapTop( Name )
VALUES  ( N'Acer'  -- Name - nvarchar(50)
)
GO 
--thêm dữ liệu bảng giới tính
INSERT INTO dbo.GioiTinh
        ( Name )
VALUES  ( N'Nam'  -- Name - nvarchar(50)
          )
GO 

INSERT INTO dbo.GioiTinh
        ( Name )
VALUES  ( N'Nữ'  -- Name - nvarchar(50)
          )
GO 

--thêm dữ liệu bảng nghề nghiệp
INSERT INTO dbo.NgheNghiep
        ( Name )
VALUES  ( N'Học sinh/sinh viên'  -- Name - nvarchar(50)
          )
		  INSERT INTO dbo.NgheNghiep
        ( Name )
VALUES  ( N'Công nhân/viên chức'  -- Name - nvarchar(50)
          )
		  INSERT INTO dbo.NgheNghiep
        ( Name )
VALUES  ( N'Nhân viên văn phòng'  -- Name - nvarchar(50)
          )
		  INSERT INTO dbo.NgheNghiep
        ( Name )
VALUES  ( N'Doanh nhân'  -- Name - nvarchar(50)
          )
		  GO


--thêm dữ liệu bảng mục đích sử dụng
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Lập trình'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Chơi game'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Giải trí'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Sử dụng Office'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Nghe nhạc'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Thiết kế chuyên nghiệp'  -- Name - nvarchar(50)
          )
GO 
INSERT INTO dbo.MucDich
        ( Name )
VALUES  ( N'Lướt web'  -- Name - nvarchar(50)
          )
GO 

--thêm dữ liệu bảng yêu cầu giá tiền
INSERT INTO dbo.YeuCauGiaTien
        ( Name )
VALUES  ( N'Trên 20 triệu'  -- Name - nvarchar(100)
          )
GO 
INSERT INTO dbo.YeuCauGiaTien
        ( Name )
VALUES  ( N'Từ 15 đến 20 triệu'  -- Name - nvarchar(100)
          )
GO 
INSERT INTO dbo.YeuCauGiaTien
        ( Name )
VALUES  ( N'Từ 10 đến 15 triệu'  -- Name - nvarchar(100)
          )
GO 
INSERT INTO dbo.YeuCauGiaTien
        ( Name )
VALUES  ( N'Dưới 10 triệu'  -- Name - nvarchar(100)
          )
GO 


--thêm dữ liệu bảng sự kiện
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Nam'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Nữ'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Học sinh/sinh viên'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Công nhân/viên chức'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Nhân viên văn phòng'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Doanh nhân'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Lập trình'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Chơi game'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Giải trí'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Sử dụng Office'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Nghe nhạc'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Thiết kế chuyên nghiệp'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Lướt web'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Trên 20 triệu'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Từ 15 đến 20 triệu'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Từ 10 đến 15 triệu'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Dưới 10 triệu'  -- Name - nvarchar(200)
          )
GO
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Lenovo'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Asus'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Dell'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'HP'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'MacBook'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'LG'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'MSI'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Acer'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Windows'  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Mac Os X'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'Linux'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'mausac Like N''%Hồng%'''  -- Name - nvarchar(200)
          )
GO 

INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'mausac Like N''%Hồng%'' and gia>=10000000 and gia<15000000'  -- Name - nvarchar(200)
          )
GO 
INSERT INTO dbo.SuKien
        ( Name )
VALUES  ( N'mausac Like N''%Đen%'' and gia>=10000000 and gia <15000000'  -- Name - nvarchar(200)
          )
GO 


--thêm dữ liệu bảng luật
INSERT INTO dbo.Luat
        ( SuKienVT ,SukienVP ,GiaiThich,DoTinCay
        )
VALUES  ( N'2' , -- SuKienVT - nvarchar(200)
          N'29' , -- SukienVP - nvarchar(200)
          N'Nữ nên chọn màu hồng' , -- GiaiThich - nvarchar(200)
          0  -- DoTinCay - int
        )
GO 
INSERT INTO dbo.Luat
        ( SuKienVT ,SukienVP ,GiaiThich,DoTinCay
        )
VALUES  ( N'2,16' , -- SuKienVT - nvarchar(200)
          N'30' , -- SukienVP - nvarchar(200)
          N'Nữ nên chọn màu hồng và giá từ 10 đến 15 triệu' , -- GiaiThich - nvarchar(200)
          0  -- DoTinCay - int
        )
GO 
INSERT INTO dbo.Luat
        ( SuKienVT ,SukienVP ,GiaiThich,DoTinCay
        )
VALUES  ( N'1,16' , -- SuKienVT - nvarchar(200)
          N'31' , -- SukienVP - nvarchar(200)
          N'Nam nên chọn màu đen và giá từ 10 đến 15 triệu' , -- GiaiThich - nvarchar(200)
          0  -- DoTinCay - int
        )
GO 
--thêm dữ liệu bảng laptop
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Lenovo IdeaPad 120S 11IAP N3350' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0001.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          1 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Hồng' , -- mausac - nvarchar(50)
          5190000 , -- gia - decimal
          11.6 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i5' , -- cpu - nvarchar(10)
          2 , -- ram - int
          256 , -- ocung - int
          1.27 , -- khoiluong - float
          4 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Asus E403NA N3350' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0002.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          2 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          5490000 , -- gia - decimal
          14 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i3' , -- cpu - nvarchar(10)
          4 , -- ram - int
          512 , -- ocung - int
          1.5 , -- khoiluong - float
          3 , -- pin - float
          0  -- cardroi - bit
        )
GO 

INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Acer Aspire ES1 432 C5J2 N3350 ' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0003.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          2 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          6390000 , -- gia - decimal
          14 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i3' , -- cpu - nvarchar(10)
          2 , -- ram - int
          512 , -- ocung - int
          1.92 , -- khoiluong - float
          5 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Asus X441NA N4200' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0004.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          2 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          7690000 , -- gia - decimal
          14 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i5' , -- cpu - nvarchar(10)
          4 , -- ram - int
          500 , -- ocung - int
          1.7 , -- khoiluong - float
          6 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Lenovo IdeaPad 110 15IBR N3710' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0005.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          1 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          6990000 , -- gia - decimal
          14 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i5' , -- cpu - nvarchar(10)
          4 , -- ram - int
          500 , -- ocung - int
          2.2 , -- khoiluong - float
          4 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop HP 15 bs571TU i3 6006U' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0006.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          4 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          10490000 , -- gia - decimal
          15.6 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i5' , -- cpu - nvarchar(10)
          4 , -- ram - int
          1024 , -- ocung - int
          1.86 , -- khoiluong - float
          4 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop HP 15 bs572TU i3 6006U' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0007.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          2 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Vàng đồng' , -- mausac - nvarchar(50)
          10990000 , -- gia - decimal
          14 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i3' , -- cpu - nvarchar(10)
          4 , -- ram - int
          500 , -- ocung - int
          1.86 , -- khoiluong - float
          5 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Dell Inspiron 3567 i3 6006U' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0008.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          3 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          11690000 , -- gia - decimal
          15.6 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i3' , -- cpu - nvarchar(10)
          4 , -- ram - int
          512 , -- ocung - int
          2.25 , -- khoiluong - float
          3 , -- pin - float
          0  -- cardroi - bit
        )
GO 
INSERT INTO dbo.Laptop
        ( Name ,MoTa ,AnhBia ,NgayCapNhat, HangLaptopId ,HeDieuHanhId ,mausac ,gia ,manhinh ,dophangiai ,cpu , ram ,ocung ,khoiluong , pin ,cardroi
        )
VALUES  ( N'Laptop Dell Vostro 3568 i3 7100U' , -- Name - nvarchar(50)
          N'Chưa có mô tả' , -- MoTa - nvarchar(200)
          N'0009.png' , -- AnhBia - nvarchar(max)
		  GETDATE() , --NgayCapNhat-datetime
          3 , -- HangLaptopId - int
          1 , -- HeDieuHanhId - int
          N'Đen' , -- mausac - nvarchar(50)
          12190000 , -- gia - decimal
          15.6 , -- manhinh - float
          1 , -- dophangiai - bit
          N'core i3' , -- cpu - nvarchar(10)
          4 , -- ram - int
          1024 , -- ocung - int
          2.18 , -- khoiluong - float
          5 , -- pin - float
          0  -- cardroi - bit
        )
GO 




--thêm dữ liệu bảng admin
INSERT INTO dbo.Admin
        ( Username, Password )

VALUES  ( 
          N'admin', -- Username - nvarchar(50)
          HASHBYTES('SHA1',N'123')  -- Password - binary
          )
GO






--thêm dữ liệu bảng khách hàng
INSERT INTO dbo.KhachHang
        (HoTen ,NgaySinh ,DienThoai ,TaiKhoan ,MatKhau , Email ,DiaChi
        )
VALUES  ( N'Võ Anh Tuấn' , -- HoTen - nvarchar(50)
		23-02-1996 , -- NgaySinh - datetime
          N'01626356708' , -- DienThoai - nvarchar(50)
          N'votuan' , -- TaiKhoan - nvarchar(50)
          N'123456' , -- MatKhau - nvarchar(50)
          N'votuanbk232@gmail.com' , -- Email - nvarchar(50)
          N'Hà Nội'  -- DiaChi - nvarchar(50)
        )
		GO 
--lấy luật có độ tin cậy cao nhất khi biết sự kiện vế trái
--SELECT TOP 1 * FROM dbo.Luat
--WHERE SuKienVT='1'
--ORDER BY  DoTinCay DESC



