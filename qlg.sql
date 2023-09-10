CREATE DATABASE QLCuaHangGame;
GO
USE QLCuaHangGame;
GO
--Bảng phân người dùng
-- Bảng phân quyền người dùng
-- Bảng phân người dùng
CREATE TABLE IDROLE(
    idRole int primary key,
    ten varchar(50) NOT NULL
)

-- Bảng tài khoản
CREATE TABLE USERS(
    id int primary key,
    email varchar(50) NOT NULL,
    tentaikhoan varchar(50) NOT NULL,
    matKhau varchar(50) NOT NULL,
    sdt varchar(10) NOT NULL,
    idRole int foreign key references IDROLE(idRole),
    ngaySinh datetime,
    FuName nvarchar(255) NOT NULL,
    Avartar nvarchar(255) NOT NULL,
    sodu int	
)

-- Bảng thể loại
CREATE TABLE THELOAI(
    idTheLoai varchar(10) primary key,
    tenTheLoai nvarchar(50) NOT NULL
)

-- Bảng trò chơi (product)
CREATE TABLE GAME(
    idGame varchar(10) primary key,
    name nvarchar(50) NOT NULL,
    moTa nvarchar(200) NOT NULL,
    gia int NOT NULL,
    idNPH int foreign key references USERS(id),
    idTheLoai varchar(10) foreign key references THELOAI(idTheLoai),
    image_url nvarchar(200),
    ngayTao date,
    giamGia int
)

-- Bảng mua hàng
CREATE TABLE MUAGAME(
    idMua varchar(10) primary key,
    idND int foreign key references USERS(id),
    Deleted bit,
    trangThai varchar(20) NOT NULL,
    ngayMua date,
    CONSTRAINT FK_MUAGAME_USERS FOREIGN KEY (idND) REFERENCES USERS(id) ON DELETE CASCADE
)

-- Bảng chi tiết mua hàng
CREATE TABLE CHITIET_MUAGAME(
    idChiTIet int primary key,
    idGame varchar(10) foreign key references GAME(idGame),
    idMua varchar(10) foreign key references MUAGAME(idMua),
    discount int,
    gia int NOT NULL,
    CONSTRAINT FK_CHITIET_MUAGAME_GAME FOREIGN KEY (idGame) REFERENCES GAME(idGame) ON DELETE CASCADE,
    CONSTRAINT FK_CHITIET_MUAGAME_MUAGAME FOREIGN KEY (idMua) REFERENCES MUAGAME(idMua) ON DELETE CASCADE
)

-- Bảng hiện người dùng sở hữu những trò chơi nào
CREATE TABLE SOHUU(
    idND int foreign key references USERS(id),
    idGame varchar(10) foreign key references GAME(idGame),
    NgaySoHuu date,
    CONSTRAINT FK_SOHUU_USERS FOREIGN KEY (idND) REFERENCES USERS(id) ON DELETE CASCADE,
    CONSTRAINT FK_SOHUU_GAME FOREIGN KEY (idGame) REFERENCES GAME(idGame) ON DELETE CASCADE
)

-- Bảng tin tức
CREATE TABLE TinTuc (
    idtintuc varchar(10) NOT NULL PRIMARY KEY,
    tieu_de VARCHAR(255) NOT NULL,
    noi_dung TEXT NOT NULL,
    ngay_dang date NOT NULL
)

-- Thêm các ràng buộc khóa ngoại cho bảng USERS
ALTER TABLE USERS ADD CONSTRAINT FK_USERS_IDROLE FOREIGN KEY (idRole) REFERENCES IDROLE(idRole) ON DELETE CASCADE

-- Thêm các ràng buộc khóa ngoại cho bảng GAME
ALTER TABLE GAME ADD CONSTRAINT FK_GAME_USERS FOREIGN KEY (idNPH) REFERENCES USERS(id) ON DELETE CASCADE
ALTER TABLE GAME ADD CONSTRAINT FK_GAME_THELOAI FOREIGN KEY (idTheLoai) REFERENCES THELOAI(idTheLoai) ON DELETE CASCADE

-- Thêm các ràng buộc khóa ngoại cho bảng SOHUU
ALTER TABLE SOHUU ADD CONSTRAINT FK_SOHUU_USERS FOREIGN KEY (idND) REFERENCES USERS(id) ON DELETE CASCADE
ALTER TABLE SOHUU ADD CONSTRAINT FK_SOHUU_GAME FOREIGN KEY (idGame) REFERENCES GAME(idGame) ON DELETE CASCADE

INSERT INTO IDROLE (idRole, ten) VALUES (1, 'Admin'),
(3, 'User'),
(2, 'Develope');

INSERT INTO USERS (id, email, tentaikhoan, matKhau, sdt, idRole,FuName,Avartar,sodu)
VALUES (1, 'thanh.nh.62.cntt@ntu.edu.vn', 'nguyenThanh', '123', '0111111111', 3,N'Nguyễn Huữ  Thành', N'avatar1.jpg', 500000),
(2, 'long.ht.62.cntt@ntu.edu.vn', 'huynhLong', '123', '0222222222', 3, N'Huỳnh Thạch Long', N'avatar2.jpg', 500000),
(3, 'loc.nt.62.cntt@ntu.edu.vn', 'tanLoc', '123', '0333333333', 2, N'Nguyễn Tấn Lộc', N'avatar3.jpg', 500000),
(4, 'viet.nh.62.cntt@ntu.edu.vn', 'hoangViet', '123', '0444444444', 2, N'Nguyễn Hoàng Việt', N'avatar4.jpg',500000),
(5, 'admin.us.62.cntt@ntu.edu.vn', 'adminUser', '123', '0555555555', 1,N'Admin', N'avatar5.jpg', 500000),
(6, 'mama@gmail.com', 'mamaboy', '123456', '0666666666', 1, N'Nguyễn Văn mama 1', N'avatar6.jpg', 500000),
(7, 'baca@gmail.com', 'lobaca', '123456', '0777777777', 2, N'Nguyễn Thị baca', N'avatar7.jpg', 500000),
(8, 'midu@gmail.com', 'midu', '123456', '0888888888', 3, N'Nguyễn Văn midu', N'avatar8.jpg', 500000),
(9, 'phila@gmail.com', 'phila', '123456', '0999999999', 3, N'Nguyễn Thị phila', N'avatar9.jpg', 500000),
(10, 'ora@gmail.com', 'ora', '123456', '0101010101', 1, N'Nguyễn Văn ora', N'avatar10.jpg', 500000),
(11, 'keng@gmail.com', 'keng', '123456', '0111113111', 2, N'Nguyễn Văn keng', N'avatar11.jpg', 500000),
(12, 'phiphai@gmail.com', 'phiphai', '123456', '0122222222', 3, N'Nguyễn Thị phiphai', N'avatar12.jpg', 500000),
(13, 'hen@example.com', 'hen', '123456', '0333333633', 2, N'Nguyễn Thị hen', N'avatar13.jpg', 500000),
(14, 'luna9@gmail.com', 'luna9', '123456', '0444474444', 1, N'Nguyễn Văn luna', N'avatar14.jpg', 500000),
(15, 'oran@gmail.com', 'oran', '123456', '0555555455', 2, N'Nguyễn Thị oran', N'avatar15.jpg', 500000),
(16, 'mico@gmail.com', 'mico', '123456', '0666666866', 3, N'Nguyễn Văn mico', N'avatar16.jpg', 500000),
(17, 'milu@gmail.com', 'milu', '123456', '0777777577', 2, N'Nguyễn Thị  milu', N'avatar17.jpg', 500000),
(18, 'ote@gmail.com', 'ote', '123456', '0888888884', 1, N'Nguyễn Thị  ote', N'avatar18.jpg', 500000),
(19, 'otp@gmail.com', 'otp', '123456', '0999999993', 3, N'Nguyễn Thị  otp', N'avatar19.jpg', 500000),
(20, 'rambo@gmail.com', 'rambo', '123456', '0101010121', 1, N'Nguyễn Văn rambo', N'avatar20.jpg', 500000),
(21, 'rambi@gmail.com', 'rambi', '123456', '0110011131', 2, N'Nguyễn Văn rambi', N'avatar21.jpg', 500000),
(22, 'rambe@gmail.com', 'rambe', '123456', '0222122242', 3, N'Nguyễn Văn rambe', N'avatar22.jpg', 500000),
(23, 'ramma@gmail.com', 'ramma', '123456', '0333233333', 2, N'Nguyễn Thị  ramma', N'avatar23.jpg', 500000),
(24, 'lalali@gmail.com', 'lalali', '123456', '0434444444', 3, N'Nguyễn Thị  lalali', N'avatar24.jpg', 500000),
(25, 'lulu@gmail.com', 'lulu', '123456', '0555554555', 3, N'Nguyễn Văn lulu', N'avatar25.jpg', 500000),
(26, 'noaon@gmail.com', 'noaon', '123456', '066666666', 3, N'Nguyễn Thị  noaon', N'avatar26.jpg', 500000),
(27, 'kem@gmail.com', 'kem', '123456', '0777777777', 1, N'Nguyễn Văn kem', N'avatar27.jpg', 500000),
(28, 'kemkem@gmail.com', 'kemkem', '123456', '0828888888', 1, N'Nguyễn Thị  kemkem', N'avatar28.jpg', 500000),
(29, 'hehe@gmail.com', 'hehe', '123456', '0999993999', 3, N'Nguyễn Văn hehe', N'avatar29.jpg', 500000),
(30, 'hihi@gmail.com', 'hihi', '123456', '0101014101', 2, N'Nguyễn Thị  hihi', N'avatar30.jpg', 500000),
(31, 'huhu@gmail.com', 'huhu', '123456', '0111115111', 1, N'Nguyễn Văn huhu', N'avatar31.jpg', 500000),
(32, 'hohoho@gmail.com', 'hohoho', '123456', '0262222222', 1, N'Nguyễn Thị  hohoho', N'avatar32.jpg', 500000),
(33, 'uawa@gmail.com', 'uawa', '123456', '0333337333', 3, N'Nguyễn Thị  uawa', N'avatar33.jpg', 500000);

INSERT INTO THELOAI (idTheLoai, tenTheLoai)
VALUES ('CT', N'Chiến Thuật'),
('GD', N'Giải Đố'),
('DK', N'Đối Kháng'),
('PL', N'Phiêu Lưu'),
('NV', N'Nhập vai'),
('TGM', N'Thế Giới Mở'),
('KD', N'Kinh Dị');

INSERT INTO GAME (idGame, name, moTa, gia, idNPH, idTheLoai, image_url, ngayTao, giamGia)
VALUES ('G001', N'Mincecraft', N'Mô tả Game Minecraft', 500000, 3, 'PL', N'anhtemp.jpg', '2023-05-13', 10),
('G002', N'Fligh Simulator', N'Mô tả Game Filight', 500000, 3, 'NV', N'flightsimulator.jpg', '2023-05-13', 10),
('G003', N'EldenRing', N'Mô tả Game Elden', 500000, 3, 'TGM', N'eldenring.jpg', '2023-05-13', 10),
('G004', N'DarkSoul', N'Mô tả Game Dark', 500000, 4, 'KD', N'darksoul.jpg', '2023-05-13', 10),
('G005', N'Residen Evil 4', N'Mô tả Game Resident', 500000, 4, 'KD', N'residentevil.jpg', '2023-05-13', 10),
('G006', N'LittleNightMare', N'Mô tả Game Littel', 500000, 4, 'PL', N'littlenightmare.jpg', '2023-05-13', 10),
('G007', N'StreetFighter', N'Mô tả Game Street', 500000, 4, 'DK', N'streetfighter.jpg', '2023-05-13', 10),
('G008', N'GrandThiefAuto', N'Mô tả Game GTA', 500000, 4, 'TGM', N'gta.jpg', '2023-05-13', 10),
('G009', N'Cyberpunk 2077', N'Mô tả Game Cyberpunk 2077', 60000, 4, 'TGM', N'Cyberpunk 2077.jpg', '2023-06-01', 10),
('G010', N'Among Us', N'Mô tả Game Among Us', 200000, 3, 'GD', N'Among Us.jpg', '2023-06-01', 20),
('G011', N'Counter-Strike: Global Offensive', N'Mô tả Game Counter-Strike: Global Offensive', 100000, 2, 'DK', N'Counter-Strike Global Offensive.jpg', '2023-06-01', 0),
('G012', N'FIFA 22', N'Mô tả Game FIFA 22', 600000, 1, 'PL', N'FIFA 22.jpg', '2023-06-01', 10),
('G013', N'Call of Duty: Warzone', N'Mô tả Game Warzone', 10000, 2, 'NV', N'Call of Duty.jpg', '2023-06-01', 0),
('G014', N'The Witcher 3: Wild Hunt', N'Mô tả Game The Witcher 3', 350000, 4, 'TGM', N'The Witcher 3.jpg', '2023-06-01', 15),
('G015', N'Apex Legends', N'Mô tả Game Apex Legends', 0, 2, 'NV', N'Apex Legends.jpg', '2023-06-01', 0),
('G016', N'Assassin is Creed Valhalla', N'Mô tả Game Assassin is Creed Valhalla', 450000, 4, 'TGM', N'Assassin is Creed Valhalla.jpg', '2023-06-01', 10),
('G017', N'Genshin Impact', N'Mô tả Game Genshin Impact', 0, 3, 'TGM', N'Genshin Impact.jpg', '2023-06-01', 0),
('G018', N'Dota 2', N'Mô tả Game Dota 2', 100000, 2, 'DK', N'Dota 2.jpg', '2023-06-01', 0),
('G019', N'Red Dead Redemption 2', N'Mô tả Game Red Dead Redemption 2', 550000, 4, 'TGM', N'Red Dead Redemption 2.jpg', '2023-06-01', 10),
('G020', N'Fortnite', N'Mô tả Game Fortnite', 0, 3, 'NV', N'Fortnite.jpg', '2023-06-01', 0),
('G021', N'The Last of Us Part II', N'Mô tả Game The Last of Us Part II', 400000, 4, 'PL', N'The Last of Us Part II.jpg', '2023-06-01', 10),
('G022', N'League of Legends', N'Mô tả Game League of Legends', 0, 2, 'DK', N'League of Legends.jpg', '2023-05-01', 0),
('G023', N'World of Warcraft', N'Mô tả Game World of Warcraft', 0, 1, 'NV', N'World of Warcraft.jpg', '2023-04-09', 0),
('G024', N'Overwatch', N'Mô tả Game Overwatch', 0, 2, 'DK', N'Overwatch.jpg', '2023-06-01', 0),
('G025', N'Final Fantasy VII Remake', N'Mô tả Game Final Fantasy VII Remake', 450000, 4, 'TGM', N'Final Fantasy VII Remake.jpg', '2023-06-01', 10),
('G026', N'PlayerUnknown is Battlegrounds', N'Mô tả Game PlayerUnknown is Battlegrounds', 0, 2, 'NV', N'PlayerUnknown is Battlegrounds.jpg', '2023-06-01', 0),
('G027', N'Monster Hunter: World', N'Mô tả Game Monster Hunter: World', 350000, 4, 'PL', N'Monster Hunter: World.jpg', '2023-06-01', 15),
('G028', N'The Elder Scrolls V: Skyrim', N'Mô tả Game The Elder Scrolls V: Skyrim', 350000, 2, 'TGM', N'The Elder Scrolls V Skyrim.jpg', '2023-06-01', 5),
('G029', N'Diablo III', N'Mô tả Game Diablo III', 250000, 2, 'NV', N'Diablo III.jpg', '2023-06-01', 0);
INSERT INTO TINTUC (idtintuc,tieu_de,noi_dung,ngay_dang)
values ('T001',N'Ra mắt resident evil 4',N'Nội dung tin tức resident evil 4 ra mắt','2023-9-30'),
('T002',N'Top 3 trò chơi trong tháng',N'Nội dung tin tức top 3 trò chơi trong tháng','2023-2-3'),
('T003',N'Cập nhật MineCraft 1.26.2',N'Nội dung tin tức cập nhật MineCraft','2023-1-30');

INSERT INTO MUAGAME (idMua, idND, Deleted, trangThai, ngayMua)
VALUES ('M001', 1, 0, 'Đã thanh toán', '2023-05-13'),
('M002', 2, 0, 'Đã thanh toán', '2023-05-13');

INSERT INTO CHITIET_MUAGAME (idChiTIet,idGame, idMua, discount, gia)
VALUES (1,'G001', 'M001', 0, 500000),
(2,'G002', 'M001', 0, 500000),
(3,'G004', 'M001', 0, 500000),
(4,'G001', 'M002', 0, 500000),
(5,'G005', 'M002', 0, 500000);