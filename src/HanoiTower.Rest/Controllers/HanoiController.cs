using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using Hangfire;
using HanoiTower.Domain.Commands.Handlers;
using HanoiTower.Domain.Commands.Inputs.Hanoi;
using HanoiTower.Domain.Commands.Inputs.Move;
using HanoiTower.Domain.Interfaces.Repositories;

namespace HanoiTower.Rest.Controllers
{
    public class HanoiController : BaseController
    {
        private readonly HanoiCommandsHandler _hanoiCommandsHandler;
        private readonly MoveCommandsHandler _moveCommandsHandler;

        public HanoiController(
            HanoiCommandsHandler hanoiCommandsHandler,
            IMoveRepository moveRepository,
            MoveCommandsHandler moveCommandsHandler)
        {
            _hanoiCommandsHandler = hanoiCommandsHandler;
            _moveCommandsHandler = moveCommandsHandler;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] CreateHanoiCommand command)
        {
            var result = _hanoiCommandsHandler.Handle(command);
            if (!string.IsNullOrEmpty(result.Id))
            {
                BackgroundJob.Enqueue(() => ProcessHanoi(Guid.Parse(result.Id)));
            }
            return await Response(result, _hanoiCommandsHandler.Notifications);
            ;
        }

        public async Task ProcessHanoi(Guid hanoiId)
        {
            await _hanoiCommandsHandler.Proccess(new ProccessHanoiCommand {HanoiId = hanoiId});
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var result = _moveCommandsHandler.Handle(new GetMoveCommand(Guid.Parse(id)));
            return await Response(result, _moveCommandsHandler.Notifications);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            var result = _hanoiCommandsHandler.Handle(new GetHanoiHistoryCommand());
            return await Response(result, _hanoiCommandsHandler.Notifications);
        }
    }
}
