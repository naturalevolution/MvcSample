using System.Web.Mvc;
using MvcSample.Repositories;

namespace MvcSample.Controllers {
    public class BaseController: Controller {
        protected MvcSampleContext _context = new MvcSampleContext();

        protected IKnightRepository KnightRepository;
        protected IPrincessRepository PrincessRepository;

    }
}