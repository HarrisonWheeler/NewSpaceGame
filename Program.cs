using System;
using System.Collections.Generic;

namespace NewSpaceGame
{
  class Program
  {
    static void Main(string[] args)
    {
      new NewSpaceGame();
    }

    class Upgrade
    {
      public String Name { get; private set; }
      public String Type { get; private set; }
      public int Price { get; set; }
      public decimal Quantity { get; set; }
      public decimal Multiplier { get; set; }

      public Upgrade(string name, string type, int price, decimal quantity, decimal multiplier)
      {
        Name = name;
        Type = type;
        Price = price;
        Quantity = quantity;
        Multiplier = multiplier;
      }

    }

    class NewSpaceGame
    {
      public bool Running { get; set; }
      public decimal Cheese { get; set; }
      public bool InShop { get; set; }
      public List<Upgrade> Shop { get; set; }

      public Dictionary<string, decimal> Stats { get; set; }

      public List<Upgrade> ClickUpgrades { get; set; }

      public NewSpaceGame()
      {
        Running = true;
        Shop = new List<Upgrade>() { };
        ClickUpgrades = new List<Upgrade>() { };
        Stats = new Dictionary<string, decimal>() { };
        Shop.Add(new Upgrade("Chris's Cheese PickAxe", "click", 1, 0, 1));
        Shop.Add(new Upgrade("George's Cheese Dagger", "click", 5, 0, 2));
        Shop.Add(new Upgrade("Darry's Drill", "click", 15, 0, 5));
        Shop.Add(new Upgrade("Manda Miner", "click", 100, 0, 25));

        PlayGame();
      }

      public void PlayGame()
      {
        while (Running)
        {
          string input = GetPlayerInput().Key.ToString().ToLower();
          switch (input)
          {
            case "spacebar":
              Mine();
              break;
            case "s":
              GoToShop();
              break;
            case "escape":
              Running = false;
              break;
          }
        }
      }

      public void GoToShop()
      {
        InShop = true;
        Console.Clear();
        Console.WriteLine("Welcome to the Mooney McShopperton BUY OR GET");
        string message = "";
        for (int i = 0; i < Shop.Count; i++)
        {
          Upgrade item = Shop[i];
          message += $"{i + 1}. {item.Name}: {item.Price}, Multiplier: {item.Multiplier}\n";
        }
        Console.WriteLine(message);
        string choice = Console.ReadLine();
        if (int.TryParse(choice, out int selection) && selection > 0 && selection <= Shop.Count)
        {
          BuyUpgrade(selection - 1);
        }
      }

      public void BuyUpgrade(int shopIndex)
      {
        Upgrade item = Shop[shopIndex];
        if (Cheese >= item.Price)
        {
          Cheese -= item.Price;
          item.Price += item.Price;
          if (item.Type == "click")
          {
            int index = ClickUpgrades.FindIndex(i => i.Name == item.Name);
            if (index == -1)
            {
              ClickUpgrades.Add(item);
              index = ClickUpgrades.Count - 1;
            }
            ClickUpgrades[index].Quantity++;
            Stats[item.Name] = ClickUpgrades[index].Quantity;
          }
          Console.WriteLine($"You purchased {item.Name}! NOW GIT OUT");
        }
        else
        {
          Console.WriteLine($"You dont have enough cheese for this {item.Name}. GIIIITTTTTTTT");
        }
        Console.WriteLine("press any key to exit");
        Console.ReadKey();
        InShop = false;
      }

      public void Mine()
      {
        Cheese++;
        ClickUpgrades.ForEach(m =>
        {
          Cheese += m.Quantity * m.Multiplier;
        });
      }

      public ConsoleKeyInfo GetPlayerInput()
      {
        DrawGameScreen();
        return Console.ReadKey();
      }

      public void DrawGameScreen()
      {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        string moon = @"
                                                                                                      
                              %%&&&&&&%                                         
                       %%%%%%%&%%%%&&&&&&&&&&&                                  
                   (###(((###%###%%&%&&%&%###(%&&%                              
                 ,***///(####((///(%%%##%%%&&%&&&%&%                            
                 .,*,*/(/#(/*//((/(///(///(%#&(((#%&&&                          
                  ..,,*/////**////////(///(((#(#((#%&&&&                        
                  ....,*/**(/**/*/#(**////(%#%%##(#%%&%&&                       
                    ..,.,,#/***,*/(##%(((%(##%%%%%%%%&&&&&                      
                     ...,,,,,*//((####(#&%%%&%###(%%&&&&&&&                     
                      ...,,..,/*/(((####%#%%%%%&#%%%&&@@&&&                     
                        .....,**////#((##%#%%%%&%&%&&&&&&&%                     
                         ....,,**////((((##%##&%%%%%&&&%%&%                     
                            .....,***//#((###%&%#%%%%&&%&%%                     
                              ......**((#######%#%%%%%%%&%                      
                                .....,*(#(#(##%((#%%%%%&%                       
                                  ...,**/##/(#%###%##%%%                        
                                      * * /#(((((% #%#                          
                                         ..*,@* /(%&                            
                                                                                
        ";
        Console.WriteLine(moon);
        string message = $@"
        Mine[SpaceBar], Shop[s], Quit[escape]
        Cheese: {Cheese};
        Inventory: ";
        foreach (var stat in Stats)
        {
          message += $"\n {stat.Key}: {stat.Value}";
        }
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
      }
    }
  }
}
