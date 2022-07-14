namespace BlogsApp.Models
{
    public class TurFirma
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int Commission { get; private set; }

        public TurFirma( int id, string name, string address, int commission )
        {
            Id = id;
            Name = name;
            Address = address;
            Commission = commission;
        }

        public void UpdateName(string newName)
        {
            Name = newName;
        }

        public void UpdateAddress(string newAddress)
        {
            Address = newAddress;
        }

        public void UpdateCommission(int newCommission)
        {
            Commission = newCommission;
        }
    }
}