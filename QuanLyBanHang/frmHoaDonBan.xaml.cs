using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuanLyBanHang.Class;
namespace QuanLyBanHang
{
    /// <summary>
    /// Interaction logic for frmHoaDonBan.xaml
    /// </summary>
    public partial class frmHoaDonBan : Window
    {
        public frmHoaDonBan()
        {
            InitializeComponent();
            Functions.LoadData("tblHoaDon", dgvHDBanHang);
            string sql = "SELECT * FROM tblNhanVien";
            Functions.LoadDataToComboBox(sql, cboMaNhanVien, "MaNhanVien", "MaNhanVien");
            sql = "SELECT * FROM tblKhach";
            Functions.LoadDataToComboBox(sql, cboMaKhachHang, "MaKhach", "MaKhach");
            sql = "SELECT * FROM tblHang";
            Functions.LoadDataToComboBox(sql, cboTenHang, "TenHang", "TenHang");
            defaultTextbox();
            btnBoQua.IsEnabled = false;
        }

        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            dtNgayBan.Text = "";
            cboMaNhanVien.SelectedIndex = -1;
            txtTenNhanVien.Text = "";
            cboMaKhachHang.SelectedIndex = -1;
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
            txtSoLuong.Text = "1";
            cboMaNhanVien.SelectedIndex = -1;
            txtGiamGia.Text = "0";
            txtDonGia.Text = "";
            txtThanhTien.Text = "0";
            txtMaHang.Text = "";
            cboTenHang.SelectedIndex = -1;
            dgvHDBanHang.SelectedItem = null;

        }
         private void defaultTextbox()
        {
            dtNgayBan.IsEnabled = false;
            cboMaNhanVien.IsEnabled = false;
            txtTenNhanVien.IsEnabled = false;
            cboMaKhachHang.IsEnabled = false;
            txtTenKhachHang.IsEnabled = false;
            txtDiaChi.IsEnabled = false;
            txtSoDienThoai.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
            cboTenHang.IsEnabled = false;
            txtGiamGia.IsEnabled = false;
            txtDonGia.IsEnabled = false;
            txtMaHang.IsEnabled = false;
            btnLuu.IsEnabled = false;
            btnIn.IsEnabled = false;
            btnHuy.IsEnabled = false;
            btnThem.IsEnabled = true;
            //
            txtTenNhanVien.IsReadOnly = false;
            txtTenKhachHang.IsReadOnly = false;
            txtDiaChi.IsReadOnly = false;
            txtSoDienThoai.IsReadOnly = false;
            txtSoLuong.IsReadOnly = false;
            txtMaHang.IsReadOnly = false;
            txtGiamGia.IsReadOnly = false;
            txtDonGia.IsReadOnly = false;
            dgvHDBanHang.SelectedItem = null;
        }

        private void WatchData()
        {
            
            txtTenNhanVien.IsEnabled = true;
            txtTenKhachHang.IsEnabled = true;
            txtDiaChi.IsEnabled = true;
            txtSoDienThoai.IsEnabled = true;
            txtSoLuong.IsEnabled = true;
            txtMaHang.IsEnabled = true;
            txtGiamGia.IsEnabled = true;
            txtDonGia.IsEnabled = true;
            btnLuu.IsEnabled = false;
            btnIn.IsEnabled = false;
            btnHuy.IsEnabled = true;
            txtTenNhanVien.IsReadOnly = true;
            txtTenKhachHang.IsReadOnly = true;
            txtDiaChi.IsReadOnly = true;
            txtSoDienThoai.IsReadOnly = true;
            txtSoLuong.IsReadOnly = true;
            txtMaHang.IsReadOnly = true;
            txtGiamGia.IsReadOnly = true;
            txtDonGia.IsReadOnly = true;
           
        }
        private void dgvHDBanHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
           
            if (dgvHDBanHang.SelectedItem != null)
            {
                DataRowView? selectionRow = (DataRowView)dgvHDBanHang.SelectedItem;
                txtMaHDBan.Text = selectionRow["MaHDBan"].ToString();
                dtNgayBan.SelectedDate = (DateTime?)selectionRow["NgayBan"];
                cboMaNhanVien.SelectedValue = selectionRow["MaNhanVien"].ToString() ;
                cboMaKhachHang.SelectedValue = selectionRow["MaKhach"].ToString();
                if (cboMaNhanVien.SelectedValue != null)
                {
                    sql = "SELECT * FROM tblNhanVien WHERE MaNhanVien='" + cboMaNhanVien.SelectedValue.ToString() + "'";
                    DataRow? DataNhanVien = Functions.GetDataRow(sql);
                    if (DataNhanVien != null)
                    {
                        txtTenNhanVien.Text = DataNhanVien["TenNhanvien"].ToString();

                    }
                }
                if (cboMaKhachHang.SelectedValue != null)
                {
                    sql = "SELECT * FROM tblKhach WHERE MaKhach='" + cboMaKhachHang.SelectedValue.ToString() + "'";
                    DataRow? DataKhachHang = Functions.GetDataRow(sql);
                    if (DataKhachHang != null)
                    {
                        txtTenKhachHang.Text = DataKhachHang["TenKhach"].ToString();
                        txtDiaChi.Text = DataKhachHang["DiaChi"].ToString();
                        txtSoDienThoai.Text = DataKhachHang["DienThoai"].ToString();
                    }
                }
                if (!string.IsNullOrEmpty(txtMaHDBan.Text))
                {
                    sql = "SELECT * FROM tblChiTietHDBan WHERE MaHDBan = '" + txtMaHDBan.Text.ToString() + "'";
                    DataRow? DataChiTiet = Functions.GetDataRow(sql);
                    if (DataChiTiet != null)
                    {
                        txtMaHang.Text = DataChiTiet["MaHang"].ToString();
                        txtSoLuong.Text = DataChiTiet["SoLuong"].ToString();
                        txtDonGia.Text = DataChiTiet["DonGia"].ToString();
                        txtGiamGia.Text = DataChiTiet["GiamGia"].ToString();
                        txtThanhTien.Text = DataChiTiet["ThanhTien"].ToString();
                    }

                    if (txtMaHang.Text != "" && txtMaHang.Text !=null)
                    {
                        sql = "SELECT * FROM tblHang WHERE MaHang = '" + txtMaHang.Text.ToString() + "'";
                        DataRow? DataHang = Functions.GetDataRow(sql);
                        if (DataHang != null)
                        {

                            cboTenHang.SelectedValue = DataHang["TenHang"].ToString();
                        }
                    }

                }
                btnLuu.IsEnabled = false;
                btnThem.IsEnabled = false;
                btnBoQua.IsEnabled = true;
            }
            WatchData();

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
            defaultTextbox();
            dtNgayBan.SelectedDate = DateTime.Now;
            txtMaHDBan.IsEnabled = false;
            dtNgayBan.IsEnabled = true;
            cboMaNhanVien.IsEnabled = true;
            cboMaKhachHang.IsEnabled = true;
            txtSoLuong.IsEnabled = true;
            txtSoLuong.IsReadOnly = false;
            txtGiamGia.IsReadOnly = false;
            txtGiamGia.IsEnabled = true;
            txtDonGia.IsEnabled = true;
            cboTenHang.IsEnabled = true;
            btnLuu.IsEnabled = true;
            btnIn.IsEnabled = false;
            btnHuy.IsEnabled = false;
            btnThem.IsEnabled = false;
            dgvHDBanHang.SelectedItem = null;
            btnBoQua.IsEnabled = true;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            string? sql, sl, soLuongCon, maHang;
            if (txtMaHDBan.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                sl = txtSoLuong.Text.ToString();
                maHang = txtMaHang.Text.ToString();
                DataRow? DataHang = Functions.GetDataRow("SELECT * FROM tblHang WHERE MaHang = '" + maHang + "'");
                if(DataHang != null)
                {
                    soLuongCon = DataHang["SoLuong"].ToString();
                    double slm = Convert.ToDouble(sl) + Convert.ToDouble(soLuongCon);
                    sql = "UPDATE tblHang SET SoLuong = " + slm + "WHERE MaHang =N'" + maHang + "'";
                    Functions.RunSQL(sql);
                }

                sql = "DELETE FROM tblHoaDon WHERE MaHDBan=N'" + txtMaHDBan.Text + "'";
                Functions.RunSQL(sql);
                sql = "DELETE FROM tblChiTietHDBan WHERE MaHDBan=N'" + txtMaHDBan.Text + "'";
                Functions.RunSQL(sql);
                ResetValues();
                btnThem.IsEnabled = true;
                Functions.LoadData("tblHoaDon", dgvHDBanHang);
                //ResetValues();
                defaultTextbox();
            }
            else
            {
                return;
            }
            
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            double sl, slc;
            if (!dtNgayBan.SelectedDate.HasValue || cboMaNhanVien.SelectedValue == null || cboMaKhachHang.SelectedValue == null || cboTenHang.SelectedValue == null || txtSoLuong.Text == ""|| txtGiamGia.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }
            if (Convert.ToDouble(txtSoLuong.Text) < 1)
            {
                MessageBox.Show("Số lượng phải lớn hơn bằng 1!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            DataRow? DataHang = Functions.GetDataRow("SELECT * FROM tblHang WHERE TenHang = N'"+ cboTenHang.SelectedValue.ToString()+ "'");
            if (DataHang != null)
            {
                txtMaHang.Text = DataHang["MaHang"].ToString();
                txtDonGia.Text = DataHang["DonGiaBan"].ToString();
                sl = Convert.ToDouble(DataHang["SoLuong"].ToString());
                slc = sl- Convert.ToDouble(txtSoLuong.Text);
                if(slc < 0)
                {
                    MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                sql = "INSERT INTO tblHoaDon(NgayBan, MaNhanVien, MaKhach, TongTien) VALUES ('" +
                       dtNgayBan.SelectedDate.Value.ToString("yyyy-MM-dd") + "',N'" + cboMaNhanVien.SelectedValue + "',N'" +
                       cboMaKhachHang.SelectedValue + "'," + Convert.ToDouble(txtThanhTien.Text) + ")";
                Functions.RunSQL(sql);
                // lay MaHDBan vua insert
                sql = "SELECT TOP 1 * FROM tblHoaDon ORDER BY ID DESC";
                DataRow? DataHangInsert = Functions.GetDataRow(sql);
                if (DataHangInsert != null && txtMaHang.Text != null)
                {
                    string? MaHDBanInsert = DataHangInsert["MaHDBan"].ToString();
                    sql = "INSERT INTO tblChiTietHDBan(MaHDBan,MaHang,SoLuong,DonGia, GiamGia,ThanhTien) VALUES(N'" + MaHDBanInsert +
                    "',N'" + txtMaHang.Text.ToString() + "'," + txtSoLuong.Text + ","
                    + txtDonGia.Text + "," + txtGiamGia.Text + "," + Convert.ToDouble(txtThanhTien.Text) + ")";
                    Functions.RunSQL(sql);
                    // cap nhat lai so luong trnog tblHang
                    sql = "UPDATE tblHang SET SoLuong =" + slc + " WHERE MaHang= N'" + txtMaHang.Text.ToString() + "'";
                    Functions.RunSQL(sql);
                }
                
            }
            Functions.LoadData("tblHoaDon", dgvHDBanHang);
            ResetValues();
            defaultTextbox();
            btnBoQua.IsEnabled = false;
            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private void btnBoQua_Click(object sender, RoutedEventArgs e)
        {
            Functions.LoadData("tblHoaDon", dgvHDBanHang);
            ResetValues();
            defaultTextbox ();
            btnBoQua.IsEnabled = false;
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cboMaKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
            if(cboMaKhachHang.SelectedValue == null)
            {
                return;
            }
            else
            {
                sql = "SELECT * FROM tblKhach WHERE MaKhach='" + cboMaKhachHang.SelectedValue.ToString() + "'";
                DataRow? DataKhachHang = Functions.GetDataRow(sql);
                if (DataKhachHang != null)
                {
                    txtTenKhachHang.Text = DataKhachHang["TenKhach"].ToString();
                    txtDiaChi.Text = DataKhachHang["DiaChi"].ToString();
                    txtSoDienThoai.Text = DataKhachHang["DienThoai"].ToString();
                }
            }
        }

        private void cboMaNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
            if (cboMaNhanVien.SelectedValue == null)
            {
                return;
            }
            else
            {
                sql = "SELECT * FROM tblNhanVien WHERE MaNhanVien='" + cboMaNhanVien.SelectedValue.ToString() + "'";
                DataRow? DataNhanVien = Functions.GetDataRow(sql);
                if (DataNhanVien != null)
                {
                    txtTenNhanVien.Text = DataNhanVien["TenNhanvien"].ToString();

                }
            }
        }

        
        private void cboTenHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string sql;
            if (cboTenHang.SelectedValue == null)
            {
                return;
            }
            else
            {
                sql = "SELECT * FROM tblHang WHERE TenHang = N'" + cboTenHang.SelectedValue.ToString() + "'";
                DataRow? DataHang = Functions.GetDataRow(sql);
                if (DataHang != null)
                {
                    txtMaHang.Text = DataHang["MaHang"].ToString();
                    txtDonGia.Text = DataHang["DonGiaBan"].ToString();
                    TinhTong();
                }
            }
        }
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            if(txtTKMaHDBan.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hóa đơn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                txtTKMaHDBan.Focus();
                return;
            }
            else
            {
                string sql = "SELECT * FROM tblHoaDon WHERE MaHDBan LIKE N'%"+ txtTKMaHDBan.Text.Trim().ToString() +"%'";
                Functions.LoadDataFind(dgvHDBanHang, sql);

            }
            btnBoQua.IsEnabled = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnTimKiem_Click(btnTimKiem, new RoutedEventArgs(Button.ClickEvent));
            }
        }
        // tinh thanh tien
        private void TinhTong()
        {
            try
            {
                if(txtSoLuong.Text != "" && txtDonGia.Text != "" && txtGiamGia.Text != "")
                {
                    int soLuong = int.Parse(txtSoLuong.Text.Trim());
                    double donGia = Convert.ToDouble(txtDonGia.Text.Trim());
                    double giamGia = Convert.ToDouble(txtGiamGia.Text.Trim());

                    // Tính tổng tiền
                    double tongTien = (soLuong * donGia) - (giamGia * (soLuong * donGia))/100;

                    // Hiển thị kết quả vào TextBox
                    txtThanhTien.Text = tongTien.ToString("N0");
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSoLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            TinhTong();
        }

        private void txtGiamGia_TextChanged(object sender, TextChangedEventArgs e)
        {
            TinhTong();
        }

       
    }
}
