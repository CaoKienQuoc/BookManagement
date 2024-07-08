using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.BookAuthor;
using PRN231_Group.Assigment.API.Repo.Interface;
using System;
using System.Collections.Generic;
using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Controllers
{
    [Route("api/book-authors")]
    [ApiController]
    public class BookAuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookAuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<BookAuthorViewModel> Get()
        {
            var bookAuthors = _unitOfWork.BookAuthorRepository.Get();
            var bookAuthorList = new List<BookAuthorViewModel>();

            foreach (var bookAuthor in bookAuthors)
            {
                var result = _mapper.Map<BookAuthorViewModel>(bookAuthor);
                bookAuthorList.Add(result);
            }

            return Ok(bookAuthorList);
        }

        [HttpGet("{id}")]
        public ActionResult<BookAuthorViewModel> Get(int id)
        {
            var bookAuthor = _unitOfWork.BookAuthorRepository.GetByID(id);

            if (bookAuthor == null)
            {
                return NotFound("Book author not found");
            }

            var result = _mapper.Map<BookAuthorViewModel>(bookAuthor);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(BookAuthorRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide book author data");
            }

            try
            {
                var bookAuthor = _mapper.Map<BookAuthor>(model);
                _unitOfWork.BookAuthorRepository.Insert(bookAuthor);
                _unitOfWork.Save();

                var result = _mapper.Map<BookAuthorViewModel>(bookAuthor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, BookAuthorRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide book author data");
            }

            var bookAuthor = _unitOfWork.BookAuthorRepository.GetByID(id);

            if (bookAuthor == null)
            {
                return NotFound("Book author not found");
            }

            try
            {
                _mapper.Map(model, bookAuthor);
                _unitOfWork.BookAuthorRepository.Update(bookAuthor);
                _unitOfWork.Save();

                var result = _mapper.Map<BookAuthorViewModel>(bookAuthor);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookAuthor = _unitOfWork.BookAuthorRepository.GetByID(id);

            if (bookAuthor == null)
            {
                return NotFound("Book author not found");
            }

            try
            {
                _unitOfWork.BookAuthorRepository.Delete(bookAuthor);
                _unitOfWork.Save();

                return Ok("Book author deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
