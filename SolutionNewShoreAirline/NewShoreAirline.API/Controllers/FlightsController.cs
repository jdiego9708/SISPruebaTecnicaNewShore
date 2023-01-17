using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NewShoreAirline.Entities.Models;
using NewShoreAirline.Entities.ModelsConfiguration;
using NewShoreAirline.Services.Interfaces;

namespace NewShoreAirline.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightsController : Controller
    {
        private readonly ILogger<FlightsController> logger;
        private IFlightService IFlightService { get; set; }
        public FlightsController(ILogger<FlightsController> logger,
            IFlightService IFlightService)
        {
            this.logger = logger;
            this.IFlightService = IFlightService;
        }

        [HttpPost]
        [Route("VerifyFlights")]
        public IActionResult VerifyFlights(JObject verifyJson)
        {
            try
            {
                logger.LogInformation("Start VerifyFlights");

                if (verifyJson == null)
                    throw new Exception("VerifyFlights info empty");

                FlightsSearchBindingModel searchModel = verifyJson.ToObject<FlightsSearchBindingModel>();
                
                if (searchModel == null)
                {
                    logger.LogInformation("Sin información de búsqueda");
                    throw new Exception("Sin información de búsqueda");
                }
                else
                {
                    RestResponseModel rpta = this.IFlightService.VerifyFlights(searchModel);
                    if (rpta.IsSucess)
                    {
                        logger.LogInformation($"Verify flight successfull");
                        return Ok(rpta.Response);
                    }
                    else
                    {
                        return BadRequest(rpta.Response);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error in controller VerifyFlights", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
