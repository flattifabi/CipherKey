   M S B A M L   `   `   `  ���� K  GModule.Passwords, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Module.PasswordsModule.Passwords  I ECipherKey.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.CoreCipherKey.Core.Converter )    #Module.Passwords.PasswordModuleView   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Module.Passwords     A =Wpf.Ui, Version=3.0.1.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  N	converter>clr-namespace:CipherKey.Core.Converter;assembly=CipherKey.Core    � 	Resources  ;  5CipherKey.Core.Converter.BooleanToVisibilityConverter    BooleanToVisibilityConverterA  ;CipherKey.Core.Converter.BooleanToVisibilityInvertConverter & "BooleanToVisibilityInvertConverterB  <CipherKey.Core.Converter.StringHasValueToVisibilityConverter ' #StringHasValueToVisibilityConverterE  ?CipherKey.Core.Converter.PasswordQualityIntegerToColorConverter * &PasswordQualityIntegerToColorConverterA  ;CipherKey.Core.Converter.ListCountZeroToVisibilityConverter & "ListCountZeroToVisibilityConverter8  2CipherKey.Core.Converter.NullToVisibilityConverter  NullToVisibilityConverter  ExpandModuleView �� 
TargetName �� TargetProperty	 W� To g� Duration  CollapseModuleView��%  &	        &	      &	 
     &	      &	      &	      &	      &	 o                 �� .��W� $ 
ModuleView��$ (ContentControl.Width)� 400$ 0:0:0.3D��� .��W� $ 
ModuleView��$ (ContentControl.Width)� 0$ 0:0:0.3D�.��� -    � ColumnDefinitions �� $	��220 ��� $��* � � RowDefinitions �� $��* �.+��� -    ��Name$ 
ModuleView��$��0��$	��100��$	��100�� K� ZIndex$ 99999��$��Right=�$��	0 25 0 25q����� *
ModuleView+	 �� UpdateSourceTrigger$	 PropertyChanged=��� -   $��1��$��0 10 0 0q�.���� 
 �� Orientation$
 Vertical=�$��2 0 0 2q�.+��� $
 
Horizontal=�.+��� $��0 0 0 9q����� *CreateTopicCommand+.���� $
 
Horizontal=�.+�   Wpf.Ui.Controls.SymbolIcon     Symbol$ Add12=�$��Center=��� $��Neue Kategorie��$��5 0 0 0q�$��Center=��� ���� *Topics+$	 PropertyChanged=� �� SelectedValue �� *SelectedTopic+$	 PropertyChanged=����� .R��� $
 Vertical=����� .R��� $
 
Horizontal=�$��	10 5 15 0q�.+���  �� CornerRadius$	 100��$��35��$��35��$��2q����� *Design.ForegroundHex+����  j� Stretch$ UniformToFill=� �� ImageSource �� *Design.TopicImage+$	 PropertyChanged=��� $��Center=�$��15 0 0 0q����� *Name+� $��1��$��1��.+�� $	 999����*  $Wpf.Ui.Markup.ThemeResourceExtension  y��� *	Passwords+$	 PropertyChanged=� �� 	Converter# [ .+��� $
 Vertical=�$��Center=�$��Center=�.+�  $ 
LockOpen16=�	  Wpf.Ui.Controls.FontIcon 	  FontSize$ 50(��� $&�� Noch keine Passwörter vorhanden���� $��Passwort hinzufügen��$��Center=�$��0 15 0 0q����� *CreatePasswordCommand+� $��0 0 0 0q� �� $��50 ��� $��* �.+��� $
 
Horizontal=�$��5 0 0 0q�$��Center=�.+��� ���� *CreatePasswordCommand+.���� $
 
Horizontal=�.+�  $ Add12=�$��Center=��� $��Neues Passwort��$��5 0 0 0q�$��Center=��� $��1��.���� $��5 0 5 0q�$��1������ *	Passwords+$	 PropertyChanged=����� .R��� $
 Vertical=����� .R�"
  Wpf.Ui.Controls.CardExpander
  $��0 0 0 5q����� $
 
Horizontal=�.+��� 
��� � �$��15��$��15��$	 100��$��5 0 5 0q��� $��10 0 0 0q�$��Center=����� *Name+�� 
���   � J� Opacity$ .5Q�$��2��$��Stretch=�$��	10 0 10 0q��� $��Erstellt am:���� $��10 0 0 0q����� *	Created+ �� StringFormat$ 
dd.MM.yyyy������ $
 Vertical=�.+��� $��Passwort Qualität:���  � Maximum$	 100Q�$��0 5 0 0q����� *PasswordScore+$	 PropertyChanged=�# [  � Value �� *PasswordScore+$	 PropertyChanged=��� $
 
Horizontal=�$��Stretch=�$��0 15 0 0q�.+��� $��	Passwort:��$��Center=��� ${�False��$��Center=�$��10 0 0 0q����� *
Password+$	 PropertyChanged=��� $��5 0 0 0q�{��� *IsCopyEnabled+$	 PropertyChanged=����� *!DataContext.CopyPasswordCommand+ �� RelativeSource ��  �� AncestorType" ������ .��  $ Copy16=�$��22���� $��5 0 0 0q�
��� � �.��  $ LockClosed24=�$��22���� $��Center=�$��36��$��5 0 0 0q�$ 5�� �� Padding$ 2q�y��� *IsCopyActive+# [  ����  �� Color$ Black�� �� Opacity$ .3Q�._��� $
 
Horizontal=�$��Center=�$��Center=�.+��� $��10 0 0 0q�$��Center=����� *AvailableSeconds+$	 PropertyChanged=� �� FallbackValue$ Nicht aktiv���� $	��Sek��$��5 0 10 0q�$��Center=��� $
 
Horizontal=�$��0 5 0 0q�y��� *
Username+# [ $	 PropertyChanged=�.+��� $��Benutzername:��$��Center=��� ${�False��$��Center=�$��10 0 0 0q����� *
Username+$	 PropertyChanged=��� $��5 0 0 0q�$
{�True������ *!DataContext.CopyUsernameCommand+ �� " ������ .��  $ Copy16=�$��22���� $
 
Horizontal=�$��0 15 0 0q�$
��Left=�.+��� 
��� ������� *!DataContext.EditPasswordCommand+ �� " ������ .���� $
 
Horizontal=�.+�  $ DocumentEdit16=��� $��
Bearbeiten��$��5 0 0 0q��� 
���  ��$��5 0 0 0q����� *!DataContext.DeletePasswordEntry+ �� " ������ .���� $
 
Horizontal=�.+�  $ Delete12=��� $��Löschen��$��5 0 0 0q��� 
�������$��5 0 0 0q����� *'%DataContext.OpenPasswordBackupCommand+ �� " ������ .���� $
 
Horizontal=�.+�  $ 	History48=��� $��Wiederherstellen��$��5 0 0 0q� � ToolTip �� $
 Vertical=�.+��� $��Passwort wiederherstellen��$
��Bold"��� $E��?Verwende ein Passwort welches du bereits früher verwendet hast��$
��Wrap=�$��0 5 0 0q��� 
��� � �$��5 0 0 0q�$��35������ *(&DataContext.AutoTypeCredentialsCommand+ �� " ������ .���� $
 
Horizontal=�.+�  $ Play12=�!  Wpf.Ui.Controls.IconElement   
Foreground
 ����� �� $
 Vertical=�.+��� $��Automatische Eingabe��$
��Bold"��� $^��XÖffnet die eingegebene Webadresse im geöffneten Browser und Meldet sich automatisch an��$
��Wrap=�$��0 5 0 0q�