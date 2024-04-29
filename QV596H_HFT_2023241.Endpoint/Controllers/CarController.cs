using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using QV596H_HFT_2023241.Endpoint.Services;
using QV596H_HFT_2023241.Logic;
using QV596H_HFT_2023241.Models;
using System.Collections.Generic;

namespace QV596H_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        ICarLogic logic;
        IHubContext<SignalRHub> hub;

        public CarController(ICarLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Car> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Car Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] Car value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CarCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Car value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CarUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var carToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CarDeleted", carToDelete);
        }
    }
}
