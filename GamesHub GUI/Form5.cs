using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GamesHub
{
    public partial class Form5 : Form
    {
        private SqlConnection connection;
        private string connectionString = ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString;
        private string currentUsername;
        private int currentUserId;

        public Form5(string username)
        {
            InitializeComponent();
            currentUsername = username;
            currentUserId = GetUserIdByUsername(username);
            InitializeDatabaseConnection();

            foreach (Control control in this.Controls)
            {
                if (control is GroupBox groupBox)
                {
                    groupBox.BackColor = Color.Green;
                }
            }

            var btnClearRentals = new Button()
            {
                Name = "btnClearRentals",
                Text = "Clear User Rentals",
                Size = new Size(140, 35),
                Location = new Point(rentaldatagrid.Location.X + rentaldatagrid.Width - 400,
                                     rentaldatagrid.Location.Y - 40),
                BackColor = Color.Green,
                ForeColor = Color.Black
            };
            btnClearRentals.Click += BtnClearRentals_Click;
            this.Controls.Add(btnClearRentals);

            var btnUpdatePassword = new Button()
            {
                Name = "btnUpdatePassword",
                Text = "Update Password",
                Size = new Size(140, 35),
                Location = new Point(btnClearRentals.Location.X - 150, btnClearRentals.Location.Y),
                BackColor = Color.Green,
                ForeColor = Color.Black
            };
            btnUpdatePassword.Click += BtnUpdatePassword_Click;
            this.Controls.Add(btnUpdatePassword);

            LoadGames();
            LoadUserRentals();
        }

        private void BtnUpdatePassword_Click(object sender, EventArgs e)
        {
            using (var updatePassForm = new UpdatePasswordForm())
            {
                if (updatePassForm.ShowDialog() == DialogResult.OK)
                {
                    if (updatePassForm.NewPassword != updatePassForm.ConfirmPassword)
                    {
                        MessageBox.Show("Passwords do not match!");
                        return;
                    }

                    try
                    {
                        connection.Open();
                        var cmd = new SqlCommand(
                            "UPDATE [USER] SET PASSWORD = @password WHERE UID = @userId",
                            connection);
                        cmd.Parameters.AddWithValue("@password", updatePassForm.NewPassword);
                        cmd.Parameters.AddWithValue("@userId", currentUserId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to update password.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating password: {ex.Message}");
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        private int GetUserIdByUsername(string username)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT UID FROM [USER] WHERE USER_NAME = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting user ID: {ex.Message}");
                return -1;
            }
        }

        private void InitializeDatabaseConnection()
        {
            connection = new SqlConnection(connectionString);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            ConfigureDataGrids();
        }

        private void ConfigureDataGrids()
        {
            gamedataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gamedataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            rentaldatagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            rentaldatagrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadGames()
        {
            try
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "SELECT GID AS 'Game ID', GAME_NAME AS 'Game Name', PRICE, RELEASE_DATE AS 'Release Date', DESCRIPTION " +
                    "FROM GAME", connection);
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                gamedataGrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading games: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadUserRentals()
        {
            try
            {
                connection.Open();
                var cmd = new SqlCommand(
                    "SELECT R.GID AS 'Game ID', G.GAME_NAME AS 'Game Name', " +
                    "R.RENT_DATE AS 'Rent Date', R.RETURN_DATE AS 'Return Date' " +
                    "FROM RENTALS R " +
                    "JOIN GAME G ON R.GID = G.GID " +
                    "WHERE R.UID = @userId", connection);
                cmd.Parameters.AddWithValue("@userId", currentUserId);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                rentaldatagrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rentals: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnrentgame_Click(object sender, EventArgs e)
        {
            using (var rentalForm = new RentalForm(currentUsername))
            {
                if (rentalForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        connection.Open();
                        var transaction = connection.BeginTransaction();

                        var cmd = new SqlCommand(
                            "INSERT INTO RENTALS (UID, GID, RENT_DATE, RETURN_DATE) " +
                            "VALUES (@uid, @gid, @rentDate, @returnDate)",
                            connection, transaction);
                        cmd.Parameters.AddWithValue("@uid", currentUserId);
                        cmd.Parameters.AddWithValue("@gid", rentalForm.GameID);
                        cmd.Parameters.AddWithValue("@rentDate", rentalForm.RentDate);
                        cmd.Parameters.AddWithValue("@returnDate", rentalForm.ReturnDate);
                        cmd.ExecuteNonQuery();

                        var paymentCmd = new SqlCommand(
                            "INSERT INTO PAYMENT (UID, GID, RENT_DATE, PAYMENT_DATE, AMOUNT) " +
                            "VALUES (@uid, @gid, @rentDate, @paymentDate, @amount)",
                            connection, transaction);
                        paymentCmd.Parameters.AddWithValue("@uid", currentUserId);
                        paymentCmd.Parameters.AddWithValue("@gid", rentalForm.GameID);
                        paymentCmd.Parameters.AddWithValue("@rentDate", rentalForm.RentDate);
                        paymentCmd.Parameters.AddWithValue("@paymentDate", DateTime.Now);
                        paymentCmd.Parameters.AddWithValue("@amount", rentalForm.CalculateAmount());
                        paymentCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error renting game: {ex.Message}");
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }

                    LoadUserRentals();
                    rentaldatagrid.Refresh();
                }
            }
        }

        private void BtnClearRentals_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                var delPayment = new SqlCommand(
                    "DELETE FROM PAYMENT WHERE UID = @uid",
                    connection, transaction);
                delPayment.Parameters.AddWithValue("@uid", currentUserId);
                delPayment.ExecuteNonQuery();

                var delRental = new SqlCommand(
                    "DELETE FROM RENTALS WHERE UID = @uid",
                    connection, transaction);
                delRental.Parameters.AddWithValue("@uid", currentUserId);
                delRental.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing rentals: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            LoadUserRentals();
            rentaldatagrid.Refresh();
        }

        private void btnviewgame_Click(object sender, EventArgs e) => LoadGames();
        private void btnviewrentals_Click(object sender, EventArgs e)
        {
            LoadUserRentals();
            rentaldatagrid.Refresh();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private class UpdatePasswordForm : Form
        {
            public string NewPassword { get; private set; }
            public string ConfirmPassword { get; private set; }
            private TextBox txtNewPassword;
            private TextBox txtConfirmPassword;

            public UpdatePasswordForm()
            {
                InitializeComponents();
            }

            private void InitializeComponents()
            {
                Text = "Update Password";
                Size = new Size(300, 170);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;

                var mainTable = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 3,
                    Padding = new Padding(10),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };
                mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

                mainTable.Controls.Add(new Label { Text = "New Password:", Anchor = AnchorStyles.Left }, 0, 0);
                txtNewPassword = new TextBox { UseSystemPasswordChar = true };
                mainTable.Controls.Add(txtNewPassword, 1, 0);

                mainTable.Controls.Add(new Label { Text = "Confirm Password:", Anchor = AnchorStyles.Left }, 0, 1);
                txtConfirmPassword = new TextBox { UseSystemPasswordChar = true };
                mainTable.Controls.Add(txtConfirmPassword, 1, 1);

                var buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 40 };
                var btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Size = new Size(75, 30),
                    Location = new Point(50, 5),
                    BackColor = Color.Green
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(75, 30),
                    Location = new Point(150, 5),
                    BackColor = Color.Green
                };
                buttonPanel.Controls.Add(btnOK);
                buttonPanel.Controls.Add(btnCancel);

                Controls.Add(mainTable);
                Controls.Add(buttonPanel);

                btnOK.Click += BtnOK_Click;
            }

            private void BtnOK_Click(object sender, EventArgs e)
            {
                NewPassword = txtNewPassword.Text;
                ConfirmPassword = txtConfirmPassword.Text;

                if (string.IsNullOrWhiteSpace(NewPassword))
                {
                    MessageBox.Show("Please enter a new password!");
                    DialogResult = DialogResult.None;
                    return;
                }

                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match!");
                    DialogResult = DialogResult.None;
                }
            }
        }

        private class RentalForm : Form
        {
            public int UserID { get; private set; }
            public int GameID { get; private set; }
            public DateTime RentDate { get; private set; }
            public DateTime ReturnDate { get; private set; }
            private ComboBox cmbGameName;
            private DateTimePicker dtpReturnDate;
            private string username;

            public RentalForm(string username)
            {
                this.username = username;
                InitializeComponents();
            }

            private void InitializeComponents()
            {
                Text = "Rent a Game";
                Size = new Size(300, 170);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;

                var mainTable = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 3,
                    Padding = new Padding(10),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                };
                mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

                mainTable.Controls.Add(new Label { Text = "Game Name:", Anchor = AnchorStyles.Left }, 0, 0);
                cmbGameName = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Left };
                PopulateGameNames();
                mainTable.Controls.Add(cmbGameName, 1, 0);

                mainTable.Controls.Add(new Label { Text = "Return Date:", Anchor = AnchorStyles.Left }, 0, 1);
                dtpReturnDate = new DateTimePicker
                {
                    Anchor = AnchorStyles.Left,
                    Value = DateTime.Now.AddDays(1),
                    MinDate = DateTime.Now.AddHours(1)
                };
                mainTable.Controls.Add(dtpReturnDate, 1, 1);

                var buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 40 };
                var btnOK = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Size = new Size(75, 30),
                    Location = new Point(50, 5),
                    BackColor = Color.Green
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(75, 30),
                    Location = new Point(150, 5),
                    BackColor = Color.Green
                };
                buttonPanel.Controls.Add(btnOK);
                buttonPanel.Controls.Add(btnCancel);

                mainTable.SetRowSpan(buttonPanel, 1);
                Controls.Add(mainTable);
                Controls.Add(buttonPanel);
            }

            private void PopulateGameNames()
            {
                try
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString))
                    {
                        conn.Open();
                        var cmd = new SqlCommand("SELECT GAME_NAME FROM GAME", conn);
                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                                cmbGameName.Items.Add(reader["GAME_NAME"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading games: {ex.Message}");
                }
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                if (DialogResult == DialogResult.OK)
                {
                    UserID = GetUserIdByUsername();
                    if (cmbGameName.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a game!");
                        e.Cancel = true;
                        return;
                    }
                    GameID = GetGameIdByName(cmbGameName.SelectedItem.ToString());
                    RentDate = DateTime.Now;
                    ReturnDate = dtpReturnDate.Value;
                    if (ReturnDate <= RentDate)
                    {
                        MessageBox.Show("Return date must be after current time!");
                        e.Cancel = true;
                    }
                }
                base.OnFormClosing(e);
            }

            private int GetUserIdByUsername()
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GamesHubDB"].ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT UID FROM [USER] WHERE USER_NAME = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            private int GetGameIdByName(string gameName)
            {
                using (var conn = new SqlConnection("Server=localhost;Database=GamesRentalDB;Integrated Security=True;"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT GID FROM GAME WHERE GAME_NAME = @gameName", conn);
                    cmd.Parameters.AddWithValue("@gameName", gameName);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            public decimal CalculateAmount()
            {
                TimeSpan duration = ReturnDate - RentDate;
                return (decimal)duration.TotalDays * GetGameDailyRate(GameID);
            }

            private decimal GetGameDailyRate(int gameId)
            {
                using (var conn = new SqlConnection("Server=localhost;Database=GamesRentalDB;Integrated Security=True;"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT PRICE FROM GAME WHERE GID = @gid", conn);
                    cmd.Parameters.AddWithValue("@gid", gameId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }
    }
}