using SQLite;

namespace EasyZTM.Services
{
    public interface IFileAccessHelper
    {
        SQLiteConnection GetConnection();
    }
}
