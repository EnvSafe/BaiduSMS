using EnvSafe.Baidu.SMS;
using System.Windows;

namespace Test
{
    /// <summary>
    /// BCEWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow(SMSSender sms)
        {
            InitializeComponent();
            SMS = sms;
        }

        public SMSSender SMS { get { return sms; } set { sms = value; this.DataContext = sms; } }
        SMSSender sms;
    }
}
