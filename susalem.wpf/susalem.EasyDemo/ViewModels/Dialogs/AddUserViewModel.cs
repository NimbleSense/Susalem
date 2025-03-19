using HandyControl.Controls;
using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Models;
using susalem.EasyDemo.Share;
using HslCommunication.Secs.Types;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using susalem.EasyDemo.Services.IServices;

namespace susalem.EasyDemo.ViewModels.Dialogs
{
    internal class AddUserViewModel : BindableBase, IDialogAware
    {
        private readonly IUserService _userService;

        public AddUserViewModel(IUserService userService)
        {
            _userService = userService;
        }
        public string Title => "新增用户";

        public event Action<IDialogResult> RequestClose;

        public List<UserModel>? Users { get; set; }

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        private string? _userName;

        public string? UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private string? _password;

        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private RoleModel currentRole;

        public RoleModel CurrentRole
        {
            get { return currentRole; }
            set
            {
                currentRole = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RoleModel> roleModels = new();

        public ObservableCollection<RoleModel> RoleModels
        {
            get { return roleModels; }
            set
            {
                roleModels = value;
                RaisePropertyChanged();
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Users") && parameters.GetValue<ObservableCollection<UserModel>>("Users") != null &&
                parameters.ContainsKey("Roles") && parameters.GetValue<ObservableCollection<RoleModel>>("Roles") != null)
            {
                Users = new List<UserModel>(parameters.GetValue<ObservableCollection<UserModel>>("Users")!);
                RoleModels = new ObservableCollection<RoleModel>(parameters.GetValue<ObservableCollection<RoleModel>>("Roles")!);
            }
        }

        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogResult dialogResult = new DialogResult(ButtonResult.Cancel);
                RequestClose?.Invoke(dialogResult);
            });
        }

        public ICommand SaveCommand
        {
            get => new DelegateCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Password is empty";
                    return;
                }

                UserModel? _user = new UserModel();
                _user!.UserName = UserName;

                _user!.Password = Cryptography.Encrypt(Password ?? string.Empty);
                //_user!.UserIcon = "Images/user.png";
                _user!.RealName = "";
                _user.RoleId = CurrentRole.RoleId;

                var userResult = _userService.AddUser(_user);

                DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                RequestClose?.Invoke(dialogResult);

            });
        }
    }
}
