using System;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Inputs.Hanoi
{
    public class ProccessHanoiCommand : ICommand
    {
        public Guid HanoiId { get; set; }
    }
}