using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace ProjectDarkness
{
    public class VersionLoader
    {
        private readonly string _dbPath;

        public VersionLoader(string dbPath = "Verzio.db")
        {
            _dbPath = dbPath;
        }

        public string? GetVersion()
        {
            string connectionString = $"Data Source={_dbPath};Version=3;";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Version FROM AppInfo LIMIT 1";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public void ShowError(Exception ex)
        {
            MessageBox.Show("Hiba történt a verzió lekérdezés során: " + ex.Message);
        }
    }
}