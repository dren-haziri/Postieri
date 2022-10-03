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
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public BusinessIntegrationService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
       public bool AddClientOrder(ClientOrderDto request)
        {
            var clientOrder = new ClientOrder()
            {
               CompanyToken = request.JWT,
               ProductId = request.ProductId,
               Date = request.Date,
               Address = request.Address,
               Phone = request.Phone,
               Email = request.Email,
               Location = request.Location,
               Price = request.Price,
            };
            var bussinesExists = _context.Businesses.Where(x => x.BusinessToken == request.JWT).FirstOrDefault();
            if (bussinesExists == null)
            {
                return false;
            }
            _context.ClientOrders.Add(clientOrder);
                _context.SaveChangesAsync();
                return true;      
        }

       public bool SaveBusiness(BusinessDto request)
        {
            var business = new Business(){};
            var alreadyExist = _context.Businesses.Where(x => x.BusinessName == request.BusinessName & x.Email == request.Email & x.PhoneNumber == request.PhoneNumber).FirstOrDefault();
            business.BusinessName = request.BusinessName;
            business.Email = request.Email;
            business.PhoneNumber = request.PhoneNumber;
            business.BusinessToken = CreateToken(business);
            if (business.BusinessName == null || business.BusinessName == "")
            {
                return false;
            }
            else if (alreadyExist != null)
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
