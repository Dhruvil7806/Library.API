using System;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Library.API.Data;

namespace Library.API.Controllers
{
    [RoutePrefix("api/borrowrecords")]
    public class BorrowRecordsController : ApiController
    {
        private readonly LibraryDbContext _context;

        public BorrowRecordsController()
        {
            _context = new LibraryDbContext();
        }

        // GET: api/borrowrecords?page=1&pageSize=10
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBorrowRecords(int page = 1, int pageSize = 10)
        {
            var records = _context.BorrowRecords
                .Include("Book") // Use string for navigation properties in EF6
                .Include("Member")
                .OrderBy(br => br.ID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(records);
        }

        // POST: api/borrowrecords
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateBorrowRecord([FromBody] BorrowRecord borrowRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BorrowRecords.Add(borrowRecord);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + borrowRecord.ID), borrowRecord);
        }

        // PUT: api/borrowrecords/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateReturnDate(int id, [FromBody] DateTime returnDate)
        {
            var record = _context.BorrowRecords.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.ReturnDate = returnDate;
            _context.SaveChanges();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
