using System;
using System.Linq;
using System.Windows;

namespace lab6
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		NORTHWNDEntities db = new NORTHWNDEntities();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Populate stock level lbx
			lbx_stock.ItemsSource = Enum.GetNames(typeof(StockLevel));

			var query1 = from s in db.Suppliers
						 orderby s.CompanyName
						 select new
						 {
							 SupplierName = s.CompanyName,
							 SupplierID = s.SupplierID,
							 Country = s.Country
						 };

			lbx_suppliers.ItemsSource = query1.ToList();

			var query2 = query1
				.OrderBy(s => s.Country)
				.Select(s => s.Country);

			var countries = query2.ToList();
			lbx_countries.ItemsSource = countries.Distinct();

			public enum StockLevel { Low, Normal, Overstocked };
		}
	}
}
