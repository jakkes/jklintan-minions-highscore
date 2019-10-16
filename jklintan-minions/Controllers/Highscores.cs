using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using jklintan_minions.Models;
using Microsoft.AspNetCore.Http;

namespace jklintan_minions.Controllers
{
    [ApiController]
    [Route("highscores")]
    public class Highscores : Controller
    {
        private string _authKey = Environment.GetEnvironmentVariable("HTTP_AUTH_KEY");
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<Highscore>> Index() {
            using (var db = new HighscoreContext()) {
                return db.Highscores
                    .OrderBy(highscore => highscore.Score)
                    .Take(10).ToList();
            }
        }

        [HttpPost]
        public ActionResult<Highscore> Index(Highscore highscore) {
            using (var db = new HighscoreContext()) {
                db.Highscores.Add(highscore);
                db.SaveChanges();
            }
            return highscore;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!string.IsNullOrEmpty(_authKey)) {
                if (context.HttpContext.Request.Headers.ContainsKey("auth-key")) {
                    if (context.HttpContext.Request.Headers["auth-key"] == _authKey) {
                        base.OnActionExecuting(context);
                        return;
                    }
                }
                context.Result = new ContentResult() {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "Unauthorized access"
                };
            } else {
                base.OnActionExecuting(context);
            }
        }
    }
}