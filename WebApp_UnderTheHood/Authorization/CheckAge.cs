using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;

namespace WebApp_UnderTheHood.Authorization
{
    public class CheckAge : IAuthorizationRequirement
    {
        public CheckAge(int age)
        {
            Age = age;
        }

        public int Age { get; }
    }
}
