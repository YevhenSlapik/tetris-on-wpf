using System;
using System.Windows;

namespace Tetris
{
    /// <summary>
    /// Окно окончания игры
    /// </summary>
    public partial class GameOver 
    {

        public GameOver()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Рестарт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
        /// <summary>
        /// Виход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public bool? ShowDialog(string score)
        {
            scoreLabel.Content += score;
            try
            {
                return base.ShowDialog();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
                                                                                   