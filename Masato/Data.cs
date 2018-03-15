using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LevelDB;
using System.IO;

namespace Masato {
  public class Data {
    public SymSpell Sym { get; }
    public DB Db { get; }
    public StreamWriter SymFile { get; }


    public Data() {
      string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      string SymPath = BaseDirectory + "masato_sym";
      string DbPath = BaseDirectory + "masato_db";

      var DbOptions = new Options();
      DbOptions.CreateIfMissing = true;

      Sym = new SymSpell(50000, 5);
      Db = new DB(DbOptions, DbPath);
      SymFile = File.AppendText(SymPath);

      LoadDictionary(SymPath);
    }

    private void LoadDictionary(string SymPath) {
      using (StreamReader sr = new StreamReader(File.OpenRead(SymPath))) {
        String line;

        while ((line = sr.ReadLine()) != null) {
          Sym.CreateDictionaryEntry(line, 1);
        }
      }
    }
  }
}
