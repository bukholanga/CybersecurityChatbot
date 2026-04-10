using CybersecurityChatbot.Audio;
using CybersecurityChatbot.Chatbot;
using CybersecurityChatbot.UI;
using System;

namespace CybersecurityChatbot
{
    /// <summary>
    /// Entry point for the Cybersecurity Awareness Chatbot application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Play voice greeting
            AudioPlayer.PlayGreeting();

            // Display ASCII art logo
            ConsoleUI.DisplayLogo();

            // Greet the user and get their name
            string userName = ConsoleUI.GetUserName();

            // Start the main chatbot session
            var bot = new ChatBot(userName);
            bot.StartSession();
        }
    }
}