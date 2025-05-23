﻿using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]

    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;

        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        [HttpGet (Name = "GetRoot")]
        public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.berkay.apiroot"))
            {
                Link link1 = new Link()
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new { }),
                    Rel = "_self",
                    Method = "GET"
                };
                Link link2 = new Link()
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.GetAllBooksAsync), new { }),
                    Rel = "books",
                    Method = "GET"
                };
                Link link3 = new Link()
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(BooksController.GetAllBooksAsync), new { }),
                    Rel = "books",
                    Method = "POST"
                };
                var list = new List<Link> ();

                list.Add (link1);
                list.Add(link2);
                list.Add(link3);

                return Ok(list);
            }
            return NoContent(); // 204 belirlenen sartlar gerceklesmedi
        }
    }
}
