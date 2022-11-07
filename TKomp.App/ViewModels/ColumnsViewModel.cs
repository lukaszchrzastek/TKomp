using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using TKomp.App.Commands;
using TKomp.Domain;
using TKomp.Domain.Model;

namespace TKomp.App.ViewModels
{
    public class ColumnsViewModel : ViewModelBase
    {        
        private readonly IMessenger _messenger;
        private readonly IColumnInfoRepository _columnInfoRepository;
        public LoadDataCommand LoadDataCommand { get; }

        private readonly ObservableCollection<ColumnInfo> _columns;
        public ObservableCollection<ColumnInfo> Columns { get { return _columns; } }

        private bool _connectionIsValid;
        public bool ConnectionIsValid
        {
            get { return _connectionIsValid; }
            set
            {
                _connectionIsValid = value;
                OnPropertyChanged(nameof(ConnectionIsValid));
            }
        }

        private bool _canExecute  = false;


        public ColumnsViewModel(IColumnInfoRepository columnInfoRepository, IMessenger messenger)
        {
            LoadDataCommand = new LoadDataCommand(LoadData, param => _canExecute);
            _messenger = messenger;
            _columnInfoRepository = columnInfoRepository;
            _columns = new ObservableCollection<ColumnInfo>();
            _messenger.Register<ConnectionStateMessage>(this, ProcessMessage);
        }

        private void ProcessMessage(ConnectionStateMessage message)
        {            
            ConnectionIsValid = message.IsValid;
            _canExecute = message.IsValid;
        }

        private void LoadData(object parameter)
        {
            var columns = _columnInfoRepository.GetColumnInfo();
            _columns.Clear();
            foreach (var column in columns)
            {
                _columns.Add(column);
            }
        }
    }
}
