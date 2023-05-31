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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Class-Level Array of Strings for test fields.
        // In production would need to replace with a function to get string[] GetReportFieldNames.
        public string[] ReportFieldNames1 { get; } = { "Vendor_ID", "Reseller_ID", "QuoteFor", "VendorRep1", "VendorRep3", "Vendor_ID", "Middle", "Agency", "State", "Email", "ResellerRep2", "Quote_ID", "Cataloged"};
        public string[] ReportFieldNames2 { get; } = {"FirstName", "LastName", "Title", "Address1", "Middle", "Agency", "State", "Email", "phoneOptOut", "Cell", "Fax", "MainAgencyLevel3", "MainAgencyLevel4"};

        public MainWindow()
        {
            InitializeComponent();
            AddExampleValuesToComboBoxes();
            DataContext = this;
        }

        private void AddExampleValuesToComboBoxes()
        {
            AddExamplesToComboBox("QuoteFor", new[] { "Acme Corp", "Globex Inc", "Techtron Solutions", "Macrosoft", "Quantum Devices" });
            AddExamplesToComboBox("Country", new[] { "USA", "CA", "UK", "AUS", "GRMY" });
            AddExamplesToComboBox("Vendor_ID", new[] { "VND-1001", "VND-2012", "VND-3420", "VND-4785", "VND-5321" });
            AddExamplesToComboBox("Reseller_ID", new[] { "RSL-2034", "RSL-3102", "RSL-4591", "RSL-6783", "RSL-7246" });
        }

        private void AddExamplesToComboBox(string comboBoxName, string[] examples)
        {
            if (FindName(comboBoxName) is ComboBox comboBox)
            {
                foreach (string example in examples)
                {
                    comboBox.Items.Add(example);
                }
                comboBox.SelectedIndex = 0;
            }
        }

    }
}
