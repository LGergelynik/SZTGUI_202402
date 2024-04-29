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
    public class RentalController : ControllerBase
    {
        IRentalLogic logic;
        IHubContext<SignalRHub> hub;

        public RentalController(IRentalLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Rental> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Rental Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Rental value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RentalCreated", value);
        }
        [HttpPut]
        public void Update([FromBody] Rental value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RentalUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var rentalToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RentalDeleted", rentalToDelete);
        }
    }
}
