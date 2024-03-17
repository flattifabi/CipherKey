﻿using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CipherKey.Core.UserControls
{
	/// <summary>
	/// Interaktionslogik für CreatePassword.xaml
	/// </summary>
	public partial class CreatePassword : UserControl, INotifyPropertyChanged
    {
		private int _passwordScore;
		public EventHandler<PasswordBase> PasswordCreated;
		public IDelegateCommand CreatePasswordCommand => new DelegateCommand(OnCreatePassword);
		public IDelegateCommand PasswordAddCommand => new DelegateCommand(OnAddPassword);

		private void OnAddPassword()
		{
			PasswordData.Password = Password.Password;
			PasswordCreated?.Invoke(this, PasswordData);
		}

		public PasswordBase PasswordData { get; set; } = new();
		public CreatePassword()
        {
            InitializeComponent();
            DataContext = this;
			Password.PasswordChanged += PasswordContentChanged;
        }
		private void OnCreatePassword()
		{
			Password.Password = PasswordHelpers.GenerateStrongPassword(18);
			OnPropertyChanged(nameof(Password));
		}

		private void PasswordContentChanged(object sender, RoutedEventArgs e)
		{
			var password = e.Source as Wpf.Ui.Controls.PasswordBox;
			password.Password = password.Password.Trim();
			PasswordScore = PasswordHelpers.CheckPasswordStrength(password.Password);
			PasswordData.PasswordScore = PasswordScore;
			PasswordData.Password = password.Password;
			OnPropertyChanged(nameof(PasswordScore));
		}
		
		public int PasswordScore
		{
			get => _passwordScore;
			set
			{
				_passwordScore = value;
				OnPropertyChanged();
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
