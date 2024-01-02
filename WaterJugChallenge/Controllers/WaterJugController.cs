using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterJugChallenge.Model;

using WaterJugChallenge.handler;
using Newtonsoft.Json;
using static WaterJugChallenge.handler.HandlerWaterJugSolution;
namespace WaterJugChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WaterJugController : ControllerBase
    {

        [HttpPost]
        [Route("/")]
        public IActionResult waterJugResponse([FromBody] WaterJugModel data)
        {
            try
            {

                List<string> solution = HandlerWaterJugSolution.SolveWaterBucketProblem(data.jugX, data.jugY,data.jugZ);
                List<WaterJugRespone> newSolution = new List<WaterJugRespone>();

                try
                {
                    foreach (string s in solution)
                    {
                        WaterJugRespone aux = JsonConvert.DeserializeObject<WaterJugRespone>(s);
                        newSolution.Add(aux);
                  

                    }
                    return StatusCode(StatusCodes.Status200OK, new { solution= newSolution });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new {  solution });

                }


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});

            }
        }
    }
}
