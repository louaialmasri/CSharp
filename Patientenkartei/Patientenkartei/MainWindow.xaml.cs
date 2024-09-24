using System.Windows;
using System.IO;
using System.Text;

namespace Patientenkartei
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string DIR_PATH = @"C:\Users\louai\OneDrive\Desktop\Louai\Coding\C#\Patientenkartei\Patientenkartei";
        public const string FILE_EXT = ".txt";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string content = textBoxContent.Text;
            string filename = textBoxFileName.Text;

            using (FileStream fs = File.Create(DIR_PATH + filename + FILE_EXT)) 
            { 
                byte[] contentConvertedToBytes = Encoding.ASCII.GetBytes(content);
                fs.Write(contentConvertedToBytes, 0, contentConvertedToBytes.Length);
            
            }

            MessageBox.Show("Datei wurde angelegt.");
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            string filename = textBoxFileName.Text;

            using (FileStream fs = File.OpenRead(DIR_PATH + filename + FILE_EXT)) 
            {
                using (StreamReader sr = new StreamReader(fs)) 
                {
                    string content = sr.ReadToEnd();
                    textBoxContent.Text = content;
                }
            }
        }
    }
}
