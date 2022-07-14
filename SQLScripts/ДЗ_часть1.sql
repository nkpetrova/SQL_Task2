-- Предметная область - туристическое агенство

CREATE DATABASE BOOKING

-- Таблица с информацией о Тур. фирме
CREATE TABLE TurFirma(
Id int identity(1,1) constraint PK_TurFirma primary key,
Name nvarchar(100),
Address nvarchar(max),
Commission int
)

-- Таблица с информацией о путевке
CREATE TABLE Voucher (
Id int identity(1,1) constraint PK_Voucher primary key,
Sanatorium nvarchar(100),
Address nvarchar(max),
Price int,
Quantity int
)

-- Таблица с информацией о заказе
CREATE TABLE Booking (
Id int identity(1,1) constraint PK_Booking primary key,
Date nvarchar(100),
Turfirma_id int constraint FK_Booking_TurFirma references TurFirma(Id) ON DELETE CASCADE,
Voucher_id int constraint FK_Booking_Voucher references Voucher(Id) ON DELETE CASCADE,
Quantity int, 
Price int
)

-- Так как на MacOS нет Sql Management Studio, а в Azure Data Studio нет возможности создавать диаграмму таблиц, 
-- то данный пункт (В Sql Management Studio создать диаграмму таблиц) выполнить не получится