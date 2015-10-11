using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMapEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Maken nieuwe kaart
            _2_ViewMapEditor.MapModel mijnMap = new _2_ViewMapEditor.MapModel(5, 3);
            //Element met coordinaten 2,2 op 6 zetten
            mijnMap.SetElement(2, 2, 6);
            //Kaart wegschrijven
            mijnMap.SaveMap("testje.map");


            //Kaart opnieuw
            _2_ViewMapEditor.MapModel mijnandereMap = new _2_ViewMapEditor.MapModel("testje.map");
            int waarde = mijnandereMap.GetElement(2, 2);
            Console.WriteLine(waarde.ToString());
        }
    }
}
