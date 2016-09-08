using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DevExpress.Xpf.Grid;

namespace Tetris.Classes
{
    class AI
    {
        private double[] _ratingCoefficients = { -0.510066,      //коеффициент суммарной высоты всех столбиков
                                                  0.760666,      //коеффициент кол-ва завершенных линий 
                                                 -0.35663,       //коеффициент "дырок"
                                                 -0.184483};     //коеффициент неравномерности столбиков

        private Board _board;
        private int _colHeight;
        private int _holes;
        private int _completeLines;
        public Tetromino OperatingFigure { get; set; }

        public AI(Board board)
        {
            _board = board;
            GetHoles();
            GetBumpiness();
            GetColHeight();
            GetCompleteLines();
        }                     

        private void GetColHeight()
        {
            var blockCtrl = _board.BlockControls;
            var totalColHeight = 0;
            for (int x = 0; x < blockCtrl.GetLength(0); x++)
            {
                var colHeight = 0;
                for (int y = 0; y < blockCtrl.GetLength(0); y++)
                {
                    if (!blockCtrl[x,y].Background.Equals(Brushes.Transparent))
                    {
                        colHeight++;
                    }
                }
                totalColHeight += colHeight;
            }

        }
        private void GetHoles()
        {

        }
        private void GetBumpiness()
        {
        }
        private void GetCompleteLines()
        {
        }
      


        private double CalculateStateRating()
        {
            return 0.0;
        }
    }
    
}
