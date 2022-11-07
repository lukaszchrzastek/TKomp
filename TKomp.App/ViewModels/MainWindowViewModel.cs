namespace TKomp.App.ViewModels
{
    public class MainWindowViewModel
    {                        
        public ConnectionViewModel ConnectionViewModel { get; set; }
        public ColumnsViewModel ColumnsViewModel { get; set; }

        public MainWindowViewModel(ConnectionViewModel connectionViewModel, ColumnsViewModel columnsViewModel)
        {                     
            ConnectionViewModel = connectionViewModel;
            ColumnsViewModel = columnsViewModel;        
        }
    }
}