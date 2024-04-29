using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QV596H_HFT_2023241.Endpoint.Services;
using QV596H_HFT_2023241.Logic;
using System.Collections;
using static QV596H_HFT_2023241.Logic.BrandsLogic;

namespace QV596H_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Statcontroller : ControllerBase
    {
        ICarLogic logic1;
        IHubContext<SignalRHub> hub;

        IBrandsLogic logic2;


        IRentalLogic logic3;
        public Statcontroller(ICarLogic logic1, IBrandsLogic logic2, IRentalLogic logic3, IHubContext<SignalRHub> hub)
        {
            this.logic1 = logic1;
            this.logic2 = logic2;
            this.logic3 = logic3;
            this.hub = hub;
        }
        [HttpGet("{brandId}")]
        public int? CountCarsForBrand(int brandId)
        {
            return this.logic2.CountCarsForBrand(brandId);
        }

        [HttpGet]
        public ActionResult<BrandWithCarCount> FindBrandWithMostCars() =>
            Ok(logic2.FindBrandWithMostCars());

        [HttpGet]
        public string GetMostPopularBrand() =>
           logic1.GetMostPopularBrand();

        [HttpGet("{carId}")]
        public int CountRentalEvents(int carId) =>
            logic1.CountRentalEvents(carId);
        [HttpGet]
        public bool IsThereAnOngoingRental() =>
            logic3.IsThereAnOngoingRental();
    }
}
