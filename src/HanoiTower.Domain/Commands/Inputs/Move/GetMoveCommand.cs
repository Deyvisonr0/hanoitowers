using System;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Inputs.Move
{
    public class GetMoveCommand : ICommand
    {
        public GetMoveCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }
    }
}