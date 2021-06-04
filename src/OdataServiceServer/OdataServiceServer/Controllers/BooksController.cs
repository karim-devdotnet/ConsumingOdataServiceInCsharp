using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OdataServiceServer.Models;
using System.Linq;

namespace OdataServiceServer.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext _db;

        public BooksController(BookStoreContext context)
        {
            _db = context;
            if (_db.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    _db.Books.Add(b);
                    _db.Presses.Add(b.Press);
                }
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// GET http://localhost:5000/odata/Books
        /// GET http://localhost:5000/odata/Books?$filter=Price le 50
        /// GET http://localhost:5000/odata/Books?$filter=Price le 50&$expand=Press($select=Name)&$select=ISBN
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Books);
        }


        /// <summary>
        /// GET http://localhost:5000/odata/Books(2)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_db.Books.FirstOrDefault(c => c.Id == key));
        }

        /// <summary>
        /// POST http://localhost:5000/odata/Books
        /// Content-Type: application/json
        ///    Content:
        ///    {
        ///      "Id":3,"ISBN":"82-917-7192-5","Title":"Hary Potter","Author":"J. K. Rowling",
        ///              "Price":199.99,
        ///              "Location":{
        ///         "City":"Shanghai",
        ///                 "Street":"Zhongshan RD"
        ///       }
        ///            }
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Created(book);
        }
    }
}
