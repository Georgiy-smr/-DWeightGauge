   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089&    System.Windows.Controls.DataGrid  �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions �� CornerRadius �� 
StartPoint �� EndPoint � Color	 � Offset
 �� Color � MappingMode=  7System.Windows.Controls.Primitives.DataGridColumnHeader   ControlMarginStyle  MyWindowStyle  TextBoxControlTemplate ��Name J� SnapsToDevicePixels �� Padding ��Name �� Triggers P� Property P� Value �� 
TargetName �� RelativeSource �� Path �� BasedOn �Name'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8   2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime  p� Value! ��Name�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %�  '         '�� �    '  �    &	  �    '�� �    &	 �    '��     &	 �    '�� 9	    7   ']� �
    '�� �
    'o� �
    '��     �� "    .���  �@  AutoGenerateColumns$ False����  �@  CanUserAddRows$ False����  �@  CanUserDeleteRows$ False����  �@  GridLinesVisibility$ Vertical=���  �@  VerticalGridLinesBrush
 �������   �@  AlternatingRowBackground
 ���� ��  �@  HeadersVisibility$ Column=���  � ��
 �������  �@  RowBackground
 ������� "  ��.���  � �� �� " ��.R��  �� $
��Auto ��� $��* �.+��� $��0��$��1q�$ 2,2,0,0������ $	 0,0&�$	 0,1&���� � $
 Gray��$		 0.0Q�� $ Black��$		 1.0Q����� $	 0,0&�$	 0,1&���� � $
 Gray��$		 0.0Q�� $ Honeydew��$		 1.0Q�._��� $��4q�$��Header��$
��True���� $��1��$��1,0,1,1q�$ 0,0,2,2������ $
 Black������ $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�.��� $ Orange��$	 0Q�� $ 	DarkKhaki��$	 1Q�._��� $��4q��� "   .���  � ��
 �������  � ��
 �   ���  � ��
 �������  � ��$ 0q���  �@J�SnapsToDevicePixels$
 True����  � ��$
 Left=���  � ��$ 0����  � ��$ 30����  �@�Cursor$
 Hand���� "  ��.���  � ��$ 5q��� "  ��.���  � ��
 ������� .���  � ��
 ������� "  ��.���  � ��
 �������  �@��
CaretBrush
 �������  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ #383B40��� $ 	#FF7F7E7E��$	 1Q���  � ��$	 4,0q��� " ��.R��� $ border��#��z��$��0q�#��z��#��z��$
 True��$ 2��$��24��$��0q�$ 0q�$��Center=�._��� $ PART_ContentHost��$|�False��$��Hidden=�$��Hidden=�$��Center=� P�  � {�$ False��. ��� $ border�� �@J�Opacity$
 0.56Q�P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �	#FF7EB4EAP�  �@J�IsKeyboardFocused$
 True��. ��� $ border�� � �� �	#FF569DE5P�  �@=�HasError$
 True��. ���  ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent� �@�ToolTip��  � ��
 �  ���� $ border�� � ��
 �  ���� $ border�� � ��$ 1q���  �@^�InitialShowDelay$ 0����  �@^�BetweenShowDelay$ 0����  �@^�BetweenShowDelay$ 0����  �@_�IsOpen$
 True����  �@^�ShowDuration$ 60000����  Z�  �@=�ErrorTemplate�� "  ��8   .���  � ��
 �������  �@J�Opacity$ 1Q���  � ��$ 12(���  �@��
LineHeight$ 14����  � ��$ 24����  � �� �� " ��.R��� $ Border��$ 3�����#383B40$��0q�$	 4,0q�._��� $��Center=�$��Center=�$��0,-2,4,0q� P�  �@J�IsMouseOver$
 True��. ��� $ Border�� � �� �#403D42�� "  ]�.���  � ��
 ������� "  ��.���  � ��
 ������� "  o�.���  � ��
 ������� "  ��.���  � �� �� " ��.R�� $
 Root��   SelectionStates��.   
Unselected��  Selected��. �� .���� $ Border��$c ](Panel.Background).                     (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��$
 Gray��r� $ (Border.BorderThickness)�$ Border��.	�  $ 0��$  1,1,1,0q�  CommonStates��.   
Normal��  	MouseOver��  Disabled��. �� .���� $ Border��$c ](Panel.Background).                     (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��$
 Gray���� $ Border��$G A(Border.BorderBrush).                     (SolidColorBrush.Color)�.g�  $ 0��$
 Gray��.+��� $ Border��$��0,0,-4,0q�$��1,1,1,1q�$ 2,12,0,0������ $

 Gray������ $	 0,0&�$	 0,1&���� � $
 Gray��$		 0.0Q�� $
 Gray��$		 1.0Q�._��� $! ContentSite��$��Center=�$��Center=�$��Header��$��	12,2,12,2q�$
��True�� P�  �@��
IsSelected$
 True��. ���  �@K�ZIndex$	 100��