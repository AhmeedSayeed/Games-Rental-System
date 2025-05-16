using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using GamesHub;

namespace GameRentalSystem
{
    public partial class LoginForm : Form
    {
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private ComboBox roleComboBox;

        public LoginForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Game Rental Login";
            this.Size = new Size(420, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 20);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label titleLabel = new Label()
            {
                Text = "Welcome",
                ForeColor = Color.FromArgb(0, 200, 100),
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = true
            };
            Size textSize = TextRenderer.MeasureText(titleLabel.Text, titleLabel.Font);
            titleLabel.Location = new Point((this.ClientSize.Width - textSize.Width) / 2, 30);
            this.Controls.Add(titleLabel);

            int spacing = 60;
            int startY = 100;

            usernameTextBox = CreateInputField("Username", startY, false);
            passwordTextBox = CreateInputField("Password", startY + spacing, true);

            Label roleLabel = new Label()
            {
                Text = "Role:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, startY + spacing * 2),
                AutoSize = true
            };
            this.Controls.Add(roleLabel);

            roleComboBox = new ComboBox()
            {
                Location = new Point(50, startY + spacing * 2 + 25),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            roleComboBox.Items.AddRange(new string[] { "User", "Admin" });
            roleComboBox.SelectedIndex = 0;
            this.Controls.Add(roleComboBox);

            // Login Button
            loginButton = new Button()
            {
                Text = "Login",
                Location = new Point((this.ClientSize.Width - 160) / 2, startY + spacing * 3 + 30),
                Size = new Size(160, 40),
                BackColor = Color.FromArgb(0, 200, 100),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 180, 90);
            loginButton.Click += LoginButton_Click;
            this.Controls.Add(loginButton);
        }

        private TextBox CreateInputField(string placeholder, int top, bool isPassword)
        {
            Label label = new Label()
            {
                Text = placeholder + ":",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, top),
                AutoSize = true
            };
            this.Controls.Add(label);

            TextBox textBox = new TextBox()
            {
                Text = placeholder,
                Tag = placeholder,
                Location = new Point(50, top + 25),
                Width = 300,
                ForeColor = Color.Gray,
                BackColor = Color.FromArgb(30, 30, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 12),
                UseSystemPasswordChar = false
            };

            bool userStartedTyping = false;

            textBox.KeyPress += (s, e) =>
            {
                if (!userStartedTyping)
                {
                    userStartedTyping = true;
                    if (textBox.Text == (string)textBox.Tag)
                    {
                        textBox.Text = "";
                        textBox.ForeColor = Color.White;
                        if (isPassword) textBox.UseSystemPasswordChar = true;
                    }
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    userStartedTyping = false;
                    textBox.Text = (string)textBox.Tag;
                    textBox.ForeColor = Color.Gray;
                    if (isPassword) textBox.UseSystemPasswordChar = false;
                }
            };

            this.Controls.Add(textBox);
            return textBox;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text == (string)usernameTextBox.Tag ? "" : usernameTextBox.Text;
            string password = passwordTextBox.Text == (string)passwordTextBox.Tag ? "" : passwordTextBox.Text;
            string role = roleComboBox.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (role == "Admin")
                    {
                        string adminQuery = "SELECT COUNT(*) FROM [ADMIN] WHERE ADMIN_NAME = @username AND PASSWORD = @password";
                        using (SqlCommand cmd = new SqlCommand(adminQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);

                            int adminCount = (int)cmd.ExecuteScalar();
                            if (adminCount > 0)
                            {
                                Form4 adminDashboard = new Form4();
                                adminDashboard.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }
                    // Then try to authenticate as User (or if Admin login failed)
                    string userQuery = "SELECT COUNT(*) FROM [USER] WHERE USER_NAME = @username AND PASSWORD = @password";
                    using (SqlCommand cmd = new SqlCommand(userQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int userCount = (int)cmd.ExecuteScalar();
                        if (userCount > 0)
                        {
                            Form5 userDashboard = new Form5(username);
                            userDashboard.Show();
                            this.Hide();
                            return;
                        }
                    }

                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        { }
    }
}
