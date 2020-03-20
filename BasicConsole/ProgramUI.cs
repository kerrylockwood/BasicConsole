using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsole
{
    public class MenuRun
    {
        public void StartMenuUI()
        {
            RunMenuUI();
        }

        //======================================
        private void RunMenuUI()
        {
            bool running = true;
            while (running)
            {
                string question = $"{"",-20}{"Welcome to the Basic Character Menu"}\n\n" +
                    "   1. Character Menu\n" +
                    "   2. Item Menu\n" +
                    "   3. ViewAllMedia - Media Menu\n" +
                    "   4. ViewMediaById - Media Menu\n" +
                    "   5. AddCharMediaLink - Media Menu\n" +
                    "   9. Exit\n\n" +
                    "Please enter your selection: ";

                int selection = GetIntAnswer(question);

                switch (selection)
                {
                    case 1:
                        CharacterMenuUI();
                        break;
                    case 2:
                        //ItemMenuUI();
                        break;
                    case 3:
                        ViewAllMedia();
                        break;
                    case 4:
                        ViewMediaById();
                        break;
                    case 5:
                        AddCharMediaLink();
                        break;
                    case 9:
                        running = false;
                        break;
                }
            }
        }

        //======================================
        private void CharacterMenuUI()
        {
            bool running = true;
            while (running)
            {
                string question = $"{"",-20}{"Welcome to the Basic Character Menu"}\n\n" +
                    "   1. View All Characters\n" +
                    "   2. View Characters - Name Search\n" +
                    "   3. View Characters by ID\n" +
                    "   4. xxx\n" +
                    "   9. Previous Menu\n\n" +
                    "Please enter your selection: ";

                int selection = GetIntAnswer(question);

                switch (selection)
                {
                    case 1:
                        ViewAllCharactersUI();
                        break;
                    case 2:
                        ViewCharacterByName();
                        break;
                    case 3:
                        ViewCharacterById();
                        break;
                    case 4:
                        //xxxxx();
                        break;
                    case 9:
                        running = false;
                        break;
                }
            }
        }

        //======================================
        private void ViewAllCharactersUI()
        {
            BasicDbService service = new BasicDbService();

            Console.Clear();
            Console.WriteLine("   All Characters View\n");

            var characters = service.GetAllCharAsync<CharShort>().Result;

            if (characters.Count > 0)
            {
                foreach (CharShort character in characters)
                {
                    Console.WriteLine($"{character.Name} --  {character.ShortDescription}");
                }
            }
            else
            {
                Console.WriteLine("No Characters found");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================

        private void ViewCharacterByName()
        {
            string name = "o";
            BasicDbService service = new BasicDbService();

            var characters = service.GetCharacterByNameAsync<CharListItem>(name).Result;
            foreach (CharListItem character in characters)
            {
                Console.WriteLine($"{character.CharId} -- { character.Name} -- { character.Name}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================
        private void ViewCharacterById()
        {
            string question = "     View Character by ID\n\n" +
                "Please enter a Character ID number:  ";
            int charId = GetIntAnswer(question);

            BasicDbService service = new BasicDbService();

            var characters = service.GetCharacterByIdAsync(charId).Result;

            Console.Clear();
            Console.WriteLine("     View Character by ID\n");

            Console.WriteLine(characters.CharId);
            Console.WriteLine(characters.Name);
            Console.WriteLine(characters.ShortDescription);
            Console.WriteLine(characters.Description);
            Console.WriteLine("  -- Items --");

            foreach (ItemGetAll item in characters.Items)
            {
                Console.WriteLine($"ID: {item.ItemId} -- Type: {item.Type} -- Name: {item.Name}");
            }
            Console.WriteLine("  -- Media --");
            foreach (MediaShort media in characters.Media)
            {
                Console.WriteLine($"ID: {media.MediaId} -- Type: {media.MediaType} -- Name: {media.Title}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================
        private void ViewAllMedia()
        {
            BasicDbService service = new BasicDbService();

            Console.Clear();
            Console.WriteLine("     View All Media\n");

            var media = service.GetAllMediaAsync<MediaGet>().Result;

            if (media.Count > 0)
            {
                foreach (MediaGet medium in media)
                {
                    Console.WriteLine($"ID: {medium.MediaId} -- Type: {medium.MediaType} -- Name: {medium.Title}");
                }
            }
            else
            {
                Console.WriteLine("No Media Found");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================
        private void ViewMediaById()
        {
            string question = "     View Media by ID\n\n" +
                "Please enter a Media ID number:  ";
            int mediaId = GetIntAnswer(question);

            BasicDbService service = new BasicDbService();

            var media = service.GetMediaByIdAsync(mediaId).Result;

            Console.Clear();
            Console.WriteLine("     View Media by ID\n");

            if (media.Title == null)
            {
                Console.WriteLine("No Media found");
            }
            else
            {
                Console.WriteLine($"Title: {media.Title}");
                Console.WriteLine($"Medium: {media.MediaType}");
                Console.WriteLine($"Description: {media.Description}");
                Console.WriteLine($"Added by: {media.AddedBy}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================
        //======================================
        private void AddCharMediaLink()
        {

            string question = "     View Media by ID\n\n" +
                "Please enter a Character ID number: ";
            int charId = GetIntAnswer(question);

            question = $"     View Media by ID\n\n" +
                $"You have selected Character ID {charId}\n\n" +
                $"Please enter a Media ID number: ";
            int mediaId = GetIntAnswer(question);

            BasicDbService service = new BasicDbService();

            string result = service.PostCharMediaAsync(charId, mediaId);

            Console.WriteLine($"\n{result}" +
                $"\n\nPress any key to continue");
            Console.ReadKey();
        }

        //======================================
        //======================================
        //======================================
        private int GetIntAnswer(string question)
        {
            int selection = -9;

            bool running = true;
            while (running)
            {
                string answer;
                Console.Clear();
                Console.Write(question);

                answer = Console.ReadLine();
                if (String.IsNullOrEmpty(answer) || !int.TryParse(answer, out selection))
                {
                    Console.WriteLine($"Your answer must be a whole number.\n" +
                        $" Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    running = false;
                }
            }
            return selection;
        }
    }
}
