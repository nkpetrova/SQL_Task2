using System;
namespace SQLTASK2.Models
{
    public class Voucher
    {
        public int Id { get; private set; }
        public string Sanatorium { get; private set; }
        public string Address { get; private set; }
        public int Price { get; private set; }
        public int Quantity { get; private set; }

        public Voucher(int id, string sanatorium, string address, int price, int quantity)
        {
            Id = id;
            Sanatorium = sanatorium;
            Address = address;
            Price = price;
            Quantity = quantity;
        }
    }
}