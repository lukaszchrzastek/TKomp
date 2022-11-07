using System;

namespace TKomp.App.Commands
{
    public sealed class ConnectionTestCommand : CommandBase
    {
        public ConnectionTestCommand(Action<object> execteMethod) : base(execteMethod, param => true)
        {
        }
    }
}