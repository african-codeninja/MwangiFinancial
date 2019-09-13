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

            var houses = context.Households.AsNoTracking();

            var seedHouseId = houses.FirstOrDefault(h => h.Name == "Demo Household").Id;
            var seedLobbyId = houses.FirstOrDefault(h => h.Name == "The Lobby").Id;

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
                    DisplayName = "The Admin",
                    HouseholdId = seedLobbyId
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
                    UserName = "mosesmwangi@mailinator.com",
                    FirstName = "Moses",
                    LastName = "Mwangi",
                    AvatarUrl = "/Uploads/dev-icon.png",
                    Email = "mosesmwangi@mailinator.com",
                    DisplayName = "The Boss",
                    HouseholdId = seedHouseId
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
                    DisplayName = "The CFO",
                    HouseholdId = seedHouseId
                    
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
                    DisplayName = "Lobbyist",
                    HouseholdId = seedLobbyId

                }, "Zuri@money");
            }
            var lobbyistId = userManager.FindByEmail("Zuri@mailinator.com");
            userManager.AddToRole(lobbyistId.Id, "LobbyMember");
            #endregion
           
            #region Budget
            //creating budget categories
            context.MyBudget.AddOrUpdate(
                b => b.BudgetName,
                new Budget { BudgetName = "Bills", HouseholdId = seedHouseId, TargetAmount = 300 },
                new Budget { BudgetName = "Food", HouseholdId = seedHouseId, TargetAmount = 400 },
                new Budget { BudgetName = "Car Expenses", HouseholdId = seedHouseId, TargetAmount = 250 },
                new Budget { BudgetName = "Home Maintenance", HouseholdId = seedHouseId, TargetAmount = 100 },
                new Budget { BudgetName = "Subscriptions", HouseholdId = seedHouseId, TargetAmount = 80 },
                new Budget { BudgetName = "Medical", HouseholdId = seedHouseId, TargetAmount = 200 },
                new Budget { BudgetName = "Entertainment", HouseholdId = seedHouseId, TargetAmount = 250 },
                new Budget { BudgetName = "Miscellaneous", HouseholdId = seedHouseId, TargetAmount = 250 }
                );
            #endregion

            #region BudgetItems
            //Instantiation
            context.SaveChanges();

            var budgets = context.MyBudget.AsNoTracking();

            var BillsId = budgets.FirstOrDefault(b => b.BudgetName == "Bills").Id;
            var FoodId = budgets.FirstOrDefault(b => b.BudgetName == "Food").Id;
            var CarId = budgets.FirstOrDefault(b => b.BudgetName == "Bills").Id;
            var HomeMaintenanceId = budgets.FirstOrDefault(b => b.BudgetName == "Car Expenses").Id;
            var Medical = budgets.FirstOrDefault(b => b.BudgetName == "Home Maintenance").Id;
            var Entertainment = budgets.FirstOrDefault(b => b.BudgetName == "Medical").Id;
            var Miscellaneous = budgets.FirstOrDefault(b => b.BudgetName == "Entertainment").Id;

            //seeding
            context.BudgetItems.AddOrUpdate(
                b =>b.ItemName,
                new BudgetItem { ItemName = "Gas Bill", BudgetId = BillsId , },
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
                new Transaction { Type = TransactionType.BankDraft, Amount = 200, Description = "To Buy stuff for Moving", Created = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Deposit, Amount = 1500, Description = "PayDay", Created = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Payment, Amount = 700, Description = "Mortgage", Created = DateTimeOffset.Now },
                new Transaction { Type = TransactionType.Withdrawal, Amount = 100, Description = "Groceries", Created = DateTimeOffset.Now }
                );
            #endregion

            #region BankAccount
            //seeding a demo account
            context.BankAccounts.AddOrUpdate(
                c => c.Name,
                new BankAccount
                {
                    Name = "Bb&t Checking Account",                 
                    Type = BankAccountType.Checkings,
                    HouseholdId = seedHouseId,
                    StartingBalance = 1000,
                    CurrentBalance = 1000,
                    LowBalance = 200,
                    Address1 =,
                    Address2 =,
                    State = State.NC,
                    Zip = 27703
                },

                new BankAccount
                {
                    Name = "Demo Saings Account",
                    Type = BankAccountType.savings,
                    HouseholdId = seedHouseId,
                    StartingBalance = 1000,
                    CurrentBalance = 1000,
                    LowBalance = 200
                }
                ); ;
            #endregion
        }
    }
}

