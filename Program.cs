using BlogsApp.Models;
using BlogsApp.Repositories;

const string connectionString = @"Server=localhost;Database=BOOKING;User Id=sa;Password=reallyStrongPwd123;MultipleActiveResultSets=true";

IBookingRepository bookingRepository = new RawSqlBookingRepository(connectionString);
IVoucherRepository voucherRepository = new RawSqlVoucherRepository(connectionString);
ITurfirmaRepository turfirmaRepository = new RawSqlTurfirmaRepository(connectionString);
PrintCommands();

while ( true )
{
    Console.WriteLine( "Введите команду:" );
    string command = Console.ReadLine();

    if ( command == "get-bookings" )
    {
        IReadOnlyList<Booking> bookings = bookingRepository.GetAll();
        if (bookings.Count == 0 )
        {
            Console.WriteLine( "Заказы не найдены!" );
            continue;
        }

        foreach ( Booking booking in bookings)
        {
            Console.WriteLine( $"Id: {booking.Id}, Date: {booking.Date},  Turfirma_id: {booking.Turfirma_id}, Voucher_id: {booking.Voucher_id}, Quantity: {booking.Quantity}, Price: {booking.Price}" );
        }
    }
    else if (command == "get-by-sanatorium")
    {
        Console.WriteLine("Введите название санатория:");
        string sanatorium = Console.ReadLine();
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
        int id = int.Parse(Console.ReadLine());
        TurFirma turfirma = turfirmaRepository.GetById(id);
        if (turfirma == null)
        {
            Console.WriteLine("Тур. фирма не найдена!");
            continue;
        }

        Console.WriteLine("Введите новое значение названия тур. фирмы:");
        string newName = Console.ReadLine();
        turfirma.UpdateName(newName);

        Console.WriteLine("Введите новое значение адреса тур. фирмы:");
        string newAddress = Console.ReadLine();
        turfirma.UpdateAddress(newAddress);

        Console.WriteLine("Введите новое значение комиссии:");
        int newCommission = int.Parse(Console.ReadLine());
        turfirma.UpdateCommission(newCommission);

        turfirmaRepository.UpdateTurFirma(turfirma);
        Console.WriteLine("Информация о тур. фирме обновлена!");
    }
    else if (command == "delete-turfirma-by-id")
    {
        Console.WriteLine("Введите id:");
        int id = int.Parse(Console.ReadLine());
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
    //else if (command == "group-by-vouchers")
    //{
    //    Console.WriteLine("Группировка по санаториям:");

    //    Voucher voucher = voucherRepository.GroupByAddress();
    //    if (voucher == null)
    //    {
    //        Console.WriteLine("Путевка не найдена!");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Id: {voucher.Id}, Name: {voucher.Sanatorium}, Address: {voucher.Address}, Price: {voucher.Price}, Quantity: {voucher.Quantity}");
    //    }
    //}
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
        Console.WriteLine( "Неправильно введенная команда" );
    }
}

void PrintCommands()
{
    Console.WriteLine( "Доступные команды:" );
    Console.WriteLine( "get-bookings - Получить список всех заказов" );
    Console.WriteLine( "get-by-sanatorium - Получить информацию о путевке по названию санатория" );
    Console.WriteLine( "update-turfirma - Обновить  информацию о тур. фирме" );
    Console.WriteLine( "delete-turfirma-by-id - Удалить тур. фирму по id");
    Console.WriteLine( "help - Список команд" );
    Console.WriteLine( "exit - Выход" );
}