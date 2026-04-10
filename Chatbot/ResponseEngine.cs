namespace CybersecurityChatbot.Chatbot
{
    /// <summary>
    /// Stores and retrieves all topic-based responses for the chatbot.
    /// Each topic maps to multiple responses that rotate for variety.
    /// </summary>
    public class ResponseEngine
    {
        // Tracks which response index to use next per topic
        private readonly Dictionary<string, int> _responseIndex = new();

        // ── Response bank ────────────────────────────────────────────────────
        private readonly Dictionary<string, string[]> _responses = new(StringComparer.OrdinalIgnoreCase)
        {
            ["how are you"] = new[]
            {
                "I'm running at full capacity and ready to help you stay safe online! How can I assist you today?",
                "All systems go! I'm here and alert, just like your antivirus should be. What can I help you with?"
            },

            ["what is your purpose"] = new[]
            {
                "My purpose is to educate South African citizens about cybersecurity threats and best practices. " +
                "I cover topics like password safety, phishing scams, safe browsing, malware, email safety, " +
                "and social media security. Type 'help' to see everything I can assist with!",

                "I was created to support the Department of Cybersecurity's awareness campaign. " +
                "My goal is to help you recognise online threats and protect yourself and your loved ones."
            },

            ["what can i ask"] = new[]
            {
                "Great question! You can ask me about:\n" +
                "  • Password safety - creating and managing strong passwords\n" +
                "  • Phishing - spotting fake emails, SMSes, and websites\n" +
                "  • Safe browsing - staying secure on the web\n" +
                "  • Malware - viruses, ransomware, spyware, and more\n" +
                "  • Email safety - protecting your inbox\n" +
                "  • Social media - privacy and scam awareness\n" +
                "Just type any of these topics and I'll give you expert advice!"
            },

            ["password safety"] = new[]
            {
                "PASSWORD SAFETY TIPS:\n\n" +
                "  1. Use at least 12 characters - longer is always better.\n" +
                "  2. Combine UPPERCASE, lowercase, numbers, and symbols.\n" +
                "  3. Never use personal info like your name or birthdate.\n" +
                "  4. Use a unique password for EVERY account - never reuse.\n" +
                "  5. Consider a trusted password manager like Bitwarden.\n" +
                "  6. Enable Two-Factor Authentication (2FA) everywhere.\n\n" +
                "  Example of a strong password: T!ger$4Kap3town2024",

                "MORE PASSWORD WISDOM:\n\n" +
                "  • A passphrase is easy to remember and hard to crack.\n" +
                "    Example: Coffee&Rain@CapeTown!\n" +
                "  • Change passwords immediately if you suspect a breach.\n" +
                "  • NEVER share your password via SMS, email, or phone.\n" +
                "  • South Africa's POPIA Act requires companies to protect\n" +
                "    your data - but YOUR passwords are YOUR responsibility."
            },

            ["phishing"] = new[]
            {
                "PHISHING AWARENESS:\n\n" +
                "  Phishing is when criminals pretend to be a trusted\n" +
                "  organisation to steal your personal details or money.\n\n" +
                "  Warning signs:\n" +
                "    • Urgent language: Act NOW or your account will close!\n" +
                "    • Suspicious sender email addresses\n" +
                "    • Links that don't match the real website\n" +
                "    • Requests for your PIN, password, or OTP\n\n" +
                "  What to do:\n" +
                "    • Never click suspicious links.\n" +
                "    • Report phishing to SABRIC.\n" +
                "    • When in doubt, phone the company directly.",

                "SMISHING AND VISHING:\n\n" +
                "  Phishing also happens via SMS (smishing) and phone (vishing).\n\n" +
                "  Common SA scams:\n" +
                "    • Fake SARS refund SMSes asking for banking details.\n" +
                "    • You have won a prize calls requesting your info.\n" +
                "    • Fake courier SMSes with malicious links.\n\n" +
                "  Rule: Your bank will NEVER ask for your PIN or OTP\n" +
                "  via SMS or phone call."
            },

            ["safe browsing"] = new[]
            {
                "SAFE BROWSING TIPS:\n\n" +
                "  1. Always check for HTTPS before entering personal info.\n" +
                "  2. Keep your browser and extensions up to date.\n" +
                "  3. Avoid public Wi-Fi for banking or sensitive tasks.\n" +
                "     If you must use it, connect via a VPN.\n" +
                "  4. Use privacy-focused browsers like Firefox or Brave.\n" +
                "  5. Block pop-ups and use an ad blocker like uBlock Origin.\n" +
                "  6. Don't save passwords on shared devices.\n\n" +
                "  Tip: Scammers clone local South African banking sites.\n" +
                "  Always double-check .co.za domains carefully.",

                "BROWSER SECURITY:\n\n" +
                "  • Clear cookies and cache regularly.\n" +
                "  • Be wary of extensions requesting access to ALL sites.\n" +
                "  • Use multi-factor authentication on Google and Microsoft\n" +
                "    accounts to protect your saved browsing data.\n" +
                "  • Check haveibeenpwned.com to see if your email\n" +
                "    has appeared in a data breach."
            },

            ["malware"] = new[]
            {
                "MALWARE EXPLAINED:\n\n" +
                "  Malware is malicious software designed to damage or gain\n" +
                "  unauthorised access to your device or data.\n\n" +
                "  Types of malware:\n" +
                "    • Virus      - spreads by attaching to files\n" +
                "    • Ransomware - encrypts your data and demands payment\n" +
                "    • Spyware   - secretly monitors your activity\n" +
                "    • Trojan    - disguises itself as legitimate software\n\n" +
                "  How to protect yourself:\n" +
                "    • Install reputable antivirus software.\n" +
                "    • Never download software from unknown sources.\n" +
                "    • Keep your OS and apps updated at all times."
            },

            ["email safety"] = new[]
            {
                "EMAIL SAFETY TIPS:\n\n" +
                "  • Enable spam filters on your email provider.\n" +
                "  • Never open attachments from unknown senders.\n" +
                "  • Check the actual sender email, not just the display name.\n" +
                "  • Use a separate email address for shopping and newsletters.\n" +
                "  • Enable 2FA on Gmail, Outlook, and Yahoo accounts.\n\n" +
                "  Important: SARS, FNB, ABSA, and Standard Bank will\n" +
                "  NEVER ask for your password via email."
            },

            ["social media"] = new[]
            {
                "SOCIAL MEDIA SAFETY:\n\n" +
                "  • Set your profiles to Private.\n" +
                "  • Never share your ID number, address, or travel plans.\n" +
                "  • Be wary of friend requests from people you don't know.\n" +
                "  • Romance scams are very common on Facebook and Instagram.\n" +
                "  • Review app permissions regularly.\n\n" +
                "  Think before you post: once online, information is\n" +
                "  very difficult to remove permanently."
            },
        };

        /// <summary>
        /// Returns the best matching response for the given input, or null if no match.
        /// </summary>
        public string? GetResponse(string input)
        {
            foreach (var kvp in _responses)
            {
                if (input.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return GetNextResponse(kvp.Key, kvp.Value);
                }
            }
            return null;
        }

        /// <summary>
        /// Rotates through available responses for variety.
        /// </summary>
        private string GetNextResponse(string topic, string[] options)
        {
            if (!_responseIndex.ContainsKey(topic))
                _responseIndex[topic] = 0;

            int idx = _responseIndex[topic] % options.Length;
            _responseIndex[topic] = idx + 1;
            return options[idx];
        }
    }
}