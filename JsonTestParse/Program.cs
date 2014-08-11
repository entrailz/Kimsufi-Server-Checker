using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Json;

namespace JsonTestParse
{
    class Program
    {

        static void Main(string[] args)
        {

            System.Threading.Timer t = new System.Threading.Timer(TimerCallback, null, 0, 20000);
            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            WebClient wc = new WebClient();
            dynamic result = JsonValue.Parse(wc.DownloadString("https://ws.ovh.com/dedicated/r2/ws.dispatcher/getAvailability2"));
            foreach (dynamic item in result.answer.availability)
            {
                if (item.reference == "142sk1")
                {
                    foreach (dynamic item1 in item.zones)
                    {
                        if (item1.availability != "unavailable")
                        {
                            Console.WriteLine("Server avaliable in zone: " + item1.zone);
                            SpeechSynthesizer voice = new SpeechSynthesizer();
                            voice.Volume = 20;
                            voice.Rate = -2;
                            voice.Speak("Get the server now!");
                            break;
                        }
                    }
                    
                }
                if (item.reference == "142sk2")
                {
                    foreach (dynamic item1 in item.zones)
                    {
                        if (item1.availability != "unavailable")
                        {
                            Console.WriteLine("Server (2) avaliable in zone: " + item1.zone);
                            SpeechSynthesizer voice = new SpeechSynthesizer();
                            voice.Volume = 20;
                            voice.Rate = -2;
                            voice.Speak("Get the server 2 now!");
                            break;
                        }
                    }

                }
            }
            Console.WriteLine("Server checking complete... putting thread to sleep.");
            GC.Collect();
        }
    }
}
