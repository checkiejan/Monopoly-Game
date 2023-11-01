using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Custom
{
    public class NoticeBox
    {
        private GameManger _game; //game manager to know the current player and perform logic move with the player
        private State _state;
        private Dice _dice1;
        private Dice _dice2;
        public NoticeBox(GameManger game)
        {
            _dice1 = new Dice(350, 200, "dice1"); 
            _dice2 = new Dice(550, 200, "dice2");
            _game = game;
            _state = new RollState(this); //set the first state to roll state
        }
        public void DrawDice()
        {
            _dice1.Draw();
            _dice2.Draw();
        }
        public void RollDice()
        {
            _dice1.Roll();
            _dice2.Roll();
        }
        public int DiceResult //get the sum of the 2 dices
        {
            get 
            {
                return _dice1.CurrentNumber + _dice2.CurrentNumber;
                
            }
        }
        public void ChangeState(State s) //change state of the notice box
        {
            _state = s;
        }
        public void Draw()
        {
            DrawPlayerName();
            _state.Draw();
        }
        public void DrawPlayerName() //draw the current player name within this turn
        {
            //280, 80
            //700,80
            int textSize = 20;
            Player p = _game.PlayerInTurn;
            string text = string.Format("{0}'s Turn", p.Name);
            float msgWidth = SplashKit.TextWidth(text, "GameFont", textSize);
            SplashKit.DrawText(text, Color.Black, "GameFont", textSize, 280 + (420 - msgWidth) / 2, 80 + 10);
        }
        public void HandleClick(Point2D pt) //check if anything needs to clicked
        {
            _state.ClickButton(pt);
        }
        public void MoveCurrentPlayer(int step) //move player for a number of steps
        {
            MoveCommand move = MoveCommand.Instance;
            Player p = _game.PlayerInTurn;
            move.Execute(p, step);
        }
        public Cell CurrentPlayerCell() //get the current cell of the player
        {
            int position = _game.PlayerInTurn.Position;
            return _game.FindCell(position);
        }
        public Player CurrentPlayer() // get the current player
        {
            return _game.PlayerInTurn;
        }
        public void NextPlayer() //next turn
        {
            _game.PlayerInTurn.IsInTurn = false;
            _game.NextTurn();
        }
    }
}
