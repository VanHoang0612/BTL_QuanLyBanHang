   M S B A M L   `   `   `  ����H  DQuanLyBanHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:QuanLyBanHangQuanLyBanHang  5   	   !    QuanLyBanHang.frmDMChatLieu   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:QuanLyBanHang     5   	     � Title$  Quản Lý Chất Liệu��5   	   $	��500��6#   $	��800��60   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   $��58��6*   �� 5      $
��Auto �6   $��64.04��6*   .+�5   
   �� .+�5      �� $��DANH MỤC CHẤT LIỆU��6   $��27��61   $
��Bold"�6=   $��14(�6O   $��0,10,0,0q�6]   $��Center=�6o   �� 5      $��30��6    �� Orientation$ 
Horizontal=�6%   .+�5      �� $��Mã chất liệu: ��6   $��77��61   $
��Left=�6<   $��30,0,0,0q�6W   �� 5      -   5         ��Name$ txtMaChatLieu��5       �� TextWrapping$
 Wrap=�61   $��20,0,0,0q�6E   $��14(�6W   $��Center=�6e   $	��218��6�    �� 
IsReadOnly$
 True��6�   5      �� 5      $��30��6   $ 
Horizontal=�6%   $��	0,10,0,10q�6>   .+�5      �� $��Tên chất liệu: ��6   $��77��62   $
��Left=�6=   $��30,0,0,0q�6X   �� 5      -   5        $ txtTenChatLieu��5      $
 Wrap=�62   $	��218��6F   $��20,0,0,0q�6R   $��14(�6d   $��Center=�6r   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgvChatLieu��5      $��1��6)   $��	10,1,10,2q�66      CanUserAddRows$ False��6I      
IsReadOnly$
 True��6`   	   AutoGenerateColumns$
	 True��6r   6�   �� 5   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   $��0,8,0,0q�6X   $	��714��6i   .+�5      �� -   5        
 ��Name$
 btnThem��5      $��Thêm��6&   $��30��65   $��89��6A   $��	15,0,15,0q�6L    J� RenderTransformOrigin$ 0.5,0.5&�6_   6   �� 5      -   5        $
 btnSua��5      $��Sửa��6%   $��30��63   $��89��6?   $��	15,0,15,0q�6J   6]   �� 5       -   5        $
 btnXoa��5       $
��Xóa��6%   $��30��63   $��89��6?   $��	15,0,15,0q�6J   ${�False��6r   �� 5!      -   5        $
 btnLuu��5!      $
��Lưu��6%   $��30��63   $��89��6?   $��	15,0,15,0q�6J   ${�False��6r   �� 5"      -   5        $
 btnBoQua��5"      $��Bỏ qua��6'   $��30��68   $��89��6D   $��	15,0,15,0q�6O   ${�False��6y   �� 5#      -	   5        $
 btnDong��5#      $��Đóng��6&   $��30��65   $��89��6A   $��	15,0,15,0q�6L   6_   5%      �� 5&   
   $��3��6   $��
10,10,10,0q�6"   .+�5'      �� $��30��6   $ 
Horizontal=�6%   $��0,10,0,0q�6>   .+�5(      �� $��Mã chất liệu:��6   $
��Left=�60   $��90��6K   �� 5)      -
   5        $ txtTKMaChatLieu��5)      $��Center=�63   $��90��6N   6Y   �� 5*      $��Tên chất liệu:��6   $
��Left=�61   $��90��6L   $��20,0,0,0q�6W   �� 5+      -   5        $ txtTKTenChatLieu��5+      $��Center=�64   $	��218��6O   6[   �� 5,      -   5        $
 
btnTimKiem��5,      $��Tìm kiếm��6-   $��20,0,0,0q�6@   $	��120��6R   $��25��6^   $ 9.213,43.316&�6j   6�   5-      5.      50      52      