using System.Collections.Generic;
using System.Linq;
using RemittanceTest.Models;

namespace RemittanceTest.Services
{
    public class RemittanceService : IRemittanceService
    {
        // 模擬資料庫 (靜態變數確保跨 Request 資料一致)
        private static readonly List<Remittance> _db = new()
        {
            new Remittance { Id = 1, AccountName = "測試企業A", AccountNumber = "12345678", Amount = 50000, Status = 0 },
            new Remittance { Id = 2, AccountName = "測試企業B", AccountNumber = "23456789", Amount = 12000, Status = 1 }, // 不可取消
            new Remittance { Id = 3, AccountName = "測試企業C", AccountNumber = "34567890", Amount = 30000, Status = 0 }
        };

        // 提示：如何確保多執行緒下的資料安全？
        private static readonly object _lockObj = new object();

        public CancelRemittanceResult CancelRemittance(int id)
        {
            if (id <= 0)
            {
                return new CancelRemittanceResult(CancelRemittanceResultType.InvalidRequest, "無效的匯款 ID。請提供正整數。");
            }

            lock (_lockObj)
            {
                var remittance = _db.FirstOrDefault(x => x.Id == id);
                if (remittance == null)
                {
                    return new CancelRemittanceResult(CancelRemittanceResultType.NotFound, "找不到指定的匯款紀錄。");
                }

                if (remittance.Status != 0)
                {
                    return new CancelRemittanceResult(CancelRemittanceResultType.InvalidRequest, "此筆匯款目前無法取消。只有待覆核的匯款可取消。");
                }

                remittance.Status = 9;
                return new CancelRemittanceResult(CancelRemittanceResultType.Success, "匯款已成功取消。");
            }
        }

        public IEnumerable<Remittance> GetRemittances()
        {
            lock (_lockObj)
            {
                return _db.Select(x => new Remittance
                {
                    Id = x.Id,
                    AccountName = x.AccountName,
                    AccountNumber = x.AccountNumber,
                    Amount = x.Amount,
                    Status = x.Status
                }).ToList();
            }
        }
    }
}