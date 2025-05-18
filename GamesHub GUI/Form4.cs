using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace GamesHub
{
    public partial class Form4 : Form
    {
        private TextBox txtGameName, txtPrice, txtCategoryId, txtReleaseDate, txtVendorId, txtDescription, txtDeleteGameName;
        private TextBox txtUpdateGameName, txtNewDescription;

        public Form4()
        {
            InitializeComponent();
            this.AutoScroll = true;
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Game Rental System";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;

            Label titleLabel = new Label()
            {
                Text = "Game Rental System",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Lime,
                Location = new Point(300, 20),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            GroupBox addGameGroup = new GroupBox()
            {
                Text = "Add Game",
                ForeColor = Color.Lime,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, 70),
                Size = new Size(400, 420)
            };
            this.Controls.Add(addGameGroup);

            string[] labels = { "Game Name", "Price", "Category ID", "Release Date", "Vendor ID", "Description" };
            int startY = 30;
            int spacing = 50;
            TextBox[] textboxes = new TextBox[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                Label label = new Label()
                {
                    Text = labels[i],
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.Green,
                    Location = new Point(20, startY + i * spacing),
                    AutoSize = true
                };
                addGameGroup.Controls.Add(label);

                textboxes[i] = new TextBox()
                {
                    Location = new Point(150, startY + i * spacing),
                    Width = 200,
                    BorderStyle = BorderStyle.FixedSingle,
                    ForeColor = Color.Green
                };
                addGameGroup.Controls.Add(textboxes[i]);

                if (labels[i] == "Description")
                {
                    textboxes[i].Height = 60;
                    textboxes[i].Multiline = true;
                }
                else if (labels[i] == "Release Date")
                {
                    textboxes[i].Text = "YYYY-MM-DD";
                }
            }

            txtGameName = textboxes[0];
            txtPrice = textboxes[1];
            txtCategoryId = textboxes[2];
            txtReleaseDate = textboxes[3];
            txtVendorId = textboxes[4];
            txtDescription = textboxes[5];

            Button addBtn = new Button()
            {
                Text = "Add Game",
                Location = new Point(150, 330),
                Size = new Size(100, 35),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            addBtn.Click += AddGame_Click;
            addGameGroup.Controls.Add(addBtn);

            // Delete Game Section
            GroupBox deleteGameGroup = new GroupBox()
            {
                Text = "Delete Game",
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(500, 70),
                Size = new Size(250, 180)
            };
            this.Controls.Add(deleteGameGroup);

            Label delLabel = new Label()
            {
                Text = "Game Name:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Red,
                Location = new Point(20, 30),
                AutoSize = true
            };
            deleteGameGroup.Controls.Add(delLabel);

            txtDeleteGameName = new TextBox()
            {
                Location = new Point(20, 55),
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Green
            };
            deleteGameGroup.Controls.Add(txtDeleteGameName);

            Button deleteBtn = new Button()
            {
                Text = "Delete Game",
                Location = new Point(20, 100),
                Size = new Size(120, 35),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            deleteBtn.Click += DeleteGame_Click;
            deleteGameGroup.Controls.Add(deleteBtn);

            // Update Description Section
            GroupBox updateDescGroup = new GroupBox()
            {
                Text = "Update Game Description",
                ForeColor = Color.Green,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(500, 270),
                Size = new Size(250, 180)
            };
            this.Controls.Add(updateDescGroup);

            Label lblGameName = new Label()
            {
                Text = "Game Name:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Green,
                Location = new Point(20, 30),
                AutoSize = true
            };
            updateDescGroup.Controls.Add(lblGameName);

            txtUpdateGameName = new TextBox()
            {
                Location = new Point(20, 55),
                Width = 200,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Green
            };
            updateDescGroup.Controls.Add(txtUpdateGameName);

            Label lblNewDesc = new Label()
            {
                Text = "New Description:",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Green,
                Location = new Point(20, 85),
                AutoSize = true
            };
            updateDescGroup.Controls.Add(lblNewDesc);

            txtNewDescription = new TextBox()
            {
                Location = new Point(20, 110),
                Width = 200,
                Multiline = true,
                Height = 50,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Green
            };
            updateDescGroup.Controls.Add(txtNewDescription);

            Button updateDescBtn = new Button()
            {
                Text = "Update Description",
                Location = new Point(20, 165),
                Size = new Size(150, 30),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            updateDescBtn.Click += UpdateDescription_Click;
            updateDescGroup.Controls.Add(updateDescBtn);

            Button viewReportsBtn = new Button()
            {
                Text = "View Reports",
                Location = new Point(500, 460),
                Size = new Size(120, 35),
                BackColor = Color.Blue,
                ForeColor = Color.White
            };
            viewReportsBtn.Click += (s, e) =>
            {
                Form6 f6 = new Form6();
                f6.ShowDialog();
            };
            this.Controls.Add(viewReportsBtn);

            Button logoutBtn = new Button()
            {
                Text = "Logout",
                Location = new Point(750, 20),
                Size = new Size(100, 35),
                BackColor = Color.DarkRed,
                ForeColor = Color.White
            };
            logoutBtn.Click += (s, e) =>
            {
                this.Close();
                Form1 loginForm = new Form1();
                loginForm.Show();
            };
            this.Controls.Add(logoutBtn);
        }

        private void AddGame_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString))
                {
                    con.Open();

                    int vendorId = int.Parse(txtVendorId.Text);
                    int categoryId = int.Parse(txtCategoryId.Text);
                    float price = float.Parse(txtPrice.Text);
                    DateTime releaseDate = DateTime.Parse(txtReleaseDate.Text);
                    string gameName = txtGameName.Text;
                    string description = txtDescription.Text;

                    SqlCommand checkAID = new SqlCommand("SELECT COUNT(*) FROM ADMIN WHERE AID = 1", con);
                    if ((int)checkAID.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("AID=1 not found in ADMIN table.");
                        return;
                    }

                    SqlCommand checkVID = new SqlCommand("SELECT COUNT(*) FROM VENDOR WHERE VID = @vid", con);
                    checkVID.Parameters.AddWithValue("@vid", vendorId);
                    if ((int)checkVID.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"VID={vendorId} not found in VENDOR table.");
                        return;
                    }

                    SqlCommand checkCID = new SqlCommand("SELECT COUNT(*) FROM CATEGORY WHERE CID = @cid", con);
                    checkCID.Parameters.AddWithValue("@cid", categoryId);
                    if ((int)checkCID.ExecuteScalar() == 0)
                    {
                        MessageBox.Show($"CID={categoryId} not found in CATEGORY table.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO GAME (AID, VID, CID, GAME_NAME, PRICE, RELEASE_DATE, DESCRIPTION) 
                        VALUES (@aid, @vid, @cid, @name, @price, @date, @desc)", con);

                    cmd.Parameters.AddWithValue("@aid", 1);
                    cmd.Parameters.AddWithValue("@vid", vendorId);
                    cmd.Parameters.AddWithValue("@cid", categoryId);
                    cmd.Parameters.AddWithValue("@name", gameName);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@date", releaseDate);
                    cmd.Parameters.AddWithValue("@desc", description);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Game Added Successfully");

                    txtGameName.Clear();
                    txtPrice.Clear();
                    txtReleaseDate.Text = "YYYY-MM-DD";
                    txtDescription.Clear();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers and date.");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteGame_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString))
                {
                    con.Open();
                    string query = "DELETE FROM GAME WHERE GAME_NAME = @name";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", txtDeleteGameName.Text);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                        MessageBox.Show("Game Deleted Successfully");
                    else
                        MessageBox.Show("Game not found");

                    txtDeleteGameName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateDescription_Click(object sender, EventArgs e)
        {
            try
            {
                string gameName = txtUpdateGameName.Text;
                string newDescription = txtNewDescription.Text;

                if (string.IsNullOrWhiteSpace(gameName))
                {
                    MessageBox.Show("Please enter a game name.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE GAME SET DESCRIPTION = @desc WHERE GAME_NAME = @name", con);
                    cmd.Parameters.AddWithValue("@desc", newDescription);
                    cmd.Parameters.AddWithValue("@name", gameName);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show("Description updated successfully.");
                    else
                        MessageBox.Show("Game not found.");
                }

                txtUpdateGameName.Clear();
                txtNewDescription.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating description: " + ex.Message);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        { }
    }
}