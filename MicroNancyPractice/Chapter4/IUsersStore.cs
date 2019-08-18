namespace Chapter4
{
    public interface IUsersStore
    {
        void AddUser(LoyalProgramUser newUser);
        void UpdateUser(int userId, LoyalProgramUser updatedUser);
        LoyalProgramUser GetUser(int userId);
    }
}