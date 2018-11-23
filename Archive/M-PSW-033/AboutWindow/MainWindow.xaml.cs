using System;
using System.Collections.Generic;
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

namespace AboutWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationName.Content = "Commander1xx DataExtractor";
            ApplicationVersion.Content = "V01.00";
            ApplicationSummary.Text =   "The Commander1xx DataExtractor Application is intended to be used to view a single datalog of a vehicle. " +
                                        "The application contains a Unit Information, Event Summuary and Log Entries tabs each of the tab will provide " +
                                        "information surrounding the datalog loaded and the filters applied." +
                                        "\nThe application has the capabilty to filter the popoulated data in the Log Entries tab, the type of filters include:" +
                                        "\n   1.    Start Date and Time " +
                                        "\n   2.    End Date and Time" +
                                        "\n   3.    Raw Data" +
                                        "\n   4.    Event Description"+
                                        "\n   5.    Event Information ( & and | )";
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
