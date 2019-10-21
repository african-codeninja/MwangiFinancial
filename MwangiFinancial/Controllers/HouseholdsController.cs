using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MwangiFinancial.Helpers;
using MwangiFinancial.Models;
using MwangiFinancial.ViewModels;
using MwangiFinancial.ExtensionMethods;

namespace MwangiFinancial.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelper roleHelper = new RoleHelper();
        private HouseholdHelper householdHelper = new HouseholdHelper();

                // GET: Households
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        //Dashboard for households

        // GET: Households Dashboard
        [Authorize(Roles = "HeadofHouse, Member")]
        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var houseId = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == userId).HouseholdId;
            var dashboardVM = new DashboardViewModel();
            dashboardVM.Household = db.Households.Find(houseId);
            return View(dashboardVM);
        }

        
    

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                household.Created = DateTimeOffset.UtcNow.ToLocalTime();
                db.Households.Add(household);
                db.SaveChanges();
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.HouseholdId = household.Id;
                db.SaveChanges();
                roleHelper.RemoveUserFromRole(userId, "LobbyMember");
                roleHelper.AddUserToRole(userId, "HeadofHouse");

                await AdminHelper.ReathorizeUserAsync(userId);
                return RedirectToAction("Dashboard");
            }
            return View(household);
        }

        //POST: Households/WizardSubmit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WizardSubmit(BankAccount bankAccount, Budget budget, BudgetItem budgetItem, int houseId)
        {
            if (ModelState.IsValid)
            {
                bankAccount.HouseholdId = houseId;
                db.BankAccounts.Add(bankAccount);
                budget.HouseholdId = houseId;
                db.MyBudget.Add(budget);
                db.SaveChanges();

                budgetItem.BudgetId = budget.Id;
                db.BudgetItems.Add(budgetItem);

                var household = db.Households.Find(houseId);
                household.IsConfigured = true;
                db.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        //POST: Households/Invite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InviteMember([Bind(Include = "RecipientEmail")] Invitation invitation)
        {
            invitation.HouseholdId = User.Identity.GetHouseholdId();
            invitation.Code = Guid.NewGuid();
            invitation.ReciepientEmail = db.Users.Find(User.Identity.GetUserId()).FullName;

            db.Invitations.Add(invitation);
            db.SaveChanges();

            //now send the inv
            await invitation.SendAsync();
            return RedirectToAction("Dashboard");
        }


        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greeting,IsConfigured,Created")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
