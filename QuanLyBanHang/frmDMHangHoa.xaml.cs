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
using System.IO;
namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for frmDMHangHoa.xaml
    /// </summary>
    public partial class frmDMHangHoa : Window
    {
        public frmDMHangHoa()
        {
            InitializeComponent();
            Functions.LoadData("tblHang", dgvHang);
            string sql = "SELECT * FROM tblChatLieu";
            Functions.LoadDataToComboBox(sql, cboMaChatLieu, "MaChatLieu", "TenChatLieu");
            defaultTextbox();
            btnLuu.IsEnabled = false;
            btnBoQua.IsEnabled = false;
        }
        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cboMaChatLieu.SelectedIndex = -1;
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            
            txtAnh.Text = "";
            picAnh.Source = null;
            txtGhiChu.Text = "";
        }
        private void defaultTextbox()
        {
            txtMaHang.IsEnabled = false;
            txtTenHang.IsEnabled = false;
            cboMaChatLieu.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
            txtDonGiaBan.IsEnabled = false;
            txtDonGiaNhap.IsEnabled = false;
            txtAnh.IsEnabled = false;
            txtGhiChu.IsEnabled = false;
            dgvHang.SelectedItem = null;
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           
            if (dgvHang.SelectedItem != null)
            {
                DataRowView? selectionRow = (DataRowView)dgvHang.SelectedItem;
                txtMaHang.Text = selectionRow["MaHang"].ToString();
                txtTenHang.Text = selectionRow["TenHang"].ToString();
                txtSoLuong.Text = selectionRow["SoLuong"].ToString();
                txtDonGiaNhap.Text = selectionRow["DonGiaNhap"].ToString();
                txtDonGiaBan.Text = selectionRow["DonGiaBan"].ToString();
                txtAnh.Text = selectionRow["Anh"].ToString();
                txtGhiChu.Text = selectionRow["GhiChu"].ToString();
                string? imagePath = txtAnh.Text; // Lấy đường dẫn ảnh từ TextBox
               
                    // Tạo BitmapImage từ đường dẫn
                if(imagePath != null)
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                    // Gán BitmapImage vào Image control
                    picAnh.Source = bitmapImage;
                }
                else
                {
                    picAnh = null;
                }

                cboMaChatLieu.SelectedValue = selectionRow["MaChatLieu"].ToString() ;
                txtTenHang.IsEnabled = true;
                cboMaChatLieu.IsEnabled = true;
                txtSoLuong.IsEnabled = true;
                txtDonGiaBan.IsEnabled = true;
                txtDonGiaNhap.IsEnabled = true;
                txtAnh.IsEnabled = true;
                txtGhiChu.IsEnabled = true;
                btnBoQua.IsEnabled = true;
                btnThem.IsEnabled = false;
                btnLuu.IsEnabled = false;
            }
            

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;
            btnBoQua.IsEnabled = true;
            btnLuu.IsEnabled = true;
            btnThem.IsEnabled = false;
            ResetValues();
            txtMaHang.Focus();
            txtMaHang.IsEnabled = true;
            txtTenHang.IsEnabled = true;
            cboMaChatLieu.IsEnabled = true;
            txtSoLuong.IsEnabled = true;
            txtDonGiaBan.IsEnabled = true;
            txtDonGiaNhap.IsEnabled = true;
            txtAnh.IsEnabled = true;
            txtGhiChu.IsEnabled = true;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaHang.Focus();
                return;
            }
            if (txtMaHang.Text.Trim().Length == 0 || txtTenHang.Text.Trim().Length == 0 || cboMaChatLieu.Text.Trim().Length == 0 || txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaHang.Focus();
                return;
            }
           
            sql = "UPDATE tblHang SET TenHang=N'" + txtTenHang.Text.Trim().ToString() +
                "',MaChatLieu=N'" + cboMaChatLieu.SelectedValue.ToString() +
                "',SoLuong=" + txtSoLuong.Text + ",DonGiaNhap=" + txtDonGiaNhap.Text + ",DonGiaBan=" +txtDonGiaBan.Text +
                ",Anh='" + txtAnh.Text + "',Ghichu=N'" + txtGhiChu.Text + "' WHERE MaHang=N'" + txtMaHang.Text + "'";
            Functions.RunSQL(sql);
            Functions.LoadData("tblHang", dgvHang);
            ResetValues();
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            btnThem.IsEnabled = true;
            defaultTextbox();
            MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                sql = "DELETE tblHang WHERE MaHang=N'" + txtMaHang.Text + "'";
                Functions.RunSQL(sql);
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnBoQua.IsEnabled = false;
                btnLuu.IsEnabled = false;
                btnThem.IsEnabled = true;
                defaultTextbox();
                Functions.LoadData("tblHang", dgvHang);
                ResetValues();
            }
            else
            {
          
                return;
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            if (txtMaHang.Text.Trim().Length == 0 || txtTenHang.Text.Trim().Length == 0 || cboMaChatLieu.Text.Trim().Length == 0 || txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "SELECT MaHang FROM tblHang WHERE MaHang=N'" + txtMaHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "SELECT MaHang FROM tblHang WHERE TenHang=N'" + txtTenHang.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Hàng này đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTenHang.Focus();
                return;
            }

            sql = "INSERT INTO tblHang(MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap, DonGiaBan,Anh,Ghichu) VALUES(N'"
                + txtMaHang.Text.Trim() + "',N'" + txtTenHang.Text.Trim() +
                "',N'" + cboMaChatLieu.SelectedValue.ToString() +
                "'," + txtSoLuong.Text.Trim() + "," + txtDonGiaNhap.Text +
                "," + txtDonGiaBan.Text + ",'" + txtAnh.Text + "',N'" + txtGhiChu.Text.Trim() + "')";

            Functions.RunSQL(sql);
            ResetValues();
            Functions.LoadData("tblHang", dgvHang);

            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            btnThem.IsEnabled = true;
            defaultTextbox();
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnBoQua_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
            defaultTextbox();
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnBoQua.IsEnabled = false;
            btnLuu.IsEnabled = false;
            btnThem.IsEnabled = true;
            txtTKMaHang.Text = "";
            txtTKTenHang.Text = "";
            Functions.LoadData("tblHang", dgvHang);

        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string? imagePath = txtAnh.Text.Trim(); // Lấy đường dẫn ảnh từ TextBox

            // Tạo BitmapImage từ đường dẫn
            if (!string.IsNullOrEmpty(imagePath))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePath));
                // Gán BitmapImage vào Image control
                picAnh.Source = bitmapImage;
            }
            else
            {
                picAnh.Source = null;
               
            }
        }

        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            if (txtTKMaHang.Text == "" &&txtTKTenHang.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hoặc tên hàng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTKMaHang.Focus();
                return;
            }
            else if (txtTKMaHang.Text == "" && txtTKTenHang.Text != "")
            {
                string sql = "SELECT * FROM tblHang WHERE TenHang LIKE N'%" + txtTKTenHang.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvHang, sql);

            }
            else if (txtTKMaHang.Text != "" && txtTKTenHang.Text == "")
            {
                string sql = "SELECT * FROM tblHang WHERE MaHang LIKE N'%" + txtTKMaHang.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvHang, sql);
            }
            else
            {
                string sql = "SELECT * FROM tblHang WHERE MaHang LIKE N'%" + txtTKMaHang.Text.Trim().ToString() + "%' AND TenHang LIKE N'%" + txtTKTenHang.Text.Trim().ToString() + "%'";
                Functions.LoadDataFind(dgvHang, sql);
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
