using System;

namespace TKomp.App.Commands
{
    public sealed class LoadDataCommand : CommandBase
    {
        public LoadDataCommand(Action<object> execteMethod, Predicate<object> canExecute) : base(execteMethod, canExecute)
        {
        }
    }
}