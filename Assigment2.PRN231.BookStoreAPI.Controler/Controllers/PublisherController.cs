using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Publisher;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PRN231_Group.Assigment.API.Repo.Interface;


namespace Assigment2.PRN231.BookStoreAPI.Controlers.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublishersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [EnableQuery]
        [HttpGet]
        public ActionResult<PublisherViewModel> Get()
        {
            var publishers = _unitOfWork.PublisherRepository.Get();

            var publisherList = new List<PublisherViewModel>();

            foreach (var publisher in publishers)
            {
                var result = _mapper.Map<PublisherViewModel>(publisher);
                publisherList.Add(result);
            }

            return Ok(publisherList);
        }
        [EnableQuery]
        [HttpGet("{id}")]
        public ActionResult<PublisherViewModel> Get(int id)
        {
            var publisher = _unitOfWork.PublisherRepository.GetByID(id);

            if (publisher == null)
            {
                return NotFound("Publisher not found");
            }

            var result = _mapper.Map<PublisherViewModel>(publisher);
            return Ok(result);
        }
        [EnableQuery]
        [HttpPost]
        public IActionResult Post(PublisherRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide publisher data");
            }

            try
            {
                var publisher = _mapper.Map<Publisher>(model);
                _unitOfWork.PublisherRepository.Insert(publisher);
                _unitOfWork.Save();

                var result = _mapper.Map<PublisherViewModel>(publisher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [EnableQuery]
        [HttpPut("{id}")]
        public IActionResult Put(int id, PublisherRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Please provide publisher data");
            }

            var publisher = _unitOfWork.PublisherRepository.GetByID(id);

            if (publisher == null)
            {
                return NotFound("Publisher not found");
            }

            try
            {
                _mapper.Map(model, publisher);
                _unitOfWork.PublisherRepository.Update(publisher);
                _unitOfWork.Save();

                var result = _mapper.Map<PublisherViewModel>(publisher);
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
            var publisher = _unitOfWork.PublisherRepository.GetByID(id);

            if (publisher == null)
            {
                return NotFound("Publisher not found");
            }

            try
            {
                _unitOfWork.PublisherRepository.Delete(publisher);
                _unitOfWork.Save();

                return Ok("Publisher deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
