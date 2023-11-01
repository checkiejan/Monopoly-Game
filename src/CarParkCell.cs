using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class CarParkCell : Cell
    {
        public CarParkCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {

        }
        public override string Description
        {
            get
            {
                return "Car Park\nNothing happens!";
            }
        }
        public override void PlayerVisit(Player p) //nothign will happen when the player visit this cell
        {
            
        }
    }
}
