using Microsoft.Win32;
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

namespace _2_ViewMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MapModel currentMap;
        private string currentMapPath = "";


        public MainWindow()
        {
            InitializeComponent();
            currentMap = new MapModel(0, 0);
            cmbBrush.SelectedIndex = 0;
        }

        #region IO stuff
        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            //TODO: check if current map needs to be saved
            MapDimensions askdims = new MapDimensions();
            askdims.ShowDialog();
            currentMap = new MapModel(askdims.Breedte, askdims.Hoogte);
            LoadMapOnView();
        }

        private void menuLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                currentMapPath = dialog.FileName;
                currentMap = new MapModel(currentMapPath);

                LoadMapOnView();
            }

        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {

            if (currentMapPath == "")
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    currentMapPath = dialog.FileName;
                    
                }
            }
			currentMap.SaveMap(currentMapPath);

        }


        private void menuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                currentMapPath = dialog.FileName;

                currentMap.SaveMap(currentMapPath);

            }
        }
        #endregion


        private void menuClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Dit zal uw huidige kaart volledig reseten.Bent u zeker?", "OPGELET", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                currentMap.ClearMap();
                LoadMapOnView();
            }
        }

        private int blokscale = 15;
        private void LoadMapOnView()
        {
            if (currentMap != null)
            {
                mapCanvas.Children.Clear();
                mapCanvas.Width = (currentMap.Breedte * blokscale) + 10;
                mapCanvas.Height = (currentMap.Hoogte * blokscale) + 10;
                for (int i = 0; i < currentMap.Hoogte; i++)
                {
                    for (int j = 0; j < currentMap.Breedte; j++)
                    {
                        Rectangle blok = new Rectangle();
                        blok.Stroke = new SolidColorBrush(Colors.Black);
                        blok.StrokeThickness = 0.3;
                        blok.Width = blokscale;
                        blok.Height = blokscale;
                        switch (currentMap.GetElement(j, i))
                        {
                            case 0:
                                blok.Fill = new SolidColorBrush(Colors.LightGray);
                                break;
                            case 1:
                                blok.Fill = new SolidColorBrush(Colors.Red);
                                break;
                            case 2:
                                blok.Fill = new SolidColorBrush(Colors.Green);
                                break;
                            case 3:
                                blok.Fill = new SolidColorBrush(Colors.Orange);
                                break;
                            case 4:
                                blok.Fill = new SolidColorBrush(Colors.Yellow);
                                break;
                            default:
                                blok.Fill = new SolidColorBrush(Colors.Black);
                                break;

                        }
                        blok.SetValue(Canvas.LeftProperty, (double)(blokscale * (j + 1)));
                        blok.SetValue(Canvas.TopProperty, (double)(blokscale * (i + 1)));

                        mapCanvas.Children.Add(blok);
                    }
                }
            }
        }

        private void mapCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Bereken blokcoordinataat:
            if (cmbBrush.SelectedIndex > -1)
            {
                Point click = e.MouseDevice.GetPosition(mapCanvas);
                int x = (int)((click.X / blokscale)) - 1;
                int y = (int)((click.Y / blokscale)) - 1;
                var t = (cmbBrush.SelectedItem as ComboBoxItem).Content.ToString();
                currentMap.SetElement(x, y, Convert.ToInt32(t));
                LoadMapOnView();
            }
        }


        private void slidesScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            blokscale = (int)e.NewValue;
            LoadMapOnView();
        }
    }
}
