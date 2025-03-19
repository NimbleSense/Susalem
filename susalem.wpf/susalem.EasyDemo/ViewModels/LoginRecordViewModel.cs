using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Models;
using HslCommunication.Core.Net;
using HslCommunication.Secs.Types;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using susalem.EasyDemo.Services.IServices;

namespace susalem.EasyDemo.ViewModels
{
    public class LoginRecordViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly IRoleService _roleService;
        public LoginRecordViewModel(IDialogService dialogService, IRegionManager regionManager,
            IUserService userService, IRoleService roleService)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _userService = userService;
            _roleService = roleService;
            
            

        }

        private ObservableCollection<RoleModel> roleModels = new ObservableCollection<RoleModel>();

        public ObservableCollection<RoleModel> RoleModels
        {
            get { return roleModels; }
            set
            {
                roleModels = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<UserModel>? _allUser = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel>? AllUser
        {
            get { return _allUser; }
            set { _allUser = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<string> _users = new ObservableCollection<string>();

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set { _users = value; RaisePropertyChanged(); }
        }

        private string? _userName;

        public string? UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        private string? _password;

        public string? Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        private async Task LoadUser()
        {
            await Task.Run(() =>
            {
                var _result = _userService.FindAllUser();

                if (_result != null && _result.Count != 0)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AllUser.Clear();
                        Users.Clear();
                        foreach (var user in _result)
                        {
                            if (!string.IsNullOrWhiteSpace(user.UserName))
                            {
                                Users?.Add(user.UserName);
                                AllUser.Add(user);
                            }
                        }
                    });
                    
                }
            });
        }

        private async Task LoadAllRoles()
        {
            await Task.Run(() =>
            {
                var _result = _roleService.FindAllRole();

                if (_result != null && _result.Count != 0)
                {
                    RoleModels.Clear();
                    foreach (var role in _result)
                    {
                        if (!string.IsNullOrWhiteSpace(role.RoleName))
                        {
                            RoleModels?.Add(role);
                        }
                    }
                }
            });
           
        }

        private void Login()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(UserName))
            {
                ErrorMessage = "UserName is empty";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Password is empty";
                return;
            }


            Task.Run(() =>
            {
                try
                {
                    UserModel resultModel = _userService.Login(UserName, Password,false);

                    if (resultModel!=null)
                    {
                        OverAllContext.User = resultModel!;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "登录成功!" } }, null);
                        });

                        //Application.Current.Dispatcher.Invoke(() => RequestClose?.Invoke(new DialogResult(ButtonResult.OK)));
                        //AppSession.IsLogon = false;
                    }
                    else
                    {
                        ErrorMessage = "登录失败";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    //logger.Error(ex.Message);
                }
            });
        }

        public ICommand RegisterCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogParameters keyValuePairs = new DialogParameters();
                keyValuePairs.Add("Users", AllUser);
                keyValuePairs.Add("Roles", RoleModels);
                _dialogService.ShowDialog("AddUserView", keyValuePairs, callback =>
                {
                    if (callback.Result == ButtonResult.OK)
                    {
                        LoadUser();
                    }
                });
            });
        }

        public ICommand LoginCommand
        {
            get => new DelegateCommand(() =>
            {
                Login();
            });
        }

        public ICommand PageLoaded
        {
            get => new DelegateCommand(async() =>
            {
                await LoadUser();
                await LoadAllRoles();
            });
        }

        public ICommand PageUnLoaded
        {
            get => new DelegateCommand(() =>
            {
                //Login();
            });
        }
    }
}
