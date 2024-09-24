using System.Data;

namespace TwinsStore
{
    public partial class Form1 : Form
    {
        private ProductService productService;
        private TextBox txtProductName;
        private TextBox txtProductPrice;
        private TextBox txtProductQuantity;
        private DataGridView dataGridViewProducts;
        private readonly TextBox txtProductId;

        public Form1()
        {
            InitializeComponent();
            productService = new ProductService(); // تهيئة الكلاس المسؤول عن إدارة المنتجات

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseHelper.CreateDatabase();
            LoadProducts();
        }
        private void LoadProducts()
        {
            DataTable products = productService.GetAllProducts();
            dataGridViewProducts.DataSource = products; // ربط DataGridView بالبيانات
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            double price = Convert.ToDouble(txtProductPrice.Text);
            int quantity = Convert.ToInt32(txtProductQuantity.Text);

            productService.AddProduct(name, price, quantity);
            MessageBox.Show("تم إضافة المنتج بنجاح!");
            LoadProducts(); // إعادة تحميل المنتجات بعد الإضافة


            // يمكن إضافة كود لتحديث قائمة المنتجات المعروضة إذا كنت تعرضها في DataGridView أو عنصر آخر.
        }
        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtProductId.Text);
            string name = txtProductName.Text;
            double price = Convert.ToDouble(txtProductPrice.Text);
            int quantity = Convert.ToInt32(txtProductQuantity.Text);

            productService.UpdateProduct(id, name, price, quantity);
            MessageBox.Show("تم إضافة المنتج بعد التحديث بنجاح!");

            LoadProducts(); // إعادة تحميل المنتجات بعد التحديث
        }
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtProductId.Text);
            productService.DeleteProduct(id);
            MessageBox.Show("تم ازاله المنتج  بنجاح!");

            LoadProducts(); // إعادة تحميل المنتجات بعد الحذف
        }


        private void dataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // التأكد من أن الصف الذي تم النقر عليه صالح
            {
                DataGridViewRow row = dataGridViewProducts.Rows[e.RowIndex];

                // عرض تفاصيل المنتج في الحقول النصية
                txtProductId.Text = row.Cells["Id"].Value.ToString();
                txtProductName.Text = row.Cells["Name"].Value.ToString();
                txtProductPrice.Text = row.Cells["Price"].Value.ToString();
                txtProductQuantity.Text = row.Cells["Quantity"].Value.ToString();
            }
        }


    }
}
