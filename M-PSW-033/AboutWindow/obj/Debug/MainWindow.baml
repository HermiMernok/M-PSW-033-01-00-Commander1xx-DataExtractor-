   M S B A M L   `   `   `  ����F  BAboutWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AboutWindowAboutWindow  5   	       AboutWindow.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:AboutWindow     5   	     � Title$  About��5   	   $	��400��6   $	��600��6$   $	��600��60   $	��400��6?    � Icon$% Resources/Icons/Mernok_Icon.ico��6O   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5      $	��130 �6   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ ApplicationName��5      $��Software Name��6)   $��16(�6A   $
��Bold"�6O   $
��Left=�6a   $��Center=�6|   $��1��6�   $��0��6�   $��2��6�   $
��10,0q�6�   �� 5   
   -   5        $ ApplicationVersion��5      $��VYY.XX��6,   $��16(�6=   $
��Bold"�6K   $
��Left=�6]   $��Center=�6x   $��2��6�   $��0��6�   $��2��6�   $
��10,0q�6�   �� 5   
   -   5        $ ApplicationCopyRight��5      $*��$Copyright © 2018 Mernok Elektronik ��6.   $��16(�6\   $
��Bold"�6j   $
��Left=�6|   $��Center=�6�   $��3��6�   $��0��6�   $��2��6�   $
��10,0q�6�   �� 5   
   -   5         ��Name$ MernokElektronikLogo��5       �� BitmapScalingMode$ HighQuality=�6.   $��1��6\   $��4��6i   $��2��6z   $��2��6�   $%��Resources/Icons/Mernok_Logo.png��6�   $��0,5,10,5q�6�   $��Right=�6�   $	��244��6�   �� 5    
   -   5         ��Name$ MernokElektronikWebsite��5       $��6��65   $��0��6B   $��4��6R   $��12(�6f   $
��Bold"�6t   $��Center=�6�   $��Center=�6�   �� 6�   -   5        $��http://www.mernok.co.za?�5    �   
���   �.�6/  www.mernok.co.za6B  6N  �� 5!   
   -   5        $ MernokElektronikSupport��5!      $��7��65   $��0��6B   $��4��6R   $��12(�6f   $
��Bold"�6t   $��Center=�6�   $��Center=�6�   �� 6�   -   5        $!��mailto:support@mernok.co.za?�5!   �   
���   �.�62  support@mernok.co.za6I  6U  �� 5"   
   -	   5         ��Name$ Button_Exit��5"      $��OK��6&   $��3��63   $��Right=�6C   $��8��6_   $��Center=�6l   $��76��6�   $��
0,15,15,15q�6�   �� 5#   
   -
   5        $ ApplicationSummary��5#      $
��Left=�60   $��
10,10,0,10q�6K   $��5��6_   $
��Wrap=�6l   $��4��6�   $
��Auto��6�   $
��Auto��6�   $G��AInsert a short summary of the purpose of the software application��6�   5$      5%      