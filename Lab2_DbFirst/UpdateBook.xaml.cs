using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Lab2_DbFirst.Data;
using Lab2_DbFirst.Models;
using Publisher = Lab2_DbFirst.Models.Publisher;

namespace Lab2_DbFirst
{
	/// <summary>
	/// Interaction logic for UpdateBook.xaml
	/// </summary>
	public partial class UpdateBook : Window
	{
		private  Inventory _inventory;
		private  Store _store;
		private List<Author> authors;
		private List<Publisher> publishers;

		public UpdateBook(Inventory inventory, Store store)
		{
			_inventory = inventory;
			_store = store;
			InitializeComponent();
			LoadAuthorAndPublisher(); 
			SetData();
		}

		private void SetData()
		{
			txtBookTitle.Text = _inventory.Isbn13Navigation.Title;
			txtISBN.Text = _inventory.Isbn13Navigation.Isbn13;
			txtLanguage.Text = _inventory.Isbn13Navigation.Language;
			txtPrice.Text = _inventory.Isbn13Navigation.Price.ToString();
			txtReleaseDate.Text = _inventory.Isbn13Navigation.ReleaseDate.ToString();
			cmbPublisher.SelectedValue = _inventory.Isbn13Navigation.PublisherId;
			cmbAuthor.SelectedValue = _inventory.Isbn13Navigation.AuthorId;
			txtStockQuatity.Text = _inventory.StockQuantity.ToString();
		}

		private void LoadAuthorAndPublisher()
		{
			using (var storeDbContext = new StoreDBContext())
			{
				authors = storeDbContext.Authors.ToList();
				publishers = storeDbContext.Publishers.ToList();
			}

			cmbAuthor.ItemsSource = authors;
			cmbAuthor.DisplayMemberPath = "FullName";
			cmbAuthor.SelectedValuePath = "AuthorId";

			cmbPublisher.ItemsSource = publishers;
			cmbPublisher.DisplayMemberPath = "FullPublisherName";
			cmbPublisher.SelectedValuePath = "PublisherId";
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			var book = new Book
			{
				Title = txtBookTitle.Text,
				Isbn13 = txtISBN.Text,
				Language = txtLanguage.Text,
				Price = decimal.Parse(txtPrice.Text),
				ReleaseDate = Convert.ToDateTime(txtReleaseDate.Text),
				AuthorId = Convert.ToInt32(cmbAuthor.SelectedValue),
				PublisherId = Convert.ToInt32(cmbPublisher.SelectedValue)
			};

			var inventory = new Inventory
			{
				InventoryId = _inventory.InventoryId,
				Isbn13 = txtISBN.Text,
				Isbn13Navigation = book,
				Store = _store,
				StoreId = _store.StoreId,
				StockQuantity = Convert.ToInt32(txtStockQuatity.Text)
			};

			using var storeDbContext = new StoreDBContext();
			storeDbContext.Inventories.Update(inventory);
			storeDbContext.SaveChanges();
			MessageBox.Show("The book has been updated successfully");
		}
	}
}
