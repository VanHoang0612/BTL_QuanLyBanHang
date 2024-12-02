using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace QuanLyBanHang.Class
{
    internal class Functions
    {
        public static SqlConnection? Con;

        public static void Connect()
        {
            Con = new SqlConnection(); //Khoi tao doi tuong
            Con.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=QuanLyBanHang;Integrated Security=True;TrustServerCertificate=True";
            Con.Open();
            if(Con.State == ConnectionState.Open)
            {
                return;
            }
            else
            {
                MessageBox.Show("Không thể kết nối với dữ liệu");
            }
        }
        public static void Disconnect()
        {
            if (Con != null && Con.State == ConnectionState.Open)
            {
                Con.Close(); // Đóng kết nối
                Con.Dispose(); // Giải phóng tài nguyên
                Con = null; // Gán null để tránh sử dụng lại
            }

        }
        public static void LoadData(string TableName, DataGrid DataGrid)
        {
            string query = $"SELECT * FROM {TableName}";
            // Sử dụng SqlDataAdapter để lấy dữ liệu
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            // Tạo DataTable để chứa dữ liệu
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            // Đổ dữ liệu vào DataGrid
            DataGrid.ItemsSource = dt.DefaultView;

        }

        public static void LoadDataFind(DataGrid DataGrid, string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            // Tạo DataTable để chứa dữ liệu
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Đổ dữ liệu vào DataGrid
            DataGrid.ItemsSource = dt.DefaultView;
        }
        public static DataRow? GetDataRow(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]; // Trả về dòng đầu tiên
            }
            return null;
        }


        //Hàm kiểm tra khoá trùng
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
        //Hàm thực hiện câu lệnh SQL
        public static void RunSQL(string query)
        {
            SqlCommand? cmd; //Đối tượng thuộc lớp SqlCommand
            cmd = new SqlCommand();
            cmd.Connection = Con; //Gán kết nối
            cmd.CommandText = query; //Gán lệnh SQL
            try
            {
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
        }

        public static object? RunSQLRS(string sql)
        {
            try
            {
                // Tạo kết nối đến cơ sở dữ liệu
                using (SqlConnection con = new SqlConnection("your_connection_string"))
                {
                    con.Open();

                    // Tạo đối tượng SqlCommand
                    SqlCommand cmd = new SqlCommand(sql, con);

                    // Trả về kết quả của câu lệnh SQL
                    object result = cmd.ExecuteScalar(); // Sử dụng ExecuteScalar để lấy giá trị đầu tiên

                    return result;
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
                return null;
            }
        }
        // lấy dữ liệu sql cho vào combobox
        public static void LoadDataToComboBox(string sql, ComboBox cbo, string valueMember, string displayMember)
        {
            try
            {
                SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
                DataTable table = new DataTable();

                dap.Fill(table);

                // Gán dữ liệu vào ComboBox
                cbo.ItemsSource = table.DefaultView;

                // Thiết lập các thuộc tính ValueMember và DisplayMember cho ComboBox
                cbo.SelectedValuePath = valueMember;  // Trường chứa giá trị sẽ được lấy khi chọn item
                cbo.DisplayMemberPath = displayMember;  // Trường sẽ được hiển thị trong ComboBox
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi khi load dữ liệu vào ComboBox: " + ex.Message);
            }
        }

    }
}
