// See https://aka.ms/new-console-template for more information
using Postieri.DTO;
using Postieri.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ClientTesting
{

    class Program
    {
        public static HttpClient httpClient = new HttpClient();
        static void Main(String[] args)
        {
            Program program = new Program();
            program.AddOrder();
        }


        private void AddOrder()
        {
            OrderDto clientOrder = new OrderDto();
            clientOrder.JWT = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQmVyc2hrYSIsImV4cCI6MTY2MzQyNTk0NX0.hEywaHHLSUJpkSXelSmonPkLqH0QkgbwAHcy0TNE7p1MXtWxQLA97BAvi01bFCrsXWfg0aE2_E2LmHpLFXtlvQ";
            clientOrder.ProductId = Guid.Parse("135CC208-FC14-4AE8-93DC-08DA97F3FDBF");
            clientOrder.Date = DateTime.Parse("2022-09-23T15:15:51+00:00");
            clientOrder.Address = "Kosovo";
            clientOrder.Phone = 044123321;
            clientOrder.Email = "reshaniflorentinaTest2@gmail.com";
            //clientOrder.Price = 10;
            clientOrder.OrderedOn = DateTime.Parse("2022-09-23T15:15:51+00:00");

            HttpResponseMessage httpResponseMessage = httpClient.PostAsJsonAsync("https://localhost:7134/api/BusinessIntegration", clientOrder).Result;
            Console.WriteLine("successfully added a new order");

        }

    }
}

