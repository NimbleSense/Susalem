using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace susalem.EasyDemo.Views
{
    /// <summary>
    /// ParameterSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterSettingView : UserControl
    {
        public ParameterSettingView()
        {
            InitializeComponent();
            
            Debug.WriteLine("页面已初始化"); // 多次切换时观察是否重复输出
        }
        // YourPage.xaml.cs

    }

}
