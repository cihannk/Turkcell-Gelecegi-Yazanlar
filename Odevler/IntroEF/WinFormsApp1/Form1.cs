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
            context = new BookstoreDbContext();
            UpdateBookGenres();
        }
        private void UpdateBookGenres()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(context.Genres.Select(x => x.Name).ToArray());
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            context.Authors.Add(new Author { BirthDate = dateTimePicker1.Value, FullName = textBox6.Text });
            int result = context.SaveChanges();

            switch (result) {
                case 1:
                    MessageBox.Show("Yazar başarıyla eklendi");
                    textBox6.Text = String.Empty;
                    dateTimePicker1.Value = DateTime.Now;

                    break;
                default:
                    MessageBox.Show("Yazar eklenirken bir sorunla karşılaşıldı");
                    break;
            }
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {

            BookstoreDbContext context = new BookstoreDbContext();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
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
            Book book = new Book
            {
                Title = textBox1.Text,
                Price = Double.Parse(textBox2.Text),
                AuthorId = Int32.Parse(textBox3.Text),
                Description = textBox4.Text
            };
            if (comboBox1.SelectedItem is not null)
            {
                string selectedItemName = comboBox1.SelectedItem.ToString();
                var genre = context.Genres.FirstOrDefault(gen => gen.Name == selectedItemName);
                book.Genres = new List<Genre> { genre };
            }
            else
            {
                MessageBox.Show("Genre seçimi yapınız");
                return;
            }
            context.Books.Add(book);

            int result = context.SaveChanges();

            if (result> 0)
            {
                MessageBox.Show("Kitap başarıyla eklendi");
                UpdateBookGenres();
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            else
            {
                MessageBox.Show("Kitap eklenirken bir sorunla karşılaşıldı");
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}