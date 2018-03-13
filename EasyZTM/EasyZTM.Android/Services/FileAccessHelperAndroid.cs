using EasyZTM.Droid.Services;
using EasyZTM.Services;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileAccessHelperAndroid))]

namespace EasyZTM.Droid.Services
{
    public class FileAccessHelperAndroid : IFileAccessHelper
    {
        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(GetLocalFilePath("stops.db"));
            return conn;
        }

        private string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = System.IO.Path.Combine(path, filename);

            CopyDatabaseIfNotExists(dbPath);

            return dbPath;
        }

        private void CopyDatabaseIfNotExists(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open("stops.db")))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}
