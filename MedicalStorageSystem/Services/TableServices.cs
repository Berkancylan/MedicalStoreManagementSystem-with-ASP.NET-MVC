
namespace MedicalStorageSystem.Services
{
    public class TableService
    {
        public (int, int, int) getDataFromTable(int? pageSize, bool? pageSizeBool, int? pageNumber)
        {
            int mevcutSayfa = pageNumber ?? 1;
            int finalPageSize = pageSize ?? 10;

            if (pageSizeBool != null)
            {
                mevcutSayfa = (pageSizeBool == true ? mevcutSayfa + 1 : mevcutSayfa - 1);
                if (mevcutSayfa < 1)
                    mevcutSayfa = 1;
            }

            int offset = (mevcutSayfa == 1 ? 0 : (mevcutSayfa - 1) * finalPageSize);

            return (mevcutSayfa, finalPageSize, offset);
        }
    }
}