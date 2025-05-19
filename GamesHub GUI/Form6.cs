using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GamesHub
{
    public partial class Form6 : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString;
        private readonly (string Title, string Query)[] reports = new[]
        {
            ("a. Most Interesting Game", @"SELECT TOP 1 with ties g.GAME_NAME AS [Game], COUNT(r.UID) AS [Renters] FROM GAME g JOIN RENTALS r ON g.GID=r.GID GROUP BY g.GAME_NAME ORDER BY Renters DESC"),
            ("b. No Renters Last Month", @"SELECT g.GAME_NAME AS [Game] FROM GAME g WHERE g.GID NOT IN (SELECT GID FROM RENTALS WHERE RENT_DATE>=DATEADD(MONTH,-1,GETDATE()))"),
            ("c. Top Client Last Month", @"SELECT TOP 1 with ties u.USER_NAME AS [Client], COUNT(r.GID) AS [Rentals] FROM [USER] u JOIN RENTALS r ON u.UID=r.UID WHERE r.RENT_DATE>=DATEADD(MONTH,-1,GETDATE()) GROUP BY u.USER_NAME ORDER BY Rentals DESC"),
            ("d. Top Vendor Last Month", @"SELECT TOP 1 with ties v.VENDOR_NAME AS [Vendor], COUNT(r.GID) AS [Rentals] FROM VENDOR v JOIN GAME g ON v.VID=g.VID JOIN RENTALS r ON g.GID=r.GID WHERE r.RENT_DATE>=DATEADD(MONTH,-1,GETDATE()) GROUP BY v.VENDOR_NAME ORDER BY Rentals DESC"),
            ("e. Vendors No Rentals Last Month", @"SELECT VENDOR_NAME AS [Vendor] FROM VENDOR WHERE VID NOT IN (SELECT DISTINCT g.VID FROM GAME g JOIN RENTALS r ON g.GID=r.GID WHERE r.RENT_DATE>=DATEADD(MONTH,-1,GETDATE()))"),
            ("f. Vendors No New Games Last Year", @"SELECT VENDOR_NAME AS [Vendor] FROM VENDOR WHERE VID NOT IN (SELECT DISTINCT VID FROM GAME WHERE RELEASE_DATE>=DATEADD(YEAR,-1,GETDATE()))")
        };

        private ComboBox combo;
        private Button showButton;
        private DataGridView grid;

        public Form6()
        {
            Text = "Game Rental Analysis";
            Size = new Size(800, 600);
            BackColor = Color.FromArgb(30, 30, 30);

            combo = new ComboBox
            {
                Location = new Point(20, 20),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 48)
            };
            foreach (var r in reports) combo.Items.Add(r.Title);
            combo.SelectedIndex = 0;
            Controls.Add(combo);

            showButton = new Button
            {
                Text = "Show",
                Location = new Point(combo.Right + 10, 18),
                Size = new Size(80, 28),
                BackColor = Color.FromArgb(76, 175, 80),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            showButton.FlatAppearance.BorderSize = 0;
            showButton.Click += (s, e) => LoadReport(combo.SelectedIndex);
            Controls.Add(showButton);

            grid = new DataGridView
            {
                Location = new Point(20, combo.Bottom + 20),
                Size = new Size(760, 500),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(63, 63, 70),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(28, 151, 234),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(45, 45, 48),
                    ForeColor = Color.White,
                    SelectionBackColor = Color.FromArgb(28, 151, 234),
                    SelectionForeColor = Color.White
                }
            };
            Controls.Add(grid);

            LoadReport(0);
        }

        private void LoadReport(int idx)
        {
            if (idx < 0 || idx >= reports.Length) return;
            try
            {
                grid.DataSource = Fetch(reports[idx].Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable Fetch(string q)
        {
            var dt = new DataTable();
            using (var c = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(q, c))
            {
                c.Open(); dt.Load(cmd.ExecuteReader());
            }
            return dt;
        }
    }
}