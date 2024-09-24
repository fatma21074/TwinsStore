using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TwinsStore
{
    public class DatabaseHelper
    {
        

    
        public static void CreateDatabase()
        {
            string connectionString = "Data Source=cashier.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // إنشاء جدول المنتجات
                string createProductsTable = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Price REAL NOT NULL,
                    Quantity INTEGER NOT NULL
                )";

                // إنشاء جدول الفواتير
                string createInvoicesTable = @"
                CREATE TABLE IF NOT EXISTS Invoices (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT NOT NULL,
                    Total REAL NOT NULL
                )";

                // إنشاء جدول تفاصيل الفواتير
                string createInvoiceDetailsTable = @"
                CREATE TABLE IF NOT EXISTS InvoiceDetails (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    InvoiceId INTEGER,
                    ProductId INTEGER,
                    Quantity INTEGER,
                    Price REAL,
                    FOREIGN KEY(InvoiceId) REFERENCES Invoices(Id),
                    FOREIGN KEY(ProductId) REFERENCES Products(Id)
                )";

                // تنفيذ أوامر SQL
                SQLiteCommand command = new SQLiteCommand(createProductsTable, conn);
                command.ExecuteNonQuery();

                command.CommandText = createInvoicesTable;
                command.ExecuteNonQuery();

                command.CommandText = createInvoiceDetailsTable;
                command.ExecuteNonQuery();
            }
        }
    }

}

