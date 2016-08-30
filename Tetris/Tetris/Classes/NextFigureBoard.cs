using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Classes
{
     /// <summary>Класс доски которая отображает следующую фигуру</summary>
    class NextFigureBoard :Board
    {
         public Tetromino NextFigure { get; set; }

         public NextFigureBoard(Grid nextFigureGrid)
        {
            Cols = nextFigureGrid.ColumnDefinitions.Count;
            Rows = nextFigureGrid.RowDefinitions.Count;
            
            BlockControls = new Label[Cols, Rows];
            for (var i = 0; i < Cols; i++)
            {
                for (var j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label
                    {
                        Background = Brushes.Transparent,
                        BorderBrush = Brushes.Transparent,
                        BorderThickness = new Thickness(1, 1, 1, 1)
                    };
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    nextFigureGrid.Children.Add(BlockControls[i, j]);
                }
            }
        }
                    
        public override void DrawFigure()
        {
            var position = NextFigure.GetFigurePosition();
            var shape = NextFigure.GetFigureShape();
            var shapeColor = NextFigure.GetFigureColor();
            foreach (var block in shape)
            {
                BlockControls[(int)(block.X + position.X) + ((Cols / 2) - 1),
                    (int)(block.Y + position.Y) + 2].Background = shapeColor;
            }                                                  
        }
        public override void EraseFigure()
        {
            var position = NextFigure.GetFigurePosition();
            var shape = NextFigure.GetFigureShape();
            foreach (var block in shape)
            {
                BlockControls[(int)(block.X + position.X) + ((Cols / 2) - 1),
                    (int)(block.Y + position.Y) + 2].Background = NoBrush;
            }
        }
    }                                
}
