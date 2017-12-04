using System;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Results.Hanoi
{
    public class GetHanoiHistoryCommandResult : ICommandResult
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Disks { get; set; }
    }
}