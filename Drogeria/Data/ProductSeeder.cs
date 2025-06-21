using Drogeria.Data;
using Drogeria.Models;

public static class ProductSeeder
{
    public static void SeedProducts(DrogeriaContext ctx)
    {
        if (ctx.Products.Any()) return;

        var categories = ctx.Categories.ToList();
        var suppliers  = ctx.Suppliers.ToList();
        var rand       = new Random();

        var productsList = new[]
        {
            ("Szampon regenerujący 400ml",     "HairX",         "Hair Care"),
            ("Odżywka wygładzająca 250ml",     "HairX",         "Hair Care"),
            ("Mydło w płynie 500ml",           "FreshClean",    "Skin Care"),
            ("Żel pod prysznic 300ml",         "Luksja",        "Skin Care"),
            ("Krem nawilżający do twarzy",     "Nivea",         "Skin Care"),
            ("Dezodorant roll-on",             "Rexona",        "Skin Care"),
            ("Perfumy damskie 50ml",           "Adidas",        "Fragrances"),
            ("Perfumy męskie 100ml",           "Hugo Boss",     "Fragrances"),
            ("Tusz do rzęs",                   "Maybelline",    "Make-Up"),
            ("Pomadka matowa",                 "Bourjois",      "Make-Up"),
            ("Korektor pod oczy",              "Eveline",       "Make-Up"),
            ("Płyn micelarny 400ml",           "Garnier",       "Skin Care"),
            ("Balsam do ciała 250ml",          "Ziaja",         "Skin Care"),
            ("Krem do rąk",                    "Neutrogena",    "Skin Care"),
            ("Szampon przeciwłupieżowy",       "Head & Shoulders", "Hair Care"),
        };

        var generated = new List<Product>();
        var usedEans = new HashSet<string>();

        foreach (var (name, brand, categoryName) in productsList)
        {
            var category = categories.FirstOrDefault(c => c.Name == categoryName);
            var supplier = suppliers[rand.Next(suppliers.Count)];

            string ean;
            do { ean = $"590{rand.Next(100000000, 999999999)}"; }
            while (!usedEans.Add(ean));

            var product = new Product
            {
                Name        = name,
                Brand       = brand,
                Size        = null,
                Price       = (decimal)Math.Round(rand.Next(15, 80) + rand.NextDouble(), 2),
                VatRate     = 0.23m,
                IsActive    = true,
                EAN         = ean,
                CategoryId  = category!.CategoryId,
                SupplierId  = supplier.SupplierId
            };

            generated.Add(product);
        }

        ctx.Products.AddRange(generated);

        foreach (var p in generated)
        {
            ctx.StockLevels.Add(new StockLevel
            {
                Product = p,
                QtyOnHand = rand.Next(10, 25),
                ReorderLevel = rand.Next(5, 10)
            });
        }

        ctx.SaveChanges();
    }
}
