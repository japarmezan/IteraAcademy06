using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BloggingPlatform.Dto;
using AutoMapper;
using BloggingPlatform.Api.Managers.Interfaces;

namespace BloggingPlatform.Api.Controllers
{
    [Produces("application/json")]
    [Route("author")]
    public class AuthorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthorManager _authorManager;

        public AuthorsController(IMapper mapper, IAuthorManager authorManager)
        {
            _mapper = mapper;
            _authorManager = authorManager;
        }
                
        [HttpGet]
        public IEnumerable<AuthorDto> GetAuthors()
        {
            return _authorManager.GetAuthors();
        }        
    }
}