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
using System.IO;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Pkcs;

namespace caofex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Product
        {
            public string? File_path { get; set; }
            public int? Line { get; set; }
            public string? Date { get; set; }
            public string? Slip_number { get; set; }
            public string? Description { get; set; }
            public string? Debit_account { get; set; }
            public string? Debit_price { get; set; }
            public string? Credit_account { get; set; }
            public string? Credit_price { get; set; }
        }
        public List<Product> Products { get; set; } = new List<Product>();

        private void FromFileAddTree(string filename)
        {
            int line;
            line = 0;

            try
            {
                

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (StreamReader sr = new(filename, Encoding.GetEncoding("Shift_JIS")))
                {
                    while (0 <= sr.Peek())
                    {
                        string[] arr = sr.ReadLine().Split('|');
                        Products.Add(new Product{File_path = filename, Line= line, Date = arr[0], Slip_number = arr[1], Description = arr[2], Debit_account = arr[3], Debit_price = arr[4], Credit_account = arr[5], Credit_price = arr[6]});
                        line++;
                    }
                    Products.Sort((a, b) => DateTime.Parse(b.Date).CompareTo(DateTime.Parse(a.Date)));
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileOpen()
        {

            DateTime dt = DateTime.Now;
            for(int i=0; i<12; i++)
            {
                FromFileAddTree(dt.AddMonths(-i).Year.ToString() + "-" + dt.AddMonths(-i).Month.ToString() + ".txt");
            }
            Products.Sort((a, b) => DateTime.Parse(b.Date).CompareTo(DateTime.Parse(a.Date)));
        }

        public MainWindow()
        {
            InitializeComponent();
            data_tree.ItemsSource = Products;
            FileOpen();
        }

        private void data_tree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
