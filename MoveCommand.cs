using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class MoveCommand //singleton pattern to move
    {
        private Board _board;
        private static MoveCommand _instance=null;
        private MoveCommand() 
        {
           
        }
        public static MoveCommand Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MoveCommand();
                }
                return _instance;
            }
        }
        public void AddBoard(Board b) //add board to know the cell to move to
        {
            _board = b;
        }
        public void Execute(Player p, int step)
        {          
            int newCell = p.Position + step;
           
            if (newCell >= _board.CellCount) //if the player pass the starting point give him the relap money
            {
                newCell = newCell % _board.CellCount;
                p.ChangeMoney(_board.MoneyRelap);
            }
            Cell c = _board.FetchCell(newCell);
            p.Position= newCell;  //set new position for the player
            p.X =   (float)c.CenterPoint().X; //set new x coordinate for the player
            p.Y = (float)c.CenterPoint().Y +7; //set new y coordinate for the player
            c.PlayerVisit(p); //let the cell decide the effect on the player
        }
        public void Execute(Player p, string name) //move with name of jail like move to jail, and no relap money
        {           
            Cell c = _board.FetchCell(name);
            p.Position = c.Position;
            p.X = (float)c.CenterPoint().X;
            p.Y = (float)c.CenterPoint().Y + 7;
            c.PlayerVisit(p);
        }
    }
}
