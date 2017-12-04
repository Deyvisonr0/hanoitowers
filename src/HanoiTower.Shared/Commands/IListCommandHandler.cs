using System.Collections.Generic;

namespace HanoiTower.Shared.Commands
{
    public interface IListCommandHandler<T> where T : ICommand
    {
        IEnumerable<ICommandResult> Handle(T command);
    }
}