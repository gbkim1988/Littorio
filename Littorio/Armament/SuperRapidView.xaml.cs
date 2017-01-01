using Littorio.Models;
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

namespace Littorio.Armament
{
    /// <summary>
    /// Interaction logic for SuperRapidView.xaml
    /// </summary>
    public partial class SuperRapidView : UserControl
    {
        private SuperRapidViewModel SRModel;
        public SuperRapidView()
        {
            this.SRModel = new SuperRapidViewModel();
            DataContext = SRModel;
            InitializeComponent();
        }
    }
}
