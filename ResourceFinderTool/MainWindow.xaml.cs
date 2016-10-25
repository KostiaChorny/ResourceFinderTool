using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ResourceFinderTool.Classes;

namespace ResourceFinderTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathTxt.Text = dialog.SelectedPath;
            }
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            var controller = new ResourceExtractorController(pathTxt.Text);
            controller.LogMessage += controller_LogMessage;
            controller.StartExtraction();
        }

        void controller_LogMessage(object sender, string e)
        {
            logTxt.Text += e + Environment.NewLine;
        }
    }
}
