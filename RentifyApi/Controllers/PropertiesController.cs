using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentify.DAL.Context;
using Rentify.Models;
using System.Security.Claims;

namespace RentifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _context.Properties.Include(p => p.Owner).ToListAsync();
            return Ok(properties);
        }

        [HttpPost]
        public async Task<IActionResult> PostProperty(Property propertyDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var property = new Property
            {
                OwnerId = userId,
                Place = propertyDto.Place,
                Area = propertyDto.Area,
                Bedrooms = propertyDto.Bedrooms,
                Bathrooms = propertyDto.Bathrooms,
                Amenities = propertyDto.Amenities
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Property posted successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, Property propertyDto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            property.Place = propertyDto.Place;
            property.Area = propertyDto.Area;
            property.Bedrooms = propertyDto.Bedrooms;
            property.Bathrooms = propertyDto.Bathrooms;
            property.Amenities = propertyDto.Amenities;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Property updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Property deleted successfully" });
        }
    }

}
