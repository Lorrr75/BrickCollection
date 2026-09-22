using BrickCollection.Models;

namespace BrickCollection.Views
{
    public partial class SetDetailPage : ContentPage
    {
        public SetDetailPage(BricksetSet set)
        {
            InitializeComponent();
            BindingContext = set;
        }
    }
}
