   M S B A M L   `   `   `  ����C  ?IPS_CALC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:IPS_CALC.Veiws.WindowsIPS_CALC.Veiws.Windows  5   	   9!clr-namespace:IPS_CALC.VIewModelsIPS_CALC.VIewModels  5   	   9!clr-namespace:IPS_CALC.ConvertersIPS_CALC.Converters  5
   	   ,    &IPS_CALC.Veiws.Windows.CargoEditWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:IPS_CALC.Veiws.Windows     5   	   0vm!clr-namespace:IPS_CALC.VIewModels     5   	   l hDotNetProjects.Wpf.Extended.Toolkit, Version=5.0.106.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   5EnumConvertor!clr-namespace:IPS_CALC.Converters  5
   	     � Title$  CargoEditWindow��5	   	   $	��310��6!   $	��500��6.    � 	Resources 5      .   (IPS_CALC.Converters.GeneralEnumConverter   GeneralEnumConverter��%   &	          5   
   6-   5      .��5      � .+�5   
   ]� .+�5      �� $	��Top=�5      $��Center=�5      ��5      �� *Id+ �� StringFormat$ Грузик:{0}��5      A� 5       A� Rows$ 1��6   $��Bottom=�5      $��Right=�5      $��5q�5      .+�5      �� $��Ok��6   $��3q�6&   $
��20,3q�61     DialogResultCommand#��[ 5      $
��True��5      �� 5      $��Cancel��6   $��3q�6*   $
��20,3q�65   #��[ 5      $��False��5       5!      � 5#      $��5q�6    � ColumnDefinitions 5$      �� 5%      $
��Auto �6'   �� 5&      �� 5'      $
��Auto �6'    � RowDefinitions 5)      �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   �� 5.      $
��Auto �6$   �� 5/      $
��Auto �6$   �� 50      $
��Auto �6$   �� 51      $
��Auto �6$   .+�54      �� $��0��6   $��0��6*   $��Название��55      $��5q�56      �� 57      $��0��6   $��1��6(   $��3q�59      ��58      ��  �� Path$
 Name�59      �� 5;      $��1��6   $��0��6*   $!��Условная масса��5<      $��5q�5=      �� 5>      $��1��6   $��1��6(   $��3q�5@      ��5?      �� $ Weight�5@      �� 5A      $��1��6   $��2��6*   $
��кг��5B      $��5q�5C      �� 5E      $��2��6   $��0��6*   $��Плотность��5F      $��5q�5G      �� 5H      $��2��6   $��1��6(   $��3q�5J      ��5I      �� $ Density�5J      �� 5K      $��2��6   $��2��6*   $��	кг/м^3��5L      $��5q�5M      �� 5O      $��3��6   $��0��6*   $��Тип��5P      $��5q�5Q      #  Xceed.Wpf.Toolkit.SplitButton  5]      $��3��6&   $��1��63     ComboButtonControlTemplate#��[ 5_   &   ��5^   &   �� *CargoTypeSelected+ �� 	Converter# [  &   Xceed.Wpf.Toolkit.DropDownButton   IsOpen 5`   &   �� *IsNameTypeSbOpened+	 �� Mode$	 TwoWay=�
   DropDownContent
 5b      �� 5c      ��6'   �� *EnumTypeCollections+��5d      �� 5e   "   .R�5f   &   �� ��5g   )   �� *ElementChangedCommand+.��5h   *   �� $��WordEllipsis=�64   $��1 0 0 0q�5i   -   ��5j   -   �� *	Element+# [  5k   '   5l   #   5n      5q      5w      5x      5y      5z      