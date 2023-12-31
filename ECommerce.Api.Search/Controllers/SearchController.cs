﻿using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
            if (searchTerm == null)
            {
                return NotFound();
            }
            var result = await searchService.SearchAsync(searchTerm.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResult);
            }
            return NotFound();


        }
    }
}
