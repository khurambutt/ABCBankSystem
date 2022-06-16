using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// This enums created to specify transactions types when any transaction occur in the system.
/// </summary>
namespace ABCBankSystem.Common.Enums
{
    public enum TransactionTypes
    {
        Deposit,
        Withdrawl,
        OverdraftCost,
        OverdraftDebit,
        DirectTransfer,
        DirectDebits
    }
}
