using System.Diagnostics;
using Flunt.Notifications;
using HanoiTower.Domain.Commands.Inputs.Move;
using HanoiTower.Domain.Commands.Results.Move;
using HanoiTower.Domain.Interfaces.Repositories;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Handlers
{
    public class MoveCommandsHandler : 
        Notifiable,
        ICommandHandler<GetMoveCommand>

    {
        private readonly IMoveRepository _moveRepository;

        public MoveCommandsHandler(IMoveRepository moveRepository)
        {
            _moveRepository = moveRepository;
        }

        public ICommandResult Handle(GetMoveCommand command)
        {
            var result = new GetMoveCommandResult();

            var move = _moveRepository.GetLastMoveByHanoi(command.Id);
            if (move == null)
            {
                AddNotification("NotFound", "Moves for specified hanoi not found");
            }
            else
            {
                result.Id = move.Hanoi.Id.ToString();
                result.MoveSnapShot = move.Snapshot;
                result.Step = move.Step;
            }

            return result;
        }
    }
}