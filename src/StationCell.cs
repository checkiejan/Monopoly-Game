using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public class StationCell: OwnableCell
    {
        public StationCell(string name, float x, float y, int width, int height, Direction direction, int position) : base(name, x, y, width, height, direction, position)
        {
            
        }
        public override void NotifyChangeOwner()
        {
            _board.CheckStationOwner(this);
        }

        public override string Description => throw new NotImplementedException();
        public override void LoadData(JObject ob)
        {
            base.LoadData(ob);
            this.AddTax((int)ob["tax0"]);
            this.AddTax((int)ob["tax1"]);
            this.AddTax((int)ob["tax2"]);
            this.AddTax((int)ob["tax3"]);
           

        }
    }
}
