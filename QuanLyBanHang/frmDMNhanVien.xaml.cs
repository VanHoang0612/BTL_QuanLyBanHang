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
    /// Interaction logic for frmDMNhanVien.xaml
    /// </summary>
    public partial class frmDMNhanVien : Window
    {
        public frmDMNhanVien()
        {
            InitializeComponent();
            Functions.LoadData("tblNhanVien", dgvNhanVien);
            defaultTextbox();
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;

        }

        private void ResetValue()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanvien.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = ""; 
            RBNam.IsChecked =false;
            RBNu.IsChecked =false;
            dtNgaySinh.SelectedDate = null;
        }
        private void defaultTextbox()
        {
            txtMaNhanVien.IsEnabled = false;
            txtTenNhanvien.IsEnabled = false;
            txtDiaChi.IsEnabled = false;
            txtSoDienThoai.IsEnabled = false;
            dtNgaySinh.IsEnabled = false;
            RBNam.IsEnabled = false;
            RBNu.IsEnabled = false;
            dgvNhanVien.SelectedItem = null;
        }
        private void dgvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvNhanVien.SelectedItem != null)
            {
                DataRowView? selectionRow = (DataRowView)dgvNhanVien.SelectedItem;
                txtMaNhanVien.Text = selectionRow["MaNhanVien"].ToString();
                txtTenNhanvien.Text = selectionRow["TenNhanVien"].ToString();
                txtDiaChi.Text = selectionRow["DiaChi"].ToString();
                txtSoDienThoai.Text = selectionRow["DienThoai"].ToString();
                dtNgaySinh.SelectedDate = (DateTime?)selectionRow["NgaySinh"];
                if (selectionRow["GioiTinh"].ToString() == "Nam")
                {
                    RBNam.IsChecked=true;
                }
                else
                {
                    RBNu.IsChecked=true;
                }
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnBoQua.IsEnabled = true;
                btnLuu.IsEnabled = false;
                btnThem.IsEnabled = false;
                txtTenNhanvien.IsEnabled = true;
                txtDiaChi.IsEnabled = true;
                txtSoDienThoai.IsEnabled = true;
                dtNgaySinh.IsEnabled = true;
                RBNam.IsEnabled = true;
                RBNu.IsEnabled = true;
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
            txtMaNhanVien.IsReadOnly = false; //cho phép nhập mới
            txtMaNhanVien.Focus();
            dgvNhanVien.SelectedItem = null;
            txtMaNhanVien.IsEnabled = true;
            txtTenNhanvien.IsEnabled = true;
            txtDiaChi.IsEnabled = true;
            txtSoDienThoai.IsEnabled = true;
            dtNgaySinh.IsEnabled = true;
            RBNam.IsEnabled = true;
            RBNu.IsEnabled = true;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            string sql, gt;
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtMaNhanVien.Text.Trim().Length == 0 || txtTenNhanvien.Text.Trim().Length == 0 || txtDiaChi.Text.Trim().Length == 0 || txtSoDienThoai.Text.Trim().Length == 0 || dtNgaySinh.SelectedDate == null || (RBNam.IsChecked == false && RBNu.IsChecked == false))
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(txtSoDienThoai.Text.Trim().Length != 10)
            {
                MessageBox.Show("Số điện thoại bao gồm 10 số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSoDienThoai.Focus();
                return;
            }
            if (RBNam.IsChecked == true) gt = "Nam";
            else gt = "Nữ";
            DateTime? NS = dtNgaySinh.SelectedDate.Value.Date;
            string NSString = NS.Value.ToString("yyyy-MM-dd");
            sql = "UPDATE tblNhanVien SET  TenNhanVien=N'" + txtTenNhanvien.Text.Trim().ToString() +
                    "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + txtSoDienThoai.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinh='" + NSString +
                    "' WHERE MaNhanVien=N'" + txtMaNhanVien.Text + "'";
            Functions.RunSQL(sql);
            ResetValue();
            btnXoa.IsEnabled = true;
            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            Functions.LoadData("tblNhanVien", dgvNhanVien);
            
            defaultTextbox();
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtMaNhanVien.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                sql = "DELETE tblNhanVien WHERE MaNhanVien=N'" + txtMaNhanVien.Text + "'";
                Functions.RunSQL(sql);
                btnXoa.IsEnabled = true;
                btnThem.IsEnabled = true;
                btnSua.IsEnabled = true;
                btnBoQua.IsEnabled = false;
                btnLuu.IsEnabled = false;
                defaultTextbox();
                Functions.LoadData("tblNhanVien", dgvNhanVien);

                ResetValue();
            }
            else
            {
                return;
            }
            
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql, gt;
            
            if(txtMaNhanVien.Text.Trim().Length == 0 || txtTenNhanvien.Text.Trim().Length == 0 || txtDiaChi.Text.Trim().Length == 0 || txtSoDienThoai.Text.Trim().Length ==0 || dtNgaySinh.SelectedDate == null || (RBNam.IsChecked == false && RBNu.IsChecked == false))
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (txtSoDienThoai.Text.Trim().Length != 10)
            {
                MessageBox.Show("Số điện thoại bao gồm 10 số", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSoDienThoai.Focus();
                return;
            }
            sql = "Select MaKhach From tblKhach where MaKhach=N'" + txtMaNhanVien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (RBNam.IsChecked == true) gt = "Nam";
            else gt = "Nữ";
            // chuyen date thanh chuoi
            DateTime? NS = dtNgaySinh.SelectedDate.Value.Date;
            
            string NSString = NS.Value.ToString("yyyy-MM-dd");
            sql = "INSERT INTO tblNhanVien(MaNhanVien,TenNhanVien,GioiTinh, DiaChi,DienThoai, NgaySinh) VALUES (N'" + txtMaNhanVien.Text.Trim() + "',N'" + txtTenNhanvien.Text.Trim() + "',N'" + gt + "',N'" + txtDiaChi.Text.Trim() + "','" + txtSoDienThoai.Text + "','" + NSString + "')";
            Functions.RunSQL(sql);
            Functions.LoadData("tblNhanVien", dgvNhanVien);
            ResetValue();
            btnXoa.IsEnabled = true;
            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            defaultTextbox();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBoQua_Click(object sender, RoutedEventArgs e)
        {
            ResetValue();
            txtMaNhanVien.Text = "";
            txtTenNhanvien.Text = "";
            btnBoQua.IsEnabled = false;
            btnThem.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnLuu.IsEnabled = false;
            defaultTextbox();
            txtTKMaNhanVien.Text = "";
            txtTKTenNhanVien.Text = "";
            Functions.LoadData("tblNhanVien", dgvNhanVien);
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // dua du lieu len khi chon dong datagrid
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            btnBoQua.IsEnabled = true;
            if (txtTKMaNhanVien.Text == "" && txtTKTenNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hoặc tên nhân viên", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTKMaNhanVien.Focus();
                return;
            }
            else if (txtTKMaNhanVien.Text != "" && txtTKTenNhanVien.Text == "")
            {
                string sql = "SELECT * FROM tblNhanVien WHERE MaNhanVien LIKE N'%" + txtTKMaNhanVien.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvNhanVien, sql);
            }
            else if (txtTKMaNhanVien.Text == "" && txtTKTenNhanVien.Text != "")
            {
                string sql = "SELECT * FROM tblNHanVien WHERE TenNhanVien LIKE N'%" + txtTKTenNhanVien.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvNhanVien, sql);
            }
            else
            {
                string sql = "SELECT * FROM tblNhanVien WHERE MaNhanVien LIKE N'%" + txtTKMaNhanVien.Text.Trim().ToString() + "%' AND  TenNhanVien LIKE N'%" + txtTKTenNhanVien.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvNhanVien, sql);
            }
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
