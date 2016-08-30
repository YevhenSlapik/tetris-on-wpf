using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Classes
{
    /// <summary> Родительский класс для досок с гридами </summary>
    abstract class Board
    {
        /// <summary> Рядки грида </summary>
        protected int Rows;
        /// <summary> Колонки грида </summary>
        protected int Cols;
        /// <summary> Массив лейбов для отображения блоков на клетках грида по пойнтам</summary>
        protected Label[,] BlockControls;
        /// <summary> Цвет клетки в гриде </summary>
        protected Brush NoBrush = Brushes.Transparent;
        /// <summary>Отрисовка фигуры на гриде</summary>
        public abstract void DrawFigure();
        /// <summary>Стирание фигуры с грида</summary>
        public abstract void EraseFigure();
    }
}
