using KitchenAppliances;

MyTrialKitchen myKitchen = new MyTrialKitchen();
myKitchen.Menu();

namespace KitchenAppliances
{
    public interface IKitchenAppliance
    {
        string Type { get; set; }
        string Brand { get; set; }
        public bool IsFunctioning { get; set; }
    }

    public abstract class Kitchen
    {
        public bool avsluta = false;

        public virtual void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("======== KÖKET ========");
            Console.WriteLine("1. Använd köksapparat");
            Console.WriteLine("2. Lägg till köksapparat");
            Console.WriteLine("3. Lista köksapparater");
            Console.WriteLine("4. Ta bort köksapparat");
            Console.WriteLine("5. Avsluta programmet");      
        }
        public virtual void UseAppliance()
        {
            Console.WriteLine("Detta är ett exempel på en abstract class.");
            Console.WriteLine("Detta meddelande kommer inte att synas för att jag gör en override på metoden.");
        }
        public abstract void AddAppliance();
        public abstract void ListAppliances(bool extendedList, bool returnToMenu);
        public abstract void RemoveAppliance();
        public abstract void EditAppliance();
        public abstract void DisplayErrorMessage(string message);
    }

    public class Appliance : IKitchenAppliance
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public bool IsFunctioning { get; set; }
        public Appliance(string Type, string Brand, bool IsFunctioning)
        {
            this.Type = Type;
            this.Brand = Brand;
            this.IsFunctioning = IsFunctioning;
        }
    }

    public class MyTrialKitchen : Kitchen
    {
        List<Appliance> appliances = new List<Appliance>()
        {
            new Appliance("Microvågsugn", "Electrolux", true),
            new Appliance("Vattenkokare", "Delonghi", true),
            new Appliance("Elvisp", "OBH Nordica", false)
        };
        public override void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("======== KÖKET ========");
            Console.WriteLine("1. Använd köksapparat");
            Console.WriteLine("2. Lägg till köksapparat");
            Console.WriteLine("3. Lista köksapparater");
            Console.WriteLine("4. Ta bort köksapparat");
            Console.WriteLine("5. Redigera köksapparat");
            Console.WriteLine("6. Avsluta programmet");
            Console.WriteLine("Ange val:");
            Console.Write("> ");

            while (!avsluta)
            {
                try
                {
                    int menuInput = int.Parse(Console.ReadLine());

                    switch (menuInput)
                    {
                        case 1:
                            UseAppliance();
                            break;
                        case 2:
                            AddAppliance();
                            break;
                        case 3:
                            ListAppliances(true, true);
                            break;
                        case 4:
                            RemoveAppliance();
                            break;
                        case 5:
                            EditAppliance();
                            break;
                        case 6:
                            try
                            {
                                Console.WriteLine("Hej då!");
                            }
                            catch (IOException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            Environment.Exit(0);
                            break;
                        default:
                            try
                            {
                                Console.WriteLine("Ogiltig input");
                            }
                            catch (IOException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            break;
                    }                   
                }
                catch (ArgumentNullException e)
                {
                    DisplayErrorMessage(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Något gick fel. Försök igen.");
                }
                catch (OverflowException e)
                {
                    DisplayErrorMessage(e.Message);
                }
            }
        }
        public override void UseAppliance()
        {
            Console.WriteLine("Välj köksapparat:");
            ListAppliances(false, false);
            Console.Write("> ");

            int numberOfAppliances = appliances.Count;

            try
            {
                int input = int.Parse(Console.ReadLine()) - 1;

                if (input < numberOfAppliances && input >= 0)
                {
                    if (appliances[input].IsFunctioning == true)
                        Console.WriteLine($"Använder {appliances[input].Type}...");
                    else
                        Console.WriteLine($"Kan inte använda {appliances[input].Type} då den är trasig.");
                }
                else
                {
                    Console.WriteLine("Numret du angav finns inte i listan. Försök igen.\n");
                    UseAppliance();
                }
            }
            catch (ArgumentNullException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Du måste fylla i ett värde från listan. Försök igen.\n");
                UseAppliance();
            }
            catch (OverflowException e)
            {
                DisplayErrorMessage(e.Message);
            }
            Menu();
        }
        public override void AddAppliance()
        {
            try
            {
                Console.Write("Ange typ: ");
                string typeInput = Console.ReadLine();

                Console.Write("Ange märke: ");
                string brandInput = Console.ReadLine();

                Console.Write("Ange om den fungerar j/n: ");
                string functionInput = Console.ReadLine().ToLower();

                if (functionInput == "j")
                {
                    if (typeInput != null && brandInput != null)
                    {
                        appliances.Add(new Appliance(typeInput, brandInput, true));
                        Console.WriteLine("Tillagd!");
                    }
                    else if (typeInput == null || brandInput == null)
                        Console.WriteLine("Något gick fel med input.");
                    Menu();
                }
                if (functionInput == "n")
                {
                    if (typeInput != null && brandInput != null)
                    {
                        appliances.Add(new Appliance(typeInput, brandInput, false));
                        Console.WriteLine("Tillagd!");
                    }
                    else if (typeInput == null || brandInput == null)
                        Console.WriteLine("Något gick fel med input.");
                    Menu();
                }
                else
                    Console.WriteLine("Något gick fel.");
            }
            catch (IOException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (OutOfMemoryException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                DisplayErrorMessage(e.Message);
            }
            Menu();
        }
        public override void ListAppliances(bool extendedList, bool returnToMenu)
        {
            if (appliances.Count > 0)
            {
                for (int i = 0; i < appliances.Count; i++)
                {
                    Console.Write($"Köksapparat {i + 1}: {appliances[i].Type}");
                    if (extendedList == false)
                        Console.WriteLine();
                    if (extendedList == true)
                    {
                        Console.Write($" - {appliances[i].Brand}");
                        if (appliances[i].IsFunctioning == true)
                            Console.Write(" (fungerande).\n");
                        else
                            Console.Write(" (trasig).\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("Du har inga köksapparater just nu.");
                returnToMenu = true;
            }
            if (returnToMenu == true)
                Menu();
        }
        public override void RemoveAppliance()
        {
            Console.WriteLine("Välj köksapparat:");
            ListAppliances(false, false);

            int numberOfAppliances = appliances.Count;

            try
            {
                int input = int.Parse(Console.ReadLine()) - 1;
                if (input < numberOfAppliances && input >= 0)
                {
                    appliances.RemoveAt(input);
                    Console.WriteLine("Borttagen.");
                }
                else
                {
                    Console.WriteLine("Numret du angav finns inte i listan.");
                    RemoveAppliance();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Du måste välja ett nummer från listan.");
                RemoveAppliance();
            }
            catch (OverflowException e)
            {
                DisplayErrorMessage(e.Message);
            }
            Menu();
        }
        public override void EditAppliance()
        {
            Console.WriteLine("Välj köksapparat:");
            ListAppliances(true, false);

            int numberOfAppliances = appliances.Count;

            try
            {
                int input = int.Parse(Console.ReadLine()) - 1;

                if (input < numberOfAppliances && input >= 0)
                {
                    Console.WriteLine("Vad vill du ändra?");
                    Console.WriteLine("1. Namnet");
                    Console.WriteLine("2. Märket");
                    Console.WriteLine("3. Skicket");
                    Console.Write("> ");

                    int input2 = int.Parse(Console.ReadLine());

                    switch (input2)
                    {
                        case 1:
                            Console.Write("För in det nya namnet: ");
                            try
                            {
                                string newType = Console.ReadLine();
                                appliances[input].Type = newType;
                                Console.WriteLine("Din ändring är genomförd.");
                            }
                            catch (IOException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            catch (OutOfMemoryException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            break;
                        case 2:
                            Console.Write("För in det nya märket: ");
                            try
                            {
                                string newBrand = Console.ReadLine();
                                appliances[input].Brand = newBrand;
                                Console.WriteLine("Din ändring är genomförd.");
                            }
                            catch (IOException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            catch (OutOfMemoryException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                DisplayErrorMessage(e.Message);
                            }
                            break;
                        case 3:
                            if (appliances[input].IsFunctioning == true)
                            {
                                Console.WriteLine($"{appliances[input].Type} är fungerande. Ändra till trasig? j/n");
                                try
                                {
                                    string changeFunction = Console.ReadLine().ToLower();
                                    if (changeFunction != "n" && changeFunction != "j")
                                        Console.WriteLine("Någonting gick fel med input.");
                                    if (changeFunction == "j")
                                    {
                                        appliances[input].IsFunctioning = false;
                                        Console.WriteLine($"{appliances[input].Type} är nu trasig.");
                                    }
                                    if (changeFunction == "n")
                                        Console.WriteLine("Ingenting ändrades.");
                                }
                                catch (IOException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                                catch (OutOfMemoryException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                            }
                            if (appliances[input].IsFunctioning == false)
                            {
                                Console.WriteLine($"{appliances[input].Type} är trasig. Ändra till fungerande? j/n");
                                try
                                {
                                    string changeFunction = Console.ReadLine().ToLower();

                                    if (changeFunction == "j")
                                    {
                                        appliances[input].IsFunctioning = true;
                                        Console.WriteLine($"{appliances[input].Type} är nu fungerande.");
                                    }
                                    else if (changeFunction == "n")
                                        Console.WriteLine("Ingenting ändrades.");
                                    else
                                        Console.WriteLine("Någonting gick fel med input.");
                                }
                                catch (IOException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                                catch (OutOfMemoryException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    DisplayErrorMessage(e.Message);
                                }
                            }
                            else
                                Console.WriteLine("Något gick fel.");
                            break;
                        default:
                            Console.WriteLine("Ogiltig input.");
                            break;
                        }
                    }
                else
                    Console.WriteLine("Numret du angav finns inte i listan.");
            }
            catch (ArgumentNullException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (FormatException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (OverflowException e)
            {
                DisplayErrorMessage(e.Message);
            }
            catch (Exception e)
            {
                DisplayErrorMessage(e.Message);
            }
            Menu();
        }
        public override void DisplayErrorMessage(string message)
        {
            Console.WriteLine("Någonting gick fel.");
            Console.WriteLine($"Visa följande kod för din programmerare: \"{message}\"");
            Console.WriteLine("Tryck på valfri tangent för att återgå till menyn.");
            Console.ReadKey();
            Menu();
        }
    }
}