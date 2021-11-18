using Application.Common.Exceptions;
using Application.HumanResources.Commands.CreateHumanResource;
using Application.HumanResources.Commands.DeleteHumanResource;
using Application.HumanResources.Commands.UpdateHumanResource;
using Application.HumanResources.Queries.GetHumanResources;
using Application.HumanResources.Queries.GetOneHumanResource;
using MediatR;
using MVC.Extensions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HumanResourceController : Controller
    {
        private readonly IMediator _mediator;

        public HumanResourceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route]
        [Route("HumanResource")]
        [Route("HumanResource/Index")]
        public async Task<ActionResult> Index(GetHumanResourcesQuery query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("HumanResource/Create")]
        public ActionResult Create()
        {
            return View("Create", new CreateHumanResourceCommand());
        }

        [HttpPost]
        [Route("HumanResource/Create")]
        public async Task<ActionResult> Create(CreateHumanResourceCommand command)
        {
            // Todo: Look into using exception filter to keep this more clean
            try
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ex.AddToModelState(ViewData.ModelState);
                return View(command);
            }
        }

        [HttpGet]
        [Route("HumanResource/Update/{humanResourceId:int}")]
        public async Task<ActionResult> Update(GetOneHumanResourceQuery query)
        {
            var model = await _mediator.Send(query);
            return View(model.ToUpdateHumanResourceCommand());
        }

        [HttpPost]
        [Route("HumanResource/Update")]
        public async Task<ActionResult> Update(UpdateHumanResourceCommand command)
        {
            // Todo: Look into using exception filter to keep this more clean
            try
            {
                await _mediator.Send(command);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ex.AddToModelState(ViewData.ModelState);
                return View(command);
            }
        }

        [HttpPost]
        [Route("HumanResource/Delete")]
        public async Task<ActionResult> Delete(DeleteHumanResourceCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }
    }
}