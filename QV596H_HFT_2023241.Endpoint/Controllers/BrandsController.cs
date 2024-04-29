using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QV596H_HFT_2023241.Endpoint.Services;
using QV596H_HFT_2023241.Logic;
using QV596H_HFT_2023241.Models;
using System.Collections.Generic;


namespace QV596H_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        IBrandsLogic logic;
        IHubContext<SignalRHub> hub;

        public BrandsController(IBrandsLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Brands> Readall()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Brands Read(int id)
        { 
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Brands value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("BrandCreated", value);
        }
        [HttpPut]
        public void Update([FromBody] Brands value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("BrandUpdated", value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var brandToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("BrandDeleted", brandToDelete);
        }
    }
}
