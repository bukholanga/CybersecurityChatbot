using CybersecurityChatbot.UI;

namespace CybersecurityChatbot.Chatbot
{
    /// <summary>
    /// Manages the chatbot session loop, routing user input to the
    /// ResponseEngine and delegating all display to ConsoleUI.
    /// </summary>
    public class ChatBot
    {
        private readonly string _userName;
        private readonly ResponseEngine _engine;

        // Commands that end the session
        private static readonly HashSet<string> ExitCommands =
            new(StringComparer.OrdinalIgnoreCase) { "quit", "exit", "bye", "goodbye" };

        // Commands that show the help menu
        private static readonly HashSet<string> HelpCommands =
            new(StringComparer.OrdinalIgnoreCase) { "help", "menu", "topics", "?" };

        public ChatBot(string userName)
        {
            _userName = userName;
            _engine = new ResponseEngine();
        }

        /// <summary>
        /// Starts the interactive chat session loop.
        /// Continues until the user types an exit command.
        /// </summary>
        public void StartSession()
        {
            // Show help menu on first launch
            ConsoleUI.ShowHelpMenu();

            while (true)
            {
                // Display input prompt with user's name
                ConsoleUI.ShowUserPrompt(_userName);

                // Read user input
                string? rawInput = Console.ReadLine();

                // Handle null input (e.g. piped input or EOF)
                if (rawInput == null) break;

                string input = rawInput.Trim();

                // ── Validate empty input ─────────────────────────────────
                if (string.IsNullOrWhiteSpace(input))
                {
                    ConsoleUI.ShowBotResponse(
                        "It looks like you didn't type anything. " +
                        "Please ask me a question or type 'help' for a list of topics.");
                    continue;
                }

                // ── Exit commands ────────────────────────────────────────
                if (ExitCommands.Contains(input))
                {
                    ConsoleUI.ShowFarewell(_userName);
                    break;
                }

                // ── Help commands ────────────────────────────────────────
                if (HelpCommands.Contains(input))
                {
                    ConsoleUI.ShowHelpMenu();
                    continue;
                }

                // ── Route to response engine ─────────────────────────────
                string? response = _engine.GetResponse(input);

                if (response != null)
                {
                    ConsoleUI.ShowBotResponse(response);
                }
                else
                {
                    // Default fallback for unrecognised input
                    ConsoleUI.ShowUnknownInput();
                }
            }
        }
    }
}