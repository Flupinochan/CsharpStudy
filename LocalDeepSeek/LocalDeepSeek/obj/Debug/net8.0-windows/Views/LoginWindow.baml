   M S B A M L   `   `   `  ����H  DLocalDeepSeek, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:LocalDeepSeek.ViewsLocalDeepSeek.Views  5   	   %    LocalDeepSeek.Views.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:LocalDeepSeek.Views     5   	     � Title$  LoginWindow��5   	   $	��400��5	   	   $	��400��5
   	    � WindowStartupLocation$ CenterScreen=�5   	      WindowStyle#��[  5   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��1.4* �6   �� 5      $��* �6   �� 5      $
��1.3* �6   �� 5      $��* �6   �� 5      $
��1.3* �6   �� 5      $
��1.5* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ TitleTextBlock��5      $��0��5      $��ログイン��5      $��24(�5      $
��Bold"�5      �� 5   
   -   5        $ UsernameTextBlock��5      $��1��5       $��ユーザー名��5!      $
��Bold"�5"      $
��Left=�5#      �� 5%   
   -   5         ��Name$ UsernameTextBox��5%      $��2��5&      $��
0, 6, 0, 6q�5'      �� 5)   
   -   5        $ PasswordTextBlock��5)      $��3��5*      $��パスワード��5+      $
��Bold"�5,      $
��Left=�5-      �� 5/   
   -   5        $ PasswordTextBox��5/      $��4��50      $��
0, 6, 0, 6q�51      � 55   
   $��5��6   $��0, 10, 0, 0q�56       � ColumnDefinitions 57      �� 58      $��* �6#   �� 59      $
��0.1* �6#   �� 5:      $��* �6#   .+�5=      d� -   5         d�Name$ NewUserToggleButton��5=      $��0��5>      $��新規作成��5?      �� 5A      -   5         ��Name$ LoginButton��5A      $��2��5B      $��ログイン��5C      5D      5E      �� 5G   
   -   5        $ PasswordResetTextBlock��5G      $��6��5H      $*��$パスワードをお忘れですか��5I      $��14(�5J      $��Center=�5K       � Cursor$
 Hand��5L        CustomBlueColor#��[ 5M      5Q      5T      