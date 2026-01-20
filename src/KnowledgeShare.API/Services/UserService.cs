using KnowledgeShare.API.Repositories;
using KnowledgeShare.API.Repositories.Interface;
using KnowledgeShare.API.Services.Interface;
using KnowledgeShare.API.ViewModels;
using KnowledgeShare.ViewModels.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeShare.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(
     string userId,
     UserChangePasswordVm vm)
        {
            // 1. Check confirm password
            if (vm.NewPassword != vm.ConfirmPassword)
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "Confirm password does not match" }
                );
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "User not found" }
                );
            }

            // 3. Change password (Identity xử lý)
            return await _userManager.ChangePasswordAsync(
                user,
                vm.OldPassword,
                vm.NewPassword
            );
        }

        public async Task<IdentityResult> CreateUserAsync(UserVm user)
        {
            var userCreate = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob.GetValueOrDefault()
            };

            return await _userRepository.CreateUserAsync(userCreate, user.Password); 
        }

        public async Task<IdentityResult> DeleteUserAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "user not found"
                });
            }

            return await _userRepository.DeleteUserAsync(user);    
        }

        public async Task<List<UserVm>> GetAllUsersAsync()
        {
            var list = await _userRepository.GetAllUsersAsync();

            var users = list.Select(u => new UserVm
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Dob = u.Dob
            }).ToList();
            return users;
        }

        public async Task<List<FunctionVm>> GetMenuByUserPermissionAsync(string userId)
        {
            var list = await _userRepository.GetMenuByUserPermission(userId);
            return list.Select(u => new FunctionVm
            {
                Id = u.Id,
                Name = u.Name,
                Url = u.Url,
                ParentId = u.ParentId,
                SortOrder = u.SortOrder,
            }).ToList();
        }

        public async Task<UserVm> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return new UserVm
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob
            }; 
        }

        public async Task<IdentityResult> UpdateUserAsync(string id, UserVm user)
        {
            var userUpdate = await _userRepository.GetUserByIdAsync(id);

            if (userUpdate == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "user not found"
                });
            }

            userUpdate.Dob = user.Dob.GetValueOrDefault();
            userUpdate.PhoneNumber = user.PhoneNumber;
            userUpdate.FirstName = user.FirstName;
            userUpdate.LastName = user.LastName;

            return await _userRepository.UpdateUserAsync(userUpdate);

        }
    }
}
