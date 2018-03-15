using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Masato.Controllers {
  [Route("[controller]")]
  public class InsertController : Controller {
    private Data DataProvider { get; }

    public InsertController(Data data) {
      DataProvider = data;
    }

    [HttpPost("{id}/{term}")]
    public IActionResult Post(string id, string term, [FromHeader] string authorization) {
      string auth = Environment.GetEnvironmentVariable("MASATO_AUTH");

      if (!authorization.Equals(auth)) {
        return StatusCode(403);
      }

      term = term.ToLower();

      DataProvider.Sym.CreateDictionaryEntry(term, 1);
      DataProvider.Db.Put(term, id);
      DataProvider.SymFile.WriteLine(term);

      string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      string SymPath = BaseDirectory + "masato_sym";

      return StatusCode(204);
    }
  }
}
