using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Author;
using PRN231_Group.Assigment.API.Repo.Interface;
using System;
using System.Collections.Generic;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Author;
using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Microsoft.AspNetCore.OData.Query; // Make sure to import AuthorViewModel

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [EnableQuery]
        [HttpGet]
        public ActionResult<AuthorViewModel> Get()
        {
            var authors = _unitOfWork.AuthorRepository.Get();
            var authorList = new List<AuthorViewModel>();

            foreach (var author in authors)
            {
                var result = _mapper.Map<AuthorViewModel>(author);
                authorList.Add(result);
            }

            return Ok(authorList);
        }
        [EnableQuery]
        [HttpGet("{id}")]
        public ActionResult<AuthorViewModel> Get(int id)
        {
            var author = _unitOfWork.AuthorRepository.GetByID(id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            var result = _mapper.Map<AuthorViewModel>(author);
            return Ok(result);
        }
        [EnableQuery]
        [HttpPost]
        public IActionResult Post(AuthorRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide author data");
            }

            try
            {
                var author = _mapper.Map<Author>(model);
                _unitOfWork.AuthorRepository.Insert(author);
                _unitOfWork.Save();

                var result = _mapper.Map<AuthorRequestModel>(author);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableQuery]
        [HttpPut("{id}")]
        public IActionResult Put(int id, AuthorRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide author data");
            }

            var author = _unitOfWork.AuthorRepository.GetByID(id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            try
            {
                _mapper.Map(model, author);
                _unitOfWork.AuthorRepository.Update(author);
                _unitOfWork.Save();

                var result = _mapper.Map<AuthorRequestModel>(author);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableQuery]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = _unitOfWork.AuthorRepository.GetByID(id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            try
            {
                _unitOfWork.AuthorRepository.Delete(author);
                _unitOfWork.Save();

                return Ok("Author deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
