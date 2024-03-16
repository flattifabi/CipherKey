using CipherKey.Core.UserControls;
using System;
using System.CodeDom;
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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace CipherKey
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : FluentWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
        }

		private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(e.Source is not CreateTopic)
			{
				var storyboard = FindResource("CollapseModuleView") as Storyboard;
				storyboard.Begin();
			}
		}
	}
}
