using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiVersion("2.0" , Deprecated = true)]
    [ApiController]
    [Route("api/v{version:apiVersion}/v2")]
    public class BooksControllerV2 : ControllerBase
    {
        private readonly IServiceManager _services;

        public BooksControllerV2(IServiceManager services)
        {
            _services = services;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var result = await _services.BookService.GetAllBooksAsync(false);
            var booksV2 = result.Select(book => new
            {
                Title = book.Title,
                Id = book.Id
            });
            return Ok(booksV2);
        }
    }
}
