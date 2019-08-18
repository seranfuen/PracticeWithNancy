using Nancy;
using Nancy.ModelBinding;

namespace Chapter4
{
    public class UserModule : NancyModule
    {
        private readonly IUsersStore _userStore;

        public UserModule(IUsersStore userStore) : base("/users")
        {
            _userStore = userStore;
            Post("/", _ =>
            {
                var newUser = this.Bind<LoyalProgramUser>();
                AddRegisteredUser(newUser);
                return CreatedResponse(newUser);
            });

            Put("/{userId:int}", parameter =>
            {
                int userId = parameter.userId;
                var updatedUser = this.Bind<LoyalProgramUser>();
                _userStore.UpdateUser(userId, updatedUser);
                return updatedUser;
            });

            Get("/{userId:int}", parameter =>
            {
                var userId = parameter.userId;
                var user = _userStore.GetUser(userId);
                return user != null ? user : HttpStatusCode.NotFound;
            });
        }

        private void AddRegisteredUser(LoyalProgramUser newUser)
        {
            _userStore.AddUser(newUser);
        }

        private dynamic CreatedResponse(LoyalProgramUser newUser)
        {
            return Negotiate.WithStatusCode(HttpStatusCode.Created)
                .WithHeader("Location", Request.Url.SiteBase + "/users/" + newUser.Id).WithModel(newUser);
        }
    }
}