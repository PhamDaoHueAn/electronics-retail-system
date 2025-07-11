USE [master]
GO
/****** Object:  Database [ShopDienMay]    Script Date: 12/9/2024 1:26:11 PM ******/
CREATE DATABASE [ShopDienMay]
 
GO
ALTER DATABASE [ShopDienMay] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopDienMay] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopDienMay] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopDienMay] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopDienMay] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopDienMay] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopDienMay] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopDienMay] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopDienMay] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopDienMay] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopDienMay] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopDienMay] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopDienMay] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopDienMay] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopDienMay] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ShopDienMay] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopDienMay] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopDienMay] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopDienMay] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopDienMay] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopDienMay] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopDienMay] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopDienMay] SET RECOVERY FULL 
GO
ALTER DATABASE [ShopDienMay] SET  MULTI_USER 
GO
ALTER DATABASE [ShopDienMay] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopDienMay] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopDienMay] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopDienMay] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShopDienMay', N'ON'
GO
USE [ShopDienMay]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 12/9/2024 1:26:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[DonHangID] [int] NOT NULL,
	[SanPhamID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaBan] [float] NOT NULL,
 CONSTRAINT [PK_CTHD] PRIMARY KEY CLUSTERED 
(
	[DonHangID] ASC,
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[DanhMucID] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DanhMucID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[DonHangID] [int] IDENTITY(1,1) NOT NULL,
	[KhachHangID] [int] NULL,
	[HoTenNguoiNhan] [nvarchar](100) NULL,
	[SoDienThoai] [char](11) NULL,
	[NgayDat] [datetime] NULL,
	[DiaChiGiao] [nvarchar](255) NULL,
	[TongTien] [float] NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DonHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[GioHangID] [int] NOT NULL,
	[SanPhamID] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_GIOHANG] PRIMARY KEY CLUSTERED 
(
	[GioHangID] ASC,
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[SanPhamID] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](255) NOT NULL,
	[DanhMucID] [int] NULL,
	[ThuongHieuID] [int] NULL,
	[Gia] [float] NOT NULL,
	[TonKho] [int] NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[CongSuat] [nvarchar](max) NULL,
	[HinhAnh] [nvarchar](255) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SanPhamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TaiKhoanID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[MatKhau] [varchar](250) NOT NULL,
	[NgayTao] [datetime] NULL,
	[VaiTro] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaiKhoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThuongHieu]    Script Date: 12/9/2024 1:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThuongHieu](
	[ThuongHieuID] [int] IDENTITY(1,1) NOT NULL,
	[TenThuongHieu] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ThuongHieuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (1, 2, 3, 17000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (1, 3, 2, 13000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (2, 4, 1, 40000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (3, 12, 2, 20000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (3, 15, 3, 1300000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (4, 7, 1, 7000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (4, 8, 1, 12000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (5, 3, 1, 13000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (6, 2, 1, 17000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (7, 3, 1, 13000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (7, 4, 1, 40000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (7, 7, 1, 7000000)
INSERT [dbo].[ChiTietDonHang] ([DonHangID], [SanPhamID], [SoLuong], [GiaBan]) VALUES (7, 11, 1, 12000000)
GO
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([DanhMucID], [TenDanhMuc], [Active]) VALUES (1, N'Ti vi', 1)
INSERT [dbo].[DanhMuc] ([DanhMucID], [TenDanhMuc], [Active]) VALUES (2, N'Tủ lạnh', 1)
INSERT [dbo].[DanhMuc] ([DanhMucID], [TenDanhMuc], [Active]) VALUES (3, N'Máy lạnh', 1)
INSERT [dbo].[DanhMuc] ([DanhMucID], [TenDanhMuc], [Active]) VALUES (4, N'Máy giặt', 1)
INSERT [dbo].[DanhMuc] ([DanhMucID], [TenDanhMuc], [Active]) VALUES (5, N'Bếp điện từ', 1)
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[DonHang] ON 

INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (1, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T12:55:22.793' AS DateTime), N'140 Lê Trọng Tấn-Tân Phú-TPHCM', 77015000, N'Đã giao hàng')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (2, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T12:55:54.627' AS DateTime), N'140 Lê Trọng Tấn-Tân Phú-TPHCM', 40015000, N'Đang giao')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (3, 5, N'Lê Văn Quyết', NULL, CAST(N'2024-12-09T12:57:16.843' AS DateTime), N'13 sơn kỳ-Tân Phú-TPHCM', 43915000, N'Đã hủy')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (4, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T13:05:37.527' AS DateTime), N'140 Lê Trọng Tấn-Tân Phú-TP.HCM', 19015000, N'Đã hủy')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (5, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T13:21:31.223' AS DateTime), N'13 sơn kỳ-Tân Phú-TP.HCM', 13015000, N'Đã hủy')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (6, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T13:22:06.673' AS DateTime), N'13 sơn kỳ-Tân Phú-TP.HCM', 17015000, N'Đã hủy')
INSERT [dbo].[DonHang] ([DonHangID], [KhachHangID], [HoTenNguoiNhan], [SoDienThoai], [NgayDat], [DiaChiGiao], [TongTien], [TrangThai]) VALUES (7, 4, N'Trần Thị Lan', NULL, CAST(N'2024-12-09T13:24:13.610' AS DateTime), N'140 Lê Trọng Tấn-Tân Phú-TP.HCM', 72015000, N'Đang xử lý')
SET IDENTITY_INSERT [dbo].[DonHang] OFF
GO
INSERT [dbo].[GioHang] ([GioHangID], [SanPhamID], [SoLuong]) VALUES (5, 2, 3)
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (1, N'Tivi Samsung  70 inch UA70DU7000', 1, 2, 13000000, 10, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'40W', N'1.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (2, N'Tivi OLED LG 4K 55 inch 55B4PSA', 1, 3, 17000000, 7, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'30w', N'2.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (3, N'Tivi Samsung 4K 65 inch UA65CU8000', 1, 2, 13000000, 9, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'30W', N'3.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (4, N'Tủ lạnh Aqua Inverter 550 lít ', 2, 4, 40000000, 10, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'180W', N'4.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (5, N'Tủ lạnh Samsung Inverter 307', 2, 2, 28000000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'5.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (6, N'Tủ lạnh LG Inverter 519 lít', 2, 3, 60000000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'120W', N'6.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (7, N'Máy lạnh Toshiba Inverter 1 HP RAS-H10Z1KCVG-V', 3, 1, 7000000, 11, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'7.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (8, N'Máy lạnh LG Inverter 1 HP V10API1', 3, 3, 12000000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'8.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (9, N'Máy lạnh AQUA Inverter 1 HP AQA-RV10QA2', 3, 4, 17000000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'9.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (10, N'Máy giặt Samsung Inverter 12 kg WW12CGC04DABSV', 4, 2, 20000000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'10.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (11, N'Máy giặt Samsung AI EcoBubble Inverter 11 kg WW11CGP44DSHSV', 4, 2, 12000000, 11, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'200W', N'11.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (12, N'Máy giặt Aqua Inverter 11 kg AW11-B4959U1K(B)', 4, 4, 20000000, 10, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'20W', N'12.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (13, N'Bếp từ Toshiba IC-20R2SV', 5, 1, 1500000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'1000W', N'13.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (14, N'Bếp từ AVA MD-DC01', 5, 1, 1300000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'1000W', NULL, 0)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (15, N'Bếp từ AVA MD-DC01', 5, 1, 1300000, 9, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'1000W', N'15.jpg', 1)
INSERT [dbo].[SanPham] ([SanPhamID], [TenSanPham], [DanhMucID], [ThuongHieuID], [Gia], [TonKho], [MoTa], [CongSuat], [HinhAnh], [Active]) VALUES (16, N'Bếp từ BlueStone ICB-6729', 5, NULL, 1700000, 12, N'Sản phẩm chất lượng, trang bị những công nghệ mới nhất', N'2000', N'16.jpg', 1)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([TaiKhoanID], [HoTen], [Email], [SoDienThoai], [DiaChi], [MatKhau], [NgayTao], [VaiTro], [Active]) VALUES (1, N'Nguyễn Văn An', N'admin@gmail.com', N'0123846259', N'123 Đường A, Quận 1', N'123456', CAST(N'2024-12-09T12:20:14.510' AS DateTime), N'Quản lý', 1)
INSERT [dbo].[TaiKhoan] ([TaiKhoanID], [HoTen], [Email], [SoDienThoai], [DiaChi], [MatKhau], [NgayTao], [VaiTro], [Active]) VALUES (2, N'Trần Thị Ba', N'nv1@gmail.com', N'0987654321', N'456 Đường B, Quận 2', N'123456', CAST(N'2024-12-09T12:20:14.510' AS DateTime), N'Nhân viên', 1)
INSERT [dbo].[TaiKhoan] ([TaiKhoanID], [HoTen], [Email], [SoDienThoai], [DiaChi], [MatKhau], [NgayTao], [VaiTro], [Active]) VALUES (3, N'Nguyễn Văn Duy', N'nv2@gmail.com', N'0123971789', N'789 Đường C, Quận 3', N'123456', CAST(N'2024-12-09T12:20:14.510' AS DateTime), N'Nhân viên', 1)
INSERT [dbo].[TaiKhoan] ([TaiKhoanID], [HoTen], [Email], [SoDienThoai], [DiaChi], [MatKhau], [NgayTao], [VaiTro], [Active]) VALUES (4, N'Trần Thị Lan', N'kh1@gmail.com', N'0987659710', N'123 Đường D, Quận 4', N'123456', CAST(N'2024-12-09T12:20:14.510' AS DateTime), N'Khách hàng', 1)
INSERT [dbo].[TaiKhoan] ([TaiKhoanID], [HoTen], [Email], [SoDienThoai], [DiaChi], [MatKhau], [NgayTao], [VaiTro], [Active]) VALUES (5, N'Lê Văn Quyết', N'kh2@gmail.com', N'0111227465', N'123 Đường Lê trọng tấn, Quận 1', N'123456', CAST(N'2024-12-09T12:20:14.510' AS DateTime), N'Khách hàng', 1)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[ThuongHieu] ON 

INSERT [dbo].[ThuongHieu] ([ThuongHieuID], [TenThuongHieu], [Active]) VALUES (1, N'Toshiba', 1)
INSERT [dbo].[ThuongHieu] ([ThuongHieuID], [TenThuongHieu], [Active]) VALUES (2, N'Samsung', 1)
INSERT [dbo].[ThuongHieu] ([ThuongHieuID], [TenThuongHieu], [Active]) VALUES (3, N'LG', 1)
INSERT [dbo].[ThuongHieu] ([ThuongHieuID], [TenThuongHieu], [Active]) VALUES (4, N'Aqua', 1)
SET IDENTITY_INSERT [dbo].[ThuongHieu] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TaiKhoan__A9D10534008FF08D]    Script Date: 12/9/2024 1:26:12 PM ******/
ALTER TABLE [dbo].[TaiKhoan] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DonHang] ADD  DEFAULT ('Ðang x? lý') FOR [TrangThai]
GO
ALTER TABLE [dbo].[TaiKhoan] ADD  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_DH] FOREIGN KEY([DonHangID])
REFERENCES [dbo].[DonHang] ([DonHangID])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_CTHD_DH]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_SP] FOREIGN KEY([SanPhamID])
REFERENCES [dbo].[SanPham] ([SanPhamID])
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK_CTHD_SP]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[TaiKhoan] ([TaiKhoanID])
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GIOHANG_SANPHAMID] FOREIGN KEY([SanPhamID])
REFERENCES [dbo].[SanPham] ([SanPhamID])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GIOHANG_SANPHAMID]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_GIOHANG_TAIKHOANID] FOREIGN KEY([GioHangID])
REFERENCES [dbo].[TaiKhoan] ([TaiKhoanID])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_GIOHANG_TAIKHOANID]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([DanhMucID])
REFERENCES [dbo].[DanhMuc] ([DanhMucID])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([ThuongHieuID])
REFERENCES [dbo].[ThuongHieu] ([ThuongHieuID])
GO
USE [master]
GO
ALTER DATABASE [ShopDienMay] SET  READ_WRITE 
GO
