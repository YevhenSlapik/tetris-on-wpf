using System.Windows.Controls;

namespace Tetris.Classes
{
     /// <summary>Класс доски которая отображает следующую фигуру</summary>
    public sealed class NextFigureBoard :Board
    {
        /// <summary>К-тор доски </summary><param name="grid">грид доски</param>
        public NextFigureBoard(Grid grid) :base(grid)
        {
           
        }
    }                                
}
