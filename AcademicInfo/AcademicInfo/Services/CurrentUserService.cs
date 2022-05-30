namespace AcademicInfo.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _contextAccessor = context;
        }

        public string? GetUserId()
        {
            return _contextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value;
        }
    }
}