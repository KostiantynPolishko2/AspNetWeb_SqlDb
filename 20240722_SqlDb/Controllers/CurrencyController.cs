using _20240722_SqlDb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace _20240722_SqlDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyContext _currencyContext;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger, CurrencyContext currencyContext) { 
            _logger = logger;
            _currencyContext = currencyContext;
        }

        [HttpGet(Name = "GetCurrencies")]
        public ActionResult<IEnumerable<Currency?>> Get() {

            if (_currencyContext.Currencies == null)
            {
                return NotFound(new { Message = "Db Currencies is empty" });
            }
            return _currencyContext.Currencies.ToList();

        }

        [HttpGet("{Name}:string")]
        //[Route("{Name}:string")]
        public ActionResult<Currency?> Get([Required] string Name) { 

            Currency? currency = _currencyContext?.Currencies?.ToList().Find(x => x.Name == Name.ToUpper());

            return currency == null ? NotFound() : Ok(currency);
        }

        [HttpPost(Name = "PostCurrency")]
        public ActionResult<Currency> Post([Required] Currency currency) {

            if (currency == null)
            {
                return NotFound(new { Message = "Db Currencies is empty" });
            }
            else if (currency.Rate <= 0)
            {
                return BadRequest("rate can not be less or equa 0");
            }
            _currencyContext?.Currencies?.Add(currency);
            _currencyContext?.SaveChanges();

            return Ok(new { StatusCode = 200, Message = "Done"});
        }

        [HttpDelete("DeleteByName")]
        public ActionResult<Currency> Delete([Required] string Name)
        {
            if (_currencyContext.Currencies == null)
            {
                return NotFound(new { Message = "Db Currencies is empty" });
            }

            Currency? currency = _currencyContext?.Currencies?.ToList().Find(x => x.Name == Name.ToUpper());
            if (currency != null) 
            {
                _currencyContext?.Currencies.Remove(currency);
                _currencyContext?.SaveChanges();
                return Ok(new { Message = "Deleted", Currency = currency });
            }

            return BadRequest($"Not found currency {Name}");
        }
    }
}
