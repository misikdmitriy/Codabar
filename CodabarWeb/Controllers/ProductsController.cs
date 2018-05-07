using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
        private const string GetAllProducts = "select * from [dbo].[Products] p order by p.[Id]";
        private const string InsertProduct = "insert into [dbo].[Products](Name, Code) values(@Name, @Code)";
        private const string DeleteProductById = "delete p from [dbo].[Products] p where p.[Id] = @Id";
        private const string GetOneProductById = "select * from [dbo].[Products] p where p.[Id] = @Id order by p.[Id]";

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

                var algorithmArgs = new AlgorithmArgs
                {
                    LineHeight = 150,
                    LineWidth = 5,
                    StartSymbol = 'a',
                    EndSymbol = 'b',
                    Text = product.Code
                };

                using (var image = new CodabarCreator().Run(algorithmArgs))
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, new PngEncoder());

                    return File(stream.GetBuffer(), MediaTypeNames.Application.Octet, $"{id}.png");
                }
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
