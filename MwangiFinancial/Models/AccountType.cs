namespace MwangiFinancial.Models
{
    public class AccountType
    {
        public int Id { get; set; }

        public int? BankAccountId { get; set; }


        public string Type { get; set; }
        public string Description { get; set; }

        //Virtual Navigation
        public virtual BankAccount BankAccount { get; set; }
    }
}