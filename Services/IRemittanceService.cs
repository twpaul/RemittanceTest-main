using RemittanceTest.Models;
using System.Collections.Generic;

namespace RemittanceTest.Services
{
    public interface IRemittanceService
    {
        IEnumerable<Remittance> GetRemittances();
        CancelRemittanceResult CancelRemittance(int id);
    }
}