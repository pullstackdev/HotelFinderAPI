using HotelFinder.Business.Abstract;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;
using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.Business.Concrete
{
    public class HotelManager : IHotelService //inherit edildi
    {
        //artık buradaki metodlar data katmanından HotelRepository'den alacak metodları

        private IHotelRepository _hotelRepository; //çağırıldı
        public HotelManager(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository; //HotelRepository'den alınacaklar bunun için startup'da scopelar yazılmalı
        }

        public Hotel CreateHotel(Hotel hotel)
        {
            return _hotelRepository.CreateHotel(hotel);
        }

        public void DeleteHotel(int id)
        {
            _hotelRepository.DeleteHotel(id);
        }

        public async Task<List<Hotel>> GetAllHotels() //Hotel repositoryde async yapıldığı için burasıda yapılmalı
        {
            return await _hotelRepository.GetAllHotels();
        }

        public Hotel GetHotelById(int id)
        {
            if (id > 0)
            {
                return _hotelRepository.GetHotelById(id);
            } //gibi logicler burada kodlanabilir
            throw new Exception("id cannot be less than 1");
        }

        public Hotel GetHotelByName(string name)
        {

            return _hotelRepository.GetHotelByName(name);
        }

        public Hotel UpdateHotel(Hotel hotel)
        {
            return _hotelRepository.UpdateHotel(hotel);
        }
    }
}
