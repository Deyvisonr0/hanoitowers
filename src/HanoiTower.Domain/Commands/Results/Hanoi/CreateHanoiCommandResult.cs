using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Results.Hanoi
{
    public class CreateHanoiCommandResult : ICommandResult
    {
        public CreateHanoiCommandResult(string identificador)
        {
            Id = identificador;
        }

        public string Id { get; set; }
    }
}