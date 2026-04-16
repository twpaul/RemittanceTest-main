namespace RemittanceTest.Services
{
    public interface IRemittanceService
    {
        CancelRemittanceResult CancelRemittance(int id);
    }
}