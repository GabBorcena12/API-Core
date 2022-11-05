using API_Core.Model.Models;
using System.Collections.Generic;

namespace API_Core.Interface
{
    public interface iTransaction
    { 
        public List<Transact> GetTransactions();
        public Transact GetTransactionsById(int id);
        public int ModifyTransaction(Transact model);
        public int DeleteTransaction(int id); 
    }
}
