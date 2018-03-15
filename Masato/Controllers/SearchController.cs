using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Masato.Controllers {
  [Route("[controller]")]
  public class SearchController : Controller {
    private Data DataProvider { get; }

    public SearchController(Data data) {
      DataProvider = data;
    }

    [HttpGet("{term}")]
    public IActionResult Get(string term) {
      var results = DataProvider.Sym.Lookup(term.ToLower(), SymSpell.Verbosity.Closest);

      if (results.Count == 0) {
        return StatusCode(404);
      }

      List<Result> result_list = new List<Result>();

      foreach (var result in results) {
        string id = DataProvider.Db.Get(result.term);

        if (id == null) {
          continue;
        }

        var res = new Result {
          Id = Int32.Parse(id),
          Title = result.term,
          Match = result.distance
        };

        result_list.Add(res);
      }

      result_list.Sort((left, right) => {
        if (left.Match == right.Match) {
          return -left.Id.CompareTo(right.Id);
        }

        return left.Match.CompareTo(right.Match);
      });

      return Json(result_list);
    }
  }
}
