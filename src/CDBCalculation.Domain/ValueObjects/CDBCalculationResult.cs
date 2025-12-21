namespace CDBCalculation.Domain.ValueObjects;

public class CdbCalculationResult
{

    public CdbCalculationResult(decimal _grossValue, decimal _netWorth)
    {
        GrossValue = _grossValue;
        NetWorth = _netWorth;   

    }

    public decimal GrossValue { get;  }
    public decimal NetWorth { get;  }



}
