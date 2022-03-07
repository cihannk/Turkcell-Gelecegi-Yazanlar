using IntroEF.Data;
using IntroEF.Models;
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
                    dataGridView1.DataSource = context.Users.ToList();
                    break;
                case "books":
                    dataGridView1.DataSource = context.Books.ToList();
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
}
