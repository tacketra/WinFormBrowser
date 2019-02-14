using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Browser : Form
    {
        private static Regex UrlRegex = new Regex(@"https?:\/\/");

        private ToolStripTextBox urlTextBox { get; set; } = new ToolStripTextBox();
        private WebBrowser webBrowserControl { get; set; } = new WebBrowser()
        {
            ScriptErrorsSuppressed = true,
            Dock = DockStyle.Fill
        };

        private ToolStrip webNavigationControlsContainer { get; set; } = new ToolStrip();
        private ToolStrip urlTextBoxContainer { get; set; } = new ToolStrip();

        private ToolStripButton goButton { get; set; } = new ToolStripButton("Go");
        private ToolStripButton backButton { get; set; } = new ToolStripButton("Back") { Enabled = false };
        private ToolStripButton forwardButton { get; set; } = new ToolStripButton("Forward") { Enabled = false };
        private ToolStripButton stopButton { get; set; } = new ToolStripButton("Stop");
        private ToolStripButton refreshButton { get; set; } = new ToolStripButton("Refresh");
        private ToolStripButton homeButton { get; set; } = new ToolStripButton("Home");
        private ToolStripButton searchButton { get; set; } = new ToolStripButton("Search");

        public Browser()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void Navigate(String url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            if (!UrlRegex.IsMatch(url))
            {
                url = $"http://{url}";
            }

            try
            {
                this.webBrowserControl.Navigate(new Uri(url));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return;
            }
        }

        #region browser events
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.urlTextBox.Text = this.webBrowserControl.Url.ToString();
        }

        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Navigate(this.urlTextBox.Text);
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            this.Navigate(this.urlTextBox.Text);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.GoBack();
        }

        private void webBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            this.backButton.Enabled = this.webBrowserControl.CanGoBack;
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.GoForward();
        }

        private void webBrowser_CanGoForwardChanged(object sender, EventArgs e)
        {
            this.forwardButton.Enabled = this.webBrowserControl.CanGoForward;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.Stop();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.Refresh();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.GoHome();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.webBrowserControl.GoSearch();
        }

        #endregion browser events

        private void InitializeForm()
        {
            this.urlTextBoxContainer.Items.Add(this.urlTextBox);
            this.webNavigationControlsContainer.Items.AddRange(new ToolStripItem[]
            {
                this.goButton,
                this.backButton,
                this.forwardButton,
                this.stopButton,
                this.refreshButton,
                this.homeButton,
                this.searchButton
            });
            this.Controls.AddRange(new Control[]
            {
                this.webBrowserControl,
                this.urlTextBoxContainer,
                this.webNavigationControlsContainer
            });

            this.goButton.Click += new System.EventHandler(goButton_Click);
            this.backButton.Click += new System.EventHandler(backButton_Click);
            this.forwardButton.Click += new System.EventHandler(forwardButton_Click);
            this.stopButton.Click += new System.EventHandler(stopButton_Click);
            this.refreshButton.Click += new System.EventHandler(refreshButton_Click);
            this.homeButton.Click += new System.EventHandler(homeButton_Click);
            this.searchButton.Click += new System.EventHandler(searchButton_Click);
            this.urlTextBox.Size = new System.Drawing.Size(500, 25);
            this.urlTextBox.KeyDown += new KeyEventHandler(urlTextBox_KeyDown);
            this.webBrowserControl.Dock = DockStyle.Fill;
            this.webBrowserControl.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
            this.webBrowserControl.CanGoBackChanged += new EventHandler(webBrowser_CanGoBackChanged);
            this.webBrowserControl.CanGoForwardChanged += new EventHandler(webBrowser_CanGoForwardChanged);
        }
    }
}
