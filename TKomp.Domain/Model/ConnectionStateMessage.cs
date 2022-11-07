namespace TKomp.Domain.Model
{    
    public class ConnectionStateMessage
    {
        public bool IsValid { get; }
        public ConnectionStateMessage(bool isValid)
        {
            IsValid = isValid;
        }
    }
}