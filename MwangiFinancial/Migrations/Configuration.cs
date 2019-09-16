namespace MwangiFinancial.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MwangiFinancial.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MwangiFinancial.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();


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

            #region SeededLobbyAndDemoHouse           

            context.Households.AddOrUpdate(
                h => h.Name,
                new Household { Name = "The Lobby", Greeting = "Welcome to the lobby!", Created = DateTimeOffset.UtcNow.ToLocalTime() },
                new Household { Name = "Demo Household", Greeting = "Welcome everyone to the Household!", Created = DateTimeOffset.UtcNow.ToLocalTime(), IsConfigured = true }
                );
            context.SaveChanges();
            #endregion

            #region User Creation
            //This code introduces code that will allow me to create a few users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var houses = context.Households.AsNoTracking();

            var seedHouseId = houses.FirstOrDefault(h => h.Name == "Demo Household").Id;
            var seedLobbyId = houses.FirstOrDefault(h => h.Name == "The Lobby").Id;

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
            var adminId = userManager.FindByEmail("Admin@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

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
            var hoHouseId = userManager.FindByEmail("mosesmwangi@mailinator.com").Id;
            userManager.AddToRole(hoHouseId, "HeadofHouse");
                        
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
            var memberId = userManager.FindByEmail("josephine@mailinator.com").Id;
            userManager.AddToRole(memberId, "Member");

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
            var lobbyistId = userManager.FindByEmail("Zuri@mailinator.com").Id;
            userManager.AddToRole(lobbyistId, "LobbyMember");
            context.SaveChanges();
            #endregion

            #region Seeding Account Types
            context.AccountTypes.AddOrUpdate(
                a => a.Type,
                    new AccountType { Type = "Checking", Description = "Default Checking" },
                    new AccountType { Type = "Savings", Description = "Default Saving" },
                    new AccountType { Type = "Money Market", Description = "Default Money Market Account" }
                );
            context.SaveChanges();
            #endregion

            #region Seeding Transaction Types
            context.TransactionTypes.AddOrUpdate(
                a => a.Name,
                    new TransactionType { Name = "Deposit", Decsription = "Deposit in an account" },
                    new TransactionType { Name = "Withdrawal", Decsription = "Withdrawal from an account" },
                    new TransactionType { Name = "Adjust Up", Decsription = "Adjustment Up" },
                    new TransactionType { Name = "Adjust Down", Decsription = "Adjustment Down" }
                );
            context.SaveChanges();
            #endregion

            #region Budget
            //creating budget categories
            context.MyBudget.AddOrUpdate(
                b => b.BudgetName,
                new Budget { BudgetName = "Bills", HouseholdId = seedHouseId, TargetAmount = 300.00 },
                new Budget { BudgetName = "Food", HouseholdId = seedHouseId, TargetAmount = 400.00 },
                new Budget { BudgetName = "Car Expenses", HouseholdId = seedHouseId, TargetAmount = 250.00 },
                new Budget { BudgetName = "Home Maintenance", HouseholdId = seedHouseId, TargetAmount = 100.00 },
                new Budget { BudgetName = "Subscriptions", HouseholdId = seedHouseId, TargetAmount = 80.00 },
                new Budget { BudgetName = "Medical", HouseholdId = seedHouseId, TargetAmount = 200.00 },
                new Budget { BudgetName = "Entertainment", HouseholdId = seedHouseId, TargetAmount = 250.00 },
                new Budget { BudgetName = "Miscellaneous", HouseholdId = seedHouseId, TargetAmount = 250.00 }
                );
            context.SaveChanges();
            #endregion

            #region BudgetItems
            var budgets = context.MyBudget.AsNoTracking();

            var BillsId = budgets.FirstOrDefault(b => b.BudgetName == "Bills").Id;
            var FoodId = budgets.FirstOrDefault(b => b.BudgetName == "Food").Id;
            var CarId = budgets.FirstOrDefault(b => b.BudgetName == "Bills").Id;
            
            //seeding
            context.BudgetItems.AddOrUpdate(
                b => b.ItemName,
                new BudgetItem { ItemName = "Gas Bill", BudgetId = BillsId, Created = DateTimeOffset.Now, Target = 120.00 },
                new BudgetItem { ItemName = "Electric Bill", BudgetId = BillsId,Created = DateTimeOffset.Now, Target = 100.00 },
                new BudgetItem { ItemName = "Water Bill", BudgetId = BillsId, Created = DateTimeOffset.Now, Target = 45.00 },
                new BudgetItem { ItemName = "Internet/Phone Bill", BudgetId = BillsId, Created = DateTimeOffset.Now, Target = 80.00 },
                new BudgetItem { ItemName = "Cellphone Bill", BudgetId = BillsId, Created = DateTimeOffset.Now, Target = 65.00 },
                new BudgetItem { ItemName = "Groceries", BudgetId = FoodId, Created = DateTimeOffset.Now, Target = 300.00 },
                new BudgetItem { ItemName = "Wine/Beer/Liquor", BudgetId = FoodId, Created = DateTimeOffset.Now, Target = 100.00 },
                new BudgetItem { ItemName = "Restaurant", BudgetId = FoodId, Created = DateTimeOffset.Now, Target = 200.00 },
                new BudgetItem { ItemName = "Takeout", BudgetId = FoodId, Created = DateTimeOffset.Now, Target = 50.00 },
                new BudgetItem { ItemName = "Gasoline Bill", BudgetId = CarId, Created = DateTimeOffset.Now, Target = 300.00 }
                );

            context.SaveChanges();
            #endregion               

            #region BankAccount
            var accountTypes = context.AccountTypes.AsNoTracking();

            //seeding a demo account
            context.BankAccounts.AddOrUpdate(
                c => c.Name,
                new BankAccount
                {
                    Name = "Bb&t Checking Account",
                    HouseholdId = seedHouseId,
                    StartingBalance = 5000.00,
                    CurrentBalance = 1000.00,
                    AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == "Checking").Id,
                    Created = DateTimeOffset.Now,
                    Description = "This is the seeded Checking Account",
                    LowBalance = 100.00,
                    OwnerUserId = hoHouseId,
                    Address1 = "2670 Mariane Springs",
                    Address2 = "Apt. 244",
                    State = Enumeration.State.NC,
                    Zip = "27713"
                },

                new BankAccount
                {
                    Name = "Coastal Savings Account",
                    HouseholdId = seedHouseId,
                    StartingBalance = 5000.00,
                    CurrentBalance = 5000.00,
                    AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == "Savings").Id,
                    Created = DateTimeOffset.Now,
                    Description = "This is the another seeded Checking Account",
                    LowBalance = 200.00,
                    OwnerUserId = hoHouseId,
                    Address1 = "8854 Rowe Coves",
                    Address2 = "Apt. 006",
                    State = Enumeration.State.NC,
                    Zip = "27703"
                }
                );               
            context.SaveChanges();
            #endregion

            #region Seed Transactions
            var budgetItems = context.BudgetItems.AsNoTracking();

            var gasBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Gas Bill").Id;
            var electricBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Electric Bill").Id;
            var waterBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Water Bill").Id;
            var phoneBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Internet/Phone Bill").Id;
            var cellphoneBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Cellphone Bill").Id;
            var groceriesBudgetItemId = budgetItems.FirstOrDefault(b => b.ItemName == "Groceries").Id;

            var checkingAccountId = accountTypes.FirstOrDefault(a => a.Type == "Checking").Id;
            var savingsAccountId = accountTypes.FirstOrDefault(a => a.Type == "savings").Id;

            var transactionTypes = context.TransactionTypes.AsNoTracking();
            var depositTransactionType = transactionTypes.FirstOrDefault(t => t.Name == "Deposit").Id;
            var withdrawalTransactionType = transactionTypes.FirstOrDefault(t => t.Name == "Withdrawal").Id;
            
            context.Transactions.AddOrUpdate(
                t => t.Payee,
                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = gasBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,                       
                        Amount = 200.00,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = waterBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,
                        Amount = 200.00,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = phoneBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,
                        Amount = 200,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = cellphoneBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,
                        Amount = 200,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = electricBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,
                        Amount = 200,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = checkingAccountId,
                        BudgetItemId = groceriesBudgetItemId,
                        EnteredById = hoHouseId,
                        TransactionTypeId = withdrawalTransactionType,
                        Amount = 200,
                        Description = "To Buy stuff for Moving",
                        Created = DateTimeOffset.Now
                    },

                    new Transaction
                    {
                        BankAccountId = savingsAccountId,
                        BudgetItemId = null,
                        EnteredById = hoHouseId,
                        TransactionTypeId = depositTransactionType,
                        Amount = 500.00,
                        Created = DateTimeOffset.Now,
                        Description = "Automated monthly Saving transer"
                    }

                );
            context.SaveChanges();
            #endregion
        }
    }
}
