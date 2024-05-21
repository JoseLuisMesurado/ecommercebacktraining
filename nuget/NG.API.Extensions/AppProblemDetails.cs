using Microsoft.AspNetCore.Mvc;

namespace NG.API.Extensions
{
    public class AppProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> ErrorsMessages { get; set; }
    }
}
