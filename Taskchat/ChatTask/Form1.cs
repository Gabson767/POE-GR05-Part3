using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;
using System.Drawing.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace ChatTask
{
    public partial class Form1 : Form
    {

        String connectionString = @"Server= (localdb)\MSSQLLocalDB; Database= Taskchat; Trusted_Connection=True;";
        String step = "";
        String pendingTitle = "";
        String pendingDescription = "";
        String pendingReminder = "";

        private string currentTopic = "";
        private string userName = "";
        private string favoriteTopic = "";
        private List<string> activityLog = new List<string>();

        private List<Quiz> quizzes = new List<Quiz>();
        private Quiz currentQuiz = null;
        private Random random = new Random();

        private int quizScore = 0;
        private int quizQuestionAsked = 0;
        private const int MaxQuestions = 5
        ;


        public Form1()
        {

           

            InitializeComponent();

            Sound wav = new Sound();
            wav.PlaySound();

            this.WindowState = FormWindowState.Maximized;



            quizzes.Add(new Quiz(
         "What does 2FA stand for?",
         "two-factor authentication"));

            quizzes.Add(new Quiz(
                "What type of cyber attack tricks people into revealing passwords?",
                "phishing"));

            quizzes.Add(new Quiz(
                "What software helps protect a computer from viruses?",
                "antivirus"));

            quizzes.Add(new Quiz(
                "Should you reuse the same password for every website? (yes/no)",
                "no"));

            quizzes.Add(new Quiz(
                "What should you regularly review on your social media accounts?",
                "privacy settings"));

            quizzes.Add(new Quiz(
                "True or False: Public Wi-Fi is always safe.",
                "false"));

            quizzes.Add(new Quiz(
                "What is the strongest type of authentication?",
                "two-factor authentication"));

            quizzes.Add(new Quiz(
                "What should you do before clicking a suspicious email link?",
                "verify the sender"));

            Send.UseVisualStyleBackColor = true;

           

            

           


            ASCII(@"

   _____      _                                        _ _                                                             
  / ____|    | |                                      (_) |             /\                                             
 | |    _   _| |__   ___ _ __ ___  ___  ___ _   _ _ __ _| |_ _   _     /  \__      ____ _ _ __ ___ _ __   ___  ___ ___ 
 | |   | | | | '_ \ / _ \ '__/ __|/ _ \/ __| | | | '__| | __| | | |   / /\ \ \ /\ / / _` | '__/ _ \ '_ \ / _ \/ __/ __|
 | |___| |_| | |_) |  __/ |  \__ \  __/ (__| |_| | |  | | |_| |_| |  / ____ \ V  V / (_| | | |  __/ | | |  __/\__ \__ \
  \_____\__, |_.__/ \___|_|  |___/\___|\___|\__,_|_|  |_|\__|\__, | /_/    \_\_/\_/ \__,_|_|  \___|_| |_|\___||___/___/
         __/ |                                                __/ |                                                    
        |___/                                                |___/                                                     

");

            Bot("Welcome to chatbot");
            Bot("Command: ");
            Bot("Add task");
            Bot("Show task");
            Bot("Reminder me to update password");
            Bot("Delete 1 ");
            Bot("Done ");
            Bot("Quiz");
            Bot("Show activity log");

            Bot(
                "Welcome to the Cybersecurity Awareness Bot! Type 'exit' to quit."
            );

            Bot(
                "Please Type your Name: "
            );

           
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please type something");
                return;
            }

            addUserMessage(message);

            textBox1.Clear();
            textBox1.Focus();

            if (!ProcessMessage(message))
            {
                HandleConversation(message);
            }
        }



        private bool ProcessMessage(string message)

        {


            string msg = message.ToLower();

            //---------------------------------
            // CHECK QUIZ ANSWER
            //---------------------------------

            if (step == "quiz")
            {
                if (message.Trim().Equals(currentQuiz.Answer, StringComparison.OrdinalIgnoreCase))
                {
                    Bot("✅ Correct!");
                    quizScore++;
                }
                else
                {
                    Bot("❌ Incorrect.");
                    Bot("Correct answer: " + currentQuiz.Answer);
                }

                quizQuestionAsked++;

                if (quizQuestionAsked >= MaxQuestions)
                {
                    Bot($"Quiz finished! Your score is {quizScore}/{MaxQuestions}");

                    step = "";
                    currentQuiz = null;
                    return true;
                }

                currentQuiz = quizzes[random.Next(quizzes.Count)];
                Bot($"Question {quizQuestionAsked + 1}:");
                Bot(currentQuiz.Question);

                return true;
            }

            if (IsDeleteRequest(message))
            {
                Bot("Deleting task");
                int id = ExtractNumber(message);
                if (id == 0)
                {
                    Bot("Please type task number. e.g: delete");
                    return true;
                }
                DeleteTask(id);
                AddActivity("Deleted task #" + id);

                return true;

            }
            if (IsCompleteRequest(message))
            {
                Bot("deleted");
                int id = ExtractNumber(message);
                if (id == 0)
                {
                    Bot("Please type task number. e.g: done 1");
                    return true;
                }
                CompleteTask(id);
                AddActivity("Completed task #" + id);
                return true;
            }

            if (step == "description")
            {
                pendingDescription = message;

                if (pendingDescription.ToLower() == "none")
                    pendingDescription = "";

                step = "reminder";
                Bot("Enter reminder or type NONE.");

                return true;
            }
            if (step == "reminder")
            {
                pendingReminder = message;

                if (pendingReminder.ToLower() == "none")
                    pendingReminder = "";

                SaveTask(pendingTitle, pendingDescription, pendingReminder);
                AddActivity("Task added: " + pendingTitle);
                ClearPendingTask();
                return true;
            }

            if (msg.Contains("show") || msg.Contains("display") || msg.Contains("list") || msg.Contains("view"))
            {
                Loadtasks();

                Bot("here are ur tasks");
                AddActivity("Viewed task list");
                return true;
            }

            if (msg.Contains("activity") ||
            msg.Contains("history") ||
            msg.Contains("what have you done"))
            {
                ShowActivityLog();
                return true;
            }



            if (msg.Contains("quiz") ||
     msg.Contains("question") ||
     msg.Contains("test"))
            {
                quizScore = 0;
                quizQuestionAsked = 0;
                currentQuiz = quizzes[random.Next(quizzes.Count)];

                step = "quiz";

                Bot("Cybersecurity Quiz");
                Bot(currentQuiz.Question);

                AddActivity("Started quiz");

                return true;
            }

            if (msg.Contains("password"))
            {
                Bot("Tip: Use strong passwords with letters, numbers and symbols.");

                AddActivity("Discussed password security");
                return true;
            }

            if (msg.Contains("phishing"))
            {
                Bot("Never click suspicious email links.");

                AddActivity("Discussed phishing");
                return true;
            }

            //---------------------------------
            // PRIVACY
            //---------------------------------

            if (msg.Contains("privacy"))
            {
                Bot("Review your privacy settings regularly.");

                AddActivity("Discussed privacy");
                return true;
            }
            string title = "";
            string description = "";
            string reminder = "";
            if (IsTaskRequest(message))
            {
                step = "title";
                Bot("Enter the task title : ");
                return true;
            }
            if (step == "title")
            {

                pendingTitle = message;
                step = "description";
                Bot("Enter the description (or type NONE) : ");

                return true;
            }
            if (step == "description")
            {
                pendingDescription = message;
                if(pendingDescription.Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    pendingDescription = "";
                }
                step = "reminder";
                Bot("Enter the reminder (or type NONE) : ");

                return true;
            }
            if (step == "reminder")
            {
                pendingReminder = message;

                if (pendingReminder.Equals("none", StringComparison.OrdinalIgnoreCase))
                {
                    pendingReminder = "";
                }
                SaveTask(pendingTitle, pendingDescription, pendingReminder);
                AddActivity("Task added: " + pendingTitle);
                Bot("Task saved succesfully!");
                ClearPendingTask();
                return true;
            }
            return false;
        }

        private void HandleConversation(string message)
        {
            string msg = message.ToLower();



            // Save user name
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = message;
                Bot("Hello " + userName + "!");
                return;
            }

            //curent topics
            if (msg.Contains("password"))
                currentTopic = "password";
            else if (msg.Contains("privacy"))
                currentTopic = "privacy";
            else if (msg.Contains("phishing"))
                currentTopic = "phishing";
            else if (msg.Contains("malware"))
                currentTopic = "malware";
            else if (msg.Contains("2fa"))
                currentTopic = "2fa";

            // Favourite topic
            if (msg.Contains("interested in"))
            {
                favoriteTopic = message.Replace("interested in", "").Trim();

                Bot("Great! I'll remember that you're interested in " +
                    favoriteTopic + ".");
                return;
            }
            // Tell me more
            if (msg.Contains("tell me more") || msg.Contains("another tip"))
            {
                if (currentTopic == "password")
                    Bot("Another password tip is to never reuse passwords.");

                else if (currentTopic == "privacy")
                    Bot("Another privacy tip is to avoid oversharing online.");

                else if (currentTopic == "phishing")
                    Bot("Always verify suspicious email senders.");



                else if (currentTopic == "malware")
                    Bot("Always keep your antivirus updated and avoid downloading files from unknown websites.");

                else if (currentTopic == "2fa")
                    Bot("Use an authenticator app whenever possible because it is more secure than SMS.");

                else
                    Bot("Please ask about password, phishing or privacy first.");

                return;
            }
            // What do I like?
            if (msg.Contains("what do i like"))
            {
                if (string.IsNullOrWhiteSpace(favoriteTopic))
                    Bot("You haven't told me your favourite cybersecurity topic yet.");
                else
                    Bot("You told me you like " + favoriteTopic + ".");

                return;
            }

            // Exit
            if (msg == "exit")
            {
                Bot("Goodbye! Stay safe online.");
                Application.Exit();
                return;
            }

            string reply = ChatMessage.BotReply(message);
            Bot(reply);

        }




        private bool ContainsKeyword(string message, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (message.Contains(keyword.ToLower()))
                    return true;
            }

            return false;
        }
        private bool IsDeleteRequest(string message)
                {
            return Regex.IsMatch(
                message, @"\b(delete|remove|erase|clear|cancel)\b", RegexOptions.IgnoreCase
                );
                }
        private bool IsTaskRequest(string message)
        {
            return Regex.IsMatch(
     message,
     @"\b(add|create|new|make)\b.*\btask\b",
     RegexOptions.IgnoreCase);
        }
        private bool IsCompleteRequest(string message)
        {
            return Regex.IsMatch(
                message, @"\b(done|finish|complete|completed)\b", RegexOptions.IgnoreCase);
        }
        
        private int ExtractNumber(string message)
        {
            Match match = Regex.Match(message, @"\d+");
            if (match.Success)
            {
                return int.Parse(match.Value);

            }
            return 0;
        }
        private string ExtractTask(string message)
        {
            string task = message.Trim();

            // Remove the words "add", "create", "new", "make", and "task"
            task = Regex.Replace(task,
                @"\b(add|create|new|make|task)\b",
                "",
                RegexOptions.IgnoreCase);

            // Remove everything after "description"
            task = Regex.Replace(task,
                @"\bdescription\b.*",
                "",
                RegexOptions.IgnoreCase);

            // Remove everything after "reminder"
            task = Regex.Replace(task,
                @"\breminder\b.*",
                "",
                RegexOptions.IgnoreCase);

            // Remove punctuation at the end
            task = Regex.Replace(task, @"[?.!]+$", "");

            return task.Trim();
        }

        private string ExtractDescription(string message)
        {

            Match match = Regex.Match(message, @"\bdescription\b\s*[:\-]?\s*(.*?)(\breminder\b|$)", RegexOptions.IgnoreCase);
            if(match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            return "";
        }

        private string ExtractReminder(string message)
        {
            Match match = Regex.Match(message, @"\breminder\b\s*[:\-]?\s*(.*)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
            return "";
        }

        private void SaveTask(string title, string description, string reminder)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO dbo.[Task] (Title, Description, Reminder, IsCompleted)
                                    VALUES (@Title, @Description, @Reminder, 0)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", SqlDbType.NVarChar).Value = title;
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Reminder", reminder);
                    cmd.ExecuteNonQuery();
                }
                Bot("Task saved: " + title);
                Bot("description: " + description);
                Bot("Reminder: " + reminder);
                Loadtasks();
            }
            catch (Exception ex)
            {
                Bot("Error: task not saved");
                MessageBox.Show(ex.Message);
            }
            
        }
        private void CompleteTask(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE dbo.Task SET IsCompleted=1 WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Bot(rows > 0 ? "Task completed" : $"Task {id} does not exist");
                }
                Loadtasks();
            }
            catch
            {
                Bot("Error: task not Completed");
            }
        }
        private void DeleteTask(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM dbo.Task WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = cmd.ExecuteNonQuery();
                    Bot(rows > 0 ? "Task deleted." : $"Task {id} does not exist.");
                }
                Loadtasks();
            }
            catch
            {
                Bot("Error: could not delete task");
                //Message
            }
        }
        private void Loadtasks()
        {
            
            ListTask.Items.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Title, Description, Reminder, IsCompleted FROM dbo.[Task] ORDER by Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string status = (bool)reader["IsCompleted"] ? "✓ Completed" : "Pending";
                      
                        ListTask.Items.Add(
                            $"{reader["Id"]}. " +
                            $"{reader["Title"]}. | " +
                            $"{reader["Description"]}. | " +
                             $"{reader["Reminder"]} |" +
                             $"{status}"
                            );

                    }
                    if (ListTask.Items.Count == 0)
                    {
                        ListTask.Items.Add("no tasks saved yet" );
                    }

                }
            }
            catch (Exception ex)
            {
                Bot("Error: failed to load tasks");
                MessageBox.Show(ex.Message);
            }
        }

        private void AddActivity(string action)
        {
            activityLog.Add(
                $"{DateTime.Now:g} - {action}");

            if (activityLog.Count > 10)
            {
                activityLog.RemoveAt(0);
            }
        }

        private void ShowActivityLog()
        {
            Bot("Recent Activity:");

            if (activityLog.Count == 0)
            {
                Bot("No activity recorded.");
                return;
            }

            foreach (string item in activityLog)
            {
                Bot(item);
            }
        }

        private void ClearPendingTask()
        {
            step = "";
            pendingTitle = "";
            pendingDescription = "";
            pendingReminder = "";
        }

      

        private void addUserMessage(string message)
        {
            Listchat.Items.Add("You: " + message);
        }

        private void Bot(string text)
        {
            Listchat.Items.Add("ChatBot: " + text);
        }

        private void ASCII(string text)
        {
            
            {
                Listchat.Items.Add(text);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Listchat.Items.Clear();
            ListTask.Items.Clear();

            Bot("Chat Cleared. ");
        }

    }
}

