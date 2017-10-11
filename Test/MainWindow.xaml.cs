using EnvSafe.Baidu;
using EnvSafe.Baidu.SMS;
using Heroius.Files;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            diaopen = new OpenFileDialog() { Filter = "百度API配置文件|*.bxml" };
            diasave = new SaveFileDialog() { Filter = "百度API配置文件|*.bxml" };

            sms = new SMSSender(new BCE(new BCESettings()), new SMSSettings());
            smsinfo = new SMSInfo();
            smsinfoshell = new SMSInfoShell(smsinfo);

            DataContext = this;
        }

        SMSSender sms;
        SMSInfo smsinfo;
        public SMSInfoShell smsinfoshell { get; set; }

        #region Menu

        OpenFileDialog diaopen;
        SaveFileDialog diasave;

        private void ImportSettings_Click(object sender, RoutedEventArgs e)
        {
            if (diaopen.ShowDialog() == true)
            {
                EntitySilo silo = new EntitySilo();
                silo.Load(diaopen.FileName);
                silo.Fetch("BCE", sms.Auth.Settings);
                silo.Fetch("SMS", sms.Settings);
            }
        }
        
        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (diasave.ShowDialog() == true)
            {
                EntitySilo silo = new EntitySilo();
                silo.Store("BCE", sms.Auth.Settings);
                silo.Store("SMS", sms.Settings);
                silo.Save(diasave.FileName);
            }
        }

        private void EditSettings_Click(object sender, RoutedEventArgs e)
        {
            new SettingWindow(sms).ShowDialog();
        }

        #endregion

        private void SendSMS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = sms.SendMessage(smsinfo);
                MessageBox.Show($"{result.Code} : {result.Message}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
