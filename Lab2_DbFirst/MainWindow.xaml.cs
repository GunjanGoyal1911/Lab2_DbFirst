using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab2_DbFirst.Data;
using Lab2_DbFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_DbFirst
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Store> stores;
		private List<Inventory> inventories;
		private Inventory inventory;
		
		public MainWindow()
		{
			InitializeComponent();
			btnUpdate.IsEnabled = false;
			btnDelete.IsEnabled = false;
			btnAddBook.IsEnabled = false;
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			using (var storeDbContext = new StoreDBContext())
			{
				stores = storeDbContext.Stores.ToList();
			}

			cmbStore.ItemsSource = stores;
			cmbStore.DisplayMemberPath = "StoreName";
			cmbStore.SelectedValuePath = "StoreId";
		}

		private void CmbStore_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			using(var storeDbContext = new StoreDBContext())
			{ 
				if (cmbStore.SelectedValue != null)
				{
					int selectedId = (int)cmbStore.SelectedValue;
					inventories = storeDbContext.Inventories.
						Where(x => x.StoreId == selectedId)
						.Include(y=>y.Isbn13Navigation)
						.ToList();
				}
			}
			dataGrid.ItemsSource = inventories;
			dataGrid.CanUserAddRows = false;
			btnAddBook.IsEnabled = true;
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			btnUpdate.IsEnabled = false;
			btnDelete.IsEnabled = false;
			btnAddBook.IsEnabled = true;
		}

		private void CheckBox_OnChecked(object sender, RoutedEventArgs e)
		{
			btnUpdate.IsEnabled = true;
			btnDelete.IsEnabled = true;
			
			var box = e.OriginalSource as CheckBox;
			inventory = (Inventory)box?.DataContext;
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			using var storeDbContext = new StoreDBContext();
			storeDbContext.Inventories.Where(a => a.InventoryId == inventory.InventoryId)
				.ExecuteDelete();
			storeDbContext.SaveChanges();
			MessageBox.Show($"Book has been deleted.");

			var selectedId = (int)cmbStore.SelectedValue;
			inventories = storeDbContext.Inventories.Where(x => x.StoreId == selectedId)
				.Include(y => y.Isbn13Navigation)
				.ToList();
			dataGrid.ItemsSource = inventories;
		}

		private void btnAddBook_Click(object sender, RoutedEventArgs e)
		{
			var store = cmbStore.SelectedItem;
			var addBooks = new AddBooks((Store)store);
			addBooks.Show();
		}

		private void BtnAddAuthor_OnClick(object sender, RoutedEventArgs e)
		{
			var addAuthors = new AddAuthors();
			addAuthors.Show();
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (cmbStore == null) return;
			var store = cmbStore.SelectedItem as Store;
			var updateBook = new UpdateBook(inventory, store);
			updateBook.Show();
		}
	}
}
