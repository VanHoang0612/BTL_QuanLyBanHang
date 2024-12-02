using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using QuanLyBanHang.Class;
namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for frmChatLieu.xaml
    /// </summary>
    public partial class frmDMChatLieu : Window
    {
        public frmDMChatLieu()
        {
            InitializeComponent();
            Functions.LoadData("tblChatLieu", dgvChatLieu);
            defaultTextBox();
            txtTKMaChatLieu.Text = "";
            txtTKTenChatLieu.Text = "";
        }
        private void ResetValue()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }
        
        private void defaultTextBox()
        {
            txtMaChatLieu.IsEnabled = false;
            txtTenChatLieu.IsEnabled = false;
            dgvChatLieu.SelectedItem = null;
        }

        // dua du lieu len khi chon dong datagrid
        private void dgvChatLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (dgvChatLieu.SelectedItem != null)
            {
                DataRowView? selectionRow = (DataRowView)dgvChatLieu.SelectedItem;
                txtMaChatLieu.Text = selectionRow["MaChatLieu"].ToString();
                txtTenChatLieu.Text = selectionRow["TenChatLieu"].ToString();
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnBoQua.IsEnabled = true;
                btnLuu.IsEnabled = false;
                txtMaChatLieu.IsEnabled = false;
                txtTenChatLieu.IsEnabled = true;
                //txtMaChatLieu.IsReadOnly = false;
                btnThem.IsEnabled = false;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;
            btnBoQua.IsEnabled = true;
            btnLuu.IsEnabled = true;
            btnThem.IsEnabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaChatLieu.IsEnabled = true;
            txtTenChatLieu.IsEnabled = true;
            txtMaChatLieu.IsReadOnly = false; //cho phép nhập mới
            txtMaChatLieu.Focus();
            dgvChatLieu.SelectedItem = null;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
           
            btnLuu.IsEnabled=false;
            string sql;
            if (txtMaChatLieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
           
            sql = "Select MaChatLieu From tblChatLieu where TenChatLieu=N'" + txtTenChatLieu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Chất liệu này đã có!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenChatLieu.Focus();
                return;
            }

            sql = "UPDATE tblChatLieu SET TenChatLieu=N'" +
                txtTenChatLieu.Text.ToString() +
                "' WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
            Class.Functions.RunSQL(sql);
            btnThem.IsEnabled = true;
            Functions.LoadData("tblChatLieu", dgvChatLieu);
            ResetValue();

            btnBoQua.IsEnabled = false;
            defaultTextBox();
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtMaChatLieu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                sql = "DELETE tblChatLieu WHERE MaChatLieu=N'" + txtMaChatLieu.Text + "'";
                Functions.RunSQL(sql);
                btnThem.IsEnabled = true;
                Functions.LoadData("tblChatLieu", dgvChatLieu);
                ResetValue();
            }
            else
            {
                
                return;
            }
            
            
            btnBoQua.IsEnabled = false;
            defaultTextBox();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaChatLieu.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaChatLieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTenChatLieu.Focus();
                return;
            }
            sql = "Select MaChatLieu From tblChatLieu where MaChatLieu=N'" + txtMaChatLieu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaChatLieu.Focus();
                return;
            }
            sql = "Select MaChatLieu From tblChatLieu where TenChatLieu=N'" + txtTenChatLieu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Chất liệu này đã có!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTenChatLieu.Focus();
                return;
            }
            sql = "INSERT INTO tblChatLieu VALUES(N'" +
                 txtMaChatLieu.Text + "',N'" + txtTenChatLieu.Text + "')";


            Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            Functions.LoadData("tblChatLieu", dgvChatLieu);
            ResetValue();
            btnXoa.IsEnabled = true;
            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            defaultTextBox();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBoQua_Click(object sender, RoutedEventArgs e)
        {
            ResetValue();
            btnBoQua.IsEnabled = false;
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnLuu.IsEnabled = false;
            defaultTextBox();
            txtTKTenChatLieu.Text = "";
            txtTKMaChatLieu.Text = "";
            Functions.LoadData("tblChatLieu", dgvChatLieu);

        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtTKTenChatLieu.Text == "" && txtTKMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hoặc tên chất liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTKMaChatLieu.Focus();
                return;
            }
            else if(txtTKMaChatLieu.Text == "" && txtTKTenChatLieu.Text != "")
            {
                string sql = "SELECT * FROM tblChatLieu WHERE TenChatLieu LIKE N'%" + txtTKTenChatLieu.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvChatLieu, sql);

            }
            else if(txtTKMaChatLieu.Text != "" && txtTKTenChatLieu.Text == "")
            {
                string sql = "SELECT * FROM tblChatLieu WHERE MaChatLieu LIKE N'%" + txtTKMaChatLieu.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvChatLieu, sql);
            }
            else
            {
                string sql = "SELECT * FROM tblChatLieu WHERE  MaChatLieu LIKE N'%" + txtTKMaChatLieu.Text.Trim().ToString()+ "%' AND TenChatLieu LIKE N'%" + txtTKTenChatLieu.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvChatLieu, sql);
            }
            btnBoQua.IsEnabled = true;
        }
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnTimKiem_Click(btnTimKiem, new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
