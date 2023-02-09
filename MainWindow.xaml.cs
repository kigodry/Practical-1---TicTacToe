using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TicTacToe
{
    public partial class MainWindow : Window
    {

        public static List<Button> buttons = new List<Button>();
        public static Symbol? AIPlayer;
        DispatcherTimer AITimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();

            
            foreach (Button btn in GameGrid.Children.OfType<Button>())
            {
                buttons.Add(btn);
            }
            
            
            GameManager.InitGame(buttons);

            
            AITimer.Tick += new EventHandler(dispatcherTimer_Tick);
            AITimer.Interval = new TimeSpan(0, 0, 1); 
        }

        private void Turn(object sender, RoutedEventArgs e)
        {
            GameManager.Turn(sender, e);
        }

        public static void AnnounceWinner(Winner? w)
        {
            if ((int)w != 0)
            {
                MessageBox.Show("В данной игре победил " + w.ToString());
            }
            else
            {
                MessageBox.Show("Ничья");
            }
        }

        public static void DisableBoard()
        {
            foreach (Button b in buttons)
            {
                b.IsEnabled = false;
            }
        }

        private void EnableBoard()
        {
            foreach (Button b in buttons)
            {
                b.IsEnabled = true;
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
            GameManager.ResetGame();
            GameManager.InitGame(buttons);
            EnableBoard();
        }

        private void ResetWindow()
        {
            foreach (Button btn in GameGrid.Children.OfType<Button>())
            {
                btn.Content = null;
            }
        }

        private void btnAiTurn_Click(object sender, RoutedEventArgs e)
        {
            GameManager.AITurn();
        }

        private void cbAISymbol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AIPlayer = GameManager.SetSymbol(cbAISymbol.SelectedValue.ToString());
            
            GameManager.SetAIPlayer(AIPlayer);

            if (AIPlayer != null)
            {
                AITimer.Start();
            }
            else
            {
                AITimer.Stop();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (GameManager.turn == (int)GameManager.AIPlayer)
            {
                GameManager.AITurn();
            }
        }
    }
}
