using System;

namespace SpaceGame
{
    public interface ILevelEndUI
    {
        void Show(int score);
        void Hide();
        event Action NextClicked;
        event Action MenuClicked;
    }
}