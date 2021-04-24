using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; //for ToList()
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository //inherit edildi ve business da metodlar buradan çağırılacak data katmanı burası
    {
        //db metodları somut..
        public Hotel CreateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                hotelDbContext.Hotels.Add(hotel);
                hotelDbContext.SaveChanges();
                return hotel;
            }
        }

        public void DeleteHotel(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                var deletedHotel = hotelDbContext.Hotels.Find(id);
                hotelDbContext.Remove(deletedHotel);
                hotelDbContext.SaveChanges();
            }
        }

        public async Task<List<Hotel>> GetAllHotels() //gelen requestleri ölçeklendirebilmek, daha fazla requeste cevap vermesi için async yapıldı
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                //return hotelDbContext.Hotels.ToList(); dışa (db işlemi vs) bağımlı ise bu metdou async yapmalıyız
                return await hotelDbContext.Hotels.ToListAsync(); //ToListAsync, await ve metoda async ve Task eklendi
            }
        }

        public Hotel GetHotelById(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return hotelDbContext.Hotels.Find(id); //id primary key olduğu için find işe yaradı. yoksa firstordefault kullanılmak zorunda kalınacaktı
            }
        }

        public Hotel GetHotelByName(string name)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return hotelDbContext.Hotels.FirstOrDefault(x => x.Name.ToLower() == name.ToLower()); //id primary key olduğu için find işe yaradı. yoksa firstordefault kullanılmak zorunda kalınacaktı
            }
        }

        public Hotel UpdateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                hotelDbContext.Hotels.Update(hotel);
                hotelDbContext.SaveChanges();
                return hotel;
            }
        }
    }
}
