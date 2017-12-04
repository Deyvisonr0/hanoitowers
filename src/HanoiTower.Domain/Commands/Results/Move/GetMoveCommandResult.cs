using System;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Results.Move
{
    public class GetMoveCommandResult : ICommandResult
    {
        public string Id { get; set; }
        public string MoveSnapShot { get; set; }
        public int Step { get; set; }
    }
}