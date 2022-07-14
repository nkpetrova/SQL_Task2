using SQLTASK2.Models;

namespace SQLTASK2.Repositories
{
    public interface IBookingRepository
    {
        IReadOnlyList<Booking> GetAll();
        IReadOnlyList<Tuple<Booking, int>> GroupByDate();
    }

    public interface IVoucherRepository
    {
        Voucher GetByNameOfSanatorium(string sanatorium);
    }

    public interface ITurfirmaRepository
    {
        void UpdateTurFirma(TurFirma turfirma);
        TurFirma GetById(int id);
        void DeleteTurFirma(TurFirma turfirma);
    }
}