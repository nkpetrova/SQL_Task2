using System;
namespace SQLTASK2.Models
{
    public class Booking
    {

        public int Id { get; private set; }
        public string Date { get; private set; }
        public int Turfirma_id { get; private set; }
        public int Voucher_id { get; private set; }
        public int Quantity { get; private set; }
        public int Price { get; private set; }

        public Booking(int id, string date, int turfirma_id, int voucher_id, int quantity, int price)
        {
            Id = id;
            Date = date;
            Turfirma_id = turfirma_id;
            Voucher_id = voucher_id;
            Quantity = quantity;
            Price = price;
        }
    }
}