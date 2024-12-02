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
using QuanLyBanHang.Class;
namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for frmDMKhachHang.xaml
    /// </summary>
    public partial class frmDMKhachHang : Window
    {
        public frmDMKhachHang()
        {
            InitializeComponent();
            Functions.LoadData("tblKhach", dgvKhachHang);
            defaultTextbox();
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;
        }

        private void ResetValue()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
        }
        private void defaultTextbox()
        {
            txtMaKhach.IsEnabled = false;
            txtTenKhach.IsEnabled = false;
            txtDiaChi.IsEnabled = false;
            txtSoDienThoai.IsEnabled = false;
            dgvKhachHang.SelectedItem = null;
        }
        private void dgvKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtMaKhach.IsReadOnly = false;
           
            if (dgvKhachHang.SelectedItem != null)
            {
                DataRowView? selectionRow = (DataRowView)dgvKhachHang.SelectedItem;
                txtMaKhach.Text = selectionRow["MaKhach"].ToString();
                txtTenKhach.Text = selectionRow["TenKhach"].ToString();
                txtDiaChi.Text = selectionRow["DiaChi"].ToString();
                txtSoDienThoai.Text = selectionRow["DienThoai"].ToString();
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnBoQua.IsEnabled = true;
                btnLuu.IsEnabled = false;
                txtMaKhach.IsEnabled = true;
                txtMaKhach.IsReadOnly = true;
                txtTenKhach.IsEnabled = true;
                txtDiaChi.IsEnabled = true;
                txtSoDienThoai.IsEnabled = true;
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
            txtMaKhach.IsReadOnly = false; //cho phép nhập mới
            txtMaKhach.Focus();
            dgvKhachHang.SelectedItem = null;
            txtMaKhach.IsEnabled = true;
            txtTenKhach.IsEnabled = true;
            txtDiaChi.IsEnabled = true;
            txtSoDienThoai.IsEnabled = true;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            
            txtMaKhach.IsReadOnly=true;
            string sql;
            if (txtMaKhach.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtMaKhach.Text.Trim().Length == 0 || txtTenKhach.Text.Trim().Length == 0 || txtDiaChi.Text.Trim().Length == 0 || txtSoDienThoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaKhach.Focus();
                return;
            }
            if (txtSoDienThoai.Text.Trim().Length != 10)
            {
                MessageBox.Show("Số điện thoại bao gồm 10 số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSoDienThoai.Focus();
                return;
            }
            sql = "UPDATE tblKhach SET TenKhach=N'" + txtTenKhach.Text.Trim().ToString() + "',DiaChi=N'" +
                txtDiaChi.Text.Trim().ToString() + "',DienThoai='" + txtSoDienThoai.Text.ToString() +
                "' WHERE MaKhach=N'" + txtMaKhach.Text + "'";
            Class.Functions.RunSQL(sql);
            Functions.LoadData("tblKhach", dgvKhachHang);
            ResetValue();

            defaultTextbox();
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;
            btnSua.IsEnabled = true;
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        }


        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
             string sql;
            if (txtMaKhach.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                sql = "DELETE tblKhach WHERE MaKhach=N'" + txtMaKhach.Text + "'";
                Functions.RunSQL(sql);
                btnThem.IsEnabled = true;
                Functions.LoadData("tblKhach", dgvKhachHang);
                ResetValue();
            }
            else
            {
                return;
            }
            
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaKhach.Text.Trim().Length == 0 || txtTenKhach.Text.Trim().Length == 0 || txtDiaChi.Text.Trim().Length == 0 || txtSoDienThoai.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaKhach.Focus();
                return;
            }
            if (txtSoDienThoai.Text.Trim().Length != 10)
            {
                MessageBox.Show("Số điện thoại bao gồm 10 số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSoDienThoai.Focus();
                return;

            }
            sql = "Select MaKhach From tblKhach where MaKhach=N'" + txtMaKhach.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaKhach.Focus();
                return;
            }

            sql = "INSERT INTO tblKhach VALUES (N'" + txtMaKhach.Text.Trim() +
                "',N'" + txtTenKhach.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtSoDienThoai.Text + "')";


            Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            Functions.LoadData("tblKhach", dgvKhachHang);
            ResetValue();
            defaultTextbox();
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;
            btnSua.IsEnabled = true;
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBoQua_Click(object sender, RoutedEventArgs e)
        {
            ResetValue();
            defaultTextbox();
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;
            btnSua.IsEnabled = true;
            txtTKMaKhach.Text = "";
            txtTKTenKhach.Text = "";
            Functions.LoadData("tblKhach", dgvKhachHang);

        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (txtTKMaKhach.Text == "" && txtTKTenKhach.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hoặc tên khách", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTKMaKhach.Focus();
                return;
            }
            else if(txtTKMaKhach.Text != "" && txtTKTenKhach.Text == "")
            {
                string sql = "SELECT * FROM tblKhach WHERE MaKhach LIKE '%" + txtTKMaKhach.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvKhachHang, sql);
            } 
            else if(txtTKMaKhach.Text == "" && txtTKTenKhach.Text != "")
            {
                string sql = "SELECT * FROM tblKhach WHERE TenKhach LIKE N'%" + txtTKTenKhach.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvKhachHang, sql);
            }
            else
            {
                string sql = "SELECT * FROM tblKhach WHERE MaKhach LIKE '%" + txtTKMaKhach.Text.Trim().ToString() + "%' AND  TenKhach LIKE N'%" + txtTKTenKhach.Text.Trim().ToString() + "%'" ;
                Functions.LoadDataFind(dgvKhachHang, sql);
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
