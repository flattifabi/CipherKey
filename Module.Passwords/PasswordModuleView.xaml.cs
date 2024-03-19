using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Module.Passwords
{
	/// <summary>
	/// Interaktionslogik für PasswordModuleView.xaml
	/// </summary>
	public partial class PasswordModuleView : UserControl
	{
		public PasswordModuleView()
		{
			InitializeComponent();
		}

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.Source is not ContentControl)
			{
				var storyboard = FindResource("CollapseModuleView") as Storyboard;
				storyboard.Begin();
			}
		}
	}
}
