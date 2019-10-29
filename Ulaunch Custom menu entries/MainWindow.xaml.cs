using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
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
        const string iconfolder = ulaunch + "\\nro\\";
        const string jsonfolder = ulaunch + "\\entries\\";
        string iconfile = "";

        class jsonnro
        {

        public string type { get; set; }
        public string nro_path { get; set; }
        public string nro_argv { get; set; }
        public string icon { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string version { get; set; }

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
            System.Windows.MessageBox.Show("use an image with the size of 256x256");
            OpenFileDialog icon = new OpenFileDialog()
            {
                Title = "Select icon for entrie",
                Filter = "image PNG (*.png)|*.png;",

                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            DialogResult res = icon.ShowDialog();
            Bitmap img = new Bitmap(icon.FileName);
            if (img.Width != 256 && img.Height != 256)
            {
                System.Windows.MessageBox.Show("please use a 256x256 image");
                return;
            }
            if (res is System.Windows.Forms.DialogResult.OK) iconfile = icon.FileName; else return;
            NROicon.Source = new BitmapImage(new Uri(icon.FileName));
            
        }

        private void MakeButton_Click(object sender, RoutedEventArgs e)
        {
            string idname = GetRandomString();
            

            if (String.IsNullOrEmpty(Namebox.Text))
            {
                System.Windows.MessageBox.Show("No application name was set.");
                return;
            }

            if (String.IsNullOrEmpty(AuthorBox.Text))
            {
                System.Windows.MessageBox.Show("No Author name was set.");
                return;
            }

            if (NROpathBox.Text == "switch/")
            {
                System.Windows.MessageBox.Show("You havent chosen any path for the file");
                return;
            }
            if (String.IsNullOrEmpty(iconfile))
            {
                System.Windows.MessageBox.Show("Icon file was not selected. This file is required to make the entrie.");
                return;
            }

            jsonnro p1 = new jsonnro 
            { type = TypeBox.Text,
                nro_path = "sdmc:/" + NROpathBox.Text,
                nro_argv = NROargvBox.Text,
                icon = "sdmc:/ulaunch/nro/" + idname + ".png",
                name = Namebox.Text,
                author = AuthorBox.Text,
                version = VersionBox.Text
            };
            string outputJSON = JsonConvert.SerializeObject(p1);
            File.WriteAllText(idname +".json", outputJSON);
            File.Copy(iconfile, outfolder + iconfolder + idname + ".png");
            File.Move(idname + ".json", outfolder + jsonfolder + idname + ".json");
            System.Windows.MessageBox.Show(Namebox.Text + " It has been created perfectly");
            Namebox.Text = null;
            AuthorBox.Text = null;
            NROpathBox.Text = "switch/";
            NROargvBox.Text = null;
            VersionBox.Text = "1.0.0";
        }
    }
}
