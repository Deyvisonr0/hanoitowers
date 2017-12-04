using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Notifications;
using HanoiTower.Domain.Commands.Inputs.Hanoi;
using HanoiTower.Domain.Commands.Results.Hanoi;
using HanoiTower.Domain.Entities;
using HanoiTower.Domain.Enums;
using HanoiTower.Domain.Interfaces.Repositories;
using HanoiTower.Shared.Commands;

namespace HanoiTower.Domain.Commands.Handlers
{
    public class HanoiCommandsHandler : 
        Notifiable,
        ICommandHandler<CreateHanoiCommand>,
        IListCommandHandler<GetHanoiHistoryCommand>
    {
        private readonly IHanoiRepository _hanoiRepository;
        private readonly IMoveRepository _moveRepository;
        private Hanoi _hanoi;
        private int _moveStep;
        public HanoiCommandsHandler(IHanoiRepository hanoiRepository, IMoveRepository moveRepository)
        {
            _hanoiRepository = hanoiRepository;
            _moveRepository = moveRepository;
        }

        public ICommandResult Handle(CreateHanoiCommand command)
        {
            try
            {
                var hanoi = new Hanoi(command.DiskCount);

                AddNotifications(hanoi.Notifications);

                if (hanoi.Invalid) { AddNotifications(hanoi.Notifications); return null; }
                    
                _hanoiRepository.NoTransactionAdd(hanoi);

                return new CreateHanoiCommandResult(hanoi.Id.ToString());

            }
            catch (Exception e)
            {
                AddNotification("Exception",e.Message);
                return null;
            }
        }


        public IEnumerable<ICommandResult> Handle(GetHanoiHistoryCommand command)
        {
            try
            {
                var hanois = _hanoiRepository.GetAllFinishedHanois();
                if(!hanois.Any())
                {
                    AddNotification("No elements", "We haven't finished any yet");
                    return null;
                }

                var retorno = hanois.Select(x => new GetHanoiHistoryCommandResult
                {
                    Id = x.Id.ToString(),
                    End = x.End.Value,
                    Start = x.CreateTime,
                    Disks = x.TotalDisks
                });

                return retorno.ToList();

            }
            catch (Exception e)
            {
                AddNotification("Exception", e.Message);
                return null;
            }
        }

        public async Task Proccess(ProccessHanoiCommand command)
        {
            _hanoi = await _hanoiRepository.GetByGuidAsync(command.HanoiId);
            Trace.Write($"Start processing Hanoi {_hanoi.Id}");
            var start = new Tower(_hanoi.TotalDisks);
            var mid = new Tower(ETowerPosition.End);
            var end = new Tower(ETowerPosition.Mid);

            await SolveTower(_hanoi.TotalDisks, start, end, mid);

            _hanoi.EndHanoi();
            _hanoiRepository.NoTransactionUpdate(_hanoi);
            Trace.Write($"End processing Hanoi {_hanoi.Id}");
        }

        private async Task SolveTower(int totalDisks, Tower start, Tower end, Tower mid)
        {
            if (totalDisks > 0)
            {
                await SolveTower(totalDisks - 1, start, mid, end);
                ++_moveStep;
                var disk = start.RemoveDisk();
                end.AddDisk(disk);
                var mov = new Move(new List<Tower> { start, end, mid }, disk, _moveStep, _hanoi);
                _moveRepository.NoTransactionAdd(mov);
                await SolveTower(totalDisks - 1, mid, end, start);
            }

        }

    }
}