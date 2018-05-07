using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Codabar.Base;
using CodabarWeb.Models;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp.Formats.Png;

namespace CodabarWeb.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class ProductsController : Controller
    {
        private static readonly string GetAllQuery =
            "select * from [dbo].[Products] p join [dbo].[Units] u on p.[UnitId] = u.[Id]" +
            "order by p.[Id]";

        private static readonly string GetOneQuery =
            "select * from [dbo].[Products] p join [dbo].[Units] u on p.[UnitId] = u.[Id]" +
            "where p.[Id] = @Id order by p.[Id]";

        [HttpGet]
        public IActionResult Get()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            using (var conn =
                new SqlConnection(builder.Build().GetConnectionString("DefaultConnection")))
            {
                var products = conn.Query<Product, Unit, Product>(GetAllQuery, (product, unit) =>
                {
                    product.Unit = unit;
                    return product;
                });

                return Ok(products);
            }
        }

        [HttpGet]
        [Route("codabar/{id}")]
        public IActionResult Codabar(int id)
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            using (var conn =
                new SqlConnection(confBuilder.Build().GetConnectionString("DefaultConnection")))
            {
                var product = conn.Query<Product>(GetOneQuery, new { Id = id })
                    .First();

                var builder = new StringRepresentationBuilder();
                var bits = builder.ToCodabar($"a{product.Code}b");

                var @params = new CodabarParams(10, 125);

                using (var image = new BitmapConverter().Convert(bits, @params))
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, new PngEncoder());
                    return File(stream.GetBuffer(), 
                        System.Net.Mime.MediaTypeNames.Application.Octet,
                        $"{id}.png");
                }
            }
        }
    }
}
