using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ulaunch_Custom_menu_entries
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string outfolder = @"SDout";
        const string ulaunch = "\\ulaunch";
        const string iconfolder = ulaunch + "\\icons\\";
        const string jsonfolder = ulaunch + "\\entries\\";
        string iconfile = "";

        class jsonnro
    {
        public int type { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int version { get; set; }
        public string nro_path { get; set; }
        public string icon { get; set; }
        }


        public MainWindow()
        {
            InitializeComponent();
       
                if (Directory.Exists(outfolder))
                {
                    return;
                }
            Directory.CreateDirectory(outfolder + iconfolder);
            Directory.CreateDirectory(outfolder + jsonfolder);

        }
        public static string GetRandomString()
        {
            string path = System.IO.Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog icon = new OpenFileDialog()
            {
                Title = "Select icon for entrie",
                Filter = "image PNG (*.png)|*.png;",

                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            DialogResult res = icon.ShowDialog();
            if (res is System.Windows.Forms.DialogResult.OK) iconfile = icon.FileName;
            //System.Windows.MessageBox.Show(iconfile);
        }

        private void MakeButton_Click(object sender, RoutedEventArgs e)
        {
            string idname = GetRandomString();
            jsonnro p1 = new jsonnro 
            { type = 2,
                nro_path = NROpathBox.Text,
                icon = outfolder + iconfolder,
                name = Namebox.Text,
                author = AuthorBox.Text,
            };
            string outputJSON = JsonConvert.SerializeObject(p1);
            File.WriteAllText(idname +".json", outputJSON);
            File.Copy(iconfile, outfolder + iconfolder + idname + ".png");
            File.Move(idname + ".json", outfolder + jsonfolder + idname + ".json");
            //System.Windows.MessageBox.Show(idname);
        }
    }
}
