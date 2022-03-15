using IntroEF.Data;
using IntroEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private readonly BookstoreDbContext context;
        public Form2(string selection)
        {
            InitializeComponent();
            context = new BookstoreDbContext();

            switch (selection)
            {
                case "users":
                    var users = context.Users.Select(user => new {
                        Id = user.Id,
                        UserName = user.UserName,
                        Password= user.Password,
                        Email = user.Email,
                        SignedUpDate = user.SignedUpDate,
                        Address = user.Address
                    }).ToList();

                    dataGridView1.DataSource = users;
                    break;

                case "books":
                    List<BookVM> books = context.Books.Include(book => book.Genres).Select(book => new BookVM
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Price = book.Price,
                        //Genres = book.Genres[0].Name,
                        AuthorId = book.AuthorId,
                        Author = book.Author.FullName,
                        Description = book.Description,
                    }).ToList();

                    foreach (var book in books)
                    {
                        string x = String.Empty;
                        Book bookDb = context.Books.Include(book => book.Genres).Where(dbBook => dbBook.Id == book.Id).FirstOrDefault();
                        bookDb.Genres.ForEach(genre => x += genre.Name + ", ");
                        x = x.Substring(0, x.Length - 2);
                        book.Genres = x;
                    }
                    dataGridView1.DataSource = books;
                    break;
                case "genres":
                    dataGridView1.DataSource = context.Genres.ToList();
                    break;
                case "authors":
                    dataGridView1.DataSource = context.Authors.ToList();
                    break;
            }
        }
    }
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Genres { get; set; }
        public int? AuthorId { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
