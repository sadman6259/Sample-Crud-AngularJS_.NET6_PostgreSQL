using Microsoft.AspNetCore.Mvc;

namespace TestNETCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private Test_dbContext context = null;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Test_dbContext context)
        {
            _logger = logger;
            this.context = context;

        }

        [HttpGet]
        [Route("~/get/accounts")]
        public async Task<List<Account>> Get()
        {
            IQueryable<Account> queryable = this.context.Set<Account>().Select(x => x);

            var res = await Task.FromResult(queryable.ToList());
            return res;

        }


        [HttpPost]
        [Route("~/add/accounts/{accounts}")]
        public void AddAccounts([FromBody] Account accounts)
        {
            this.context.Accounts.Add(accounts);
            context.SaveChanges();
        }

        [HttpPost]
        [Route("~/update/accounts/{accounts}")]
        public void UpdateAccounts([FromBody] Account accounts)
        {
           
            this.context.Accounts.Update(accounts);
            this.context.SaveChanges();
        }

        [HttpPost]
        [Route("~/delete/accounts/{id}")]

        public void DeleteAccounts(int id)
        {
            var entity = this.context.Accounts.FirstOrDefault(t => t.UserId == id);
            if(entity != null)
            {
                this.context.Accounts.Remove(entity);
                this.context.SaveChanges();
            }
          
        }
    }
}