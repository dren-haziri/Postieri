using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postieri.Data;
using Postieri.DTO;
using Postieri.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Postieri.Services
{
    public class BusinessIntegrationService : IBusinessIntegrationService
    {
        public Business business = new Business();
        public ClientOrder clientOrder = new ClientOrder();
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public BusinessIntegrationService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       public bool AddClientOrder(ClientOrderDto request)
        {
            var bussinesExists = _context.Businesses.Where(x => x.BusinessToken == request.JWT).FirstOrDefault();
            clientOrder.CompanyToken = request.JWT;
            clientOrder.ProductId = request.ProductId;
            clientOrder.Date = request.Date;
            clientOrder.Address = request.Address;
            clientOrder.Phone = request.Phone;
            clientOrder.Email = request.Email;
            clientOrder.Location = request.Location;
            clientOrder.Price = request.Price;
            if (bussinesExists == null)
            {
                return false;
            }
            else
            {  _context.ClientOrders.Add(clientOrder);
                _context.SaveChangesAsync();
                return true;
            }       
        }

       public bool SaveBusiness(BusinessDto request)
        {
            business.BusinessName = request.BusinessName;
            business.Email = request.Email;
            business.PhoneNumber = request.PhoneNumber;
            business.BusinessToken = CreateToken(business);

            if (business.BusinessName == null || business.BusinessName == "")
            {
                return false;
            }
            else
            {
               string token = CreateToken(business);
               _context.Businesses.Add(business);
               _context.SaveChangesAsync();
               return true;
            }
        }

        private string CreateToken(Business b)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, b.BusinessName),
                new Claim(ClaimTypes.Email, b.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
