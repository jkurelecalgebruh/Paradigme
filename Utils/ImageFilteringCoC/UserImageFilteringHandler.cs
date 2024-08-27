using Back.Db;
using Back.Models;
using Back.Services;

namespace Back.Utils.ImageFilteringCoC
{
    public class UserImageFilteringHandler : BaseImageFilteringHandler
    {
        private readonly UserService _userService;
        public UserImageFilteringHandler(BaseImageFilteringHandler next, UserService userService) : base(next) => _userService = userService;

        public UserImageFilteringHandler() { }

        public override List<Image> Handle(List<Image> filtered) =>
            _requests.User != null
                ? HandleNext(filtered.Where(x => _userService.Find(x.Author).Username == _requests.User).ToList())
                : base.HandleNext(filtered);
    }
}
