namespace MwangiFinancial.Migrations
{
    using MwangiFinancial.Enumeration;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MwangiFinancial.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration <ApplicationDbContext>
    {
        //if(!System.Diagnostics.Debugger.IsAttached)
        //System.Diagnostics.Debugger.Launch();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region Roles creation
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "HeadofHouse"))
            {
                roleManager.Create(new IdentityRole { Name = "HeadofHouse" });
            }
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }
            if (!context.Roles.Any(r => r.Name == "LobbyMember"))
            {
                roleManager.Create(new IdentityRole { Name = "LobbyMember" });
            }
            #endregion

            //This code introduces code that will allow me to create a few users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            #region SeededLobbyAndDemoHouse           

            context.Households.AddOrUpdate(
                h => h.Name,
                new Household { Name = "The Lobby", Greeting = "Welcome to the lobby!", Created = DateTimeOffset.UtcNow.ToLocalTime() },
                new Household { Name = "Demo Household", Greeting = "Welcome everyone!", Created = DateTimeOffset.UtcNow.ToLocalTime(), IsConfigured = true }
                );
            #endregion

            context.SaveChanges();
            var seedHouseId = context.Households.AsNoTracking().FirstOrDefault(h => h.Name == "Demo Household").Id;
            var seedLobbyId = context.Households.AsNoTracking().FirstOrDefault(h => h.Name == "The Lobby").Id;

            #region Seeding Admin
            //Seeding Admin
            if (!context.Users.Any(u => u.Email == "Admin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Admin@Mailinator.com",
                    Email = "Admin@Mailinator.com",
                    FirstName = "Admin",
                    LastName = "Administrator",
                    AvatarUrl = "/Uploads/admin-icon.jpg",
                    DisplayName = "The Admin"
                }, "Admin@money");
            }
            var adminId = userManager.FindByEmail("Admin@mailinator.com");
            userManager.AddToRole(adminId.Id, "Admin");
            #endregion

            #region SeedingHOH
            //Seeding Head of Household
            if (!context.Users.Any(u => u.Email == "mosesmwangi@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Moses",
                    LastName = "Mwangi",
                    DisplayName = "The Boss",
                    AvatarUrl = "/Uploads/dev-icon.png",
                    Email = "mosesmwangi@mailinator.com",
                    UserName = "mosesmwangi@mailinator.com"

                }, "Mwangi@money");
            }
            var hoHouseId = userManager.FindByEmail("mosesmwangi@mailinator.com");
            userManager.AddToRole(hoHouseId.Id, "HeadofHouse");
            #endregion

            #region SeedingMember
            //Seeding Member
            if (!context.Users.Any(u => u.Email == "josephine@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "josephine@Mailinator.com",
                    Email = "josephine@Mailinator.com",
                    FirstName = "Jose",
                    LastName = "CFO",
                    AvatarUrl = "/Uploads/pm-icon.png",
                    DisplayName = "The CFO"
                }, "Josephine@money");
            }
            var memberId = userManager.FindByEmail("josephine@mailinator.com");
            userManager.AddToRole(memberId.Id, "Member");
            #endregion

            #region SeedingLobbyMemeber
            //Seeding Lobby Memeber
            if (!context.Users.Any(u => u.Email == "Zuri@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Zuri@Mailinator.com",
                    Email = "Zuri@Mailinator.com",
                    FirstName = "Zuri",
                    LastName = "Mbutha",
                    AvatarUrl = "/Uploads/sub-icon.png",
                    DisplayName = "Lobbyist"
                }, "Zuri@money");
            }
            var lobbyistId = userManager.FindByEmail("Zuri@mailinator.com");
            userManager.AddToRole(lobbyistId.Id, "LobbyMember");
            #endregion

            #region BankAccount
            //seeding a demo account
            context.BankAccounts.AddOrUpdate(
                c => c.Name,
                new BankAccount { Name = "Demo Checking Account", Type = BankAccountType.Checkings , HouseholdId = seedHouseId, StartingBalance = 1000, CurrentBalance = 1000, LowBalance = 200 },
                new BankAccount { Name = "Demo Saings Account", Type = BankAccountType.savings, HouseholdId = seedHouseId, StartingBalance = 1000, CurrentBalance = 1000, LowBalance = 200 }
                ); ;
            #endregion

            #region Budget
            //creating budget categories
            context.MyBudget.AddOrUpdate(
                b => b.BugetCategoryName,
                new Budget { BugetCategoryName = "Bills", HouseholdId = seedHouseId, TargetAmount = 300 },
                new Budget { BugetCategoryName = "Food", HouseholdId = seedHouseId, TargetAmount = 400 },
                new Budget { BugetCategoryName = "Car Expenses", HouseholdId = seedHouseId, TargetAmount = 250 },
                new Budget { BugetCategoryName = "Home Maintenance", HouseholdId = seedHouseId, TargetAmount = 100 },
                new Budget { BugetCategoryName = "Subscriptions", HouseholdId = seedHouseId, TargetAmount = 80 },
                new Budget { BugetCategoryName = "Medical", HouseholdId = seedHouseId, TargetAmount = 200 },
                new Budget { BugetCategoryName = "Entertainment", HouseholdId = seedHouseId, TargetAmount = 250 },
                new Budget { BugetCategoryName = "Miscellaneous", HouseholdId = seedHouseId, TargetAmount = 250 }
                );
            #endregion

            #region BudgetItems
            //Instantiation
            context.SaveChanges();

            var BillsId = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Bills").FirstOrDefault().Id;
            var FoodId = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Food").FirstOrDefault().Id;
            var CarId = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Car Expenses").FirstOrDefault().Id;
            var HomeMaintenanceId = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Home Maintenance").FirstOrDefault().Id;
            var Medical = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Medical").FirstOrDefault().Id;
            var Entertainment = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Entertainment").FirstOrDefault().Id;
            var Miscellaneous = context.MyBudget.AsNoTracking().Where(b => b.BugetCategoryName == "Miscellaneous").FirstOrDefault().Id;

            //seeding
            context.BudgetItems.AddOrUpdate(
                b =>b.ItemName,
                new BudgetItem { ItemName = "Gas Bill", BudgetId = BillsId },
                new BudgetItem { ItemName = "Electric Bill", BudgetId = BillsId },               
                new BudgetItem { ItemName = "Water Bill", BudgetId = BillsId },
                new BudgetItem { ItemName = "Internet/Phone Bill", BudgetId = BillsId },
                new BudgetItem { ItemName = "Cellphone Bill", BudgetId = BillsId },
                new BudgetItem { ItemName = "Groceries", BudgetId = FoodId },
                new BudgetItem { ItemName = "Wine/Beer/Liquor", BudgetId = FoodId },
                new BudgetItem { ItemName = "Restaurant", BudgetId = FoodId },
                new BudgetItem { ItemName = "Takeout", BudgetId = FoodId },
                new BudgetItem { ItemName = "Gasoline Bill", BudgetId = CarId },
                new BudgetItem { ItemName = "Oil Change/Lubricants", BudgetId = CarId },
                new BudgetItem { ItemName = "Tires/Brakes", BudgetId = CarId },
                new BudgetItem { ItemName = "Wiper/Coolant/WasherFluid", BudgetId = CarId },
                new BudgetItem { ItemName = "AC Mainteance", BudgetId = HomeMaintenanceId },
                new BudgetItem { ItemName = "Lawn Cutting", BudgetId = HomeMaintenanceId },
                new BudgetItem { ItemName = "General House Maintenance", BudgetId = HomeMaintenanceId },
                new BudgetItem { ItemName = "Doctor Visit", BudgetId = Medical },
                new BudgetItem { ItemName = "Prescritions", BudgetId = Medical },
                new BudgetItem { ItemName = "Remedies", BudgetId = Medical },
                new BudgetItem { ItemName = "Movie Night", BudgetId = Entertainment },
                new BudgetItem { ItemName = "Girls Night Out", BudgetId = Entertainment },
                new BudgetItem { ItemName = "Entertaining", BudgetId = Entertainment },
                new BudgetItem { ItemName = "Boys Night out", BudgetId = Entertainment },
                new BudgetItem { ItemName = "Tithe", BudgetId = Miscellaneous },
                new BudgetItem { ItemName = "School supplies/Office Supplies", BudgetId = Miscellaneous },
                new BudgetItem { ItemName = "Camera Supplies", BudgetId = Miscellaneous }
                );
            #endregion               

            #region Transaction
            context.Transactions.AddOrUpdate(
                new Transaction { Type = TransactionType.BankDraft, Amount = 200, Description = "To Buy stuff for Moving", Date = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Deposit, Amount = 1500, Description = "PayDay", Date = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Payment, Amount = 700, Description = "Mortgage", Date = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Withdrawal, Amount = 100, Description = "Groceries", Date = DateTimeOffset.Now }
                );
            #endregion
        }
    }
}

