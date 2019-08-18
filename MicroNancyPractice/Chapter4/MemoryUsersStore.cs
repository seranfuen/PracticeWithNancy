using System;
using System.Collections.Generic;

namespace Chapter4
{
    public class MemoryUsersStore : IUsersStore
    {
        private readonly Dictionary<int, LoyalProgramUser> _users = new Dictionary<int, LoyalProgramUser>();

        public void AddUser(LoyalProgramUser newUser)
        {
            if (_users.ContainsKey(newUser.Id)) throw new InvalidOperationException("User already exists");
            _users.Add(newUser.Id, newUser);
        }

        public void UpdateUser(int userId, LoyalProgramUser updatedUser)
        {
            if (!_users.ContainsKey(userId)) throw new InvalidOperationException("User does not exist");
            _users[userId] = updatedUser;
        }

        public LoyalProgramUser GetUser(int userId)
        {
            return _users.ContainsKey(userId) ? _users[userId] : null;
        }
    }
}