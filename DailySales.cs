using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinsStore
{
    public class DailySales
    {
        public double GetDailySales(DateTime date)
        {
            string connectionString = "Data Source=cashier.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT SUM(Total) FROM Invoices WHERE Date = @date";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                    return Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
        }

    }
}
