using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore; //for DbContext
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelFinder.DataAccess
{
    public class HotelDbContext: DbContext //eklendi
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=DESKTOP-HGL39QT; database=HotelDb; integrated security=true;"); //connectionstring
        }

        public DbSet<Hotel> Hotels { get; set; } //db tablo ismi büyük ve çoğul öneri

        // Not: migration için 1) core.tools ve core.sqlserver yüklenmeli 2)dataaccess projet set up yapılmalı ve 3) console da default project dataaccess olmalı
    }
}
