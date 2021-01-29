using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityFramework.DAL;
using EntityFramework.DatabaseContext;
using EntityFramework.Models;

namespace EntityFramework
{
    public partial class BookGrid : Form
    {
        public BookGrid()
        {
            InitializeComponent();
            GetAllData();
            GetGenre();
        }
        BookDAL bookdb = new BookDAL();
        private void GetAllData()
        {
            using (BookDbContext context = new BookDbContext())
            {
                var bookdata = context.Books.Select(book => new
                {
                    book.Id,
                    book.Name,
                    book.Author,
                    book.GenreId,
                    Genre = book.Genre.Name

                }).ToList();
                dgvBook.DataSource = bookdata;
            }
        }

        private void GetGenre()
        {
            using (BookDbContext context = new BookDbContext())
            {
                foreach (Genre genre in context.Genres.ToList())
                {
                    cmbGenre.Items.Add(genre.Id + "-" + genre.Name);
                }
            }
        }
        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txbName.Text = dgvBook.CurrentRow.Cells[1].Value.ToString();
            txbAuthor.Text = dgvBook.CurrentRow.Cells[2].Value.ToString();
            cmbGenre.SelectedItem = dgvBook.CurrentRow.Cells[3].Value.ToString() + "-" + dgvBook.CurrentRow.Cells[4].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Book book = new Book()
            {
                Id = Convert.ToInt32(dgvBook.CurrentRow.Cells[0].Value),
                Name = txbName.Text,
                Author = txbAuthor.Text,
                GenreId = Convert.ToInt32(cmbGenre.Text.Split('-')[0])

            };
            bookdb.UpdateBook(book);
            GetAllData();
        }
    }
}
