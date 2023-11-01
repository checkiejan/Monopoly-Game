using Newtonsoft.Json.Linq;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class CellFactory
    {
        // dictionary to store the type need to create
        private Dictionary<string, Type> _cellType = new Dictionary<string, Type>();
        private JArray data; // the data of all the cells
        private Board _board;
        public CellFactory(Board b)
        {
            StreamReader file = new StreamReader("test.json"); // open the json file
            JObject o1 = file.ReadJson(); //load the data into o1
            file.Close(); //close the file
            data = (JArray)o1["cells"]; //get information of the cell
            _board = b;
        }
        // add type that can be created
        public void AddCellType(string typeName, Type type)
        {
            _cellType[typeName] = type;
        }
        public Cell CreateCell(float x, float y, int width, int height, Direction direction, int position)
        {
            if (_cellType.ContainsKey((string)data[position]["cellType"]))
            {
                   Cell  c = (Cell)Activator.CreateInstance(_cellType[(string)data[position]["cellType"]],new Object[] { (string)data[position]["name"] ,x, y, width, height, direction, position });               
                   c.LoadData((JObject)data[position]); //load data depending on the type of cell
                    if(c is OwnableCell)
                    {
                        OwnableCell cell = c as OwnableCell;
                        cell.AddBoardObserve(_board);
                    }
                   return c;
            }
            return null;
        }
    }
}
