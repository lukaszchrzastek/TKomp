using GalaSoft.MvvmLight.Messaging;
using TKomp.App.Commands;
using TKomp.Domain;
using TKomp.Domain.Model;

namespace TKomp.App.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        public ConnectionTestCommand ConnectionTestCommand { get; }
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly IMessenger _messenger;

        private string _userName;
        private string _password;
        private string _connectionStatusInfo;

        public ConnectionViewModel(ISqlConnectionProvider sqlConnectionProvider, IMessenger messenger)
        {            
            _sqlConnectionProvider = sqlConnectionProvider;
            _messenger = messenger;
            ConnectionTestCommand = new ConnectionTestCommand(TestConnection);
        }


        public string ConnectionStatusInfo
        {
            get { return _connectionStatusInfo; }
            set
            {
                _connectionStatusInfo = value;
                OnPropertyChanged(nameof(ConnectionStatusInfo));
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (!string.Equals(_userName, value))
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));                    
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (!string.Equals(_password, value))
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private void TestConnection(object parameter)
        {            
            var connectionIsValid = _sqlConnectionProvider.Test(_userName, _password);
            _messenger.Send<ConnectionStateMessage>(new ConnectionStateMessage(connectionIsValid));
            if (connectionIsValid)
                _sqlConnectionProvider.SetCredentials(_userName, _password);            
            ConnectionStatusInfo = connectionIsValid ? Resource.ConnectionSuccessful : Resource.ConnectionFailure;
        }
    }
}