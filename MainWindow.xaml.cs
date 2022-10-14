using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using System.Drawing.Text;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace FT_Edited_version
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        WindowState ws;
        WindowState wsl;
        NotifyIcon notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
            icon();
            contextMenu();
            wsl = WindowState;
            this.Hide();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        //HotKey hotkey = new HotKey();

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    hotkey.Regist(this.HandlesScrolling, (int)HotKey.HotkeyModifiers.control + (int)HotKey.HotkeyModifiers.space, showMenuItem_Click);
        //}

        private void MainWindowDeactivated(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void icon()
        {
            string path = System.IO.Path.GetFullPath(@"resources\images\icon.ico");
            if(File.Exists(path))
            {
                this.notifyIcon = new NotifyIcon();
                this.notifyIcon.Text = "Fast Translation";
                System.Drawing.Icon icon = new System.Drawing.Icon(path);
                this.notifyIcon.Icon = icon;
                this.notifyIcon.Visible = true;
                notifyIcon.MouseDoubleClick += OnNotifyIconDouble_Click;
            }
        }

        private void OnNotifyIconDouble_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = wsl;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            ws = this.WindowState;
            if(ws == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void contextMenu()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            ToolStripMenuItem showMenuItem = new ToolStripMenuItem();
            showMenuItem.Text = "Show";
            showMenuItem.Click += new EventHandler(showMenuItem_Click);

            ToolStripMenuItem aboutMenuItem = new ToolStripMenuItem();
            aboutMenuItem.Text = "About";
            aboutMenuItem.Click += new EventHandler(aboutMenuItem_Click);

            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem();
            exitMenuItem.Text = "Exit";
            exitMenuItem.Click += new EventHandler(exitMenuItem_Click);

            contextMenuStrip.Items.Add(showMenuItem);
            contextMenuStrip.Items.Add(aboutMenuItem);
            contextMenuStrip.Items.Add(exitMenuItem);
        }

        /*
         * Keyboard events
         * If you want use the event,you must define event in MainWindow.xaml
         */
        private void InputTextbox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Translation_Click(sender, e);
            }
            if (e.Key == Key.Delete)
            {
                this.Purge_Click(sender, e);
            }
        }

        private void Translation_Click(object sender, EventArgs e)
        {
            string inputtext = InputTextbox.Text;
            char[] c = inputtext.ToCharArray();
            /*
             * Determine if the input text is Chinese. If it is, call TraCtoE, if it is not, call TraEtoC.
             */
            for(int i = 0; i < c.Length; i++)
            {
                if (c[i] > 127/*c[i] >= 0xe4e00 && c[i] <= 0x9fbb*/)//If the inputtext = Kanji
                {
                    TraCtoE input = new TraCtoE();
                    string Word = input.TtraCtoEWord(inputtext);
                    string TraEN = input.TtraCtoEEN(inputtext);
                    TraENTextbox.Text = Word;
                    TraCNTextbox.Text = TraEN;
                }
                else
                {
                    TraEtoC input = new TraEtoC();
                    string TraEN = input.TtraEtoCEN(inputtext);
                    string TraCN = input.TtraEtoCCN(inputtext);
                    TraENTextbox.Text = TraEN;
                    TraCNTextbox.Text = TraCN;
                }
            }
        }

        private void Purge_Click(object sender, EventArgs e)
        {
            InputTextbox.Clear();
            TraENTextbox.Clear();
            TraCNTextbox.Clear();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.Show();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            System.Windows.Application.Current.Shutdown();
        }
    }
}
