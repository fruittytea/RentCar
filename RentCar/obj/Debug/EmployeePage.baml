   M S B A M L   `   `   `  ����B  >RentCar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:RentCarRentCar  5          RentCar.EmployeePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:RentCar     5        N� Title$  Сотрудник��5	      .��5      � ���#EDF2F4 � RowDefinitions 5   
   �� 5      $	��137 �6   �� 5      $��* �6   �� 5      $��17 �6   �� 5      $��70 �6   �� 5      $��21 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� $%��База сотрудников��6   $
��48px(�60   ���#D90429$��0��6U   �� 5      -   5         ��Name$ 	LoadedBtn��5   8   $��3��6   $��	100 0 0 0q�6c   ��6"   Z� .��5      �� $��60��6   $ ��Resources/update_arrow.png��6#   $��1��6G   6Y   5      5      �� 5   
   -   5         ��Name$ Table��5   f   $��1��6   $��52, 0, 61, 0q�6    ���#8D99AE$��3,3,3,3q�6L   ��5      �� 5      .R�5      �  � Orientation$ Vertical=�6    $	��740��67   5      ��5      �� 5       .R�5!      � $	��740��6    � ColumnDefinitions 5"      �� 5#      $	��170 �6/   �� 5$      .+�5&      ��  ��Name$ CarImage��6    $	��170��62   ��5'      �� 5(   "    �� Path$ Photo�6*   	 �� TargetNullValue	 5)   &   ��5*   *   Resources/NoPhoto.png��6M   5,   #   5.      � 5/      $��1��6    50      �� 51   "   �� 52   "   �� 53   "   �� 54   "   .+�56      �� $ 
Horizontal=�6)   $��Center=�6B   $��0��6_   .+�57   "   �� 
 ��Name$
 ClientSurname��6y   $��0,0,8,0q�6E   $��28(�6V   ���#D90429��6,   �� *	Surname+6y   �� 58   "   $
 
ClientName��6v   $��0,0,8,0q�6B   $��28(�6S   ���#D90429��6,   �� *Name+6v   �� 59   "   $
 ClientFathername��6k   $��28(�6H   ���#D90429��6,   �� *
Fathername+6k   5:      �� 5;      $ 
Horizontal=�6)   $��Center=�6B   $��1��6_   .+�5<   "   �� $
 EmailTB��6C   $��0,0,10,0q�6T   $
��20px(�6f   ��6,   �� *Email+6f   �� 5=   "   $
 Phone��6C   $
��20px(�6R   ��6,   �� *Phone+6R   5>      �� 5?      $ 
Horizontal=�6)   $��Center=�6B   $��2��6_   .+�5@   "   �� $��Должность:��6,   $��0,0,10,0q�6>   $
��20px(�6P   �� 5A   "   $
 EmployeePost��6[   $
��20px(�6q   ��6,   �� *RentCar_EmployeeRole.RoleName+6q   5B      �� 5C      $ DelEmp��6_   -   5        ���#8D99AE$��3��5C   :   $	��383��6G   $��50��6S   ��6�   �� .��5D   "   �� $��Уволить��6,   $
��20px(�6;   5E      5F      5G      5H      5J      � 5K   
   $��3��6    5L      �� 5M      $��52 �6#   �� 5N      $	��340 �6#   �� 5O      �� 5P      $��70 �6#   �� 5Q      $��61 �6#   .+�5S      �� -   5         ��Name$ SearchTextBox��5S   N   $	��340��6   $��70��6"   $��1��6.   $
��20px(�6>   6e   �� 5T      -   5        $ AddEmployeeBtn��5T   <   $��70��6   $��70��6    $��3��6,   .��5U      �� $��+��6   
�������$��48(�68   5V      5W      5X      5Y      