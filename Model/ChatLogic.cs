using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ChatApp.Entity;

namespace ChatApp.Model
{
    public class ChatLogic
    {
        public async Task<string> CheckMessage(string userMessage, List<ChatMessage> chatMessages)
        {
            string reply = string.Empty;
            string bestMatch = null;
            int bestScore = int.MaxValue;

            userMessage = Nomalize(userMessage);

            foreach(var chatMessage in chatMessages)
            {
                string normalized = Nomalize(chatMessage.UserMessage);

                if(normalized.Contains(userMessage) || userMessage.Contains(normalized))
                {
                    bestMatch = chatMessage.BotReply;
                    bestScore = 0;
                    break;

                }

                int distance = Fastenshtein.Levenshtein.Distance(userMessage, chatMessage.UserMessage);
                if (distance < bestScore)
                {
                    bestScore = distance;
                    bestMatch = chatMessage.BotReply;
                }
            }

            if (bestScore > 5)
            {
                reply = "すみません。よくわかりません。";
            }
            else
            {
                reply = bestMatch;
            }

            return reply;
        }

        private string Nomalize(string text)
        {
            text = text.ToLower();
            text = Regex.Replace(text, @"[\s　]+", ""); // 空白除去
            text = Regex.Replace(text, @"[！!？?。、,.]", ""); // 記号除去
            return text;
        }
    }
}
