   M S B A M L   `   `   `  ���� D  @CipherKey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CipherKey	CipherKey  ;"clr-namespace:CipherKey.ViewModelsCipherKey.ViewModels  I ECipherKey.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.CoreCipherKey.Core.Converter     CipherKey.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:CipherKey     1vm"clr-namespace:CipherKey.ViewModels     T	converter>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.Core     A =Wpf.Ui, Version=3.0.1.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml    � WindowStartupLocation$  CenterScreen=�"  Wpf.Ui.Controls.FluentWindow!   ExtendsContentIntoTitleBar$
 True�� � 
ResizeMode$ NoResize=� � Icon$1 +/assets/images/CodeQuestLogoWithoutText.png��   WindowCornerPreference$ Round=� � Title$ 	CipherKey��$	��600��$	��950�� � 	Resources ;  5CipherKey.Core.Converter.BooleanToVisibilityConverter    BooleanToVisibilityConverterA  ;CipherKey.Core.Converter.BooleanToVisibilityInvertConverter & "BooleanToVisibilityInvertConverterB  <CipherKey.Core.Converter.StringHasValueToVisibilityConverter ' #StringHasValueToVisibilityConverterE  ?CipherKey.Core.Converter.PasswordQualityIntegerToColorConverter * &PasswordQualityIntegerToColorConverterA  ;CipherKey.Core.Converter.ListCountZeroToVisibilityConverter & "ListCountZeroToVisibilityConverter8  2CipherKey.Core.Converter.NullToVisibilityConverter  NullToVisibilityConverter  ExpandModuleView �� 
TargetName �� TargetProperty		 W� To
 g� Duration  CollapseModuleView��%  &	        &	      &	 
     &	      &	      &	      &	      &	 o                 �� .��W� $ 
ModuleView��$ (ContentControl.Width)�	 400$
 0:0:0.3D��� .��W� $ 
ModuleView��$ (ContentControl.Width)�	 0$
 0:0:0.3D�.���  � ColumnDefinitions �� $��80 ��� $��* � � RowDefinitions �� $��50 ��� $��* �.+���  �� Orientation$ 
Horizontal=� K� ZIndex$ 999999��$	��100��$	��100��$��Right=�$��Bottom=�$��5q�.+��� $��	Made with��$��20 0 0 0q�   Wpf.Ui.Controls.SymbolIcon     Symbol$ Heart12=�$	��Top=�$��5 2 0 0q�!	  Wpf.Ui.Controls.IconElement 	  
Foreground
 �  ���� $��by WebWatchDog��$��5 0 0 0q�'
  !Wpf.Ui.Controls.SnackbarPresenter
  -    
 Name$ SnackbarPresenter��$	��100��$	��100��$ 999999��$��Bottom=��� -    ��Name$ ContentPresenter��$	��100��$	��100��$ 	999999999��$��Stretch=�$��Stretch=��� -    ��Name$ 
ModuleView��$��0��$	��100��$	��100��$	 999��$	��Top=�$��Right=�$��	0 25 0 25q����� *
ModuleView+ �� UpdateSourceTrigger$ PropertyChanged=�� -   $	��100����*  $Wpf.Ui.Markup.ThemeResourceExtension  �� $ 
Horizontal=�$	��100��$��15q�$ 99��.+��� $��	CipherKey���� $ 99��$ 
Horizontal=�$	��100��$	��100��$��Right=�$	��Top=�$��15q�.+��� $��0q�$	��100��$	��100��$��Right=�$	��Top=�$��5 0 0 0q����� *MinimizeCommand+.��  $ ArrowMinimize16=�
 � ���$��20��$��20��  Wpf.Ui.Controls.FontIcon   FontSize$ 20(��� $��0q�$	��100��$	��100��$��Right=�$	��Top=�$��5 0 0 0q����� *CloseCommand+.��  $ ArrowExit20=�
 �  ��$��20��$��20��$ 20(��� $
��Left=�$��5 0 0 0q�$��1������ *	Modules+$ PropertyChanged=� �� SelectedValue �� *SelectedModule+$ PropertyChanged=����� .R��� $ Vertical=����� .R�� 
������ $��55��$��55��.+�  $ 40(�$��Center=�$��Center=� �� *Symbol.Symbol+$ PropertyChanged=� ^� ToolTip _� ���� *
ModuleName+ ^� InitialShowDelay$	 500���� $��2��$��2 0 2 0q�$��1��$��Right=� �� CornerRadius$	 100������ -   GradientBrush�� �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF1730FF�� � Offset$ 0Q�� -   TransparentStop��$ Transparent��$ .4Q� � Triggers :�  :� RoutedEvent$ Loaded��.[��� .���� .��W� $ TransparentStop��$ Offset�  W� From  0.4	 1$
 0:0:1D�! g� AccelerationRatio$	! 0.2Q��� $��5 0 0 0q�$
 9999��$��1��$��1������ *SelectedModule.View+$ PropertyChanged=�