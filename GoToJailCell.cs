using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class GoToJailCell: Cell
    {
        public GoToJailCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {

        }
        public override string Description
        {
            get
            {
                return "You will go to jail";
            }
        }
        public override void PlayerVisit(Player p)
        { 
            
            MoveCommand move = MoveCommand.Instance; //use move command to move the user to jail
            move.Execute(p, "Jail");
            p.IsInJail = true; 
            
        }
    }
}
