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
using System.Windows.Shapes;

namespace _2_ViewMapEditor
{
    /// <summary>
    /// Interaction logic for MapDimensions.xaml
    /// </summary>
    public partial class MapDimensions : Window
    {
        public MapDimensions()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: controleer ingegeven waarden 
            Hoogte = Convert.ToInt32(txbHoogte.Text);
            Breedte = Convert.ToInt32(txbBreedte.Text);
            this.Close();
        }

        public int Hoogte { get; set; }
        public int Breedte { get; set; }
    }
}
