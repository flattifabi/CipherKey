   M S B A M L   `   `   `  ���� P  LCipherKey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5db1929376959a91clr-namespace:CipherKey.ViewsCipherKey.Views  ;"clr-namespace:CipherKey.ViewModelsCipherKey.ViewModels  )    #CipherKey.Views.ConfigurationWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:CipherKey.Views     1vm"clr-namespace:CipherKey.ViewModels     A =Wpf.Ui, Version=3.0.1.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml    � WindowStyle$
  None=� � Icon$1 +/assets/images/CodeQuestLogoWithoutText.png�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Title$ CipherKey | Einrichten��$	��600��$	��400��.��� .+��� $��0q�$��Right=�$	��Top=�$��15q����� *CloseCommand+.��   Wpf.Ui.Controls.SymbolIcon     Symbol$ ArrowExit20=�!  Wpf.Ui.Controls.IconElement   
Foreground
 �  ��$��20��$��20��  Wpf.Ui.Controls.FontIcon   FontSize$ 20(���  �� Orientation$ Vertical=�$��Center=�$��Center=�.+��� $ 
Horizontal=�$��Center=�.+��� $1��+/assets/images/CodeQuestLogoWithoutText.png��$	��100��$	��100��$��Center=��� $��
ClipherKey��$��0 7 0 0q�$��Center=��� $��10q�$����Als Passwort-Tresor für dich oder für dein Team, ClipherKey ist der richtige und wichtige Weg! Mit einigen Sicherheitsprozesse zählt ClipherKey zu einer der Sichersten Passwort-Safes der Welt!��$
��Wrap=��� $ Vertical=�$��
10 15 10 0q�.+��� $��Master-Passwort vergeben��$
��Bold"�!  Wpf.Ui.Controls.PasswordBox  -   	  Name$	 password��$	��200��$��0 7 0 0q�  Wpf.Ui.Controls.TextBox
   PlaceholderEnabled$

 True��   PlaceholderText$ Master-Passwort��  -   $	 repeatPassword��$	��200��$��0 7 0 0q�$

 True��$! Master-Passwort wiederholen���� $P��J* Dein Masterpasswort ist der einzige schlüssel zu deinem Passwort Tresor��$
��Wrap=� J� Opacity$ .3Q�$��0 5 0 0q��� $��0 15 0 0q�$��Right=����� *CreateCommand+.���� $ 
Horizontal=�.+�  $ LockClosed12=�$ 20(�$��Center=��� $��	Erstellen��$��5 0 0 0q�$��Center=�