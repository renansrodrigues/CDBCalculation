namespace CDBCalculation.Domain.Entities;

public class CdbCalculation
{

    public decimal InitialValue { get; }
    public int TermMonths { get; }

    public CdbCalculation(decimal initialValue, int termMonths)
    {
        InitialValue = initialValue;
        TermMonths = termMonths;
    }

}
