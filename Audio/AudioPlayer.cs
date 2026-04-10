namespace CybersecurityChatbot.Audio
{
    /// <summary>
    /// Handles playback of the voice greeting WAV file on application startup.
    /// Gracefully skips if file is missing or platform does not support audio.
    /// </summary>
    public static class AudioPlayer
    {
        private const string WavFileName = "assets/greeting.wav";

        /// <summary>
        /// Plays the greeting WAV file if available on Windows.
        /// </summary>
        public static void PlayGreeting()
        {
            try
            {
                string wavPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, WavFileName);

                if (!File.Exists(wavPath))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("  [Audio] Greeting file not found. Skipping audio.");
                    Console.ResetColor();
                    return;
                }

                PlayWav(wavPath);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"  [Audio] Could not play greeting: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Plays the WAV file using SoundPlayer on Windows.
        /// </summary>
        private static void PlayWav(string path)
        {
            if (OperatingSystem.IsWindows())
            {
                using var player = new System.Media.SoundPlayer(path);
                player.PlaySync();
            }
        }
    }
}