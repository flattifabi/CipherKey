   M S B A M L   `   `   `  ���� D  @CipherKey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CipherKey	CipherKey  I ECipherKey.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW<clr-namespace:CipherKey.Core.Helpers;assembly=CipherKey.CoreCipherKey.Core.Helpers [>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.CoreCipherKey.Core.Converter ;"clr-namespace:CipherKey.ViewModelsCipherKey.ViewModels      CipherKey.Splash   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:CipherKey     Phelpers<clr-namespace:CipherKey.Core.Helpers;assembly=CipherKey.Core     T	converter>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.Core     1vm"clr-namespace:CipherKey.ViewModels     A =Wpf.Ui, Version=3.0.1.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  -   "  Wpf.Ui.Controls.FluentWindow   Name$  Window�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Title$ CipherKey | Auth��$	��620��$	��800�� � Icon$1 +/assets/images/CodeQuestLogoWithoutText.png�� � 	Resources 4  .CipherKey.Core.Converter.SecureStringConverter   SecureStringConverter;  5CipherKey.Core.Converter.BooleanToVisibilityConverter   BooleanToVisibilityConverter  ExpandModuleView �� 
TargetName �� TargetProperty		 W� To
 g� Duration  CollapseModuleView��%�   &	        &	      &	 
     &	 [         �� .��W� $ 
ModuleView��$ (ContentControl.Width)�	 400$
 0:0:0.3D��� .��W� $ 
ModuleView��$ (ContentControl.Width)�	 0$
 0:0:0.3D�.��� .+��� -    ��Name$ 
ModuleView��$��0��$	��100��$	��100�� K� ZIndex$ 99999��$��Right=�$��	0 25 0 25q����� *
ModuleView+ �� UpdateSourceTrigger$ PropertyChanged=��� $��0q�$��Right=�$	��Top=�$��15q����� *CloseCommand+.��   Wpf.Ui.Controls.SymbolIcon     Symbol$ ArrowExit20=�!  Wpf.Ui.Controls.IconElement   
Foreground
 �  ��$��20��$��20��  Wpf.Ui.Controls.FontIcon   FontSize$ 20(���  �� Orientation$ Vertical=�$��50q�.+��� $1��+/assets/images/CodeQuestLogoWithoutText.png��$	��120���� $ Vertical=�$��Center=�$��0 15 0 0q�.+��� $��	CipherKey��$��23(�$��Center=�$��0 5 0 0q�$��SemiBold"��� $>��8Öffne deinen Passwort Tresor mit deinem Master-Passwort��$��0 5 0 0q��� $��0 20 0 0q�$��2q�
�������.@��� $��Lokal��.���� $ Vertical=�.+�!  Wpf.Ui.Controls.PasswordBox  -     Name$ localMasterPassword��$��Stretch=�  Wpf.Ui.Controls.TextBox   PlaceholderEnabled$
 True��   PlaceholderText$ Master Passwort��$��0 15 0 0q����� *EnteredLocalStoragePassword+ �� 	Converter# [  $ PropertyChanged=��� $��50��$��5 0 5 0q�$��Center=�$��0 20 0 0q����� *LoadLocalStorage+.���� $ 
Horizontal=�.+�  
 � � �$ LockClosed12=�$ 20(�$��Center=�$��Center=�$��10 0 0 0q��� $��Verifizieren��$��Center=�$��	10 0 10 0q��� $
��Pfad��.���� $ Vertical=�.+�� $��0 10 0 0q� � ColumnDefinitions �� $��* ��� $��80 �.+�  -     Name$ 
RemotePath��$��Stretch=�$
 True��$ .cipher Pfad������ *EnteredRemotePath+$ PropertyChanged=��� $��1��$��5 0 0 0q�$��75������ *+)SelectedRemoteSourceFromFileSystemCommand+.���� $ 
Horizontal=�.+��� $��Suchen��  -   $ RemotePassword��$��Stretch=�$
 True��$ Passwort��$��0 10 0 0q��� $��50��$��5 0 5 0q�$��Center=�$��0 20 0 0q����� *LoadPathStorage+.���� $ 
Horizontal=�.+�  
 � � �$ LockClosed12=�$ 20(�$��Center=�$��Center=�$��10 0 0 0q��� $��Verifizieren��$��Center=�$��	10 0 10 0q��� $��- oder -��$��	0 10 0 10q�$��Center=��� $��50��$��Center=�$��0 10 0 0q����� *CreateSourceCommand+.���� $ 
Horizontal=�.+�  $ AddCircle12=�$ 20(�$��Center=��� $��	Erstellen��$��	10 0 10 0q�$��Center=��� $ 
Horizontal=�$��Bottom=�$��Center=�$��10q�y��� *	IsError+$ PropertyChanged=�# [ .+�  $ ErrorCircle12=�
 �  ��$��Center=��� $��10 0 0 0q�$��Center=����� *	ErrorText+$ PropertyChanged=��� $ 
Horizontal=�$��Bottom=�$
��Left=�$��10q�.+��� $!��/assets/images/wwd_logo.png��$��35��$��35��$��Center=��� $��WebWatchDog CipherKey��
�������$��10 0 0 0q�$��Bottom=�