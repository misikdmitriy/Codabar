using System.Data.Common;
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
        private static readonly string GetAllProducts = "select * from [dbo].[Products] p order by p.[Id]";
        private static readonly string InsertProduct = "insert into [dbo].[Products](Name, Code) values(@Name, @Code)";
        private static readonly string DeleteProductById = "delete p from [dbo].[Products] p where p.[Id] = @Id";
        private static readonly string GetOneProductById = "select * from [dbo].[Products] p where p.[Id] = @Id order by p.[Id]";

        [HttpGet]
        public IActionResult ProductGet()
        {
            using (var conn = CreateConnection())
            {
                var products = conn.Query<Product>(GetAllProducts);

                return Ok(products);
            }
        }

        [HttpPost]
        public IActionResult ProductSave([FromBody]Product product)
        {
            using (var conn = CreateConnection())
            {
                conn.Execute(InsertProduct, new { Name = product.Name, Code = product.Code });
            }

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult ProductDelete(int id)
        {
            using (var conn = CreateConnection())
            {
                conn.Execute(DeleteProductById, new { Id = id });
            }

            return Ok();
        }


        [HttpGet]
        [Route("codabar/{id}")]
        public IActionResult Codabar(int id)
        {
            using (var conn = CreateConnection())
            {
                var product = conn.Query<Product>(GetOneProductById, new { Id = id })
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

        [HttpPost]
        [Route("decode/{fileName}")]
        public IActionResult Decode(string fileName)
        {
            using (var conn = CreateConnection())
            {
                var id = int.Parse(fileName.Split(".")[0]);

                var product = conn.Query<Product>(GetOneProductById, new {Id = id})
                    .FirstOrDefault();

                if (product == null)
                {
                    return BadRequest("Unrecognised image");
                }

                return Ok(product);
            }
        }

        private DbConnection CreateConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return new SqlConnection(builder.Build().GetConnectionString("DefaultConnection"));
        }
    }
}
