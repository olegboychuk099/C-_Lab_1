using System.Windows.Controls;
using Lab_1_Boychuk.ViewModel;

namespace Lab_1_Boychuk.View
{
    /// <summary>
    /// Interaction logic for Birthday.xaml
    /// </summary>
    public partial class Birthday : UserControl
    {
        public Birthday()
        {

            InitializeComponent();
            DataContext = new PickDateViewModel();

        }
    }
}
