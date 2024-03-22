using System;

namespace lib.Lab.Models.Enum;

public enum Labs
{
    [LabData("Шифрование данных методом подстановки (1)", true)]
    Lab1,
    [LabData("Шифрование данных методом перестановки (2)", true)]
    Lab2,
    [LabData("Линейное шифрование данных (Гаммирование) (3)", true)]
    Lab3,
    [LabData("Класический криптографический алгоритм DESC (4)", true)]
    Lab4,
    [LabData("Работа алгоритма DES в режиме CBC (5)", true)]
    Lab5,
    [LabData("Работа алгоритма DES в режиме CFB (6)", true)]
    Lab6,
    [LabData("Работа алгоритма DES в режиме OFB (7)", true)]
    Lab7,
    [LabData("Метод изменения интервала между предложениями (8)", true)]
    Lab8,
    [LabData("Метод изменения кол-ва пробелов в конце текстовых строк (9)", true)]
    Lab9
}



[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class LabDataAttribute : Attribute
{
    public string Description { get; }
    public bool IsInclude { get; }

    public LabDataAttribute(string description, bool isInclude)
    {
        Description = description;
        IsInclude = isInclude;
    }
}


