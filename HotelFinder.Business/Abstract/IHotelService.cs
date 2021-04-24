using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.Business.Abstract
{
    public interface IHotelService // içi boş imza metodlarım burada olacak ve inherit edenlere fikir verecek böylece -> concrete(somut içi dolu)'den çağırılacaklar
    {
        Task<List<Hotel>> GetAllHotels();
        Hotel GetHotelById(int id);
        Hotel GetHotelByName(string name);
        Hotel CreateHotel(Hotel hotel);
        Hotel UpdateHotel(Hotel hotel);
        void DeleteHotel(int id);
    }
}
