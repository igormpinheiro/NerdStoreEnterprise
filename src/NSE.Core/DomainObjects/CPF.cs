using NSE.Core.Utils;

namespace NSE.Core.DomainObjects;

public class CPF
{
    public const int CpfMaxLength = 11;
    public string Number { get; private set; }

    // EF Constructor
    protected CPF()
    { }

    public CPF(string number)
    {
        if (!Validate(number)) throw new DomainException("CPF inválido");

        Number = number;
    }

    public static bool Validate(string cpf)
    {
        var cleanCpf = StringUtils.OnlyNumbers(cpf);

        if (cleanCpf.Length != CpfMaxLength)
            return false;

        if (cleanCpf == "00000000000")
            return false;

        var tempCpf = cleanCpf.Substring(0, 9);
        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * (10 - i);

        var rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        var digit = rest.ToString();
        tempCpf = tempCpf + digit;
        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * (11 - i);

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest;
        return cleanCpf.EndsWith(digit);
    }

    public override string ToString()
    {
        return Number;
    }
}
