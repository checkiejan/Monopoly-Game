using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public abstract class State
    {
        protected List<Button> _buttons;
        private NoticeBox _notice;
        public State(NoticeBox nb)
        {
            _notice = nb;
            _buttons = new List<Button>();
        }
        public void AddButton(Button b)
        {
            _buttons.Add(b);
        }
        protected NoticeBox Notice
        {
            get
            {
                return _notice;
            }
        }
        public abstract void Draw();
        public abstract void ClickButton(Point2D pt);
        public void DrawButton()
        {
            foreach (Button b in _buttons)
            {
                b.Draw();
            }
        }
    }
}
