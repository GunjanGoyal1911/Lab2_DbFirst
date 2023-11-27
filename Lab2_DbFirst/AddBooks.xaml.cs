using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Lab2_DbFirst.Data;
using Lab2_DbFirst.Models;

namespace Lab2_DbFirst
{
	/// <summary>
	/// Interaction logic for AddBooks.xaml
	/// </summary>
	public partial class AddBooks : Window
    {
	    private List<Author> authors;
	    private List<Publisher> publishers;
	    private Store _store;
        public AddBooks(Store store)
        {
	        _store = store;
			InitializeComponent();
        }

        private void AddBooks_OnLoaded(object sender, RoutedEventArgs e)
        {
	        using (var storeDbContext = new StoreDBContext())
	        {
		        authors = storeDbContext.Authors.ToList();
				publishers= storeDbContext.Publishers.ToList();
	        }

	        cmbAuthor.ItemsSource = authors;
	        cmbAuthor.DisplayMemberPath = "FullName";
	        cmbAuthor.SelectedValuePath = "AuthorId";

	        cmbPublisher.ItemsSource = publishers;
	        cmbPublisher.DisplayMemberPath = "FullPublisherName";
	        cmbPublisher.SelectedValuePath = "PublisherId";
        }

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			var book = new Book
			{
				Title = txtBookTitle.Text,
				Isbn13 = txtISBN.Text,
				Language = txtLanguage.Text,
				Price = decimal.Parse(txtPrice.Text),
				ReleaseDate =Convert.ToDateTime(txtReleaseDate.Text),
				AuthorId = Convert.ToInt32(cmbAuthor.SelectedValue),
				PublisherId = Convert.ToInt32(cmbPublisher.SelectedValue)
			};

			var inventory = new Inventory
			{
				Isbn13Navigation = book,
				StoreId = _store.StoreId,
				StockQuantity = Convert.ToInt32(txtStockQuatity.Text)
			};

			using (var storeDbContext = new StoreDBContext())
			{
				storeDbContext.Inventories.Add(inventory);
				storeDbContext.SaveChanges();
				MessageBox.Show("The book has been added successfully");
			}
			txtBookTitle.Clear();
			txtISBN.Clear();	
			txtLanguage.Clear();
			txtPrice.Clear();
			txtReleaseDate.Clear();
			txtStockQuatity.Clear();
			cmbAuthor.ItemsSource = null;
			cmbPublisher.ItemsSource = null;
		}
	}
}
