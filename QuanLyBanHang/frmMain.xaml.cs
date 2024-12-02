using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
        }

        private void mnuThoat_Click(object sender, RoutedEventArgs e)
        {
            Class.Functions.Disconnect();
            Application.Current.Shutdown();
        }
        private void ChatLieu_Click(object sender, RoutedEventArgs e)
        {
            frmDMChatLieu frmChatLieu = new frmDMChatLieu(); // khoi tao doi tuong
            frmChatLieu.ShowDialog();
        }
        private void NhanVien_Click(object sender, RoutedEventArgs e) { 
            frmDMNhanVien frmNhanVien = new frmDMNhanVien();
            frmNhanVien.ShowDialog();
        }
        private void KhachHang_Click(object sender, RoutedEventArgs e)
        {
            frmDMKhachHang frmKhachHang = new frmDMKhachHang();
            frmKhachHang.ShowDialog();
        }
        private void HangHoa_Click(object sender, RoutedEventArgs e)
        {
            frmDMHangHoa frmHangHoa = new frmDMHangHoa();
            frmHangHoa.ShowDialog();
        }
        private void HoaDonBan_Click(object sender, RoutedEventArgs e)
        {
            frmHoaDonBan frmHDBan = new frmHoaDonBan();
            frmHDBan.ShowDialog();
        }

       
    }
}
