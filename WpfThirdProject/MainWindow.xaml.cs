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

namespace WpfThirdProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand;
        DispatcherTimer timer;
        int N = 15;
        int w = 150, h = 50;
        public MainWindow()
        {
            InitializeComponent();
            rand = new Random();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(550);
            timer.Tick += new EventHandler(onTick);
            Reset();

        }

        private void onTick(object sender, EventArgs e)
        {
            if (grid1.Children.Count == N * 2)
            {
                timer.Stop();
                MessageBoxResult result = MessageBox.Show("Вы проиграли. Начать новую игру ?",
                  "Failed", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Reset();
                else
                    Close();
                    
            }
            if (grid1.Children.Count == 0)
            {
                timer.Stop();
                MessageBoxResult result = MessageBox.Show("Вы выиграли. Начать новую игру ?",
                  "Win", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    Reset();
                else
                    Close();                        
            }
            else
            {
                CreateLines(1);
            }
               
        }

        private void Reset()
        {
            grid1.Children.Clear();
            CreateLines(N);
            timer.Start();
        }

        private void CreateLines(int n)
        {
            Random rgb = new Random();
            for (int i = 0; i< n; i++)
            {
                Rectangle r = new Rectangle();
                
                Color c = Color.FromRgb(
                    (byte)rgb.Next(10, 250),
                    (byte)rgb.Next(10, 250),
                    (byte)rgb.Next(10, 250)
                    );
                r.Fill = new SolidColorBrush(c);
                Random dir = new Random();
                if(dir.Next(0,2) == 1)
                {
                    r.Width = w;
                    r.Height = h;
                }
                else
                {
                    r.Width = h;
                    r.Height = w;
                }
                r.HorizontalAlignment = HorizontalAlignment.Left;
                r.VerticalAlignment = VerticalAlignment.Top;
                r.Margin = new Thickness(rand.Next(700), rand.Next(425), 0, 0);
                r.Stroke = new SolidColorBrush(Colors.Black);
                r.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseLeftButtonDown);
                grid1.Children.Add(r);
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            Rect rect = new Rect(r.Margin.Left, r.Margin.Top, r.Width, r.Height);
            int ri = grid1.Children.IndexOf(r);
            var children = grid1.Children.OfType<Rectangle>().ToList();
            foreach (Rectangle rects in children)
            {
                Rect r_check = new Rect(rects.Margin.Left, rects.Margin.Top, rects.Width, rects.Height);
                if (rect.IntersectsWith(r_check))
                {
                    if (ri < grid1.Children.IndexOf(rects))
                    {
                        return; // Если перекрыт, ничего не делается
                    }
                }
            }
            grid1.Children.Remove((UIElement)sender);
            return;
        }
    }
}
