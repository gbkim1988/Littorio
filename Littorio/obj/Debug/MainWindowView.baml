   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.4.1.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   C ?Littorio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Littorio.ArmamentLittorio.Armament 5   	   #clr-namespace:LittorioLittorio 5
   	      Littorio.MainWindowView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   R NMahApps.Metro.IconPacks, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null=	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   .Armamentclr-namespace:Littorio.Armament  5   	   ;
Converters*http://metro.mahapps.com/winfx/xaml/shared   5	   	   "localclr-namespace:Littorio   5
   	     � Title$  Littorio Class Battleship��5   	   $��0q�5   	   (   "MahApps.Metro.Controls.MetroWindow   	GlowBrush
 �   � � 
ResizeMode$ CanResizeWithGrip=�5   	      WindowTransitionsEnabled .   � WindowStartupLocation$ CenterScreen=�5   	   $	��869��5   	   $
��1600��6   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   $��5q�5      $
��Left=�5       $��Stretch=�5!      .@�5"      m� $��Theme��6   ��6&   �� *	AppThemes+ �� Mode$ OneWay=�5#      =   7MahApps.Metro.Controls.MetroAnimatedSingleRowTabControl  5$      -   5          Name$ MainTabControl��5%   	   $��1��6!   $��	0,10,0,12q�6.   $��2��6A   .@�5+   
   )   #MahApps.Metro.Controls.MetroTabItem  $��Vanguard��6    .��5,      $  Littorio.Armament.VanguardView  5-      59      �� 5;      $��2��6   .@�5<   
   �� .��6   Littorio DEV Application62   �� 5=   
      MetroStatusBarSeparator#��[  6   �� 5>   
   .��6   Selected Tab:6'   �� 5?   
   ��6   ��  �� ElementName$ MainTabControl��	 �� Path$	 SelectedItem.Header�$ OneWay=�5@      5B      5C      