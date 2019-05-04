using System;
using System.Collections.Generic;

namespace SDScode_challenge
{
    class Program
    {

        static IList<Item> Items = new List<Item>
            {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new Item { Name = "Sulfuras", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes", SellIn = 15, Quality = 20 },
            new Item { Name = "Conjured", SellIn = 3, Quality = 6 }
            };
        /// <summary>
        /// This is the dictionary used in companion with the "Quality" class that includes all the items except the "Backstage Pass"    
        /// </summary>
        static IDictionary<string, Quality> Qualities = new Dictionary<string, Quality>
        {
            ["+5 Dexterity Vest"] = new Quality { Name = "+5 Dexterity Vest", SellinChange = -1, QualityChange = -1 },
            ["Aged Brie"] = new Quality { Name = "Aged Brie", SellinChange = -1, QualityChange = 1 },
            ["Elixir of the Mongoose"] = new Quality { Name = "Elixir of the Mongoose", SellinChange = -1, QualityChange = -1 },
            ["Sulfuras"] = new Quality { Name = "Sulfuras", SellinChange = 0, QualityChange = 0},
            ["Conjured"] = new Quality { Name = "Conjured", SellinChange = -1, QualityChange = -2}


        };
        /// <summary>
        /// in the main method i created a for loop and ran the updatequality method 20 times to make sure all items were decremented correctly
        /// even after the sell by date had passed
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Print();
            for(int idx = 0; idx <20; idx++)
            {
                UpdateQuality();
                Print();
            }

        }
        /// <summary>
        /// This method is called to update all items except the "Backstage Pass"
        /// </summary>
        /// <param name="item"> an instance of the "Item" class</param>
        static void Update(Item item)
        {
            var quality = Qualities[item.Name];
            item.SellIn += quality.SellinChange;
            if (item.Quality < 50)
            {
                item.Quality +=(item.SellIn <= 0)? quality.QualityChange *2 : quality.QualityChange;
            }
            if(item.Quality < 0 )
            {
                item.Quality = 0;
            }
        }
        /// <summary>
        /// This is called to update the "Backstage Pass"
        /// </summary>
        /// <param name="item">an instance of the "Item" class</param>
        static void BackstagePassUpdate(Item item)
        {
            item.SellIn -= 1;
            if(item.SellIn <= 0)
            {
                item.Quality = 0;
            }
            else if(item.SellIn<= 5)
            {
                item.Quality += 3;
            }
            else if(item.SellIn <= 10)
            {
                item.Quality += 2;
            }
            else
            {
                item.Quality += 1;
            }
        }

        /// <summary>
        /// this is used to see that the "updatequality" and "backstagepassupdate" methods were outputting the correct values
        /// </summary>
        static void Print()
        {
            foreach (var Item in Items)
            {
                Console.WriteLine($"Name = {Item.Name}, Sellin = {Item.SellIn}, Quality = {Item.Quality}");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// this was used instead of the original "updatequality" method as to refactor all the nested "if" statements
        /// </summary>
        static void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if(item.Name.Contains("Backstage"))
                {
                    BackstagePassUpdate(item);
                }
                else
                {
                    Update(item);
                }
            }
        }
        static void UpdateQualityOriginal()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage Passes")
                {
                    if (Items[i].Quality > 0)
                    {
                        if(Items[i].Name == "Conjured")
                        {
                            Items[i].Quality = Items[i].Quality - 2;
                        }
                        else if (Items[i].Name != "Sulfuras")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                        if (Items[i].Name == "Backstage Passes")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }
                if (Items[i].Name != "Sulfuras")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }
                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }







    }
}
