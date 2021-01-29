using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DatabaseContext;
using EntityFramework.Models;

namespace EntityFramework.DAL
{
    class BookDAL
    {
        public void UpdateBook(Book book)
        {
            using(BookDbContext context = new BookDbContext())
            {
                context.Books.AddOrUpdate(book);
                context.SaveChanges();
            }
        }
    }
}
