   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089l hDotNetProjects.Wpf.Extended.Toolkit, Version=5.0.106.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4   ComboButtonControlTemplate  �� 
TargetType#   Xceed.Wpf.Toolkit.SplitButton �Name J� SnapsToDevicePixels � ColumnDefinitions ��Name �� Padding �� CornerRadius d�Name d� 	IsChecked	 �� Mode
 �� RelativeSource �� 
TargetType �� Property �� Value ��Name J� IsHitTestVisible �� Path �� 	Converter<  6Xceed.Wpf.Toolkit.Core.Converters.InverseBoolConverter F�Name  	ArrowDown �Name � AllowsTransparency � HorizontalOffset&   Xceed.Wpf.Toolkit.DropDownButton   DropDownPosition � 	StaysOpen � VerticalOffset �� ElementName   DropDownContent �� Triggers P� Property P� Value �� 
TargetName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      4xctk)http://schemas.xceed.com/wpf/xaml/toolkit  %  &	        7  �� "    .R�� $ MainGrid��$
 True�� �� $��* ��� $
��Auto �.+��� $ ActionBorder��$��0��$ 0q�$��0q�$ 3,0,0,3��#��z�����#3A3A3A$��1,1,0,1q�._�d� $ PART_ActionButton��
�������$��0q�#��z��#��z�� �� *IsOpen+$	 TwoWay=�
 �� *TemplatedParent+���� " d�.���  � �� �� "  d�.R��� $ LeftButtonBackground�����	#00000000$��0q�$ 3,0,0,3��$��0q�$ 3,0,0,0q�._��� .���� $ ActionButtonContent��#��z��#��z��$��Stretch=�#��z��$
��True��#��z���� $ ToggleBorder��$��1��#��z�����#3A3A3A$ 0q�$��0q�$��0,1,1,1q�$ 0,3,3,0��._�d� $ PART_ToggleButton�� �� *IsOpen+$	 TwoWay=�
 �� *TemplatedParent+ �� $ IsOpen�
 �� *TemplatedParent+   ���� " d�.���  � �� �� "  d�.R��� $ RightButtonBackground�����	#00000000$��0q�$ 0,3.3,3.3,0��$��0,0,10,0q�$ 1q�._��� .��� $ 
arrowGlyph��$ False��$	��4,3q�.+�F� $ Arrow��8��  
�������$��5��$��0,0,0,0q�$��8��� $ 
PART_Popup��$
 True��$|�False��$ 0��#��z $ False��$ 1��#��z������ *	IsChecked+$ PART_ToggleButton��.���� $ 3��$��0,2,0,0q����#3A3A3A._��� $ PART_ContentPresenter��#��z  P�  �@J�IsMouseOver$
 True��. ��� $ ToggleBorder�� �@J�Opacity$	 0.7Q��� $ ActionBorder�� �@J�Opacity$	 0.7Q�P�  � {�$ False��. ��� $ ToggleBorder�� �@J�Opacity$	 0.4Q��� $ ActionBorder�� �@J�Opacity$	 0.4Q�