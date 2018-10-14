using AlphaCinema.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core
{
    class Engine : IEngine
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IData data;

        //Първото е името на командата, второто са стойностите, а това накрая са просто координати за принтиране
        private readonly List<string> menus = new List<string>() { "Menu", "BuyTickets", "LogAsAdmin", "ShowInfo", "PdfExport", "Exit", "3", "2" };//, "AddMovie", "AddProjection"

        public Engine(ICommandProcessor commandProcessor, IData data)//, IAlphaConsole alphaConsole)
        {
            this.commandProcessor = commandProcessor;
            this.data = data;
        }
        public void Run()
        {
            data.Load(); // Ако сте попълнили базата го закоментирайте този метод
            var parameters = menus as IEnumerable<string>;
            while (true)
            {
                //Тук взимаме командата
                var command = this.commandProcessor.ParseCommand(parameters.First());

                // Тук задаваме параметри на командата и я извикваме
                parameters = command.Execute(parameters);
                // Всяка команда връща входните параметри, необходими за следващата
            }
        }
    }
}
