using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Inputs.Hanoi
{
    public class CreateHanoiCommand : ICommand
    {
        public int DiskCount { get; set; }
    }
}