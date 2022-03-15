using IntroEF.Data;
using IntroEF.Models;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        readonly BookstoreDbContext context;
        public Form1()
        {
            InitializeComponent();
            // initialize context
            context = new BookstoreDbContext();
            // updating necessary selections
            UpdateBookGenres();
            UpdateAuthors();
        }
        private void UpdateBookGenres()
        {
            checkedListBox1.DataSource = context.Genres.Select(x => x.Name).ToList();
            checkedListBox1.SelectedItem = null;
        }
        private void UpdateAuthors()
        {
            var authors = context.Authors.Select(author => new { Id= author.Id, FullName = author.FullName}).ToList();
            comboBox2.DataSource = authors;
            comboBox2.DisplayMember = "FullName";
            comboBox2.ValueMember = "Id";
            comboBox2.SelectedItem = null;
        }
        private bool IsBookInputsValid()
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty || comboBox2.SelectedItem == null || checkedListBox1.SelectedItem == null)
            {
                return false;
            }

            return true;
        }
        private bool IsUserInputsValid()
        {
            if (textBox7.Text == String.Empty || textBox8.Text == String.Empty || textBox9.Text == String.Empty)
            {
                return false;
            }

            return true;
        }private bool IsBookGenreInputsValid()
        {
            if (textBox5.Text == String.Empty)
            {
                return false;
            }

            return true;
        }private bool IsAuthorInputsValid()
        {
            if (textBox6.Text == String.Empty)
            {
                return false;
            }

            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!IsAuthorInputsValid())
            {
                MessageBox.Show("Zorunlu alanlardan bir veya birkaçını doldurmadın.");
                return;
            }
            context.Authors.Add(new Author { BirthDate = dateTimePicker1.Value, FullName = textBox6.Text });
            int result = context.SaveChanges();

            switch (result) {
                case 1:
                    MessageBox.Show("Yazar başarıyla eklendi");
                    textBox6.Text = String.Empty;
                    dateTimePicker1.Value = DateTime.Now;
                    UpdateAuthors();
                    break;
                default:
                    MessageBox.Show("Yazar eklenirken bir sorunla karşılaşıldı");
                    break;
            }
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsBookGenreInputsValid())
            {
                MessageBox.Show("Zorunlu alanlardan bir veya birkaçını doldurmadın.");
                return;
            }
            context.Genres.Add(new Genre { Name= textBox5.Text});
            int result = context.SaveChanges();

            switch (result)
            {
                case 1:
                    MessageBox.Show("Tür başarıyla eklendi");
                    UpdateBookGenres();
                    textBox5.Text = String.Empty;
                    break;
                default:
                    MessageBox.Show("Tür eklenirken bir sorunla karşılaşıldı");
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!IsUserInputsValid())
            {
                MessageBox.Show("Zorunlu alanlardan bir veya birkaçını doldurmadın.");
                return;
            }
            context.Users.Add(new User { UserName = textBox7.Text, Address = textBox10.Text, Email = textBox9.Text, Password = textBox8.Text, SignedUpDate=DateTime.Now });
            int result = context.SaveChanges();

            switch (result)
            {
                case 1:
                    MessageBox.Show("Kullanıcı başarıyla eklendi");
                    UpdateBookGenres();
                    textBox7.Text = String.Empty;
                    textBox8.Text = String.Empty;
                    textBox9.Text = String.Empty;
                    textBox10.Text = String.Empty;

                    break;
                default:
                    MessageBox.Show("Kullanıcı eklenirken bir sorunla karşılaşıldı");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsBookInputsValid())
            {
                MessageBox.Show("Zorunlu alanlardan bir veya birkaçını doldurmadın.");
                return;
            }

            Book book = new Book
            {
                Title = textBox1.Text,
                Price = Double.Parse(textBox2.Text),
                Description = textBox4.Text
            };

            List<Genre> checkedGenres = new List<Genre>();
            for (int i = 0; i< checkedListBox1.CheckedItems.Count; i++)
            {
                //checkedGenres.Add();
                string genreName = checkedListBox1.CheckedItems[i].ToString();
                Genre genreSelected = context.Genres.FirstOrDefault(genre => genre.Name == genreName);
                if (genreSelected != null)
                    checkedGenres.Add(genreSelected);
            }

            checkedGenres.ForEach(genre => book.Genres.Add(genre));
            //var genre = context.Genres.FirstOrDefault(gen => gen.Name == selectedItemName);
            //book.Genres.Add(genre);

            book.Author = context.Authors.FirstOrDefault(x => x.Id == (int)comboBox2.SelectedValue);


            context.Books.Add(book);

            int result = context.SaveChanges();

            // after adding
            if (result> 0)
            {
                MessageBox.Show("Kitap başarıyla eklendi");
                UpdateBookGenres();
                UpdateAuthors();
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox4.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Kitap eklenirken bir sorunla karşılaşıldı");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 dataForm = new Form2("users");
            dataForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 dataForm = new Form2("books");
            dataForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 dataForm = new Form2("genres");
            dataForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 dataForm = new Form2("authors");
            dataForm.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}