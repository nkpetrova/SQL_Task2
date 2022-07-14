-- INSERT
INSERT INTO TurFirma VALUES 
(N'Отдых', N'Воркута', 10),
(N'Ланжерон', N'Евпатория', 2),
(N'Тур-Вояж', N'Москва', 5),
(N'Импрессо', N'Москва',5),
(N'ИнтерТур', N'Н.Новгород', 7),
(N'Круиз', N'Челябинск', 10)

INSERT INTO Voucher VALUES 
(N'Буревестник', N'Н.Новгород', 120000,10),
(N'Лазурный', N'Евпатория',200000,15 ),
(N'Сосновый Бор', N'Челябинск',110000,12 ),
(N'Прибрежный', N'Евпатория', 280000,20),
(N'Раздолье', N'Москва', 150000, 10),
(N'Якорь', N'Евпатория', 180000,22),
(N'Отрада', N'Евпатория', 190000,11)

INSERT INTO Booking VALUES 
(N'март', 2, 7, 5, 950000),
(N'март', 6, 7, 8, 1520000),
(N'апрель', 6, 6, 12, 2160000),
(N'апрель', 5, 1, 6, 720000 ),
(N'апрель', 4, 4, 8, 2040000 ),
(N'апрель', 2, 2, 10, 2000000 ),
(N'июнь', 1, 3, 8, 880000 ),
(N'июнь', 2, 2, 11, 2200000),
(N'июнь', 5, 7, 3, 570000 ),
(N'июль', 1, 4, 4, 1520000 ),
(N'август', 2, 3, 9, 990000 ),
(N'август', 6, 3, 5, 550000 ),
(N'август', 2, 4, 2, 560000),
(N'сентябрь', 1, 5, 7, 1050000 ),
(N'сентябрь', 1, 3, 11, 1210000 ),
(N'сентябрь', 2, 1, 1, 120000 ),
(N'октябрь', 3, 5, 2, 300000 )

-- SELECT
SELECT * FROM TurFirma
SELECT * FROM Voucher
SELECT * FROM Booking

SELECT Price, Sanatorium FROM Voucher WHERE address = N'Евпатория'
SELECT Name, Address FROM TurFirma WHERE Commission < 5
SELECT Id, Date, Quantity FROM Booking WHERE Price <= 600000
SELECT * FROM Voucher WHERE Price between 150000 and 250000
SELECT TOP 3 * FROM Booking WHERE Voucher_id = 3

-- UPDATE
UPDATE Booking SET Price = 123456 WHERE Turfirma_id = 2 and Voucher_id = 3
UPDATE TOP(1) Voucher SET Quantity = 100 WHERE Address = N'Евпатория'

-- DELETE
DELETE TOP(1) FROM Booking WHERE Quantity < 5

-- GROUP BY + функции агрегации
SELECT Date, sum(quantity) AS Sum_quantity FROM Booking GROUP BY Date
SELECT Sanatorium, avg(price) AS Max_price FROM Voucher WHERE address = N'Евпатория' GROUP BY sanatorium
SELECT Address, min(commission) AS Min_comission FROM TurFirma WHERE Id between 2 and 5 GROUP BY Address

-- GROUP BY + having
SELECT Address FROM Voucher GROUP BY Address HAVING min(price) <= 150000
SELECT Date FROM Booking GROUP BY Date HAVING sum(Quantity) > 15
SELECT t.name, sum(b.price) AS Sum_price FROM Booking b JOIN TurFirma t ON b.TurFirma_id = t.id GROUP BY t.name HAVING sum(b.price) <= 4000000 ORDER BY Sum_price DESC

-- JOIN таблиц
SELECT b.id, t.name, b.date, b.quantity FROM Booking b JOIN TurFirma t ON (b.turfirma_id = t.id) ORDER BY b.quantity
SELECT * FROM Booking b JOIN Voucher v ON (b.voucher_id = v.id) WHERE b.Date = N'сентябрь' and (v.Price >= 200000 or b.quantity <= 10)