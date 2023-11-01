﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class StartCell : Cell
    {
        
        public StartCell(string name, float x, float y, int width, int height, Direction direction, int position): base(name,x,y,width,height,direction, position)
        {
            
        } 
        public override string Description
        {
            get
            {
                return "Go!\nCollect 200$ when you pass";
            }
        }
        public override void PlayerVisit(Player p)
        {
          
        }
    }
}
