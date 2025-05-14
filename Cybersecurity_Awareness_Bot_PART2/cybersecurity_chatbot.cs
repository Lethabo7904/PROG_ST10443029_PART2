using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cybersecurity_Awareness_Bot_PART1
{
    public class CybersecurityChatBot
    {
        static string userName = "";
        static string favoriteTopic = "";
        static string rememberedTopic = "";
        static string detectedSentiment = "";

        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> { "Using strong passwords is like locking your door with a deadbolt – it’s basic but crucial.", "You’re doing great! Just remember not to reuse passwords across accounts.", "Password managers are lifesavers – they keep things safe and simple." } },
            { "scam", new List<string> { "Scams can trick anyone – awareness is your best defense.", "Always verify before you trust, especially online.", "You’ve got this! If it feels off, trust your instincts." } },
            { "privacy", new List<string> { "Protecting your privacy is like shielding your diary – it's personal and important.", "You're making smart choices by asking about privacy – keep it up!", "Check those app permissions – small steps make a big impact." } },
            { "phishing", new List<string> { "Watch out for fake links – even seasoned pros fall for phishing.", "You're smart to ask – phishing is sneaky, but knowledge is power.", "Pause before you click. You’re already staying safer by being cautious." } }
        };

        static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            { "worried", "It's completely understandable to feel that way. Scammers can be very convincing. Just remember, staying informed is your best defense." },
            { "curious", "Curiosity is a great attitude! Exploring cybersecurity will definitely make you more secure online." },
            { "frustrated", "Cybersecurity can feel overwhelming at times, but don't worry. Small steps can make a big difference." }
        };

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            DisplayHeader("WELCOME TO THE CYBERSECURITY AWARENESS CHATBOT.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Please enter your name: \n");
            userName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayAiChat($"Hello, {userName}!");
            DisplayAiChat("Feel free to ask me anything about cybersecurity.\nType 'exit' when you're done.");
            Console.ResetColor();

            Dictionary<string, string> responses = new Dictionary<string, string>
            {
                { "how are you?", "I'm running smoothly and ready to chat! How about you?" },
                { "what is your purpose?", "I’m here to help you become more cyber aware – let’s learn together!" },
                { "what can i ask you about?", "Anything related to cybersecurity: scams, phishing, passwords, privacy, and more." }
            };

            string[] phishingInfo = {
                "Phishing is a type of cyberattack that uses fraudulent emails, text messages, phone calls, or websites to trick people into sharing sensitive data.",
                "Be cautious of emails that urge quick action or request sensitive data.",
                "You're already ahead by learning to spot phishing attempts!"
            };

            string[] passwordSafetyInfo = {
                "Strong passwords use 12+ characters with a mix of types.",
                "Never use the same password twice – even if it’s strong.",
                "Password managers are your friends – they simplify strong security!"
            };

            string[] malwareInfo = {
                "Malware includes viruses, trojans, spyware, and more.",
                "Keep your software updated – patches help block malware.",
                "You’re being proactive just by learning about malware. Great job!"
            };

            string userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{userName}: ");
                userInput = Console.ReadLine().ToLower();

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayAiChat("Goodbye. Stay cyber smart, and take care!");
                    break;
                }

                string cleanedInput = RemoveIgnoredWords(userInput);

                if (DetectAndRespondToSentiment(cleanedInput)) continue;

                CheckAndRememberTopic(cleanedInput);

                if (responses.TryGetValue(cleanedInput, out string response))
                {
                    DisplayAiChat(response);
                }
                else if (cleanedInput.Contains("phishing"))
                {
                    DisplayAiChat("Here's some insight about phishing:");
                    foreach (string info in phishingInfo)
                    {
                        Console.WriteLine($"- {info}");
                    }
                }
                else if (cleanedInput.Contains("password safety"))
                {
                    DisplayAiChat("Here's how you can strengthen your password security:");
                    foreach (string info in passwordSafetyInfo)
                    {
                        Console.WriteLine($"- {info}");
                    }
                }
                else if (cleanedInput.Contains("malware"))
                {
                    DisplayAiChat("Here's some key malware info:");
                    foreach (string info in malwareInfo)
                    {
                        Console.WriteLine($"- {info}");
                    }
                }
                else
                {
                    bool keywordFound = false;
                    foreach (var keyword in keywordResponses.Keys)
                    {
                        if (cleanedInput.Contains(keyword))
                        {
                            RespondToKeyword(keyword);
                            keywordFound = true;
                            break;
                        }
                    }

                    if (!keywordFound)
                    {
                        HandleUnknownInput(cleanedInput);
                    }
                }

                Console.ResetColor();
            } while (true);
        }

        private static void CheckAndRememberTopic(string cleanedInput)
        {
            foreach (var keyword in keywordResponses.Keys)
            {
                if (cleanedInput.Contains(keyword))
                {
                    rememberedTopic = keyword;
                    DisplayAiChat($"I noticed you're interested in {keyword}. I'll remember that!");
                    break;
                }
            }
        }

        private static void DisplayHeader(string header)
        {
            string border = new string('=', 80);
            Console.WriteLine(border);
            Console.WriteLine(header.PadLeft((border.Length + header.Length) / 2));
            Console.WriteLine(border);
        }

        private static void DisplayAiChat(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ChatBot: ");
            Console.ResetColor();
            TypingEffect(message);
        }

        private static void TypingEffect(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private static string RemoveIgnoredWords(string input)
        {
            string[] ignoredWords = { "please", "can", "you", "tell", "me", "about", "do", "know", "explain", "the" };
            foreach (string word in ignoredWords)
            {
                input = Regex.Replace(input, $@"\b{Regex.Escape(word)}\b", "", RegexOptions.IgnoreCase);
            }
            return input.Trim();
        }

        private static void RespondToKeyword(string keyword)
        {
            Random rand = new Random();
            var responses = keywordResponses[keyword];
            string randomResponse = responses[rand.Next(responses.Count)];
            Console.WriteLine($"\nChatBot ({keyword} tip): {randomResponse}");
        }

        private static void HandleUnknownInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nChatBot: Hmm, I’m not sure how to answer that yet. But you’re doing great just by asking!");

            if (!string.IsNullOrEmpty(rememberedTopic) && keywordResponses.ContainsKey(rememberedTopic.ToLower()))
            {
                Console.WriteLine($"Since you mentioned you're interested in {rememberedTopic}, here's a tip:");
                RespondToKeyword(rememberedTopic.ToLower());
            }
            else
            {
                Console.WriteLine("Try asking about areas like phishing, privacy, or password safety.");
            }
        }

        private static bool DetectAndRespondToSentiment(string input)
        {
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    detectedSentiment = sentiment;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    DisplayAiChat($"I can sense you're feeling {sentiment}. {sentimentResponses[sentiment]}");
                    Console.ResetColor();
                    return true;
                }
            }
            return false;
        }
    }
}
