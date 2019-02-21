using System.Collections.Generic;
using System.ServiceProcess;
using Telegram.Bot;

namespace dwhService
{
    public partial class Service1 : ServiceBase
    {
        Dictionary<char, char> keyValuePairs = new Dictionary<char, char>() {
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
        TelegramBotClient bot = new TelegramBotClient("api key");
        Dictionary<char, string> leetDict;
        bool isLeet = false;

        public Service1()
        {
            InitializeComponent();
            bot.OnMessage += Bot_OnMessage;
        }

        void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if (!isLeet && message.Text == "1337")
            {
                isLeet = true;
                leetDict = new Dictionary<char, string>();
                foreach (var pair in keyValuePairs)
                {
                    leetDict.Add(pair.Key, pair.Value.ToString());
                }

                AddLeet();
                bot.SendTextMessageAsync(message.Chat.Id, "KPACAB4Er");
                return;
            }
            else if (isLeet && message.Text == "!1337")
            {
                isLeet = false;
                leetDict.Clear();
                leetDict = null;
                bot.SendTextMessageAsync(message.Chat.Id, "ACTAJIABuCTA");
                return;
            }

            if (message == null || message.Type != Telegram.Bot.Types.Enums.MessageType.Text) return;
            bot.SendTextMessageAsync(message.Chat.Id, Translate(message.Text));
        }
        string Translate(string input)
        {
            string output = string.Empty;
            if (!isLeet)
                foreach (var item in input.ToLower())
                    output += keyValuePairs.TryGetValue(item, out char ch) ? ch : item;
            else
                foreach (var item in input.ToLower())
                    output += leetDict.TryGetValue(item, out string ch) ? ch : item.ToString();
            return output;
        }

        void AddLeet()
        {
            leetDict.Add('а', "А");
            leetDict.Add('б', "6");
            leetDict.Add('в', "B");
            leetDict.Add('г', "r");
            leetDict.Add('д', "D");
            leetDict.Add('е', "Е");
            leetDict.Add('ё', "Е");
            leetDict.Add('ж', ">|<");
            leetDict.Add('з', "3");
            leetDict.Add('и', "u");
            leetDict.Add('й', "u*");
            leetDict.Add('к', "K");
            leetDict.Add('л', "JI");
            leetDict.Add('м', "M");
            leetDict.Add('н', "H");
            leetDict.Add('о', "O");
            leetDict.Add('п', "/7");
            leetDict.Add('р', "P");
            leetDict.Add('с', "C");
            leetDict.Add('т', "T");
            leetDict.Add('у', "y");
            leetDict.Add('ф', "(I)");
            leetDict.Add('х', "X");
            leetDict.Add('ц', "LL");
            leetDict.Add('ч', "4");
            leetDict.Add('ш', "LLI");
            leetDict.Add('щ', "LLL");
            leetDict.Add('ъ', "`b");
            leetDict.Add('ы', "bl");
            leetDict.Add('ь', "b");
            leetDict.Add('э', "-)");
            leetDict.Add('ю', "IO");
            leetDict.Add('я', "9I");
            leetDict.Add('%', "o\\o");
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
