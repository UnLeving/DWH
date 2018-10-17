using System.Collections.Generic;
using System.ServiceProcess;
using Telegram.Bot;

namespace dwhService
{
    public partial class Service1 : ServiceBase
    {
        static Dictionary<char, char> keyValuePairs = new Dictionary<char, char>() {
            { 'q', 'й' },
            { 'w', 'ц' },
            { 'e', 'у' },
            { 'r', 'к' },
            { 't', 'е' },
            { 'y', 'н' },
            { 'u', 'г' },
            { 'i', 'ш' },
            { 'o', 'щ' },
            { 'p', 'з' },
            { '[', 'х' },
            { ']', 'ъ' },
            { 'a', 'ф' },
            { 's', 'ы' },
            { 'd', 'в' },
            { 'f', 'а' },
            { 'g', 'п' },
            { 'h', 'р' },
            { 'j', 'о' },
            { 'k', 'л' },
            { 'l', 'д' },
            { ';', 'ж' },
            { '\'', 'э' },
            { 'z', 'я' },
            { 'x', 'ч' },
            { 'c', 'с' },
            { 'v', 'м' },
            { 'b', 'и' },
            { 'n', 'т' },
            { 'm', 'ь' },
            { ',', 'б' },
            { '.', 'ю' },
            { '/', '.' },
            { '&', '?' },
            { '#', '№' },
            { '@', '"' },
            { '$', ';' },
            { '~', 'Ё' },
        };
        static TelegramBotClient bot = new TelegramBotClient("Properties.Resources.Key");
        public Service1()
        {
            InitializeComponent();
            bot.OnMessage += Bot_OnMessage;
        }
        void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return;
            bot.SendTextMessageAsync(message.Chat.Id, Translate(message.Text));
        }
        string Translate(string input)
        {
            string output = string.Empty;
            foreach (var item in input.ToLower())
            {
                output += keyValuePairs.TryGetValue(item, out char ch) ? ch : item;
            }
            return output;
        }
        protected override void OnStart(string[] args)
        {
            bot.StartReceiving();
        }

        protected override void OnStop()
        {
            bot.StopReceiving();
        }
    }
}
