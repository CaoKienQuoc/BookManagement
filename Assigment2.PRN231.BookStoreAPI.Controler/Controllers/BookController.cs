using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PRN231_Group.Assigment.API.Repo.Implement;
using PRN231_Group.Assigment.API.Repo.Interface;
using System.Linq.Expressions;
// Make sure to import BookViewModel and BookRequestModel

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public int? currentPage { get; set; }
        public int TotalPages { get; set; }
        public int? pageSize { get; set; }

        public BooksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            pageSize = _unitOfWork.BookRepository.Get().Count();
            currentPage = 1;
        }
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<BookModelResponse>> GetBooks([FromQuery] BookSearchModel search)
        {
            if (search.pageSize != null)
            {
                pageSize = search.pageSize;
            }
            if (search.currentPage != null)
            {
                currentPage = search.currentPage;
            }
            Expression<Func<Book, bool>> filter = null;
            if (search.pubId.HasValue)
            {
                filter = filter.And(p => p.PubId == search.pubId);
            }
            if (search.title != null)
            {
                filter = filter.And(p => p.Title.Contains(search.title));
            }
            if (search.type != null)
            {
                filter = filter.And(p => p.Type.Contains(search.type));
            }
            if (search.minPrice.HasValue || search.maxPrice.HasValue)
            {
                if (filter == null)
                {
                    filter = p => true;
                }
                if (search.minPrice.HasValue)
                {
                    filter = filter.And(p => p.Price >= search.minPrice);
                }
                if (search.maxPrice.HasValue)
                {
                    filter = filter.And(p => p.Price <= search.maxPrice);
                }
            }
            if (search.minDate.HasValue || search.maxDate.HasValue)
            {
                if (filter == null)
                {
                    filter = p => true;
                }
                if (search.minDate.HasValue)
                {
                    filter = filter.And(p => p.PublishedDate >= search.minDate);
                }
                if (search.maxDate.HasValue)
                {
                    filter = filter.And(p => p.PublishedDate <= search.maxDate);
                }
            }
            Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null;
            if (search.sortByPrice == true && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p => p.Price);
            }
            else if (search.sortByPrice == true && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.Price);
            }
            else if (search.sortByDate == true && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p => p.PublishedDate);
            }
            else if (search.sortByDate == true && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.PublishedDate);
            }
            else if (search.sortByPrice == false && search.sortByDate == false && search.descending == true)
            {
                orderBy = p => p.OrderByDescending(p => p.Title);
            }
            else if (search.sortByPrice == false && search.sortByDate == false && search.descending == false)
            {
                orderBy = p => p.OrderBy(p => p.Title);
            }


            var books = _unitOfWork.BookRepository.Get(filter, orderBy, includeProperties: "Pub", currentPage, pageSize).ToList();
            var list = new List<BookViewModel>();
            foreach (var book in books)
            {
                var dto = new BookViewModel
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Type = book.Type,
                    Price = book.Price,
                    Advance = book.Advance,

                    Publisher = new BookViewModel.PublisherModel
                    {
                        Name = book.Pub.PublisherName,
                    }
                };
                list.Add(dto);
            }

            var total = _unitOfWork.BookRepository.Get(filter).Count();
            BookModelResponse result = new BookModelResponse();
            result.total = total;
            result.currentPage = currentPage.Value;
            result.books = list.ToList();

            return Ok(result);
        }
        [EnableQuery]
        [HttpPost]
        public IActionResult Post(BookRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide book data");
            }
            var pub = _unitOfWork.PublisherRepository.Get(p => p.PublisherName == model.PubName).FirstOrDefault();

            try
            {
                var book = _mapper.Map<Book>(model);
                book.PubId = pub.PubId;
                _unitOfWork.BookRepository.Insert(book);
                _unitOfWork.Save();

                var result = _mapper.Map<BookRequestModel>(book);
                result.PubName = model.PubName;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableQuery]
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateBookRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide book data");
            }

            var book = _unitOfWork.BookRepository.GetByID(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            try
            {
                _mapper.Map(model, book);
                _unitOfWork.BookRepository.Update(book);
                _unitOfWork.Save();

                var result = _mapper.Map<UpdateBookRequestModel>(book);
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
            var book = _unitOfWork.BookRepository.GetByID(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            try
            {
                _unitOfWork.BookRepository.Delete(book);
                _unitOfWork.Save();

                return Ok("Book deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
