using GameRentalSystem;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GamesHub
{
    public partial class Form2 : Form
    {
        private TextBox nameTextBox;
        private TextBox usernameTextBox;
        private TextBox emailTextBox;
        private TextBox phoneTextBox;
        private TextBox passwordTextBox;
        private TextBox confirmPasswordTextBox;
        private ComboBox roleComboBox;

        public Form2()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "GamesHub - Sign Up";
            this.Size = new Size(420, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label titleLabel = new Label()
            {
                Text = "Join Us",
                ForeColor = Color.FromArgb(76, 175, 80),
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true
            };
            Size textSize = TextRenderer.MeasureText(titleLabel.Text, titleLabel.Font);
            titleLabel.Location = new Point((this.ClientSize.Width - textSize.Width) / 2, 25);
            this.Controls.Add(titleLabel);

            int startY = 90;
            int spacing = 70;

            nameTextBox = CreateLabelAndTextbox("Name:", startY, false, "Enter your name");
            usernameTextBox = CreateLabelAndTextbox("Username:", startY + spacing, false, "Enter username");
            emailTextBox = CreateLabelAndTextbox("Email:", startY + spacing * 2, false, "Enter email");
            phoneTextBox = CreateLabelAndTextbox("Phone:", startY + spacing * 3, false, "Enter phone");
            passwordTextBox = CreateLabelAndTextbox("Password:", startY + spacing * 4, true, "Enter password");
            confirmPasswordTextBox = CreateLabelAndTextbox("Confirm Password:", startY + spacing * 5, true, "Confirm password");
            roleComboBox = CreateRoleComboBox("Role:", startY + spacing * 6);

            Button signUpButton = new Button()
            {
                Text = "Sign Up",
                Location = new Point((this.ClientSize.Width - 160) / 2, startY + spacing * 7 + 20),
                Size = new Size(160, 45),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            signUpButton.FlatAppearance.BorderSize = 0;
            signUpButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(56, 155, 60);
            signUpButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(36, 135, 40);
            signUpButton.Click += SignUpButton_Click;

            this.Controls.Add(signUpButton);
        }

        private TextBox CreateLabelAndTextbox(string labelText, int top, bool isPassword, string placeholder)
        {
            Label label = new Label()
            {
                Text = labelText,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, top),
                AutoSize = true
            };

            TextBox textBox = new TextBox()
            {
                Location = new Point(50, top + 25),
                Width = 300,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                BackColor = Color.FromArgb(30, 30, 30),
                BorderStyle = BorderStyle.FixedSingle,
                Text = placeholder,
                Tag = placeholder,
                UseSystemPasswordChar = false
            };

            textBox.KeyPress += (sender, e) =>
            {
                if (textBox.Text == (string)textBox.Tag)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.White;
                    if (isPassword) textBox.UseSystemPasswordChar = true;
                }
            };

            textBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = (string)textBox.Tag;
                    textBox.ForeColor = Color.Gray;
                    if (isPassword) textBox.UseSystemPasswordChar = false;
                }
            };

            this.Controls.Add(label);
            this.Controls.Add(textBox);
            return textBox;
        }

        private ComboBox CreateRoleComboBox(string labelText, int top)
        {
            Label label = new Label()
            {
                Text = labelText,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, top),
                AutoSize = true
            };

            ComboBox comboBox = new ComboBox()
            {
                Location = new Point(50, top + 25),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 12),
                BackColor = Color.White
            };

            comboBox.Items.Add("Client");
            comboBox.Items.Add("Admin");
            comboBox.SelectedIndex = 0;

            this.Controls.Add(label);
            this.Controls.Add(comboBox);
            return comboBox;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text == (string)nameTextBox.Tag ? "" : nameTextBox.Text;
            string username = usernameTextBox.Text == (string)usernameTextBox.Tag ? "" : usernameTextBox.Text;
            string email = emailTextBox.Text == (string)emailTextBox.Tag ? "" : emailTextBox.Text;
            string phone = phoneTextBox.Text == (string)phoneTextBox.Tag ? "" : phoneTextBox.Text;
            string password = passwordTextBox.Text == (string)passwordTextBox.Tag ? "" : passwordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text == (string)confirmPasswordTextBox.Tag ? "" : confirmPasswordTextBox.Text;
            string role = roleComboBox.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || 
                    string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < phoneTextBox.Text.Length; ++i)
            {
                if (phoneTextBox.Text[i] < '0' || phoneTextBox.Text[i] > '9')
                {
                    MessageBox.Show("Phone number consists of digits only.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString;
                string tableName = role == "Admin" ? "[ADMIN]" : "[USER]";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE " +
                                      (role == "Admin" ? "ADMIN_NAME" : "USER_NAME") + " = @Username";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", role == "Admin" ? name : username);
                        int userExists = (int)checkCmd.ExecuteScalar();

                        if (userExists > 0)
                        {
                            MessageBox.Show(role == "Admin" ? "Admin name already exists." : "Username already exists.",
                                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertQuery;
                    if (role == "Admin")
                    {
                        insertQuery = $"INSERT INTO {tableName} (ADMIN_NAME, USER_EMAIL, USER_PHONE, PASSWORD) " +
                                     $"VALUES (@Name, @Email, @UserPhone, @Password)";
                    }
                    else
                    {
                        insertQuery = $"INSERT INTO {tableName} (USER_NAME, USER_EMAIL, USER_PHONE, JOIN_DATE, PASSWORD) " +
                                     $"VALUES (@Username, @Email, @UserPhone, @JoinDate, @Password)";
                    }

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@UserPhone", phone); 
                        cmd.Parameters.AddWithValue("@Password", password); 

                        if (role == "Admin")
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@JoinDate", DateTime.Now);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            LoginForm login = new LoginForm();
                            login.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to create account.", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e) { }
    }
}
