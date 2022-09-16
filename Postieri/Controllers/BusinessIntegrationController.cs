using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
//using System.Web.Http;
using System.Web;
//using System.Runtime.Remoting.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postieri.Data;
using Postieri.DTO;
using Postieri.Models;
namespace Postieri.Controllers
{
    public class BusinessIntegrationController :ControllerBase
        
    {
        public Business business = new Business();
        public ClientOrder clientOrder = new ClientOrder();

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public BusinessIntegrationController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("AddClientOrder")]
        public async Task<ActionResult<string>> AddClientOrder(ClientOrderDto request, string jwt)
        {
            //decryp
            //var convertjwt to normal and check if its valid
            var bussinesExists = _context.Businesses.Where(x => x.BusinessToken == jwt).FirstOrDefault();
            //var businestoken = _context.Bussinesses.DistinctBy(x => x.BusinessToken).ToList();
            if (bussinesExists == null)
            {
                return BadRequest("token not found");
            }
            clientOrder.CompanyToken = jwt;
            clientOrder.ProductId = request.ProductId;
            clientOrder.Date = request.Date;
            clientOrder.Address = request.Address;
            clientOrder.Phone = request.Phone;
            clientOrder.Email = request.Email;
            clientOrder.Location = request.Location;
            clientOrder.Price = request.Price;
            _context.ClientOrders.Add(clientOrder);
            await _context.SaveChangesAsync();
            //return Ok("order successfully created  with the id as followos:"+order.OrderId);
            return Ok(clientOrder);
        }
        [HttpPost("SaveBusiness")]
        public async Task<ActionResult<string>> SaveBusiness(BusinessDto request)
        {
            business.BusinessName = request.BusinessName;
            business.BusinessToken = CreateToken(business);

            if (business.BusinessName != request.BusinessName)
            {
                return BadRequest("not found");
            }

            string token = CreateToken(business);
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();
            return Ok(token);
        }
        private string CreateToken(Business b)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, b.BusinessName),

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
