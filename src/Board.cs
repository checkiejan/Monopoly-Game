using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class Board
    {
        private List<Cell> _cells; //cells of the board
        private CellFactory _cellFactory;// factory to create celss
        private int _moneyRelap;// the money of relap when the users pass the starting point
        private DisplayInfo _display;// observer when cell is clicked
        public Board()
        {

            _cells = new List<Cell>();
            _cellFactory = new CellFactory(this); 
            _moneyRelap = 200;
            AddCellType();
            InitailizeCells();
        }
        public int MoneyRelap 
        {
            get
            {
                return _moneyRelap;
            }
        }
        public void AddCell(Cell c) //add cell 
        {
            _cells.Add(c);
        }
        public void Draw() //draw all the cells
        {
            foreach(Cell c in _cells)
            {
                c.Draw();
            }
        }
        private void InitailizeCells() 
        {
            StreamReader reader = new StreamReader("map2.txt"); // read x,y coordinate, width, and height, and direction of the cells
            try
            {
               
                int count = reader.ReadInteger(); //read the total number of cells
                for (int i = 0; i < count; i++)
                {

                    int x = reader.ReadInteger(); //x coordinate
                    int y = reader.ReadInteger(); // y coordinate
                    int width = reader.ReadInteger(); //width
                    int height = reader.ReadInteger(); //height
                    Direction d = reader.ReadDirection();
                    Cell c = _cellFactory.CreateCell(x, y, width, height, d,i); //create a cell with x,y,width, height at the position i
                    AddCell(c); //add cell to the list

                }
            }
            finally
            {
                reader.Close();
            }
            
        }
        public Cell FetchCell(int position) //fecth cell at the position
        {
            if(position >=0 && position < _cells.Count)
            {
                return _cells[position];
            }
            return null;
        }
        public Cell FetchCell(string name) //fetch cell with the name
        {
            foreach(Cell c in _cells)
            {
                if(c.Name == name)
                {
                    return c;
                }
            }
            return null;
        }
     
        public int CellCount //total number of cells
        {
            get
            {
                return _cells.Count;
            }
        }
        private void AddCellType()//add cell type that the factory can create
        {
            _cellFactory.AddCellType("property", typeof(PropertyCell));
            _cellFactory.AddCellType("station", typeof(StationCell));
            _cellFactory.AddCellType("carPark", typeof(CarParkCell));
            _cellFactory.AddCellType("tax", typeof(TaxCell));
            _cellFactory.AddCellType("jail", typeof(JailCell));
            _cellFactory.AddCellType("goToJail", typeof(GoToJailCell));
            _cellFactory.AddCellType("start", typeof(StartCell));
        }
        public void CheckStationOwner(StationCell s) //station cell will notify the board when the owner is changed. implementation of observer pattern
        {
            List<StationCell> list = new List<StationCell>(); 
            foreach(Cell c in _cells)
            {
                if(c is StationCell)
                {
                    list.Add((c as StationCell)); //get all station cell from _cells
                }
            }
            List<Player> owner = new List<Player>();
            foreach(StationCell sc in list)
            {
                if (sc.IsOwned)
                {
                    owner.Add(sc.Owner); //get all owners of the station cells
                }
            }
           foreach(Player p in owner)
            {
                int sum = 0;
                foreach(StationCell cell in list) //count each owner owns how many station cells and update the state tax of the station cell
                {
                    if (cell.IsOwned)
                    {

                        if (cell.Owner.Name == p.Name)
                        {
                            sum++;
                        }
                    }
                   
                }
                foreach (StationCell cell in list) //update the state tax of the station cell with the same owner
                {
                    if (cell.IsOwned)
                    {

                        if (cell.Owner.Name == p.Name)
                        {
                            cell.StateTax = sum - 1; //statetax of station cells equal to the number of station cells that the owner has
                        }
                    }
                }

            }
        }
        public void CheckPropertyOwner(PropertyCell p) //check if the the owner has all property with the same color to allow building house
        {
            
            List<PropertyCell> list = new List<PropertyCell>();
            foreach(Cell c in _cells)
            {
                if(c is PropertyCell)
                {
                    PropertyCell temp = (PropertyCell)c;
                    if(temp.CellColor == p.CellColor)
                    {
                        list.Add(temp); //get all properties of the required color
                    }
                }
            }
            Player player = list[0].Owner;
            if(p is null) // if the first property is not owned
            {
                foreach (PropertyCell c in list)
                {
                    c.HouseAllowed = false; //no property of that color is allowed to build house
                }
            }
            else
            {
                bool flag = true;
                foreach (PropertyCell c in list)
                {
                    if(c.Owner is null || c.Owner.Name != player.Name) // if detecting any different owner, not allow building house
                    {
                        flag = false; 
                        break;
                    }
                }
                if (flag)
                {
                    foreach (PropertyCell c in list)
                    {
                        c.HouseAllowed = true;
                    }
                }
                else
                {
                    foreach (PropertyCell c in list)
                    {
                        c.HouseAllowed = false;
                    }
                }
            }
          
        }
        public void AddDisplay(DisplayInfo display) //add observer display
        {
            _display = display;
        }

        public void HandleClick(Point2D pt) //check if any cell is clicked, notify the display
        {
            foreach(Cell c in _cells)
            {
                if (c.IsAt(pt))
                {
                    if(c is not OwnableCell)
                    {
                        _display.StrategyDisplay(new DisplayNormalCell()); // change the strategy of display to display cell
                    }
                    else
                    {
                        if(c is PropertyCell)
                        {
                            _display.StrategyDisplay(new DisplayProperty());
                        }
                        else
                        {
                            _display.StrategyDisplay(new DisplayStation());
                        }
                    }
                    _display.AddDisplay(c); // tell the display to display the cell information
                    break;
                }
            }
        }

       
    }
}
