using MVVM_SQL.ViewModel;

namespace MVVM_SQL.View;

public partial class CRUDPage : ContentPage
{
    public CRUDPage()
    {
        InitializeComponent();
        BindingContext = new CRUDViewModel();
    }
}