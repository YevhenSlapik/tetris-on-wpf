using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tetris.Classes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private Board _tetrisBoard;
        public MainWindow()
        {   
            InitializeComponent();
        }
        public void MainWindow_Initialized(object sender,EventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Timer_Tick); 
            GameStart();
        }

        private void GameStart()                                  
        {
            tetrisGrid.Children.Clear();
            _tetrisBoard = new Board(tetrisGrid);
            _timer.Stop();
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 400);
            _tetrisBoard.CurrentFigureMoveDown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if(_timer.IsEnabled)_tetrisBoard.CurrentFigureMoveLeft();
                    break;
                case Key.Right:
                    if (_timer.IsEnabled)                                 
                        _tetrisBoard.CurrentFigureMoveRight();
                    break;
                case Key.Down:
                        _tetrisBoard.CurrentFigureMoveDown();
                    break;
                case Key.Space:
                    _tetrisBoard.CurrentFigureMoveRotate();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F1:
                    GamePause();
                    break;

            }
        }
        private void GamePause()
        {
            if (_timer.IsEnabled)
            {
                  _timer.Stop();
            }
            else
            {
                _timer.Start();                         
            }
        }
    }
}
