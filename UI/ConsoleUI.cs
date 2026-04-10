namespace CybersecurityChatbot.UI
{
    /// <summary>
    /// Handles all console display, formatting, and user interaction UI.
    /// Provides coloured text, ASCII art, borders, and typing effects.
    /// </summary>
    public static class ConsoleUI
    {
        // ── Colour palette ──────────────────────────────────────────────────
        private static readonly ConsoleColor AccentColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
        private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        private static readonly ConsoleColor BotColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor UserColor = ConsoleColor.White;
        private static readonly ConsoleColor HeaderColor = ConsoleColor.DarkCyan;
        private static readonly ConsoleColor BorderColor = ConsoleColor.DarkGray;

        // ── ASCII art logo ───────────────────────────────────────────────────
        private static readonly string[] Logo =
        {
            @"  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗",
            @" ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝",
            @" ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   ",
            @" ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   ",
            @" ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   ",
            @"  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝   ╚═╝   ",
            @"",
            @"          ┌─────────────────────────────────────────────┐",
            @"          │   🛡️  CYBERSECURITY AWARENESS CHATBOT  🛡️   │",
            @"          │      Protecting South African Citizens       │",
            @"          └─────────────────────────────────────────────┘",
        };

        /// <summary>
        /// Displays the full ASCII art logo with a surrounding border.
        /// </summary>
        public static void DisplayLogo()
        {
            Console.Clear();
            Console.WriteLine();
            foreach (var line in Logo)
            {
                Console.ForegroundColor = AccentColor;
                Console.WriteLine(line);
            }
            Console.ForegroundColor = BorderColor;
            PrintDivider();
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Asks the user for their name and returns it.
        /// Validates that a non-empty name is provided.
        /// </summary>
        public static string GetUserName()
        {
            PrintWithColor("  Welcome to the Cybersecurity Awareness Bot!", AccentColor);
            PrintWithColor("  Brought to you by the Department of Cybersecurity.", ConsoleColor.DarkCyan);
            Console.WriteLine();

            string name = string.Empty;

            while (string.IsNullOrWhiteSpace(name))
            {
                PrintWithColor("  🔹 Please enter your name to get started: ", WarningColor, newLine: false);
                name = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(name))
                {
                    PrintWithColor("  ⚠  Name cannot be empty. Please try again.", ErrorColor);
                }
            }

            Console.WriteLine();
            TypeWrite($"  Hello, {name}! Great to have you here. 👋", SuccessColor);
            TypeWrite("  I'm here to help you stay safe online.", BotColor);
            Console.WriteLine();
            PrintDivider();
            Console.WriteLine();

            return name;
        }

        /// <summary>
        /// Displays the bot's response with consistent formatting.
        /// </summary>
        public static void ShowBotResponse(string response)
        {
            Console.WriteLine();
            Console.ForegroundColor = BorderColor;
            Console.Write("  ╔══ ");
            Console.ForegroundColor = BotColor;
            Console.Write("🤖 CyberBot");
            Console.ForegroundColor = BorderColor;
            Console.WriteLine(" ══════════════════════════════════════╗");
            Console.ResetColor();

            var lines = WrapText(response, 60);
            foreach (var line in lines)
            {
                Console.ForegroundColor = BorderColor;
                Console.Write("  ║  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(line);
            }

            Console.ForegroundColor = BorderColor;
            Console.WriteLine("  ╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the user input prompt with their name.
        /// </summary>
        public static void ShowUserPrompt(string userName)
        {
            Console.ForegroundColor = WarningColor;
            Console.Write($"  [{userName}] ➤ ");
            Console.ForegroundColor = UserColor;
        }

        /// <summary>
        /// Displays available commands and topics.
        /// </summary>
        public static void ShowHelpMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = AccentColor;
            PrintDivider();
            Console.WriteLine("  📋  TOPICS YOU CAN ASK ABOUT:");
            PrintDivider();
            Console.ResetColor();

            var topics = new[]
            {
                ("🔐", "password safety",  "Tips for creating strong passwords"),
                ("🎣", "phishing",         "How to spot and avoid phishing scams"),
                ("🌐", "safe browsing",    "Best practices for browsing securely"),
                ("🦠", "malware",          "What malware is and how to stay safe"),
                ("📧", "email safety",     "How to protect your inbox"),
                ("📱", "social media",     "Staying safe on social platforms"),
            };

            foreach (var (icon, cmd, desc) in topics)
            {
                Console.ForegroundColor = WarningColor;
                Console.Write($"    {icon}  ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{cmd,-20}");
                Console.ForegroundColor = BorderColor;
                Console.WriteLine($"→  {desc}");
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = BorderColor;
            Console.WriteLine("  Type 'help' to see this menu again.");
            Console.WriteLine("  Type 'quit' or 'exit' to leave the chatbot.");
            PrintDivider();
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a farewell message when the user exits.
        /// </summary>
        public static void ShowFarewell(string userName)
        {
            Console.WriteLine();
            PrintDivider();
            TypeWrite($"  Goodbye, {userName}! Stay safe online. 🛡️", SuccessColor);
            TypeWrite("  Remember: cybersecurity starts with YOU.", AccentColor);
            PrintDivider();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays an error message for unrecognised input.
        /// </summary>
        public static void ShowUnknownInput()
        {
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine();
            Console.WriteLine("  ⚠  I didn't quite understand that. Could you rephrase?");
            Console.ForegroundColor = BorderColor;
            Console.WriteLine("     Try asking about: password safety, phishing, safe browsing,");
            Console.WriteLine("     malware, email safety, or social media.");
            Console.WriteLine("     Type 'help' to see the full menu.");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ── Private helpers ──────────────────────────────────────────────────

        /// <summary>Prints coloured text with optional newline.</summary>
        private static void PrintWithColor(string text, ConsoleColor color, bool newLine = true)
        {
            Console.ForegroundColor = color;
            if (newLine) Console.WriteLine(text);
            else Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>Prints text one character at a time for a typing effect.</summary>
        public static void TypeWrite(string text, ConsoleColor color = ConsoleColor.White, int delayMs = 18)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        /// <summary>Prints a full-width divider line.</summary>
        public static void PrintDivider()
        {
            Console.ForegroundColor = BorderColor;
            Console.WriteLine("  " + new string('─', 64));
            Console.ResetColor();
        }

        /// <summary>Word-wraps text to a maximum width.</summary>
        private static List<string> WrapText(string text, int maxWidth)
        {
            var lines = new List<string>();
            var words = text.Split(' ');
            var current = string.Empty;

            foreach (var word in words)
            {
                if (current.Length + word.Length + 1 > maxWidth)
                {
                    lines.Add(current.TrimEnd());
                    current = word + " ";
                }
                else
                {
                    current += word + " ";
                }
            }

            if (!string.IsNullOrWhiteSpace(current))
                lines.Add(current.TrimEnd());

            return lines;
        }
    }
}