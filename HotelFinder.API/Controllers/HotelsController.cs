using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers //web project ve api controller eklendi
{
    [Route("api/[controller]")] // [controller] ile controller ismi dinamik gelir
    [ApiController] //mvc controllerdan farklı özellikler kazandırır örneğin kendisi model gibi bazı validation kontrollerini yapar. default gelir zorunlu değil, olmasada çalışır
    public class HotelsController : ControllerBase
    {
        //buradaki halka açık metodlar oluşturulacak, angular vs buradaki httpget/post/put/delete ile çağıracak
        //db metodlarına artık businessdan ulaşacağız; business data katmanından alıyor, bu taraf ise businessdan ulaşacak metodlara

        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService; //HotelManager'den alınacaklar bunun için startup'da scopelar yazılmalı
        }
        //default olarak [HttpGet] tir
        // /api/hotels
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        //public List<Hotel> Get() //ismi get olmak zorunda değil
        //{
        //    return _hotelService.GetAllHotels(); //List<Hotel> dönüyor ancak bir response handle yok, bunun için IActionResult kullanılmalı
        //}
        public async Task<IActionResult> Get() //ismi get olmak zorunda değil | burası bir endpointtir async çalışan
        {
            var hotels = await _hotelService.GetAllHotels(); //hotelrepositoryde async yapıldığı için buradada yapılmalı
            return Ok(hotels); // 200 + data dönecek
        }

        // /api/hotels/7
        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("{id}")] //id bekliyor ve int id deli isim ile aynı olmak zorunda ama id olmak zorunda değiller
        [Route("GetHotelById/{id}")] //api/hotels/gethotelbyid/2 //action overloading yaşanmaması için route kullanırız
        //[Route("[action]/{id}")] // böylece action namei dinamik alabiliriz
        //public Hotel Get(int id)
        //{
        //    return _hotelService.GetHotelById(id);
        //}
        public IActionResult Get(int id)
        {
            var hotel = _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel); //200 + data
            }
            return NotFound(); //404
        }

        [Route("GetHotelByName/{name}")] //api/hotels/GetHotelByName/hilton
        public IActionResult Get(string name) //int id ile çakışma yaşar action overloading
        {
            var hotel = _hotelService.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel); //200 + data
            }
            return NotFound(); //404
        }

        [Route("GetHotelByIdAndName{id}/{name}")] //api/hotels/GetHotelByIdAndName/2/hilton
        public IActionResult Get(int id, string name) //int id ile çakışma yaşar action overloading
        {
            return Ok();
        }
        [Route("GetHotelByIdAndName")] //querystring ile //api/hotels/GetHotelByIdAndName?id=1&name=hilton
        public IActionResult Get2(int id, string name)
        {
            return Ok();
        }

        /// <summary>
        /// Create the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost] //default değildir eklenmelidir
        [Route("CreateHotel")] // post ile /api/hotels/CreateHotel
        //public Hotel Post([FromBody]Hotel hotel) //ismi post olmak zorunda değil, requestin body'sinde hotel nesnesi olmalı:FromBody
        //{
        //    return _hotelService.CreateHotel(hotel);
        //}
        public IActionResult Post([FromBody]Hotel hotel) //ismi post olmak zorunda değil, requestin body'sinde hotel nesnesi olmalı:FromBody
        {
            if (ModelState.IsValid) //gelen hotel datası geçerli ise, modeldeki annotionlara görea. apicontroller üstte var ise gerek yok buna 
            {
                var createdHotel = _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201 Created + data ve url location vs döner
            }
            return BadRequest(ModelState);//400 + validation errors
        }

        // /api/hotels postmandan kontrol
        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateHotel")] // post ile /api/hotels/UpdateHotel
        //public Hotel Put([FromBody]Hotel hotel)
        //{
        //    return _hotelService.UpdateHotel(hotel);
        //}
        public IActionResult Put([FromBody]Hotel hotel)
        {
            if (_hotelService.GetHotelById(hotel.Id) != null)
            {
                _hotelService.UpdateHotel(hotel);
                return Ok(hotel); //200+data
            }
            return NotFound();
        }

        // api/hotels/1
        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id"></param>
        //[HttpDelete("{id}")] gerek kalmadı route ile id verildi
        [HttpDelete]
        [Route("DeleteHotel/{id}")] // post ile /api/hotels/DeleteHotel/2
        //public void Delete(int id)
        //{
        //    _hotelService.DeleteHotel(id);
        //}
        public IActionResult Delete(int id)
        {
            if (_hotelService.GetHotelById(id) != null)
            {
                _hotelService.DeleteHotel(id);
                return Ok(); //200
            }
            return NotFound();
        }
    }
}