using SQLTASK2.Models;
using SQLTASK2.Repositories;

const string connectionString = @"Server=localhost;Database=BOOKING;User Id=sa;Password=reallyStrongPwd123;MultipleActiveResultSets=true";

IBookingRepository bookingRepository = new RawSqlBookingRepository(connectionString);
IVoucherRepository voucherRepository = new RawSqlVoucherRepository(connectionString);
ITurfirmaRepository turfirmaRepository = new RawSqlTurfirmaRepository(connectionString);
PrintCommands();

while ( true )
{
    Console.WriteLine( "Введите команду:" );
    string command = Console.ReadLine();

    if (command == "get-bookings")
    {
        IReadOnlyList<Booking> bookings = bookingRepository.GetAll();
        if (bookings.Count == 0)
        {
            Console.WriteLine("Заказы не найдены!");
            continue;
        }

        foreach (Booking booking in bookings)
        {
            Console.WriteLine($"Id: {booking.Id}, Date: {booking.Date},  Turfirma_id: {booking.Turfirma_id}, Voucher_id: {booking.Voucher_id}, Quantity: {booking.Quantity}, Price: {booking.Price}");
        }
    }
    else if (command == "get-by-sanatorium")
    {
        Console.WriteLine("Введите название санатория:");
        string sanatorium = Console.ReadLine();
        if (string.IsNullOrEmpty(sanatorium))
        {
            Console.WriteLine("Неккоректный ввод!");
            continue;
        }

        Voucher voucher = voucherRepository.GetByNameOfSanatorium(sanatorium);
        if (voucher == null)
        {
            Console.WriteLine("Путевка не найдена!");
        }
        else
        {
            Console.WriteLine($"Id: {voucher.Id}, Name: {voucher.Sanatorium}, Address: {voucher.Address}, Price: {voucher.Price}, Quantity: {voucher.Quantity}");
        }

    }
    else if (command == "update-turfirma")
    {
        Console.WriteLine("Введите Id:");
        string strid = Console.ReadLine();
        int id;
        if (!int.TryParse(strid, out id))
        {
            Console.WriteLine("Неккоректный ввод!");
            continue;
        }

        TurFirma turfirma = turfirmaRepository.GetById(id);
        if (turfirma == null)
        {
            Console.WriteLine("Тур. фирма не найдена!");
            continue;
        }

        Console.WriteLine("Введите новое значение названия тур. фирмы:");
        string newName = Console.ReadLine();
        if (string.IsNullOrEmpty(newName))
        {
            Console.WriteLine("Неккоректное название тур. фирмы!");
            continue;
        }
        turfirma.UpdateName(newName);

        Console.WriteLine("Введите новое значение адреса тур. фирмы:");
        string newAddress = Console.ReadLine();
        if (string.IsNullOrEmpty(newAddress))
        {
            Console.WriteLine("Неккоректный адрес тур. фирмы!");
            continue;
        }
        turfirma.UpdateAddress(newAddress);

        Console.WriteLine("Введите новое значение комиссии:");
        string newCommission = Console.ReadLine();
        int newCom;
        if (!int.TryParse(newCommission, out newCom))
        {
            Console.WriteLine("Неккоректный ввод!");
            continue;
        }
        turfirma.UpdateCommission(newCom);

        turfirmaRepository.UpdateTurFirma(turfirma);
        Console.WriteLine("Информация о тур. фирме обновлена!");
    }
    else if (command == "delete-turfirma-by-id")
    {
        Console.WriteLine("Введите id:");
        string strid = Console.ReadLine();
        int id;
        if (!int.TryParse(strid, out id))
        {
            Console.WriteLine("Неккоректный ввод!");
            continue;
        }

        TurFirma turfirma = turfirmaRepository.GetById(id);
        if (turfirma == null)
        {
            Console.WriteLine("Тур. фирма не найдена!");
        }
        else
        {
            turfirmaRepository.DeleteTurFirma(turfirma);
            Console.WriteLine("Тур. фирма удалена!");
        }
    }
    else if (command == "group-by-date")
    {

        IReadOnlyList<Tuple<Booking, int>> bookings = bookingRepository.GroupByDate();
        if (bookings.Count == 0)
        {
            Console.WriteLine("Заказ не найден!");
            continue;
        }

        foreach (var booking in bookings)
        {
            Console.WriteLine($"Date: {booking.Item1.Date}, TotalPrice: {booking.Item2}");
        }
    }
    else if (command == "help")
    {
        PrintCommands();
    }
    else if (command == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Неправильно введенная команда");
    }
}

void PrintCommands()
{
    Console.WriteLine( "Доступные команды:" );
    Console.WriteLine( "get-bookings - Получить список всех заказов" );
    Console.WriteLine( "get-by-sanatorium - Получить информацию о путевке по названию санатория" );
    Console.WriteLine( "group-by-date - Вывести общую сумму заказов по датам" );
    Console.WriteLine( "update-turfirma - Обновить  информацию о тур. фирме" );
    Console.WriteLine( "delete-turfirma-by-id - Удалить тур. фирму по id");
    Console.WriteLine( "help - Список команд" );
    Console.WriteLine( "exit - Выход" );
}