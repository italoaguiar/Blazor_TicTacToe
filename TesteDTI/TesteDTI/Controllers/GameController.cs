using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TesteDTI.Data;
using TesteDTI.Models;

namespace TesteDTI.Controllers
{
    /// <summary>
    /// Game Controller
    /// </summary>
    [Route("/[controller]")]
    public class GameController : Controller
    {
        /// <summary>
        /// Initialize the controller
        /// </summary>
        /// <param name="_service">GameService singleton instance</param>
        public GameController(GameService _service)
        {
            gameServiceInstance = _service;
        }

        //game service singleton instance
        GameService gameServiceInstance;


        /// <summary>
        /// POST:/game
        /// Generate a new game
        /// </summary>
        /// <returns>Object representing the created game</returns>
        [HttpPost]
        public ActionResult<GameRoom> Index()
        {
            return gameServiceInstance.NewGame();
        }


        /// <summary>
        /// Performs user movement
        /// </summary>
        /// <param name="id">Game identifier</param>
        /// <param name="m">Request Body</param>
        /// <returns>Empty if game is in progress. Winner if finalized.</returns>
        [HttpPost("{id}/movement")]
        public ActionResult MakeMovement(Guid id, [FromBody]Movement m)
        {
            //validates request body data
            if (ModelState.IsValid)
            {
                try
                {
                    var r = gameServiceInstance.MakeMovement(m);
                    if (r.IsEnded)
                    {
                        return Json(new
                        {
                            msg = "Partida finalizada.",
                            winner = r.Winner
                        });
                    }
                    return StatusCode(200);
                }               
                catch (Exception e) //Catch game validation errors
                {
                    return StatusCode(400,
                        new
                        {
                            msg = e.Message
                        }
                    );
                }
            }
            else
            {
                //request body is invalid
                return StatusCode(400,
                    new
                    {
                        msg = ModelState.Select(x=> x.Value)
                        .Select(y=> y.Errors.Select(x=> x.ErrorMessage))
                        .ToArray()
                    }
                );
            }
        }
    }
}