using EasyZTM.iOS.Services;
using EasyZTM.Services;
using Foundation;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileAccessHelperIOS))]

namespace EasyZTM.iOS.Services
{
    public class FileAccessHelperIOS : IFileAccessHelper
    {
        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(GetLocalFilePath("stops.db"));
            return conn;
        }

        private string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string dbPath = Path.Combine(libFolder, filename);

            CopyDatabaseIfNotExists(dbPath);

            return dbPath;
        }

        private void CopyDatabaseIfNotExists(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("stops", "db");
                File.Copy(existingDb, dbPath);
            }
        }
    }
}
