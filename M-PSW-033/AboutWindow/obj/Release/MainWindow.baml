   M S B A M L   `   `   `  ���� F  BAboutWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AboutWindowAboutWindow      AboutWindow.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:AboutWindow       � Title$  About��$	��400��$	��600��$	��600��$	��400�� � Icon$ Mernok_Icon.ico��.���  � RowDefinitions �� $��20 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� �� $��20 ��� $��20 ��� $��50 � � ColumnDefinitions �� $	��196 ��� $	��132 ��� �� .+��� -    ��Name$ ApplicationName��$��Application Name��$��16(�$
��Bold"�$
��Left=�$��Center=�$��1��$��0��$��2��$
��10,0q��� -   $ ApplicationVersion��$��Application Version��$��16(�$
��Bold"�$
��Left=�$��Center=�$��2��$��0��$��2��$
��10,0q��� -   $ ApplicationCopyRight��$*��$Copyright © 2017 Mernok Elektronik ��$��16(�$
��Bold"�$
��Left=�$��Center=�$��3��$��0��$��2��$
��10,0q��� -    ��Name$ MernokElektronikLogo�� �� BitmapScalingMode$ HighQuality=�$��1��$��4��$��2��$��2��$��Mernok_Logo.png��$��0,5,10,5q�$��Right=�$	��244���� -    ��Name$ MernokElektronikWebsite��$��6��$��0��$��4��$��12(�$
��Bold"�$��Center=�$��Center=��� -   $��http://www.mernok.co.za?�
���   �.�www.mernok.co.za�� -   $ MernokElektronikSupport��$��7��$��0��$��4��$��12(�$
��Bold"�$��Center=�$��Center=��� -   $!��mailto:support@mernok.co.za?�
���   �.�support@mernok.co.za�� -	    ��Name$ button_Exit��$��OK��$��3��$��Right=�$��8��$��Center=�$��76��$��
0,15,15,15q��� -
   $ TextBlock_About_Info��$
��Left=�$��	10,10,0,0q�$��5��$
��Wrap=�$	��Top=�$��4��$��79��$	��572���� $m�gThe LightPro Suite connects to a Titan FLOW through a CAN Tester. The application allows the following:���� �� $
�    ���� $%�- Change SID and CAN bus speed.���� �� $
�    ���� $L�F- Full demonstration of a FLOW unit. (Demonstration Password Required)���� �� $
�    ���� $H�B- Functional testing of a FLOW unit (Technician Password Required)���� �� 