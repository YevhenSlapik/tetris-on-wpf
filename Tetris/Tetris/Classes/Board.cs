using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Classes
{
    /// <summary> Родительский класс для досок с гридами </summary>
    public abstract class Board
    {
        #region К-тор
        /// <summary>Базовый к-тор для всех досок. Задает размеры и начальные параметры контрола блоков </summary> 
        /// <param name="grid">оперируемый грид</param>
        protected Board(Grid grid)
        {
            Cols = grid.ColumnDefinitions.Count;
            Rows = grid.RowDefinitions.Count;

            BlockControls = new Label[Cols, Rows];
            for (var i = 0; i < Cols; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label
                    {
                        Background = NoBrush,
                        BorderBrush = NoBrush,
                        BorderThickness = new Thickness(1, 1, 1, 1)
                    };
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    grid.Children.Add(BlockControls[i, j]);
                }
            }
        }
        #endregion

        #region Поля класса
        /// <summary> Рядки грида </summary>
        protected int Rows;
        /// <summary> Колонки грида </summary>
        protected int Cols;
        /// <summary>Контрол блоков для грида в виде  2-мерного массива лейбов для отображения блоков на клетках грида по пойнтам</summary>
        protected Label[,] BlockControls;
        /// <summary> Цвет клетки в гриде </summary>
        protected Brush NoBrush = Brushes.Transparent; 
        #endregion

        #region Стирание/отрисовка фигур
        /// <summary>отрисовка фигуры на гриде </summary>
        /// <param name="figure">фигура которую нужно стереть</param>
        /// <param name="startPos">начальная позиция фигуры относительно Y </param>
        public virtual void DrawFigure(Tetromino figure, int startPos)
        {
            var position = figure.GetFigurePosition();
            var shape = figure.GetFigureShape();
            var shapeColor = figure.GetFigureColor();
            foreach (var block in shape)
            {
                BlockControls[(int)(block.X + position.X) + ((Cols / 2) - 1),
                    (int)(block.Y + position.Y) + startPos].Background = shapeColor;
            }
        }
        /// <summary>Стирание фигуры с грида </summary>
        /// <param name="figure">фигура которую нужно стереть</param>
        /// <param name="startPos">начальная позиция фигуры относительно Y </param>
        public virtual void EraseFigure(Tetromino figure, int startPos)
        {
            var position = figure.GetFigurePosition();
            var shape = figure.GetFigureShape();
            foreach (var block in shape)
            {
                BlockControls[(int)(block.X + position.X) + ((Cols / 2) - 1),
                    (int)(block.Y + position.Y) + startPos].Background = NoBrush;
            }
        } 
        #endregion                                                                            

    }                                      
}
