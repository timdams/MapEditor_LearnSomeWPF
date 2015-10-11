using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_ViewMapEditor
{
    class MapModel
    {
        //Constructors
        public MapModel(int breedte, int hoogte)
        {
            _map = new byte[hoogte, breedte];
        }
        public MapModel(byte[,] map)
        {
          
            _map = map;
        }
        public MapModel(string path)
        {
            this.LoadMap(path);
        }

        //Properties
        private byte[,] _map;
        public byte[,] Map
        {
            get
            {
                if (_map != null)
                    return _map;
                throw new NullReferenceException("Map not created");
            }

        }
        
        //ReadOnly Properties
        public int Hoogte
        {
            get { return _map.GetLength(0); }
        }
        public int Breedte
        {
            get { return _map.GetLength(1); }
        }

     
        //methods
        public void SetElement(int x, int y, int value)
        {
            _map[y, x] = (byte)value; //Todo check if valid x, y value
        }
        public int GetElement(int x, int y)
        {
            return Convert.ToInt32(_map[y, x]); //Todo check if valid x, y value
        }
        public void ClearMap()
        {
            for (int i = 0; i < Hoogte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                {
                    _map[i,j] = 0;  //We could also just say _map= new byte[Hoogte, Breedte] ;
                }
            }
        }


        //FileIO
        public  void LoadMap(string path)
        {
            StreamReader reader = new StreamReader(path);

            int breedte = Convert.ToInt32(reader.ReadLine());
            int hoogte = Convert.ToInt32(reader.ReadLine());

            byte[,] resultaat = new byte[hoogte, breedte];
            for (int i = 0; i < hoogte; i++)
            {
                //lees lijn per lijn
                var lijn = reader.ReadLine();
                //Splits komma's weg
                var gesplitst = lijn.Split(',');
                for (int j = 0; j < gesplitst.Length; j++) //Todo: controleren of Length overeenkomt met beloofde breedte aan begin file
                {
                    resultaat[i, j] = (byte)Convert.ToInt32(gesplitst[j]);
                }

            }
            reader.Close();
            _map = resultaat;
        }
        public void SaveMap(string path)
        {
            //Mapdimensies uitlezen 
            int breedte = _map.GetLength(1);
            int hoogte = _map.GetLength(0);


            //Klaar om te schrijven
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(breedte);
            writer.WriteLine(hoogte);

            for (int i = 0; i < hoogte; i++)
            {
                for (int j = 0; j < breedte; j++)
                {
                    writer.Write(_map[i, j]);
                    if (j < breedte - 1)
                        writer.Write(","); //Geen komma op einde van tekst
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        //ToString
        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < Hoogte; i++)
            {
                for (int j = 0; j < Breedte; j++)
                {
                    res += _map[i, j].ToString();
                }
                res += Environment.NewLine;
            }
            return res;
        }

    }
}
