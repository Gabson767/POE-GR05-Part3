using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatTask
{
    public class ChatMessage
    {


        static Random random = new Random();

        // Cybersecurity topics randoms responses
        static Dictionary<string, List<string>> responses =
            new Dictionary<string, List<string>>()
        {
            {
                "password",
                new List<string>()
                {
                    "Use strong passwords with symbols and numbers.",
                    "Avoid using your birthday in passwords.",
                    "Use a unique password for every account."
                }
                },
                
                    {
    "malware",
    new List<string>()
    {
        "Install antivirus software and keep it updated.",
        "Do not download files from unknown websites.",
        "Keep Windows updated to protect against malware."
    }
},
                {
    "2fa",
    new List<string>()
    {
        "Enable two-factor authentication wherever possible.",
        "2FA provides an extra layer of security.",
        "Authenticator apps are safer than SMS verification."
    }
},

            {
                "privacy",
                new List<string>()
                {
                    "Review your privacy settings regularly.",
                    "Do not share personal information publicly.",
                    "Avoid using public Wi-Fi for sensitive activities."
                }
            },

            {
                "phishing",
                new List<string>()
                {
                    "Never click suspicious email links.",
                    "Check email addresses carefully.",
                    "Phishing messages often create urgency."
                }
            },

            {
                "scam",
                new List<string>()
                {
                    "Scammers often pretend to be trusted companies.",
                    "Never send money to unknown people online.",
                    "Be careful of fake investment opportunities."
                }
            }
        };

        public static string BotReply(string msg)
        {
            try
            {
                msg = msg.ToLower().Trim();

                // SENTIMENT DETECTION

                if (msg.Contains("worried"))
                {
                    return "It's understandable to feel worried. Cybersecurity threats are common, but staying informed helps keep you safe.";
                }

                if (msg.Contains("frustrated"))
                {
                    return "I understand your frustration. Let me try simplify cybersecurity tips for you.";
                }

                if (msg.Contains("confused"))
                {
                    return "No problem. I'll explain things in a simpler way.";
                }

                if (msg.Contains("curious"))
                {
                    return "Curiosity is great! Learning cybersecurity helps protect your digital life.";
                }


                // KEYWORD RECOGNITION

                foreach (var item in responses)
                {
                    if (msg.Contains(item.Key))
                    {
                        List<string> replyList = item.Value;

                        return replyList[random.Next(replyList.Count)];
                    }
                }

                // GREETINGS

                if (msg.Contains("how are you"))
                {
                    return "I am doing great, thank you for asking! How can I assist you today?";
                }

                if (msg == "good" ||
    msg == "great" ||
    msg == "fine" ||
    msg.Contains("i am good") ||
    msg.Contains("i am fine"))

                {
                    return "That's wonderful to hear! How can I assist you with cybersecurity today?";
                }
                if (msg.Contains("hello") ||
     msg.Contains("hi") ||
     msg.Contains("hey"))

                {
                    return "Hey! How can I assist you today?";
                }
                if (msg.Contains("thank"))
                {
                    return "You're welcome! Stay safe online.";
                }
                if (msg.Contains("bye"))
                {
                    return "Goodbye! Stay cyber safe.";
                }
                // DEFAULT RESPONSE

                return "I can help with passwords, phishing, scams, privacy, malware, or 2FA. You can also manage tasks by typing commands like 'Add task', 'Show tasks', or 'Delete 1'.";
            }

            catch (Exception)
            {
                return "Oops! Something went wrong. Please try again.";
            }
        }

    }
}
