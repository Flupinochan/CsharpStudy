   M S B A M L   `   `   `  ����G  CFrameResizer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FrameResizerFrameResizer  5   	       FrameResizer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FrameResizer     5   	   c _MaterialDesignThemes.MahApps, Version=5.2.2.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.2.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	      MaterialDesignWindow#��[  5
   	     � Title$  FrameResizer��5   	   $	��450��5   	   $	��520��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ FinishedDialog��5       $��Stretch=�5       $��Stretch=�5          ApplyBlurBackground$
 True��5          
BlurRadius$ 5Q�5          CloseOnClickAway$
 True��5          DialogContent 5   
   � 5      $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� $��0��6   $!��処理が完了しました��5      $��Center=�5       $��Center=�5!      $��16(�5"       �� Padding$ 0, 0, 0, 14q�5#      �� 5$      -   5        	 ��Name$	 CloseDialogButton��5$      $��5��5%      $��Center=�5&      $��Center=�5'      $��OK��5(         MaterialDesignOutlinedButton#��[ 5*      {�5)      �� *DataContext.ControlsEnabled+
 �� RelativeSource
 �� *FindAncestor+ �� AncestorType
 Window5*   K   5+      .��5/   
   �� $
��Auto=�6   $
��Auto=�50      .��51      � $��20q�6    52      �� 53      $
��Auto �6$   �� 54      $
��Auto �6$   �� 55      $
��Auto �6$   �� 56      $
��Auto �6$   �� 57      $
��Auto �6$   �� 58      $��* �6$   .+�5;      � $��0��6    � ColumnDefinitions 5<      �� 5=      $
��Auto �6+   �� 5>      $
��Auto �6+   �� 5?      $��* �6+   .+�5B      �� -   5        $	 SelectFileButton��5B      $��0��5C      $	��Top=�5D        MaterialDesignIconButton#��[ 5F       � ToolTip$ MaterialDesignIconButton��5G      {�5E      �� *DataContext.ControlsEnabled+
 �� *FindAncestor+
 Window.��5I      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ FilePlusOutline=�62   5J      �� 5L      -   5        $	 SelectFolderButton��5L      $��1��5M      $	��Top=�5N      #��[ 5P      $ MaterialDesignIconButton��5Q      {�5O      �� *DataContext.ControlsEnabled+
 �� *FindAncestor+
 Window.��5S        $ FolderPlusOutline=�62   5T      9� 5V      $��2��6   $��Stretch=�5W      $$��選択した画像ファイル��5X      .��5Y      �� -   5         ��Name$ SelectedFileListBox��5Y   "   $��Stretch=�5[      $	��164��5\      $
��Auto=�5]      {�5Z      �� *	IsChecked+ �� ElementName$ EnableListBox����5^      �� ��5_      �� 5`   "    �� 
TargetType" ��6(    �� BasedOn# [B��6A   .�5a   &   ��  �� Property � �� �� Value$ 15, 0, 0, 0q�6?   5b   #   5d      5e      5f      � 5h      $��1��6    5i      �� 5j      $
��Auto �6+   �� 5k      $
��Auto �6+   .+�5n      �� -   5        $	 SelectOutputFolderButton��5n      $��0��5o      $��0, 10, 0, 0q�5p      #��[ 5r      $ MaterialDesignIconButton��5s      {�5q      �� *DataContext.ControlsEnabled+
 �� *FindAncestor+
 Window.��5u        $ FolderPlusOutline=�62   5v      �� 5x      -   5         ��Name$ OutputFolderTextBox��5x      $��1��5y      $��15, 10, 0, 0q�5z      $��Center=�5{      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$L F出力フォルダ ※デフォルトでは画像を上書きします��5|         FloatingScale$	 0.8Q�5}      .  (MaterialDesignThemes.Wpf.TextFieldAssist   TextBoxViewMargin$ 0 10 0 0q�5~       % !MaterialDesignFloatingHintTextBox#��[ 5      5�      � 5�      $��2��6   $��15, 28, 0, 0q�5�       5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6    $ 0, 0, 0, 10q�5�      $f��`選択した方を基準にリサイズします ※Zennでは最大Widthが708pxのようです��5�      � 5�      $��1��6   $��0, 0, 0, 10q�6(    5�      �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   .+�5�      	� -   5         	�Name$ WidthRadioButton��5�   &   $��0��5�   "   $��Width��5�   "     MaterialDesignRadioButton#��[ 5�   "    	� 	GroupName$ WidthHeightGroup��5�   "    d� 	IsChecked$
 TrueY�5�   "   ,  &MaterialDesignThemes.Wpf.DecimalUpDown  5�      -	   5          Name$ WidthDecimalUpDown��5�   7   $��1��5�   3   �  �MaterialDesignThemes.Wpf.UpDownBase`1[[System.Decimal, System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]   	ValueStep$ 1~�5�   3      Minimum$ 1~�5�   3   $��67, 3, 0, 0q�5�   3      Value$	 708~�5�   3   5�      � 5�      $��2��6    5�      �� 5�      $
��Auto �6/   �� 5�      $
��Auto �6/   .+�5�      	� -
   5        $ HeightRadioButton��5�   &   $��0��5�   "   $��Height��5�   "   #��[ 5�   "   $ WidthHeightGroup��5�   "   5�   "     5�      -   5        $ HeightDecimalUpDown��5�   7   $��1��5�   3   $ 1~�5�   3   $ 1~�5�   3   $��62, 3, 0, 0q�5�   3   $ 0~�5�   3   ${�False��5�   3   5�      5�      � 5�      $��3��6   $��15, 24, 0, 0q�5�       5�      �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6    $ 
0, 5, 0, 0q�5�      $��枠線の色 (Hex)��5�      �� 5�      -   5        $ BorderColorTextBox��5�      $��1��5�      $��36, 0, 0, 0q�5�      $��
0, 4, 0, 0q�5�      $	��100��5�      $	 0.1Q�5�      $ 0 0 0 4q�5�      #��[ 5�      $��#444444��5�      5�      � 5�      $��4��6   $��15, 24, 0, 0q�5�       5�      �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      �� $��0��6    $ 
0, 5, 0, 0q�5�      $��枠線の太さ (px)��5�        5�      -   5        $ BorderSizeDecimalUpDown��5�   3   $��1��5�   /   $ 1~�5�   /   $ 1~�5�   /   $��35, 0, 0, 0q�5�   /   $ 1~�5�   /   5�      �� 5�      -   5        $	 ExecuteButton��5�      $��5��5�      $��Right=�5�      $��Bottom=�5�      $��実行��5�      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist    IsIndeterminate$  False��5�      !   IsIndicatorVisible$! False��5�      "   Value$" -1Q�5�      #��[ 5�      {�5�      �� *DataContext.ControlsEnabled+
 �� *FindAncestor+
 Window5�   K   5�      5�      5�      5�      