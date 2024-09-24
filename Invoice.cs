using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinsStore
{
    public class Invoice
    {
        public void CompleteInvoice(List<Tuple<int, int>> products) // List of (ProductId, Quantity)
        {
            string connectionString = "Data Source=cashier.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Start transaction
                using (var transaction = conn.BeginTransaction())
                {
                    string insertInvoice = "INSERT INTO Invoices (Date, Total) VALUES (@date, @total)";
                    using (SQLiteCommand cmd = new SQLiteCommand(insertInvoice, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@total", products.Sum(p => GetProductPrice(p.Item1) * p.Item2));
                        cmd.ExecuteNonQuery();
                    }

                    long invoiceId = conn.LastInsertRowId;

                    foreach (var product in products)
                    {
                        string insertDetails = "INSERT INTO InvoiceDetails (InvoiceId, ProductId, Quantity, Price) VALUES (@invoiceId, @productId, @quantity, @price)";
                        using (SQLiteCommand cmd = new SQLiteCommand(insertDetails, conn))
                        {
                            cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                            cmd.Parameters.AddWithValue("@productId", product.Item1);
                            cmd.Parameters.AddWithValue("@quantity", product.Item2);
                            cmd.Parameters.AddWithValue("@price", GetProductPrice(product.Item1));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Commit transaction
                    transaction.Commit();
                }
            }
        }

        public double GetProductPrice(int productId)
        {
            string connectionString = "Data Source=cashier.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Price FROM Products WHERE Id = @productId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    return Convert.ToDouble(cmd.ExecuteScalar());
                }
            }
        }

    }
}
