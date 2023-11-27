using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Lab2_DbFirst.Data;
using Lab2_DbFirst.Models;

namespace Lab2_DbFirst
{
	/// <summary>
	/// Interaction logic for AddAuthors.xaml
	/// </summary>
	public partial class AddAuthors : Window
	{
		private List<Author> _authors;
		public AddAuthors()
		{
			InitializeComponent();
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(txtFirstName.Text))
			{
				MessageBox.Show("First Name can not be empty.");
			}

			var author = new Author
			{
				AuthorFirstName = txtFirstName.Text,
				AuthorLastName = txtLastName.Text
			};

			using (var storeDbContext = new StoreDBContext())
			{
				storeDbContext.Authors.Add(author);
				storeDbContext.SaveChanges();
				MessageBox.Show("Author has been added successfully.");
			}
			txtFirstName.Clear();
			txtLastName.Clear();
			LoadAuthor();
		}

		private void AllAuthors_OnLoaded(object sender, RoutedEventArgs e)
		{
			LoadAuthor();
		}

		private void LoadAuthor()
		{
			using (var storeDbContext = new StoreDBContext())
			{
				_authors = storeDbContext.Authors.ToList();

			}
			cmbAllAuthors.ItemsSource = _authors;
			cmbAllAuthors.DisplayMemberPath = "FullName";
			cmbAllAuthors.SelectedValuePath = "AuthorId";
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			using (var storeDbContext = new StoreDBContext())
			{
				var authorToDelete = storeDbContext.Authors.Find((int)cmbAllAuthors.SelectedValue);

				if (authorToDelete != null)
				{
					var booksToDelete = storeDbContext.Books.Where(b => b.AuthorId == (int)cmbAllAuthors.SelectedValue).ToList();
					foreach (var book in booksToDelete)
					{
						storeDbContext.Books.Remove(book);
					}

					storeDbContext.Authors.Remove(authorToDelete);
					MessageBox.Show("Author has been deleted successfully");
					storeDbContext.SaveChanges();
				}
			}
			LoadAuthor();
		}
	}
}
